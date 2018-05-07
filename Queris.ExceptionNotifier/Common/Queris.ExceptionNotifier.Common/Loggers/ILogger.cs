namespace Queris.ExceptionNotifier.Common.Loggers
{
    public interface ILogger
    {
        void Info(string message);
        void Info(string format, params object[] paramList);
        void Debug(string message);
        void Debug(string format, params object[] paramList);
        void Warning(string message);
        void Warning(string format, params object[] paramList);
        void Error(string message);
        void Error(string format, params object[] paramList);
    }
}