namespace Queris.ExceptionNotifier.ConfigManager.Models
{
    public class Config
    {
        public Client[] Clients { get; set; }
        public Reader[] Readers { get; set; }
        public Filters[] Filters { get; set; }
        public Aggregators[] Aggregators { get; set; }
        public Rules[] Rules { get; set; }
        public FilterRules[] FilterRules { get; set; }
        public AggregatorRules[] AggregatorRules { get; set; }
    }
}
