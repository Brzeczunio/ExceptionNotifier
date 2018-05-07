namespace Queris.ExceptionNotifier.Common.Entities
{
    public class AggregationInfo
    {
        public string[] MessageAggregateColumnNames { get; set; }

        public override string ToString()
        {
            return $"{nameof(MessageAggregateColumnNames)}: [{string.Join(",", MessageAggregateColumnNames)}]";
        }
    }
}
