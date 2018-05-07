using System.Collections.Generic;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationReader
    {
        List<string> Read();
    }
}