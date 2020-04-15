using LightInject;

namespace Module.ElectronCoverage
{
    public class CompositionRoot : ICompositionRoot
    {
        #region Implementation of ICompositionRoot
        public void Compose(IServiceRegistry serviceRegistry) => serviceRegistry.Register<ElectronWindowGetterGetterStrategy>();
        #endregion Implementation of ICompositionRoot
    }
}
