using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using System;
using System.Collections.Generic;
using Queris.ExceptionNotifier.PingReader.Helper;

namespace Queris.ExceptionNotifier.PingReader
{
    public class PingLivingReader : INotificationDataReader
    {
        private readonly PingDataReaderParams _params;
        private readonly CounterReader _reader;
        private readonly TimeSpan _startTime;
        private bool _done;
        private int _occurs;
        private readonly TimeSpan _interval;

        public PingLivingReader(ADataReaderParams readerParams, ACounterReader reader)
        {
            _params = readerParams != null ? readerParams as PingDataReaderParams : PingReaderHelper.GetDefault;
            _reader = reader as CounterReader;
            _startTime = TimeSpan.Parse(_params?.StartTime ?? PingReaderHelper.StartTime);
            _interval = TimeSpan.Parse(_params?.Interval ?? PingReaderHelper.Interval);
        }
        public List<NotificationMessage> GetData()
        {
            var ret = new List<NotificationMessage>();
            if (TimeSpan.Compare(_startTime, DateTime.Now.TimeOfDay).Equals(1)) { if (_done) Initialize(!_done, 0); return ret; }
            if (_done) return ret;
            var tmp = _interval.TotalMilliseconds;
            if (_startTime.Add(TimeSpan.FromMilliseconds(tmp * _occurs)) > DateTime.Now.TimeOfDay) return ret;

            var livingField = new FieldInfo{ Name = "Living Message", Value = "I'm living !" };
            var message = new List<FieldInfo>{ livingField };
            ret.Add(new NotificationMessage{ Fields = message, ReaderId = _reader.GetId, LogicalStorage = _params.LogicalStorage });
            _occurs++;
            if (!_occurs.Equals(_params.Occurs)) return ret;
            Initialize(true, 0);
            return ret;
        }

        private void Initialize(bool isDone, int val) { _done = isDone; _occurs = val; }
    }
}
