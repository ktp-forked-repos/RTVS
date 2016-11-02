﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.R.Components.ConnectionManager;
using Microsoft.R.Components.ConnectionManager.Implementation;
using Microsoft.R.Components.Test.Stubs;
using Microsoft.R.Interpreters;

namespace Microsoft.R.Components.Test.StubFactories {
    [ExcludeFromCodeCoverage]
    public sealed class RSettingsStubFactory {
        public static RSettingsStub CreateForExistingRPath(string connectionName) {
            var connection = new ConnectionInfo {
                Name = connectionName ?? "Test",
                Path = new RInstallation().GetCompatibleEngines().FirstOrDefault()?.InstallPath
            };

            return new RSettingsStub {
                Connections = new IConnectionInfo[] { connection },
                LastActiveConnection = connection
            };
        }
    }
}
