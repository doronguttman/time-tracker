using LightInject;

namespace TimeTracker
{
    class CompositionRoot : ICompositionRoot
    {
        #region Implementation of ICompositionRoot

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<Services.ServiceCompositionRoot>();
            serviceRegistry.RegisterFrom<Views.ViewCompositionRoot>();
            serviceRegistry.RegisterFrom<ModuleCompositionRoot>();
        }

        #endregion Implementation of ICompositionRoot
    }
}
