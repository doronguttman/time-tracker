using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using LightInject;

namespace TimeTracker.Services
{
    internal class ApplicationLifeCycle
    {
        private readonly Application _application;
        private readonly IServiceContainer _container;

        public ApplicationLifeCycle(Application application, IServiceContainer container)
        {
            _application = application;
            _container = container;
            this.StartUp = Observable.FromEvent<StartupEventHandler, StartupEventArgs>(a => (sender, args) => a(args), h => this._application.Startup += h, h => this._application.Startup -= h);
            this.Exit = Observable.FromEvent<ExitEventHandler, ExitEventArgs>(a => (sender, args) => a(args), h => this._application.Exit += h, h => this._application.Exit -= h);
        }

        public IObservable<IServiceContainer> Bootstrap { get; } = new Subject<IServiceContainer>();

        public IObservable<StartupEventArgs> StartUp { get; }

        public IObservable<ExitEventArgs> Exit { get; }

        public void TriggerBootstrap()
        {
            var bootstrap = (Subject<IServiceContainer>) this.Bootstrap;
            bootstrap.OnNext(this._container);
            bootstrap.OnCompleted();
        }
    }
}
