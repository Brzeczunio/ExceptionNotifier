using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.ExtMethods;
using Queris.ExceptionNotifier.Common.Helpers;
using Queris.ExceptionNotifier.Common.Loggers;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Timers;

namespace Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Decorators
{
    public class NotifierProcessorDecorator : ANotifierProcessorDecorator
    {
        private readonly INotificationDataReader _dataReader;
        private readonly INotificationClientsManager _clientsManager;
        private readonly ILogger _logger;

        private readonly Timer _timer;
        private readonly DateTime _lifetime;

        public NotifierProcessorDecorator(ANotifierProcessorParams notifierProcessorParams) : base(notifierProcessorParams.NotifierProcessor)
        {
            if (CheckTimeForCloseApp(((NotifierProcessorDecoratorParams) notifierProcessorParams).Lifetime)) return;
            _dataReader = ((NotifierProcessorDecoratorParams) notifierProcessorParams).DataReader;
            _clientsManager = notifierProcessorParams.ClientsManager;
            _logger = notifierProcessorParams.Logger;

            if (((NotifierProcessorDecoratorParams) notifierProcessorParams).Lifetime != DateTime.MinValue)
            {
                _lifetime = ((NotifierProcessorDecoratorParams) notifierProcessorParams).Lifetime;
                _timer = new Timer(notifierProcessorParams.Interval) {AutoReset = true};
                _timer.Elapsed += (sender, eventArgs) =>
                {
                    Close();
                };

                _timer.Start();
            }

            Execute();
        }

        private void Close()
        {
            var method = UtilHelper.GetMethodName(MethodBase.GetCurrentMethod());
            _logger?.Info($"{method} => start");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (!CheckTimeForCloseApp(_lifetime)) { _logger?.Debug("Not time to die..."); return; }

            try
            {
                var data = _dataReader.GetData();

                var sendedMessages = new Dictionary<string, int>();
                foreach (var message in data)
                {
                    _clientsManager.Send(message);
                    sendedMessages.AddOrIncrement(message.LogicalStorage, 1);
                }

                foreach (var message in sendedMessages)
                {
                    _logger?.Debug($"Sended: {message.Value} messages about application exit from {message.Key}");
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                _logger?.Info($"{method} => end, Time taken: {elapsedMs}ms");
            }
            catch (Exception ex)
            {
                _logger?.Error(ex.Message);
            }
            finally
            {
                _timer.Stop();
                Environment.Exit(0);
            }
        }

        public sealed override void Execute()
        {
            NotifierProcessor.Execute();
        }

        private static bool CheckTimeForCloseApp(DateTime time)
        {
            if (time.Equals(DateTime.MinValue)) return false;
            switch (DateTime.Now.CompareTo(time))
            {
                case 1:
                    return true;
                default:
                    return false;
            }
        }
    }
}
