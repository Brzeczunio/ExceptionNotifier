using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.PingReader.Helper
{
    internal static class PingReaderHelper
    {
        public static string Interval => "00:00:00";
        public static string StartTime => "07:00:00";
        public static PingDataReaderParams GetDefault => 
            new PingDataReaderParams { LogicalStorage = "UNDEFINED-PING", Occurs = 0, Interval = Interval, StartTime = StartTime};
    }
}