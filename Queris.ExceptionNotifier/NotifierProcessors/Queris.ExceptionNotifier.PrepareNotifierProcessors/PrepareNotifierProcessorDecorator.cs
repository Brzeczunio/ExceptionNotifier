using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Decorators;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities;
using Queris.ExceptionNotifier.PrepareNotifierProcessors.Abstract;

namespace Queris.ExceptionNotifier.PrepareNotifierProcessors
{
    public class PrepareNotifierProcessorDecorator : IPrepareNotifierProcessor
    {
        public INotifierProcessor Prepare(params object[] paramList)
        {
            ANotifierProcessorParams notifierProcessorDecoratorParams = new NotifierProcessorDecoratorParams(paramList);

            return new NotifierProcessorDecorator(notifierProcessorDecoratorParams);
        }
    }
}
