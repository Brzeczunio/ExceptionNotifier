using System;

namespace Queris.ExceptionNotifier.Common.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($@"INFO: {message}");
        }

        public void Info(string format, params object[] paramList)
        {
            Console.WriteLine($@"INFO: {GenerateMessage(format, paramList)}");
        }

        public void Debug(string message)
        {
            Console.WriteLine($@"DEBUG: {message}");
        }

        public void Debug(string format, params object[] paramList)
        {
            Console.WriteLine($@"DEBUG: {GenerateMessage(format, paramList)}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($@"WARNING: {message}");
        }

        public void Warning(string format, params object[] paramList)
        {
            Console.WriteLine($@"WARNING: {GenerateMessage(format, paramList)}");
        }

        public void Error(string message)
        {
            Console.WriteLine($@"ERROR: {message}");
        }

        public void Error(string format, params object[] paramList)
        {
            Console.WriteLine($@"ERROR: {GenerateMessage(format, paramList)}");
        }

        private static string GenerateMessage(string format, params object[] paramList)
        {
            return string.Format(format, paramList);
        }
    }
}