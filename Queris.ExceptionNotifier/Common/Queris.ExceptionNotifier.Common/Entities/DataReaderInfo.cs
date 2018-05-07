using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class DataReaderInfo
    {
        public INotificationDataReader Reader { get; set; }
        public ADataReaderParams ReaderParams { get; set; }
    }
}