namespace Queris.ExceptionNotifier.SlackNotificationClient.Models
{
    public class Fields
    {
        public string Title { get; set; }

        public string Value { get; set; }

        public bool Short { get; set; } = false;
    }
}
