using log4net;
using log4net.Appender;
using Queris.ExceptionNotifier.Common.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4NetLogger.config", Watch = true)]
namespace Queris.ExceptionNotifier.Log4netLogger
{
    public class Log4NetLogger : ILogger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Timer _timer;
        private bool _lock;

        public Log4NetLogger(int daysToKeep)
        {
            var rootAppender = LogManager.GetRepository().GetAppenders().OfType<FileAppender>().Select(x => x.File);

            if (daysToKeep <= 0) return;

            _timer = new Timer(TimeSpan.FromHours(24).TotalMilliseconds) {AutoReset = true};
            _timer.Elapsed += (sender, eventArgs) =>
            {
                CleanupLogs(rootAppender, daysToKeep);
            };

            _lock = false;

            Start();
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Info(string format, params object[] paramList)
        {
            Log.InfoFormat(format, paramList);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Debug(string format, params object[] paramList)
        {
            Log.DebugFormat(format, paramList);
        }

        public void Warning(string message)
        {
            Log.Warn(message);
        }

        public void Warning(string format, params object[] paramList)
        {
            Log.WarnFormat(format, paramList);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string format, params object[] paramList)
        {
            Log.ErrorFormat(format, paramList);
        }

        internal void CleanupLogs(IEnumerable<string> logPath, int maxAgeInDays)
        {
            try
            {
                if (_lock) return;

                _lock = true;

                foreach (var path in logPath)
                {
                    if (!File.Exists(path)) return;

                    const string datePattern = "yyyy-MM-dd";
                    var logPatternsToKeep = new List<string>();
                    for (var i = 0; i < maxAgeInDays; i++)
                    {
                        logPatternsToKeep.Add(DateTime.Now.AddDays(-i).ToString(datePattern));
                    }

                    var fi = new FileInfo(path);

                    if (fi.Directory == null) return;
                    var logFiles = fi.Directory.GetFiles()
                        .Where(x => logPatternsToKeep.All(y => !x.Name.Contains(y)));

                    foreach (var log in logFiles)
                    {
                        if (File.Exists(log.FullName)) File.Delete(log.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                _lock = false;
            }
        }

        internal void Start()
        {
            _timer.Start();
        }

        internal void Stop()
        {
            _timer.Stop();
        }

        internal void Close()
        {
            _timer.Close();
        }

        ~Log4NetLogger()
        {
            if(!(_timer is null)) Close();
        }
    }
}
