using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.AggregatorsValidator
{
    public class NotificationAggregatorsValidator : INotificationAggregatorsValidator
    {
        private AAggregators _aggregators;

        public bool AggregateIfPossible(List<NotificationMessage> listMessages, FieldsContainer fields)
        {
            if (_aggregators == null || !fields.Fields.Any() || listMessages.Count == 0) return false;

            var aggregationColumnNameList = PrepareAggregationColumnNameList(fields);

            if (!aggregationColumnNameList.Any()) return false;

            NotificationMessage message = null;

            foreach (var m in listMessages)
            {
                var aggregationColumnNameListCount = 0;

                aggregationColumnNameListCount += aggregationColumnNameList.Count(a => m.Fields.Any(x => x.Value.Equals(a.Value)));

                if (aggregationColumnNameListCount == aggregationColumnNameList.Count)
                {
                    message = m;
                    break;
                }
            }

            if (message == null) return false;

            IncermenetCount(message);
            return true;
        }

        private void IncermenetCount(NotificationMessage message)
        {
            if (message.AggregatedMessagesCount > 0)
            {
                message.AggregatedMessagesCount++;
            }
            else
            {
                message.AggregatedMessagesCount = 2;
            }
        }

        private List<FieldInfo> PrepareAggregationColumnNameList(FieldsContainer message)
        {
            var aggregationColumnNameList = new List<FieldInfo>();

            foreach (var aggregationParams in _aggregators.AggregationParams)
            {
                foreach (var columnName in aggregationParams.MessageAggregateColumnNames)
                {
                    if (message.Fields.FirstOrDefault(x => x.Name.Equals(columnName)) != null)
                    aggregationColumnNameList.Add(message.Fields.FirstOrDefault(x => x.Name.Equals(columnName)));
                }
            }

            return aggregationColumnNameList;
        }

        public void SetAggregators(AAggregators aggregators) => _aggregators = aggregators;

        public override string ToString() => _aggregators?.ToString() ?? "NONE";
    }
}
