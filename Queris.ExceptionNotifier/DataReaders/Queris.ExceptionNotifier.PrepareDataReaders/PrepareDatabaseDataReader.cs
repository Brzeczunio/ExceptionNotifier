using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.PrepareDataReaders.Abstract;

namespace Queris.ExceptionNotifier.PrepareDataReaders
{
    public class PrepareDatabaseDataReader : IPrepareDataReader
    {
        public INotificationDataReader Prepare(ConfigManager.Models.Reader reader, ConfigManager.ConfigManager configManager)
        {
            var databaseDataReaderParams = new DatabaseDataReaderParams
            {
                LogicalStorage = reader.LogicalName,
                Schema = reader.ReaderAdditionalParams.Schema ?? "dbo",
                TableName = reader.ReaderAdditionalParams.TableName,
                OrderByColumnName = reader.ReaderAdditionalParams.OrderByColumnName,
                InitialCounter = reader.InitialCounter,
                MessageTypeColumnName = reader.ReaderAdditionalParams.MessageTypeColumnName,
                ColumnsNames = reader.ColumnNames
            };

            var setCounterReader = new CounterReader(configManager, reader.ReaderId);

            var notificationDataReaderDecorator = new NotificationDataReaderDecorator(
                new DatabaseDataReader.DatabaseDataReader(reader.ConnectionString, databaseDataReaderParams, setCounterReader), reader.ReaderId);

            return notificationDataReaderDecorator;
        }
    }
}
