using Queris.ExceptionNotifier.Common.Entities;
using System.Linq;
using System.Text;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class AAggregators
    {
        public int Id { get; set; }

        public AggregationInfo[] AggregationParams { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{ nameof(Id)}: {Id}, ");
            sb.AppendLine($"Aggregators: {AggregationParams.Length}: {{");
            if (!AggregationParams.Any()) return sb.ToString();
            foreach (var f in AggregationParams) sb.AppendLine($"\t{f}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
