using System.Collections.Generic;
using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationDataReader
    {
        List<NotificationMessage> GetData();
    }
}