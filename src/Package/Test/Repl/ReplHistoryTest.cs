﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.R.Editor.ContentType;
using Microsoft.R.Host.Client;
using Microsoft.R.Support.Settings;
using Microsoft.UnitTests.Core.XUnit;
using Microsoft.VisualStudio.Editor.Mocks;
using Microsoft.VisualStudio.InteractiveWindow;
using Microsoft.VisualStudio.R.Package.History;
using Microsoft.VisualStudio.R.Package.Repl;
using Microsoft.VisualStudio.R.Package.Shell;
using Microsoft.VisualStudio.R.Package.Test.Mocks;
using Microsoft.VisualStudio.R.Package.Test.Utility;
using Microsoft.VisualStudio.Shell.Mocks;
using Xunit;

namespace Microsoft.VisualStudio.R.Package.Test.Repl {
    [ExcludeFromCodeCoverage]
    [Collection(CollectionNames.NonParallel)]
    public sealed class ReplHistoryTest {
        [Test]
        [Category.Repl]
        public async Task HistoryTest01() {
            using (new VsRHostScript()) {
                var sessionProvider = VsAppShell.Current.ExportProvider.GetExportedValue<IRSessionProvider>();
                var historyProvider = VsAppShell.Current.ExportProvider.GetExportedValue<IRHistoryProvider>();
                var rInteractive = new RInteractive(sessionProvider, historyProvider, RToolsSettings.Current);
                var history = historyProvider.CreateRHistory(rInteractive);

                var session = sessionProvider.GetInteractiveWindowRSession();
                using (var eval = new RInteractiveEvaluator(session, history, RToolsSettings.Current)) {
                    var tb = new TextBufferMock(string.Empty, RContentTypeDefinition.ContentType);
                    var tv = new WpfTextViewMock(tb);

                    var iwm = new InteractiveWindowMock(tv);
                    eval.CurrentWindow = iwm;

                    var result = await eval.InitializeAsync();
                    result.Should().Be(ExecutionResult.Success);
                    session.IsHostRunning.Should().BeTrue();

                    result = await eval.ExecuteCodeAsync("x <- c(1:10)");
                    result.Should().Be(ExecutionResult.Success);
                    history.HasEntries.Should().BeTrue();
                    history.HasSelectedEntries.Should().BeFalse();
                    history.SelectHistoryEntry(0);
                    history.HasSelectedEntries.Should().BeTrue();

                    tb.Clear();
                    history.SendSelectedToTextView(tv);
                    string text = tb.CurrentSnapshot.GetText();
                    text.Should().Be("x <- c(1:10)");
                }
            }
        }

        [Test]
        [Category.Repl]
        public async Task HistoryTest02() {
            using (var script = new VsRHostScript()) {
                var sessionProvider = VsAppShell.Current.ExportProvider.GetExportedValue<IRSessionProvider>();
                var historyProvider = VsAppShell.Current.ExportProvider.GetExportedValue<IRHistoryProvider>();
                var rInteractive = new RInteractive(sessionProvider, historyProvider, RToolsSettings.Current);
                var history = historyProvider.CreateRHistory(rInteractive);

                var session = sessionProvider.GetInteractiveWindowRSession();
                using (var eval = new RInteractiveEvaluator(session, history, RToolsSettings.Current)) {
                    var tb = new TextBufferMock(string.Empty, RContentTypeDefinition.ContentType);
                    var tv = new WpfTextViewMock(tb);

                    var iwm = new InteractiveWindowMock(tv);
                    eval.CurrentWindow = iwm;

                    var rw = new ReplWindowMock();
                    ReplWindow.Current = rw;

                    var result = await eval.InitializeAsync();
                    result.Should().Be(ExecutionResult.Success);
                    session.IsHostRunning.Should().BeTrue();

                    result = await eval.ExecuteCodeAsync("x <- c(1:10)");
                    result.Should().Be(ExecutionResult.Success);

                    await eval.ExecuteCodeAsync("\r\n");

                    result = await eval.ExecuteCodeAsync("x <- c(1:20)");
                    result.Should().Be(ExecutionResult.Success);

                    history.HasEntries.Should().BeTrue();
                    history.HasSelectedEntries.Should().BeFalse();

                    int eventCount = 0;
                    history.SelectionChanged += (s, e) => {
                        eventCount++;
                    };

                    history.SelectHistoryEntry(1);
                    history.HasSelectedEntries.Should().BeTrue();
                    eventCount.Should().Be(1);

                    history.SelectPreviousHistoryEntry();
                    eventCount.Should().Be(3); // event fires twice?

                    tb.Clear();
                    history.SendSelectedToTextView(tv);
                    string text = tb.CurrentSnapshot.GetText();
                    text.Should().Be("x <- c(1:10)");

                    history.SelectNextHistoryEntry();
                    eventCount.Should().Be(5);

                    tb.Clear();
                    history.SendSelectedToTextView(tv);
                    text = tb.CurrentSnapshot.GetText();
                    text.Should().Be("x <- c(1:20)");

                    history.PreviousEntry();
                    rw.EnqueuedCode.Should().Be("x <- c(1:20)");

                    history.PreviousEntry();
                    rw.EnqueuedCode.Should().Be("x <- c(1:10)");

                    history.NextEntry();
                    rw.EnqueuedCode.Should().Be("x <- c(1:20)");

                    tb.Clear();
                    history.SelectHistoryEntries(new int[] { 0, 1 });
                    history.SendSelectedToTextView(tv);
                    text = tb.CurrentSnapshot.GetText();
                    text.Should().Be("x <- c(1:10)\r\nx <- c(1:20)");
                    eventCount.Should().Be(6);

                    history.ToggleHistoryEntrySelection(1);
                    tb.Clear();
                    history.SendSelectedToTextView(tv);
                    text = tb.CurrentSnapshot.GetText();
                    text.Should().Be("x <- c(1:10)");
                    eventCount.Should().Be(7);

                    history.DeselectHistoryEntry(0);
                    history.HasSelectedEntries.Should().BeFalse();
                    eventCount.Should().Be(8);

                    history.SelectAllEntries();
                    history.HasSelectedEntries.Should().BeTrue();
                    text = history.GetSelectedText();
                    text.Should().Be("x <- c(1:10)\r\nx <- c(1:20)");

                    history.ToggleHistoryEntrySelection(1);
                    history.DeleteSelectedHistoryEntries();
                    history.SelectAllEntries();
                    text = history.GetSelectedText();
                    text.Should().Be("x <- c(1:20)");

                    history.DeleteAllHistoryEntries();
                    history.HasEntries.Should().BeFalse();
                    history.HasSelectedEntries.Should().BeFalse();
                    text = history.GetSelectedText();
                    text.Should().Be(string.Empty);
                }
            }
        }

        private void History_SelectionChanged(object sender, System.EventArgs e) {
            throw new System.NotImplementedException();
        }
    }
}
