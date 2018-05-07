using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationClient
    {
        bool Send(NotificationMessage message);
    }
}