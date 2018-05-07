namespace Queris.ExceptionNotifier.ConfigManager.Models
{
    public class ClientAdditionalParam
    {
        public string ConfigPath { get; set; }
        public string Url { get; set; }
        public string Channel { get; set; }
        public string UserName { get; set; }
        public bool ExtractTheHeader { get; set; }
    }
}
