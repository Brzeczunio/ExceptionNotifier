using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Loggers;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract;
using System;

namespace Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities
{
    public class NotifierProcessorDecoratorParams : ANotifierProcessorParams
    {
        public INotificationDataReader DataReader { get; set; }
        public DateTime Lifetime { get; set; } = DateTime.MinValue;

        public NotifierProcessorDecoratorParams(params object[] paramList)
        {
            foreach (var param in paramList)
            {
                switch (param)
                {
                    case INotifierProcessor processor:
                        NotifierProcessor = processor;
                        break;
                    case INotificationDataReader dataReader:
                        DataReader = dataReader;
                        break;
                    case INotificationClientsManager clientsManager:
                        ClientsManager = clientsManager;
                        break;
                    case ILogger log:
                        Logger = log;
                        break;
                    case DateTime time:
                        Lifetime = time;
                        break;
                    case int inter:
                        Interval = inter;
                        break;
                }
            }
        }
    }
}
