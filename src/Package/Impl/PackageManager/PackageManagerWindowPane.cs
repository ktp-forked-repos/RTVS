﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.R.Components.History.Implementation;
using Microsoft.R.Components.PackageManager;
using Microsoft.R.Components.PackageManager.Implementation;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.R.Package.Commands;
using Microsoft.VisualStudio.R.Package.Interop;
using Microsoft.VisualStudio.R.Package.Shell;
using Microsoft.VisualStudio.R.Packages.R;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.R.Package.PackageManager {
    [Guid(WindowGuidString)]
    internal class PackageManagerWindowPane : VisualComponentToolWindow<IRPackageManagerVisualComponent>, IOleCommandTarget {
        public const string WindowGuidString = "363F84AD-3397-4FDE-97EA-1ABD73C64BB3";
        public static Guid WindowGuid { get; } = new Guid(WindowGuidString);

        private IOleCommandTarget _commandTarget;

        public PackageManagerWindowPane() {
            Caption = Resources.PackageManagerWindowCaption;
        }

        protected override void OnCreate() {
            Component = new RPackageManagerVisualComponent(this);
            _commandTarget = new CommandTargetToOleShim(null, Component.Controller);

            base.OnCreate();
        }

        public override void OnToolWindowCreated() {
            Guid commandUiGuid = VSConstants.GUID_TextEditorFactory;
            ((IVsWindowFrame)Frame).SetGuidProperty((int)__VSFPROPID.VSFPROPID_InheritKeyBindings, ref commandUiGuid);
            base.OnToolWindowCreated();
        }

        protected override void Dispose(bool disposing) {
            if (disposing && _commandTarget != null) {
                _commandTarget = null;
                Component = null;
            }
            base.Dispose(disposing);
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText) {
            return _commandTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdId, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut) {
            return _commandTarget.Exec(ref pguidCmdGroup, nCmdId, nCmdexecopt, pvaIn, pvaOut);
        }
    }
}
