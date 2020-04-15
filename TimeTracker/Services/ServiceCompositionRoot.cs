using LightInject;

namespace TimeTracker.Services
{
    class ServiceCompositionRoot : ICompositionRoot
    {
        #region Implementation of ICompositionRoot
        public void Compose(IServiceRegistry container)
        {
            container.RegisterFrom<Logging.LoggingCompositionRoot>();
            container.Register<ApplicationLifeCycle>();
            container.Register<FocusTrackerService>();
        }
        #endregion Implementation of ICompositionRoot
    }
}
