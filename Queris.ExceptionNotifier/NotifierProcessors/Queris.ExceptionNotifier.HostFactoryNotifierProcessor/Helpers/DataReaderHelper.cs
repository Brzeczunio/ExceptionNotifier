using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Helpers
{
    internal static class DataReaderHelper
    {
        internal static List<NotificationMessage> GetData(INotificationDataReader[] readers, INotificationFiltersValidator filtersValidator,
            IFiltersValidatorRepository filtersValidatorRepository, INotificationAggregatorsValidator aggregatorsValidator,
            IAggregatorsValidatorRepository aggregatorsValidatorRepository)
        {
            var ret = new List<NotificationMessage>();
            if (!readers.Any()) return ret;

            foreach (var r in readers)
            {
                if (r is NotificationDataReaderDecorator decorator)
                {
                    filtersValidator?.SetFilters(filtersValidatorRepository?.GetFilters(decorator.GetId()));
                    (r as INotificationValidator).SetValidator(filtersValidator);
                    aggregatorsValidator?.SetAggregators(aggregatorsValidatorRepository?.GetAggregators(decorator.GetId()));
                    (r as INotificationAggregator).SetAggregator(aggregatorsValidator);
                }
                ret.AddRange(r.GetData());
            }

            return ret;
        }
    }
}