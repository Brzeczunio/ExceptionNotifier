using Queris.ExceptionNotifier.AggregatorsValidator;
using Queris.ExceptionNotifier.App.Entities;
using Queris.ExceptionNotifier.App.Providers;
using Queris.ExceptionNotifier.ClientsManager;
using Queris.ExceptionNotifier.Common.Loggers;
using Queris.ExceptionNotifier.FiltersValidator;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities;
using Queris.ExceptionNotifier.Log4netLogger;
using Queris.ExceptionNotifier.PrepareNotifierProcessors;
using Queris.ExceptionNotifier.PrepareNotifierProcessors.Enums;
using Queris.ExceptionNotifier.Serializers.Json;
using System;

namespace Queris.ExceptionNotifier.App
{
    internal static class Program
    {
        private static void Main()
        {
            var jsonSerializer = new JsonSerializer();
            var configManager = new ConfigManager.ConfigManager(ConfigProvider.FileName, jsonSerializer);
            
            var logger = ConfigProvider.Log4NetLogger ? (ILogger) new Log4NetLogger(ConfigProvider.Log4NetLoggerDaysToKeepLogs) : new ConsoleLogger();

            try
            {
                var param = new Params(configManager, jsonSerializer);

                new PrepareNotifierProcessorFactory().Create(NotifierProcessorType.NotifierProcessorDecoratorType).Prepare(
                    new HostFactoryNotifierProcessor.HostFactoryNotifierProcessor(new HostFactoryNotifierProcessorParams(param.Readers.ToArray(),
                    new NotificationsClientsManager(param.ClientsManagerParams), new NotificationFiltersValidator(), param.FiltersValidatorRepository,
                    new NotificationAggregatorsValidator(), param.AggregatorsValidatorRepository, logger, ConfigProvider.SendNotificationInterval)),
                    param.ExitReader, new NotificationsClientsManager(param.ClientsManagerParams), logger, ConfigProvider.ApplicationLifetime,
                    ConfigProvider.CheckApplicationLifetimeInterval);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
