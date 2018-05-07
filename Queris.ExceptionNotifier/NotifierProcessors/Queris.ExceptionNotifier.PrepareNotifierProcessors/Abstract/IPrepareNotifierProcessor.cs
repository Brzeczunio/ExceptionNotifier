using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.PrepareNotifierProcessors.Abstract
{
    public interface IPrepareNotifierProcessor
    {
        INotifierProcessor Prepare(params object[] paramList);
    }
}
