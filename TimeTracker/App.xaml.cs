using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Extensions;
using Interfaces;
using LightInject;
using TimeTracker.Services;
using TimeTracker.Services.Logging;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServiceContainer _container;

        public App() => Bootstrap(this);

        public static IServiceContainer Container => _container;

        private static void Bootstrap(Application app)
        {
            var logger = LoggerFactory.Build<App>();
            try
            {
                AppDomain.CurrentDomain.UnhandledException += (sender, args) => logger.Error(args.ExceptionObject.ToString());
                logger.Info(nameof(Bootstrap));

                Action<LogEntry> LogFactory(Type type) => entry =>
                {
                    if (entry.Level == LogLevel.Warning)
                        logger.Error($"{type.FullName} -> {entry.Message}");
                    else
                        logger.Info($"{type.FullName} -> {entry.Message}");
                };

                var containerOptions = new ContainerOptions { LogFactory = LogFactory };
                _container = new ServiceContainer(containerOptions);
                _container.EnableAnnotatedPropertyInjection();

                _container.RegisterInstance(Container);
                _container.RegisterFallback((type, s) => true, request => request.ServiceType.CreateInstance());
                _container.RegisterInstance(app);
                _container.RegisterAssembly(typeof(App).Assembly);

                var lifeCycle = _container.GetInstance<ApplicationLifeCycle>();
                lifeCycle.StartUp.Subscribe(_ => AutoStart(logger, Container.GetAllInstances<IAutoStart>()).Wait());
                lifeCycle.TriggerBootstrap();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                app.Shutdown(500);
            }
        }
        
        private static async Task AutoStart(ILogger logger, IEnumerable<IAutoStart> autoStarts)
        {
            try
            {
                foreach (var autoStart in autoStarts)
                {
                    logger.Info($"{nameof(AutoStart)}: Starting {autoStart}...");
                    await autoStart.Start();
                }
                logger.Info($"{nameof(AutoStart)}: Done");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
    }
}
