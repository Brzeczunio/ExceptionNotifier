using Queris.ExceptionNotifier.Common.Entities;
using System.Linq;
using System.Text;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class AFilters
    {
        public int Id { get; set; }

        public FilterInfo[] FilterParams { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{ nameof(Id)}: {Id}, ");
            sb.AppendLine($"Filters: {FilterParams.Length}: {{");
            if (!FilterParams.Any()) return sb.ToString();
            foreach (var f in FilterParams) sb.AppendLine($"\t{f}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
