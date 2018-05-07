namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface IAggregatorsValidatorRepository
    {
        AAggregators GetAggregators(int readerId);
    }
}
