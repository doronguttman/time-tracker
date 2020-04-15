using System;
using Interfaces;

namespace TimeTracker.Services.Logging
{
    class NullLogger<T> : ILogger<T>
    {
        #region Implementation of ILogger
        public string Name { get; } = typeof(T).FullName;
        public void Debug(string message) { }
        public void Info(string message) { }
        public void Error(string message) { }
        public void Error(Exception exception) { }
        #endregion Implementation of ILogger
    }
}
