using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class FileDataReaderParams : ADataReaderParams
    {
        public string FileName { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(FileName)}: {FileName}";
        }
    }
}