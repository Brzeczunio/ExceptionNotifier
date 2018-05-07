using System;
using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class PingDataReaderParams : ADataReaderParams
    {
        public string StartTime { get; set; }
        public int Occurs { get; set; }
        public string Interval { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(StartTime)}: {StartTime}, {nameof(Occurs)}: {Occurs}, {nameof(Interval)}: {Interval}";
        }
    }
}