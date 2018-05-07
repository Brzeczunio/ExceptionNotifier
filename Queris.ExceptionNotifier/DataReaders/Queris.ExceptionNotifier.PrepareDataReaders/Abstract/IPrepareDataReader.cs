using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.PrepareDataReaders.Abstract
{
    public interface IPrepareDataReader
    {
        INotificationDataReader Prepare(ConfigManager.Models.Reader reader, ConfigManager.ConfigManager configManager);
    }
}
