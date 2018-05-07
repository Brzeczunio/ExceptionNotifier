using Queris.ExceptionNotifier.PrepareDataReaders.Abstract;
using Queris.ExceptionNotifier.PrepareDataReaders.Enums;
using System;

namespace Queris.ExceptionNotifier.PrepareDataReaders
{
    public class PrepareDataReaderFactory : APrepareDataReaderFactory
    {
        public override IPrepareDataReader Create(DataReaderType dataReaderType)
        {
            switch (dataReaderType)
            {
                case DataReaderType.DbReader:
                    return new PrepareDatabaseDataReader();
                case DataReaderType.PingReader:
                    return new PreparePingLivingReader();
                case DataReaderType.ExitReader:
                    return new PrepareExitReader();
                default:
                    throw new ArgumentException($"{dataReaderType} cannot be created");
            }
        }
    }
}
