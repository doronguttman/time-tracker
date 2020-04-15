using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using Interfaces;
using Interfaces.Model;

namespace Module.WindowsCoverage
{
    internal class FocusMonitor : IFocusMonitor
    {
        private readonly Subject<(IElementInfo window, IElementInfo element)> _focused = new Subject<(IElementInfo window, IElementInfo element)>();
        private readonly ILogger<FocusMonitor> _logger;
        private readonly ICollection<IWindowGetterStrategy> _windowGetterStrategies;
        private AutomationElement _currentWindow = null;

        public FocusMonitor(
            ILogger<FocusMonitor> logger,
            ICollection<IWindowGetterStrategy> windowGetterStrategies
        )
        {
            _logger = logger;
            _windowGetterStrategies = windowGetterStrategies;
        }

        #region Implementation of IFocusMonitor
        public Task Start()
        {
            try
            {
                this._logger.Info(nameof(Start));
                Automation.AddAutomationFocusChangedEventHandler(OnFocusChanged);
                Task.Run(() => this.ProcessFocusChanged(AutomationElement.FocusedElement));
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                throw;
            }
            return Task.CompletedTask;
        }

        public Task Stop() => Task.Run(() =>
        {
            this._logger.Info(nameof(Stop));
            Automation.RemoveAutomationFocusChangedEventHandler(OnFocusChanged);
            this._focused.OnCompleted();
            this._currentWindow = null;
        });

        IObservable<(IElementInfo window, IElementInfo element)> IFocusMonitor.Focused => this._focused;
        #endregion Implementation of IFocusMonitor

        private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            if (!(sender is AutomationElement element)) return;
            ProcessFocusChanged(element);
        }

        private void ProcessFocusChanged(AutomationElement element)
        {
            var window = element.GetWindow(this._windowGetterStrategies);
            if (this._currentWindow == window) return;

            this._currentWindow = window;
            var data = (window.AsElementInfo(), element.AsElementInfo());
            this._focused.OnNext(data);
        }
    }
}