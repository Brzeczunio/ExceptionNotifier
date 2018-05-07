namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface IFiltersValidatorRepository
    {
        AFilters GetFilters(int readerId);
    }
}
