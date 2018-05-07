using Queris.ExceptionNotifier.Common.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class FiltersValidatorRepository : IFiltersValidatorRepository
    {
        public List<AFilters> Filters { get; set; }
        /// <summary>
        /// Rules:
        /// key - filterId
        /// values - collection of readersIds
        /// </summary>
        public Dictionary<int, int[]> Rules { get; set; }

        public AFilters GetFilters(int readerId)
        {
            return Filters.FirstOrDefault(x => Rules.Where(y => y.Value.Contains(readerId)).Any(z => z.Value.Contains(x.Id)));
        }
    }
}
