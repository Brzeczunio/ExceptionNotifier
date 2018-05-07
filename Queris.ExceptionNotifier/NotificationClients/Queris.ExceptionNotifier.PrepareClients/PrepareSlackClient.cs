using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.ConfigManager.Models;
using Queris.ExceptionNotifier.PrepareClients.Abstract;
using Queris.ExceptionNotifier.Serializers;
using Queris.ExceptionNotifier.SlackNotificationClient.Entities;

namespace Queris.ExceptionNotifier.PrepareClients
{
    public class PrepareSlackClient : IPrepareClient
    {
        public INotificationClient Prepare(Client client, ISerializer serializer) => new SlackNotificationClient.SlackNotificationClient(new SlackInitParams
        {
            Url = client.ClientAdditionalParams.Url,
            Channel = client.ClientAdditionalParams.Channel,
            Username = client.ClientAdditionalParams.UserName,
            ExtractTheHeader = client.ClientAdditionalParams.ExtractTheHeader,
            ConfigPath = client.ClientAdditionalParams.ConfigPath
        }, client.ClientId, serializer);
    }
}
