using Queris.ExceptionNotifier.Common.Abstract;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class NotificationDataReaderDecorator : ANotificationDataReaderDecorator
    {
        public NotificationDataReaderDecorator(INotificationDataReader notificationDataReader, int id) : base(notificationDataReader)
        {
            _id = id;
        }

        public override List<NotificationMessage> GetData()
        {
            return NotificationDataReader.GetData();
        }

        public override void SetAggregator(INotificationAggregatorsValidator aggregator)
        {
            (NotificationDataReader as INotificationAggregator)?.SetAggregator(aggregator);
        }

        public override void SetValidator(INotificationFiltersValidator validator)
        {
            (NotificationDataReader as INotificationValidator)?.SetValidator(validator);
        }

        private readonly int _id;

        public int GetId() => _id;
    }
}
