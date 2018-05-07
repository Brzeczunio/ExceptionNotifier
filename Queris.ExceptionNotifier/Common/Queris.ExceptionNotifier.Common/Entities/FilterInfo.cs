namespace Queris.ExceptionNotifier.Common.Entities
{
    public class FilterInfo
    {
        public string ColumnName { get; set; }
        public string[] Patterns { get; set; }
        public string MatchPatternOperator { get; set; }

        public override string ToString()
        {
            return $"{nameof(ColumnName)}: {ColumnName}, {nameof(Patterns)}: [{string.Join(",", Patterns)}], " +
                   $"{nameof(MatchPatternOperator)}: {MatchPatternOperator}";
        }
    }
}
