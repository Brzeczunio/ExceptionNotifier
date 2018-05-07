using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class NotificationMessage
    {
        public int ReaderId { get; set; }
        /// <summary>
        /// Typ wiadomości
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// Nazwa magazynu, z którego pochodzą dane
        /// </summary>
        public string LogicalStorage { get; set; }
        /// <summary>
        /// Licznik agregacji wiadomości
        /// </summary>
        public int AggregatedMessagesCount { get; set; }
        /// <summary>
        /// Zestaw danych do wysłania [key-value] (nazwaPola-wartoscPola)
        /// </summary>
        public List<FieldInfo> Fields { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(ReaderId)}: {ReaderId}, ");
            sb.AppendLine($"{nameof(MessageType)}: {MessageType}, ");
            sb.AppendLine($"{nameof(LogicalStorage)}: {LogicalStorage}, ");
            sb.AppendLine($"{nameof(AggregatedMessagesCount)}: {AggregatedMessagesCount}, ");
            sb.AppendLine($"{nameof(Fields)}: {Fields.Count}: {{");
            if (!Fields.Any()) return sb.ToString();
            foreach (var field in Fields) { sb.AppendLine($"\t{field}"); }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}