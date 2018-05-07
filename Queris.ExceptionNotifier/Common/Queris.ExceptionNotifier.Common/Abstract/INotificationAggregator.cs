namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationAggregator
    {
        void SetAggregator(INotificationAggregatorsValidator aggregator);
    }
}
