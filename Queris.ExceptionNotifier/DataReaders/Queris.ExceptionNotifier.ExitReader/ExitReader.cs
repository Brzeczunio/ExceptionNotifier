using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using System;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.ExitReader
{
    public class ExitReader : INotificationDataReader
    {
        private readonly ExitDataReaderParams _params;
        private readonly CounterReader _reader;

        public ExitReader(ADataReaderParams readerParams, ACounterReader reader)
        {
            _params = readerParams?.LogicalStorage != null ? 
                readerParams as ExitDataReaderParams : new ExitDataReaderParams { LogicalStorage = "UNDEFINED-EXIT-READER" };
            _reader = reader as CounterReader;
        }

        public List<NotificationMessage> GetData()
        {
            var ret = new List<NotificationMessage>();

            var exitField = new FieldInfo { Name = "Exit Message", Value = "I'm dying !" };
            var message = new List<FieldInfo> { exitField };
            ret.Add(new NotificationMessage { Fields = message, ReaderId = _reader.GetId, LogicalStorage = _params.LogicalStorage, MessageType = "ERROR" });

            return ret;
        }
    }
}
