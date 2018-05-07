using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.ConfigManager.Models;
using Queris.ExceptionNotifier.Serializers;

namespace Queris.ExceptionNotifier.PrepareClients.Abstract
{
    public interface IPrepareClient
    {
        INotificationClient Prepare(Client client, ISerializer serializer);
    }
}
