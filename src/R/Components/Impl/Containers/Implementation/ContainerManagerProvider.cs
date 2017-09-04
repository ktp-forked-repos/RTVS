// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.R.Components.InteractiveWorkflow;

namespace Microsoft.R.Components.Containers.Implementation {
    internal class ContainerManagerProvider : IContainerManagerProvider {
        public IContainerManager CreateContainerManager(IRInteractiveWorkflow interactiveWorkflow) => new ContainerManager(interactiveWorkflow);
    }
}