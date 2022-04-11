using NLog;
using System;

namespace ProductTurnover.Infra
{
    public class NLogLoggingFacility<TSource> : ILoggingFacility<TSource>
        where TSource : class
    {
        private readonly Logger Log = LogManager.GetLogger(typeof(TSource).FullName);

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(Exception ex)
        {
            Log.Error(ex);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Trace(string message)
        {
            Log.Trace(message);
        }

        public void Warning(string message)
        {
            Log.Warn(message);
        }
    }
}
