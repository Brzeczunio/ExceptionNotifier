using System;
using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.Common.ExtMethods;
using Queris.ExceptionNotifier.PrepareClients;
using Queris.ExceptionNotifier.PrepareClients.Enums;
using Queris.ExceptionNotifier.PrepareDataReaders;
using Queris.ExceptionNotifier.PrepareDataReaders.Enums;
using Queris.ExceptionNotifier.Serializers;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.App.Entities
{
    internal class Params
    {
        internal List<INotificationDataReader> Readers { get; set; }
        internal INotificationDataReader ExitReader { get; set; }
        internal List<INotificationClient> Clients { get; set; }
        internal ClientsManagerParams ClientsManagerParams { get; set; }
        internal FiltersValidatorRepository FiltersValidatorRepository { get; set; }
        internal AggregatorsValidatorRepository AggregatorsValidatorRepository { get; set; }

        internal Params(ConfigManager.ConfigManager configManager, ISerializer serializer)
        {
            CheckParams(configManager, serializer);

            PrepareReaders(configManager);
            PrepareClients(configManager, serializer);
            PrepareClientsManagerParams(configManager);
            PrepareFiltersValidatorRepository(configManager);
            PrepareAggregatorsValidatorRepository(configManager);
        }

        private void PrepareReaders(ConfigManager.ConfigManager configManager)
        {
            Readers = new List<INotificationDataReader>();
            ExitReader = null;

            if (configManager.Config.Readers is null) return;

            foreach (var reader in configManager.Config.Readers)
            {
                if (!reader.ReaderType.ToEnum<DataReaderType>().Equals(DataReaderType.ExitReader))
                {
                    var pDataReader = new PrepareDataReaderFactory().Create(reader.ReaderType.ToEnum<DataReaderType>());
                    Readers.Add(pDataReader.Prepare(reader, configManager));
                }
                else
                {
                    ExitReader = new PrepareDataReaderFactory().Create(reader.ReaderType.ToEnum<DataReaderType>())
                        .Prepare(reader, configManager);
                }
            }
        }

        private void PrepareClients(ConfigManager.ConfigManager configManager, ISerializer serializer)
        {
            Clients = new List<INotificationClient>();

            if (configManager.Config.Clients is null) return;

            var clientFactory = new PrepareClientFactory();
            foreach (var client in configManager.Config.Clients)
            {
                if (Clients.Any(x => x is AClient && ((AClient)x).Id == client.ClientId)) continue;
                var pClient = clientFactory.Create(client.ClientType.ToEnum<ClientType>());
                Clients.Add(pClient.Prepare(client, serializer));
            }
        }

        private void PrepareClientsManagerParams(ConfigManager.ConfigManager configManager)
        {
            if (configManager.Config.Rules is null) return;

            ClientsManagerParams = new ClientsManagerParams { Clients = Clients, Rules = new Dictionary<int, int[]>() };
            foreach (var rule in configManager.Config.Rules)
            { ClientsManagerParams.Rules.Add(rule.ClientId, rule.ReaderId); }
        }

        private void PrepareFiltersValidatorRepository(ConfigManager.ConfigManager configManager)
        {
            if (configManager.Config.Filters is null || configManager.Config.FilterRules is null) return;

            var filters = new List<AFilters>();
            foreach (var f in configManager.Config.Filters)
            { filters.Add(new Filters { Id = f.FilterId, FilterParams = f.FilterParams }); }

            FiltersValidatorRepository = new FiltersValidatorRepository { Filters = filters, Rules = new Dictionary<int, int[]>() };
            foreach (var filterRules in configManager.Config.FilterRules)
            { FiltersValidatorRepository.Rules.Add(filterRules.FilterId, filterRules.ReaderId); }
        }

        private void PrepareAggregatorsValidatorRepository(ConfigManager.ConfigManager configManager)
        {
            if (configManager.Config.Aggregators is null || configManager.Config.AggregatorRules is null) return;

            var aggregators = new List<AAggregators>();
            foreach (var a in configManager.Config.Aggregators)
            { aggregators.Add(new Aggregators { Id = a.AggregatorId, AggregationParams = a.AggregationParams }); }

            AggregatorsValidatorRepository = new AggregatorsValidatorRepository { Aggregators = aggregators, Rules = new Dictionary<int, int[]>() };
            foreach (var aggregatorRules in configManager.Config.AggregatorRules)
            { AggregatorsValidatorRepository.Rules.Add(aggregatorRules.AggregatorId, aggregatorRules.ReaderId); }
        }

        void CheckParams(ConfigManager.ConfigManager configManager, ISerializer serializer)
        {
            if (configManager is null) throw new ArgumentNullException($"{nameof(configManager)} cannot be null");
            if (serializer is null) throw new ArgumentNullException($"{nameof(serializer)} cannot be null");
        }
    }
}
