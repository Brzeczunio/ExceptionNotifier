namespace Queris.ExceptionNotifier.MailNotificationClient.Entities
{
    public class MailInitParams
    {
        public string ConfigPath { get; set; }

        public override string ToString()
        {
            return $"{nameof(ConfigPath)}: {ConfigPath}";
        }
    }
}
