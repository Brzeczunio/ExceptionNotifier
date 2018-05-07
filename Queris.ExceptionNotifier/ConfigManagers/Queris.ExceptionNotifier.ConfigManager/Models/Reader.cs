namespace Queris.ExceptionNotifier.ConfigManager.Models
{
    public class Reader
    {
        public string ReaderType { get; set; }
        public int ReaderId { get; set; }
        public string ReaderName { get; set; }
        public string LogicalName { get; set; }
        public string[] ColumnNames { get; set; }
        public int InitialCounter { get; set; }
        public string ConnectionString { get; set; }
        public ReaderAdditionalParam ReaderAdditionalParams { get; set; }
    }
}
