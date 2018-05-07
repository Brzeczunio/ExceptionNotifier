using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.ExtMethods;
using Queris.ExceptionNotifier.Common.Helpers;
using Queris.ExceptionNotifier.Common.Loggers;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using Queris.ExceptionNotifier.Common.Entities;
using Topshelf;

namespace Queris.ExceptionNotifier.HostFactoryNotifierProcessor
{
    public class HostFactoryNotifierProcessor : INotifierProcessor
    {
        private readonly INotificationDataReader[]  _dataReaders;
        private readonly INotificationClientsManager _clientsManager;
        private readonly INotificationFiltersValidator _filtersValidator;
        private readonly IFiltersValidatorRepository _filtersValidatorRepository;
        private readonly INotificationAggregatorsValidator _aggregatorsValidator;
        private readonly IAggregatorsValidatorRepository _aggregatorsValidatorRepository;
        private readonly ILogger _logger;

        private readonly Timer _timer;

        private bool _lock;

        public HostFactoryNotifierProcessor(ANotifierProcessorParams notifierProcessorParams)
        {
            _dataReaders = (notifierProcessorParams as HostFactoryNotifierProcessorParams)?.DataReaders;
            _clientsManager = notifierProcessorParams.ClientsManager;
            _logger = notifierProcessorParams.Logger;
            _filtersValidator = (notifierProcessorParams as HostFactoryNotifierProcessorParams)?.FiltersValidator;
            _filtersValidatorRepository = (notifierProcessorParams as HostFactoryNotifierProcessorParams)?.FiltersValidatorRepository;
            _aggregatorsValidator = (notifierProcessorParams as HostFactoryNotifierProcessorParams)?.AggregatorsValidator;
            _aggregatorsValidatorRepository = (notifierProcessorParams as HostFactoryNotifierProcessorParams)?.AggregatorsValidatorRepository;

            _lock = false;

            _timer = new Timer(notifierProcessorParams.Interval) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) =>
            {
                ProcessingData();
            };
        }

        public void Execute()
        {
            HostFactory.Run(x =>
            {
                x.Service<HostFactoryNotifierProcessor>(s => 
                {
                    s.ConstructUsing(name => this);
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                const string serviceName = "Queris ExceptionNotification";
                x.SetDescription(serviceName);
                x.SetDisplayName(serviceName);
                x.SetServiceName("QuerisExceptionNotification");
                x.StartAutomatically();
            });
        }

        internal void Start()
        {
            ProcessingData();
            _timer.Start();
        }

        internal void Stop()
        {
            _timer.Stop();
        }

        internal void ProcessingData()
        {
            try
            {
                if (_lock) return;

                _lock = true;

                var method = UtilHelper.GetMethodName(MethodBase.GetCurrentMethod());
                _logger?.Info($"{method} => start");
                var watch = System.Diagnostics.Stopwatch.StartNew();
                
                var data = DataReaderHelper.GetData(
                    _dataReaders, _filtersValidator, _filtersValidatorRepository, _aggregatorsValidator, _aggregatorsValidatorRepository);
                if (!data.Any())
                {
                    _logger?.Debug("No data found...");
                    return;
                }

                var sendedMessages = new Dictionary<SendedMessages, int>();
                foreach (var message in data)
                {
                    if (!_clientsManager.Send(message))
                    {
                        _logger?.Debug("No clients defined to send...");
                        return;
                    }
                    var sendedMessage = new SendedMessages {Name = message.LogicalStorage, Id = message.ReaderId};
                    sendedMessages.AddOrIncrement(sendedMessage, 1);
                }

                foreach (var message in sendedMessages)
                {
                    _logger?.Debug($"Sended: {message.Value} messages from {message.Key}");
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                _logger?.Info($"{method} => end, Time taken: {elapsedMs}ms");   
            }
            catch (Exception ex)
            {
                _logger?.Error(ex.Message);
            }
            finally
            {
                _lock = false;
            }
        }
    }
}
