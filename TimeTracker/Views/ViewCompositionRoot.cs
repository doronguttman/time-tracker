using LightInject;

namespace TimeTracker.Views
{
    class ViewCompositionRoot : ICompositionRoot
    {
        #region Implementation of ICompositionRoot
        public void Compose(IServiceRegistry container)
        {
            container.Register<MainWindow>();
        }
        #endregion
    }
}
