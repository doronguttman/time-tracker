using System;
using System.IO;
using System.Reflection;
using Interfaces;
using LightInject;

namespace TimeTracker
{
    internal class ModuleCompositionRoot: ICompositionRoot
    {
        [Inject]
        public ILogger<ModuleCompositionRoot> Logger { get; set; }

        #region Implementation of ICompositionRoot
        public void Compose(IServiceRegistry serviceRegistry)
        {
            App.Container.InjectProperties(this);
            var files = GetFiles();
            foreach (var file in files)
            {
                RegisterFile(file, serviceRegistry);
            }
        }

        private void RegisterFile(string file, IServiceRegistry serviceRegistry)
        {
            try
            {
                this.Logger.Debug($"{nameof(RegisterFile)}: {file}");
                var assembly = Assembly.LoadFrom(file);
                serviceRegistry.RegisterAssembly(assembly);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
            }
        }

        private static string[] GetFiles()
        {
            var folder = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(folder, "Module.*.dll", new EnumerationOptions
            {
                AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
                IgnoreInaccessible = true,
                MatchCasing = MatchCasing.CaseSensitive,
                MatchType = MatchType.Simple,
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            });
            return files;
        }

        #endregion Implementation of ICompositionRoot
    }
}
