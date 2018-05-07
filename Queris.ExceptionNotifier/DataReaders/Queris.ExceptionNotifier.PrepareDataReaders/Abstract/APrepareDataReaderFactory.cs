using Queris.ExceptionNotifier.PrepareDataReaders.Enums;

namespace Queris.ExceptionNotifier.PrepareDataReaders.Abstract
{
    public abstract class APrepareDataReaderFactory
    {
        public abstract IPrepareDataReader Create(DataReaderType dataReaderType);
    }
}
