namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class ADataReaderParams
    {
        public string LogicalStorage { get; set; }
        public string[] ColumnsNames { get; set; }
        public int InitialCounter { get; set; }
        
        public override string ToString()
        {
            return $"{nameof(LogicalStorage)}: {LogicalStorage}, {nameof(InitialCounter)}: {InitialCounter}, " +
                   $"{nameof(ColumnsNames)}: [{string.Join(",", ColumnsNames)}]";
        }
    }
}