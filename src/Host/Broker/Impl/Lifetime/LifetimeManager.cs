﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.R.Host.Broker.Services;

namespace Microsoft.R.Host.Broker.Lifetime {
    public class LifetimeManager {
        private readonly LifetimeOptions _options;
        private readonly IExitService _exitService;
        private readonly ILogger _logger;

        private CancellationTokenSource _cts;

        public LifetimeManager(IExitService exitService, IOptions<LifetimeOptions> options, ILogger<LifetimeManager> logger) {
            _options = options.Value;
            _exitService = exitService;
            _logger = logger;
        }

        public void Initialize() {
            if (_options.ParentProcessID != null) {
                int pid = _options.ParentProcessID.Value;
                Process process;
                try {
                    process = Process.GetProcessById(pid);
                    process.EnableRaisingEvents = true;
                } catch (ArgumentException) {
                    _logger.LogCritical(Resources.Critical_ParentProcessNotFound, pid);
                    _exitService.Exit();
                    return;
                }

                _logger.LogInformation(Resources.Info_MonitoringParentProcess, pid);
                process.Exited += delegate {
                    _logger.LogInformation(Resources.Info_ParentProcessExited, pid);
                    _exitService.Exit();
                };
            }

            Ping();
        }

        public void Ping() {
            if (_options.PingTimeout == null) {
                return;
            }

            var cts = new CancellationTokenSource(_options.PingTimeout.Value);
            cts.Token.Register(PingTimeout, cts);
            var oldCts = Interlocked.Exchange(ref _cts, cts); 
            oldCts?.Dispose();
        }

        private void PingTimeout(object state) {
            var cts = (CancellationTokenSource) state;
            if (_cts == cts) {
                _logger.LogCritical(Resources.Critical_PingTimeOut);
                _exitService.Exit();
            }
        }
    }
}
