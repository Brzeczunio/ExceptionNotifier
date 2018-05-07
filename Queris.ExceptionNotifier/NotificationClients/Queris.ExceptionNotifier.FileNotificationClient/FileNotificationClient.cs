using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.Serializers;

namespace Queris.ExceptionNotifier.FileNotificationClient
{
    public class FileNotificationClient : AClient, INotificationClient
    {
        public FileNotificationClient(int id, ISerializer serializer) : base(id) { }
        public bool Send(NotificationMessage message)
        {
            //tutaj zapis danych do pliku

            return true;
        }
    }
}
