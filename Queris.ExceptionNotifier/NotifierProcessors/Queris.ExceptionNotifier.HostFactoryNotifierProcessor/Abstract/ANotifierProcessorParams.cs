using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Loggers;

namespace Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract
{
    public abstract class ANotifierProcessorParams
    {
        public INotifierProcessor NotifierProcessor { get; set; } = null;
        public INotificationClientsManager ClientsManager { get; set; } = null;
        public ILogger Logger { get; set; } = null;
        public int Interval { get; set; } = 0;
    }
}
