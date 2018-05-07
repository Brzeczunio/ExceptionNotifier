using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface INotificationFiltersValidator
    {
        bool Validate(FieldsContainer message);
        void SetFilters(AFilters filters);
    }
}
