using System;

namespace Interfaces
{
    public interface ILogger
    {
        string Name { get; }
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Error(Exception exception);
    }

    public interface ILogger<T> : ILogger
    {
        
    }
}
