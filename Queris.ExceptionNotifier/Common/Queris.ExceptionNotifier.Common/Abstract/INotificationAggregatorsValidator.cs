using Queris.ExceptionNotifier.Common.Entities;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationAggregatorsValidator
    {
        bool AggregateIfPossible(List<NotificationMessage> listMessages, FieldsContainer message);
        void SetAggregators(AAggregators aggregators);
    }
}
