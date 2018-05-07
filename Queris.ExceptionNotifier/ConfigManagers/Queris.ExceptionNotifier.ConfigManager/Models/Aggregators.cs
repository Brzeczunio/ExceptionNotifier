using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.ConfigManager.Models
{
    public class Aggregators
    {
        public int AggregatorId { get; set; }
        public AggregationInfo[] AggregationParams { get; set; }
    }
}
