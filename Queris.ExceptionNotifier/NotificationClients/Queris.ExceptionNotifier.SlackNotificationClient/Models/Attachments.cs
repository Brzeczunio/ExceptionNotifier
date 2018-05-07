using System.Collections.Generic;

namespace Queris.ExceptionNotifier.SlackNotificationClient.Models
{
    public class Attachments
    {
        public string Color { get; set; } = "#439FE0";

        public string Title { get; set; }

        public string Text { get; set; }

        public string Pretext { get; set; }

        public string[] Mrkdwn_in { get; set; }

        public List<Fields> Fields { get; set; }
    }
}
