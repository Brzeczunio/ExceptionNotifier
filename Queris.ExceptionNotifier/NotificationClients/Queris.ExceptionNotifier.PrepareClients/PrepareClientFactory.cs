using Queris.ExceptionNotifier.PrepareClients.Abstract;
using Queris.ExceptionNotifier.PrepareClients.Enums;
using System;

namespace Queris.ExceptionNotifier.PrepareClients
{
    public class PrepareClientFactory : APrepareClientFactory
    {
        public override IPrepareClient Create(ClientType clientType)
        {
            switch (clientType)
            {
                case ClientType.SlackClient:
                    return new PrepareSlackClient();
                case ClientType.MailClient:
                    return new PrepareMailClient();
                case ClientType.FileClient:
                    return new PrepareFileClient();
                default:
                    throw new ArgumentException($"{clientType} cannot be created");
            }
        }
    }
}
