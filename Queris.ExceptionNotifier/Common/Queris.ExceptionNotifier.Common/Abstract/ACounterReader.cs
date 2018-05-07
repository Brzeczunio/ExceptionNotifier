namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class ACounterReader
    {
        protected ISetCounterConfig Config;
        protected int Id;

        protected ACounterReader(ISetCounterConfig config) { Config = config; }
    }
}
