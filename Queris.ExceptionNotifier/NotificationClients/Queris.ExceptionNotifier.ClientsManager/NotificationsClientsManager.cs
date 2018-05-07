using System.Collections.Generic;
using System.Linq;
using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.ClientsManager
{
    public class NotificationsClientsManager : INotificationClientsManager
    {
        private readonly ClientsManagerParams _params;
        private readonly List<INotificationClient> _clients;

        public NotificationsClientsManager(ClientsManagerParams managerParams)
        {
            _params = managerParams;
            _clients = new List<INotificationClient>();
        }


        public bool Send(NotificationMessage message)
        {
            SetClient(message.ReaderId);
            if (!_clients.Any()) return false;
            foreach (var c in _clients) { c.Send(message); }
            return true;
        }

        private void SetClient(int readerId)
        {
            _clients.Clear();
            foreach (var c in _params.Clients)
            {
                var idClient = _params.Rules.Where(x => x.Value.Contains(readerId));

                foreach (var id in idClient)
                {
                    if (_params.Rules.Any(x => x.Value.Contains(readerId) && ((AClient)c).Id.Equals(id.Key)))
                        _clients.Add(c);
                }
            }
        }
    }
}