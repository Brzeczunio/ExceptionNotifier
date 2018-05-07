namespace Queris.ExceptionNotifier.SlackNotificationClient.Entities
{
    public class SlackInitParams
    {
        public string Url { get; set; }
        public string Channel { get; set; }
        public string Username { get; set; }
        public bool ExtractTheHeader { get; set; }
        public string ConfigPath { get; set; }

        public override string ToString()
        {
            return $"{nameof(Url)}: {Url}, {nameof(Channel)}: {Channel}, {nameof(Username)}: {Username}," +
                   $" {nameof(ExtractTheHeader)}: {ExtractTheHeader}, {nameof(ConfigPath)}: {ConfigPath}";
        }
    }
}