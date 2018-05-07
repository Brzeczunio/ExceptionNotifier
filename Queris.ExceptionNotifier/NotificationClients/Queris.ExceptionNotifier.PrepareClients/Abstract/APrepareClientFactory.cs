using Queris.ExceptionNotifier.PrepareClients.Enums;

namespace Queris.ExceptionNotifier.PrepareClients.Abstract
{
    public abstract class APrepareClientFactory
    {
        public abstract IPrepareClient Create(ClientType clientType);
    }
}
