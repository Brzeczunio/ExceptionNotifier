using System.Collections.Generic;
using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class ClientsManagerParams
    {
        public List<INotificationClient> Clients { get; set; }
        /// <summary>
        /// Rules:
        /// key - clientId
        /// values - collection of readersIds
        /// </summary>
        public Dictionary<int, int[]> Rules { get; set; }
    }
}