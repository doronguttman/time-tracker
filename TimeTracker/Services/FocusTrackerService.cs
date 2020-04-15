using System;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Model;

namespace TimeTracker.Services
{
    internal class FocusTrackerService : IAutoStart
    {
        private readonly ILogger<FocusTrackerService> _logger;
        private readonly IFocusMonitor _monitor;
        
        public FocusTrackerService(
            ILogger<FocusTrackerService> logger,
            IFocusMonitor monitor
        )
        {
            _logger = logger;
            _monitor = monitor;
            this._monitor.Focused.Subscribe(OnFocused);
        }

        #region Implementation of IAutoStart
        Task IAutoStart.Start() => this._monitor.Start();
        #endregion Implementation of IAutoStart
        
        private void OnFocused((IElementInfo window, IElementInfo element) data)
        {
            this._logger.Debug($"{nameof(OnFocused)}: {data.window?.Selector}");
        }
    }
}
