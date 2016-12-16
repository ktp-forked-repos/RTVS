﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebSockets.Client;
using Microsoft.Common.Core;
using Microsoft.Common.Core.Disposables;
using Microsoft.Common.Core.Json;
using Microsoft.Common.Core.Logging;
using Microsoft.Common.Core.Net;
using Microsoft.R.Host.Client.BrokerServices;
using Microsoft.R.Host.Protocol;

namespace Microsoft.R.Host.Client.Host {
    internal abstract class BrokerClient : IBrokerClient {
        private static readonly TimeSpan HeartbeatTimeout =
#if DEBUG
            // In debug mode, increase the timeout significantly, so that when the host is paused in debugger,
            // the client won't immediately timeout and disconnect.
            TimeSpan.FromMinutes(10);
#else
            TimeSpan.FromSeconds(5);
#endif
        private static IReadOnlyDictionary<Type, string> _typeToEndpointMap = new Dictionary<Type, string>() {
            { typeof(AboutHost), "info/about"},
            { typeof(HostLoad), "info/load"}
        };

        private readonly string _interpreterId;
        private readonly ICredentialsDecorator _credentials;
        private readonly IConsole _console;
        private readonly string _parametersHash;

        protected DisposableBag DisposableBag { get; } = DisposableBag.Create<BrokerClient>();
        protected IActionLog Log { get; }
        protected WebRequestHandler HttpClientHandler { get; private set; }
        protected HttpClient HttpClient { get; private set; }

        public string Name { get; }
        public Uri Uri { get; }
        public string RCommandLineArguments { get; }
        public bool IsRemote => !Uri.IsFile;
        public bool IsVerified { get; protected set; }

        protected BrokerClient(string name, Uri brokerUri, string rCommandLineArguments, string interpreterId, ICredentialsDecorator credentials, IActionLog log, IConsole console) {
            Name = name;
            Uri = brokerUri;
            Log = log;

            RCommandLineArguments = rCommandLineArguments?.Trim() ?? string.Empty;
            _interpreterId = interpreterId;
            _credentials = credentials;
            _console = console;
            _parametersHash = CalculateAdditionalParametersHash();
        }

        protected void CreateHttpClient(Uri baseAddress) {
            HttpClientHandler = new WebRequestHandler {
                PreAuthenticate = true,
                Credentials = _credentials
            };

            HttpClient = new HttpClient(HttpClientHandler) {
                BaseAddress = baseAddress,
                Timeout = TimeSpan.FromSeconds(30),
            };

            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose() => DisposableBag.TryDispose();

        public async Task<T> GetHostInformationAsync<T>(CancellationToken cancellationToken) {
            string result = null;
            try {
                string endpoint;
                if (!_typeToEndpointMap.TryGetValue(typeof(T), out endpoint)) {
                    throw new ArgumentException($"There is no endpoint for type {typeof(T)}");
                }

                if (HttpClient != null) {
                    var response = await HttpClient.GetAsync(endpoint, cancellationToken);
                    result = response != null ? await response.Content.ReadAsStringAsync() : null;
                }

                return !string.IsNullOrEmpty(result) ? Json.DeserializeObject<T>(result) : default(T);
            } catch (HttpRequestException ex) {
                throw new RHostDisconnectedException(Resources.Error_HostNotResponding.FormatInvariant(Name, ex.Message), ex);
            }
        }

        public async Task DeleteProfileAsync(CancellationToken cancellationToken) {
            await TaskUtilities.SwitchToBackgroundThread();
            try {
                var sessionsService = new ProfileWebService(HttpClient, _credentials);
                await sessionsService.DeleteAsync(cancellationToken);
            } catch (HttpRequestException ex) {
                throw new RHostDisconnectedException(Resources.Error_HostNotResponding.FormatInvariant(ex.Message), ex);
            }
        }

        public virtual async Task<RHost> ConnectAsync(BrokerConnectionInfo connectionInfo, CancellationToken cancellationToken = default(CancellationToken)) {
            DisposableBag.ThrowIfDisposed();
            await TaskUtilities.SwitchToBackgroundThread();

            var sessionName = connectionInfo.Name + _parametersHash;
            try {
                var sessionExists = connectionInfo.PreserveSessionData && await IsSessionRunningAsync(sessionName, cancellationToken);
                if (sessionExists) {
                    var terminateRDataSave = await _console.PromptYesNoAsync(Resources.AbortRDataAutosave, cancellationToken);
                    if (!terminateRDataSave) {
                        while (await IsSessionRunningAsync(sessionName, cancellationToken)) {
                            await Task.Delay(500, cancellationToken);
                        }
                    }
                }

                await CreateBrokerSessionAsync(sessionName, connectionInfo.UseRHostCommandLineArguments, cancellationToken);
                var webSocket = await ConnectToBrokerAsync(sessionName, cancellationToken);
                return CreateRHost(sessionName, connectionInfo.Callbacks, webSocket);
            } catch (HttpRequestException ex) {
                throw await HandleHttpRequestExceptionAsync(ex);
            }
        }

        public Task TerminateSessionAsync(string name, CancellationToken cancellationToken = default(CancellationToken)) {
            var sessionsService = new SessionsWebService(HttpClient, _credentials);
            return sessionsService.DeleteAsync(name, cancellationToken);
        }

        protected virtual Task<Exception> HandleHttpRequestExceptionAsync(HttpRequestException exception)
            => Task.FromResult<Exception>(new RHostDisconnectedException(Resources.Error_HostNotResponding.FormatInvariant(Name, exception.Message), exception));

        private string CalculateAdditionalParametersHash() {
            if (string.IsNullOrEmpty(RCommandLineArguments)) {
                return string.Empty;
            }

            var bytes = Encoding.Unicode.GetBytes(RCommandLineArguments);
            byte[] hashBytes;
            using (var hash = new SHA256Cng()) {
                hashBytes = hash.ComputeHash(bytes);
            }

            var hashCharsLength = (int)(hashBytes.Length * 4.0d / 3.0d);
            if (hashCharsLength % 4 != 0) {
                hashCharsLength += 4 - hashCharsLength % 4;
            }

            var hashChars = new char[hashCharsLength];

            Convert.ToBase64CharArray(hashBytes, 0, hashBytes.Length, hashChars, 0);
            return new StringBuilder()
                .Append("_")
                .Append(hashChars)
                .Replace('+', '-')
                .Replace('/', '.')
                .Replace('=', '_')
                .ToString();
        }

        private async Task<bool> IsSessionRunningAsync(string name, CancellationToken cancellationToken) {
            var sessionsService = new SessionsWebService(HttpClient, _credentials);
            var sessions = await sessionsService.GetAsync(cancellationToken);
            return sessions.Any(s => s.Id == name);
        }

        private async Task CreateBrokerSessionAsync(string name, bool useRCommandLineArguments, CancellationToken cancellationToken) {
            var rCommandLineArguments = useRCommandLineArguments && RCommandLineArguments != null ? RCommandLineArguments : null;
            var sessions = new SessionsWebService(HttpClient, _credentials);
            try {
                await sessions.PutAsync(name, new SessionCreateRequest {
                    InterpreterId = _interpreterId,
                    CommandLineArguments = rCommandLineArguments,
                }, cancellationToken);
            } catch (BrokerApiErrorException apiex) {
                throw new RHostDisconnectedException(apiex);
            }
        }

        private async Task<WebSocket> ConnectToBrokerAsync(string name, CancellationToken cancellationToken) {
            var wsClient = new WebSocketClient {
                KeepAliveInterval = HeartbeatTimeout,
                SubProtocols = { "Microsoft.R.Host" },
                InspectResponse = response => {
                    if (response.StatusCode == HttpStatusCode.Forbidden) {
                        throw new UnauthorizedAccessException();
                    }
                }
            };

            var pipeUri = new UriBuilder(HttpClient.BaseAddress) {
                Scheme = HttpClient.BaseAddress.IsHttps() ? "wss" : "ws",
                Path = $"sessions/{name}/pipe"
            }.Uri;

            while (true) {
                var request = wsClient.CreateRequest(pipeUri);

                using (await _credentials.LockCredentialsAsync(cancellationToken)) {
                    try {
                        request.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
                        request.Credentials = HttpClientHandler.Credentials;
                        return await wsClient.ConnectAsync(request, cancellationToken);
                    } catch (UnauthorizedAccessException) {
                        _credentials.InvalidateCredentials();
                    } catch (Exception ex) when (ex is InvalidOperationException) {
                        throw new RHostDisconnectedException(Resources.HttpErrorCreatingSession.FormatInvariant(Name, ex.Message), ex);
                    }
                }
            }
        }

        private RHost CreateRHost(string name, IRCallbacks callbacks, WebSocket socket) {
            var transport = new WebSocketMessageTransport(socket);
            return new RHost(name, callbacks, transport, Log);
        }

        public virtual Task<string> HandleUrlAsync(string url, CancellationToken cancellationToken)  => Task.FromResult(url);
    }
}
