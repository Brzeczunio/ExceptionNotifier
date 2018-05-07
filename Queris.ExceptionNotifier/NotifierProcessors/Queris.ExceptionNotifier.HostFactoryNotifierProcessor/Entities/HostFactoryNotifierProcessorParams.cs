using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Loggers;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract;

namespace Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities
{
    public class HostFactoryNotifierProcessorParams : ANotifierProcessorParams
    {
        public INotificationDataReader[] DataReaders { get; set; }
        public INotificationFiltersValidator FiltersValidator { get; set; }
        public IFiltersValidatorRepository FiltersValidatorRepository { get; set; }
        public INotificationAggregatorsValidator AggregatorsValidator { get; set; }
        public IAggregatorsValidatorRepository AggregatorsValidatorRepository { get; set; }

        public HostFactoryNotifierProcessorParams(params object[] paramList)
        {
            foreach (var param in paramList)
            {
                switch (param)
                {
                    case INotifierProcessor processor:
                        NotifierProcessor = processor;
                        break;
                    case INotificationDataReader[] dataReaders:
                        DataReaders = dataReaders;
                        break;
                    case INotificationClientsManager clientsManager:
                        ClientsManager = clientsManager;
                        break;
                    case INotificationFiltersValidator notificationFiltersValidator:
                        FiltersValidator = notificationFiltersValidator;
                        break;
                    case IFiltersValidatorRepository filtersRepository:
                        FiltersValidatorRepository = filtersRepository;
                        break;
                    case INotificationAggregatorsValidator notificationAggregatorsValidator:
                        AggregatorsValidator = notificationAggregatorsValidator;
                        break;
                    case IAggregatorsValidatorRepository aggregatorsRepository:
                        AggregatorsValidatorRepository = aggregatorsRepository;
                        break;
                    case ILogger log:
                        Logger = log;
                        break;
                    case int inter:
                        Interval = inter;
                        break;
                }
            }
        }
    }
}
