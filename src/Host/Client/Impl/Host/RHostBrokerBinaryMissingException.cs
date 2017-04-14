﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Microsoft.R.Host.Client {
    [Serializable]
    public sealed class RHostBrokerBinaryMissingException : ComponentBinaryMissingException {
        public RHostBrokerBinaryMissingException()
            : base("Microsoft.R.Host.Broker.Windows.exe") { }
    }
}
