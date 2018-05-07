using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class DatabaseDataReaderParams : ADataReaderParams
    {
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string MessageTypeColumnName { get; set; }
        public string OrderByColumnName { get; set; }
        public bool MessageAggregate { get; set; }
        public string[] MessageAggregateColumnNames { get; set; }

        public override string ToString()
        {
            return $"{nameof(Schema)}: {Schema}, {nameof(TableName)}: {TableName}, {nameof(MessageTypeColumnName)}: {MessageTypeColumnName}, " +
                $"{nameof(OrderByColumnName)}: {OrderByColumnName}, {nameof(MessageAggregate)}: {MessageAggregate}, " +
                $"{nameof(MessageAggregateColumnNames)}: [{string.Join(",", MessageAggregateColumnNames)}]";
        }
    }
}