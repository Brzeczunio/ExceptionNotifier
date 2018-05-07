namespace Queris.ExceptionNotifier.ConfigManager.Models
{
    public class ReaderAdditionalParam
    {
        public string FileName { get; set; }
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string MessageTypeColumnName { get; set; }
        public string OrderByColumnName { get; set; }
        public string StartTime { get; set; }
        public int Occurs { get; set; }
        public string Interval { get; set; }
    }
}
