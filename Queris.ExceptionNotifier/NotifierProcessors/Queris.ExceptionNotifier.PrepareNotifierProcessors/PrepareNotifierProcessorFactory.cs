using Queris.ExceptionNotifier.PrepareNotifierProcessors.Abstract;
using Queris.ExceptionNotifier.PrepareNotifierProcessors.Enums;
using System;

namespace Queris.ExceptionNotifier.PrepareNotifierProcessors
{
    public class PrepareNotifierProcessorFactory : APrepareNotifierProcessorFactory
    {
        public override IPrepareNotifierProcessor Create(NotifierProcessorType notifierProcessorType)
        {
            switch (notifierProcessorType)
            {
                case NotifierProcessorType.HostFactoryNotifierProcessorType:
                    return new PrepareHostFactoryNotifierProcessor();
                case NotifierProcessorType.NotifierProcessorDecoratorType:
                    return new PrepareNotifierProcessorDecorator();
                default:
                    throw new ArgumentException($"{notifierProcessorType} cannot be created");
            }
        }
    }
}
