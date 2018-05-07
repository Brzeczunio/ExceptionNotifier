using Queris.ExceptionNotifier.Common.Entities;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class ANotificationDataReaderDecorator : INotificationDataReader, INotificationValidator, INotificationAggregator
    {
        protected INotificationDataReader NotificationDataReader;

        protected ANotificationDataReaderDecorator(INotificationDataReader notificationDataReader) => NotificationDataReader = notificationDataReader;

        public abstract List<NotificationMessage> GetData();

        public abstract void SetAggregator(INotificationAggregatorsValidator aggregator);

        public abstract void SetValidator(INotificationFiltersValidator validator);
    }
}
