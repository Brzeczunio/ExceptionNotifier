using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Abstract;
using Queris.ExceptionNotifier.HostFactoryNotifierProcessor.Entities;
using Queris.ExceptionNotifier.PrepareNotifierProcessors.Abstract;

namespace Queris.ExceptionNotifier.PrepareNotifierProcessors
{
    public class PrepareHostFactoryNotifierProcessor : IPrepareNotifierProcessor
    {
        public INotifierProcessor Prepare(params object[] paramList)
        {
            ANotifierProcessorParams hostFactoryNotifierProcessorParams = new HostFactoryNotifierProcessorParams(paramList);

            return new HostFactoryNotifierProcessor.HostFactoryNotifierProcessor(hostFactoryNotifierProcessorParams);
        }
    }
}
