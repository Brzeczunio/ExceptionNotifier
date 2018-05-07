using Queris.ExceptionNotifier.Common.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class AggregatorsValidatorRepository : IAggregatorsValidatorRepository
    {
        public List<AAggregators> Aggregators { get; set; }
        /// <summary>
        /// Rules:
        /// key - aggregatorId
        /// values - collection of readersIds
        /// </summary>
        public Dictionary<int, int[]> Rules { get; set; }

        public AAggregators GetAggregators(int readerId)
        {
            return Aggregators.FirstOrDefault(x => Rules.Where(y => y.Value.Contains(readerId)).Any(z => z.Value.Contains(x.Id)));
        }
    }
}
