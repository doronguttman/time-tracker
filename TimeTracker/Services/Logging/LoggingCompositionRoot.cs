using Interfaces;
using LightInject;

namespace TimeTracker.Services.Logging
{
    class LoggingCompositionRoot : ICompositionRoot
    {
        #region Implementation of ICompositionRoot
        public void Compose(IServiceRegistry serviceRegistry)
        {
#if DEBUG
            serviceRegistry.Register(typeof(ILogger<>), typeof(ConsoleLogger<>));
#else
            serviceRegistry.Register(typeof(ILogger<>), typeof(NullLogger<>));
#endif
        }
        #endregion Implementation of ICompositionRoot
    }
}
