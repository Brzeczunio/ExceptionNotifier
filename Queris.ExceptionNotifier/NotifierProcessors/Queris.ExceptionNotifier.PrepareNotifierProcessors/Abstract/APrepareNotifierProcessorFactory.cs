using Queris.ExceptionNotifier.PrepareNotifierProcessors.Enums;

namespace Queris.ExceptionNotifier.PrepareNotifierProcessors.Abstract
{
    public abstract class APrepareNotifierProcessorFactory
    {
        public abstract IPrepareNotifierProcessor Create(NotifierProcessorType notifierProcessorType);
    }
}
