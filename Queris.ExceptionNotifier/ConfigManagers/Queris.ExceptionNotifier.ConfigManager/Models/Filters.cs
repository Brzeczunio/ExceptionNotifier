using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.ConfigManager.Models
{
    public class Filters
    {
        public int FilterId { get; set; }
        public FilterInfo[] FilterParams { get; set; }
    }
}
