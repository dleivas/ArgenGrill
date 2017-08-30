using System;

namespace Argengrill.Core.Logging
{
    public partial class NullLogger : ILogger
    {
        private static readonly ILogger s_instance = new NullLogger();

        public static ILogger Instance
        {
            get
            {
                return s_instance;
            }
        }

        public bool IsEnabledFor(LogLevel level)
        {
            return false;
        }

        public void Log(LogLevel level, Exception exception, string message, object[] args)
        {
        }
    }

    public class NullLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(Type type) => NullLogger.Instance;

        public ILogger GetLogger(string name) => NullLogger.Instance;

        public void FlushAll()
        {
        }
    }
}