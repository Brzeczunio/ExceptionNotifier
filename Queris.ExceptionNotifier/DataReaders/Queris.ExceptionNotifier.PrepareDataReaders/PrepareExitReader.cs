using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.PrepareDataReaders.Abstract;

namespace Queris.ExceptionNotifier.PrepareDataReaders
{
    public class PrepareExitReader : IPrepareDataReader
    {
        public INotificationDataReader Prepare(ConfigManager.Models.Reader reader, ConfigManager.ConfigManager configManager)
        {
            var exitReaderParams = new ExitDataReaderParams { LogicalStorage = reader.LogicalName };
            var setCounterReader = new CounterReader(configManager, reader.ReaderId);

            return new ExitReader.ExitReader(exitReaderParams, setCounterReader);
        }
    }
}
