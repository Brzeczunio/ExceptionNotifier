using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Crytpo;
using Queris.ExceptionNotifier.ConfigManager.Models;
using Queris.ExceptionNotifier.MailNotificationClient.Entities;
using Queris.ExceptionNotifier.PrepareClients.Abstract;
using Queris.ExceptionNotifier.Serializers;

namespace Queris.ExceptionNotifier.PrepareClients
{
    public class PrepareMailClient : IPrepareClient
    {
        public INotificationClient Prepare(Client client, ISerializer serializer) => 
            new MailNotificationClient.MailNotificationClient(new MailInitParams
            {
                ConfigPath = client.ClientAdditionalParams.ConfigPath
            }, client.ClientId, serializer, new Crypto());
    }
}
