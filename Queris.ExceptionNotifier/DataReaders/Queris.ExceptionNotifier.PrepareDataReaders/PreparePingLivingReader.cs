using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.PingReader;
using Queris.ExceptionNotifier.PrepareDataReaders.Abstract;

namespace Queris.ExceptionNotifier.PrepareDataReaders
{
    public class PreparePingLivingReader : IPrepareDataReader
    {
        public INotificationDataReader Prepare(ConfigManager.Models.Reader reader, ConfigManager.ConfigManager configManager)
        {
            var pingReaderParams = new PingDataReaderParams
            {
                LogicalStorage = reader.LogicalName,
                StartTime = reader.ReaderAdditionalParams.StartTime,
                Interval = reader.ReaderAdditionalParams.Interval,
                Occurs = reader.ReaderAdditionalParams.Occurs
            };

            var setCounterReader = new CounterReader(configManager, reader.ReaderId);

            return new PingLivingReader(pingReaderParams, setCounterReader);
        }
    }
}
