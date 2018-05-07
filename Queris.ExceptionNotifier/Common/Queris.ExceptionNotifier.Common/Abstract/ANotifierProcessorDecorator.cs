namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class ANotifierProcessorDecorator : INotifierProcessor
    {
        protected INotifierProcessor NotifierProcessor;

        protected ANotifierProcessorDecorator(INotifierProcessor notifierProcessor) => NotifierProcessor = notifierProcessor;

        public abstract void Execute();
    }
}
