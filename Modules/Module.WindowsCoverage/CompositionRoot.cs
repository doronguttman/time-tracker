using Interfaces;
using LightInject;

namespace Module.WindowsCoverage
{
    class CompositionRoot : ICompositionRoot
    {
        #region Implementation of ICompositionRoot
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IWindowGetterStrategy, DefaultWindowGetterGetterStrategy>();
            serviceRegistry.RegisterTransient<IFocusMonitor, FocusMonitor>();
        }
        #endregion Implementation of ICompositionRoot
    }
}
