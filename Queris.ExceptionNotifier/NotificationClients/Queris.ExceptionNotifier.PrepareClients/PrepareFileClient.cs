using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.ConfigManager.Models;
using Queris.ExceptionNotifier.PrepareClients.Abstract;
using Queris.ExceptionNotifier.Serializers;

namespace Queris.ExceptionNotifier.PrepareClients
{
    public class PrepareFileClient : IPrepareClient
    {
        public INotificationClient Prepare(Client client, ISerializer serializer) => 
            new FileNotificationClient.FileNotificationClient(client.ClientId, serializer);
    }
}
