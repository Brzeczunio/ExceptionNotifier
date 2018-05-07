using System.Collections.Generic;

namespace Queris.ExceptionNotifier.SlackNotificationClient.Models
{
    public class Payload
    {
        public string Channel { get; set; }

        public string Username { get; set; }

        public string Text { get; set; }

        public List<Attachments> Attachments { get; set; }
    }
}
