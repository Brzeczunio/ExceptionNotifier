using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.PrepareDataReaders
{
    public static class PrepareDataReader
    {
        public static INotificationDataReader Prepare(ConfigManager.Models.Reader reader, ConfigManager.ConfigManager configManager)
        {
            var databaseDataReaderParams = new DatabaseDataReaderParams { LogicalStorage = reader.LogicalName, TableName = reader.AdditionalParams.TableName, InitialCounter = reader.InitialCounter, MessageTypeColumnName = reader.AdditionalParams.MessageTypeColumnName, ColumnsNames = reader.ColumnNames };
            var setCounterReader = new Reader(configManager, reader.ReaderId);

            return new DatabaseDataReader.DatabaseDataReader(reader.ConnectionString, databaseDataReaderParams, setCounterReader);
        }
    }
}
