namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationValidator
    {
        void SetValidator(INotificationFiltersValidator validator);
    }
}
