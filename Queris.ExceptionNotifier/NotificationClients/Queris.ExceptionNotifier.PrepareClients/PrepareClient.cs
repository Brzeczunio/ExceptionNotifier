using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.ConfigManager.Models;
using Queris.ExceptionNotifier.Serializers;
using Queris.ExceptionNotifier.SlackNotificationClient.Entities;

namespace Queris.ExceptionNotifier.PrepareClients
{
    public static class PrepareClient
    {
        public static INotificationClient Prepare(Client client, ISerializer serializer)
        {
            if (client.ClientType.Equals("slackClient"))
                return PrepareSlackClient(client, serializer);
            else
                return PrepareFileClient(client);
        }

        private static SlackNotificationClient.SlackNotificationClient PrepareSlackClient(Client c, ISerializer serializer)
        {
            return new SlackNotificationClient.SlackNotificationClient(new SlackInitParams { Url = c.AdditionalParams.Url, Channel = c.AdditionalParams.channel, Username = c.AdditionalParams.UserName }, c.ClientId, serializer);
        }

        private static FileNotificationClient.FileNotificationClient PrepareFileClient(Client c)
        {
            return new FileNotificationClient.FileNotificationClient(c.ClientId);
        }
    }
}
