using Interfaces;

namespace TimeTracker.Services.Logging
{
    internal static class LoggerFactory
    {
        public static ILogger<T> Build<T>()
        {
#if DEBUG
            return new ConsoleLogger<T>();
#else
            return new NullLogger<T>();
#endif
        }
    }
}
