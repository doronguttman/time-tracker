using System;
using System.Threading;
using Extensions;
using Interfaces;

namespace TimeTracker.Services.Logging
{
    internal class ConsoleLogger<T> : ILogger<T>
    {
        static ConsoleLogger() => Native.Kernel32.AllocConsole();

        #region Implementation of ILogger
        public string Name { get; } = typeof(T).GetShortName();

        public void Debug(string message) => Console.WriteLine(this.FormatMessage("DBG", message));
        public void Info(string message) => Console.WriteLine(this.FormatMessage("INF", message));
        public void Error(string message) => Console.WriteLine(this.FormatMessage("ERR", message));
        public void Error(Exception exception) => this.Error(exception.GetBaseException().ToString());
        #endregion Implementation of ILogger

        private string FormatMessage(string level, string message) => $"{DateTime.Now:s} {level} [{Thread.CurrentThread.ManagedThreadId}] {this.Name}: {message}";
    }
}