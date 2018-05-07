using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.Common.Helpers;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.DatabaseDataReader
{
    public class DatabaseDataReader : INotificationDataReader, INotificationValidator, INotificationAggregator
    {
        private readonly FieldsContainer _container;
        private readonly DatabaseDataReaderParams _params;
        private readonly CounterReader _counterReader;
        private INotificationFiltersValidator _validator;
        private INotificationAggregatorsValidator _aggregator;
        private readonly dynamic _db;

        public DatabaseDataReader(string connStrName, ADataReaderParams readerParams, ACounterReader counterReader)
        {
            _params = readerParams as DatabaseDataReaderParams;
            _counterReader = counterReader as CounterReader;
            CheckParams(_params);
            _container = new FieldsContainer();
            _db = Database.OpenConnection(connStrName);
        }

        public List<NotificationMessage> GetData() => GetDataFromDb();
        public void SetValidator(INotificationFiltersValidator validator)
        {
            _validator = validator;
        }

        public void SetAggregator(INotificationAggregatorsValidator aggregator)
        {
            _aggregator = aggregator;
        }

        private int Count => _db[_params.Schema][_params.TableName].GetCount();

        private void GetColumnsByColumnsNames(dynamic columns)
        {
            foreach (var f in columns)
                { if (Array.Exists(_params.ColumnsNames, s => s.Equals(f.Key))) _container.Add(f.Key, value: f.Value?.ToString() ?? string.Empty); }
        }

        private string GetMessageType(List<FieldInfo> record)
        {
            var ret = string.Empty;
            if (string.IsNullOrEmpty(_params.MessageTypeColumnName)) return ret;

            foreach (var f in record)
            {
                if (f.Name == _params.MessageTypeColumnName)
                {
                    ret = f.Value;
                    break;
                }
            }
            return ret;
        }

        private List<NotificationMessage> GetDataFromDb()
        {
            var ret = new List<NotificationMessage>();
            var paramsInitialCounter = _params.InitialCounter;
            var recsToTake = RowsHelper.MaxRowsToGet(Count, ref paramsInitialCounter);
            _params.InitialCounter = paramsInitialCounter;
            if (recsToTake.Equals(0))
            {
                _counterReader.Save(_params.InitialCounter);
                return ret;
            }
            _container.Clear();

            foreach (var r in _db[_params.Schema][_params.TableName].All().Take(recsToTake).OrderByDescending(_db[_params.Schema][_params.TableName][_params.OrderByColumnName]))
            {
                _container.Clear();
                GetColumnsByColumnsNames(r);

                if (_validator?.Validate(_container) == true) continue;
                if (_aggregator?.AggregateIfPossible(ret, _container) != false) continue;
                
                ret.Add(CreateMessage());
            }

            _counterReader.Save(_params.InitialCounter);

            ret.Reverse();
            return ret;
        }

        private NotificationMessage CreateMessage()
        {
            return new NotificationMessage
            {
                ReaderId = _counterReader.GetId,
                LogicalStorage = _params.LogicalStorage,
                MessageType = GetMessageType(_container.Fields),
                Fields = new List<FieldInfo>(_container.Fields)
            };
        }

        private void CheckParams(DatabaseDataReaderParams p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (string.IsNullOrEmpty(p.TableName)) throw new ArgumentNullException($"{nameof(p.TableName)} is empty");
            if (!p.ColumnsNames.Any()) throw new ArgumentException("Column doesn't exist!");
        }
    }
}
