namespace Queris.ExceptionNotifier.MailNotificationClient.Models
{
    public class Config
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string From { get; set; }

        public string[] To { get; set; }

        public string[] CC { get; set; }

        public string[] Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}