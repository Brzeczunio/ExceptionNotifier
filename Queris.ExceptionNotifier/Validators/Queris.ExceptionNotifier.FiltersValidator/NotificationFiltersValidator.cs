using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.FiltersValidator
{
    public class NotificationFiltersValidator : INotificationFiltersValidator
    {
        private AFilters _filters;

        public bool Validate(FieldsContainer message)
        {
            if (_filters == null || !message.Fields.Any()) return false;
            foreach (var field in message.Fields)
            {
                var filtersParams = _filters.FilterParams.Where(x => x.ColumnName.Equals(field.Name)).ToList();
                if (IsFieldValid(filtersParams, field)) { return true; }
                
            }
            return false;
        }

        private bool IsFieldValid(List<FilterInfo> filtersParams, FieldInfo field)
        {
            foreach (var filterInfo in filtersParams)
            {
                bool isValid;
                switch (filterInfo.MatchPatternOperator)
                {
                    case "=":
                        isValid = filterInfo.Patterns.Any(x => field.Value.Equals(x));
                        break;
                    case ">":
                        isValid = filterInfo.Patterns.Any(x => int.Parse(field.Value) > int.Parse(x));
                        break;
                    case "<":
                        isValid = filterInfo.Patterns.Any(x => int.Parse(field.Value) < int.Parse(x));
                        break;
                    case ">=":
                        isValid = filterInfo.Patterns.Any(x => int.Parse(field.Value) >= int.Parse(x));
                        break;
                    case "<=":
                        isValid = filterInfo.Patterns.Any(x => int.Parse(field.Value) <= int.Parse(x));
                        break;
                    default:
                        isValid = filterInfo.Patterns.Any(x => field.Value.Contains(x));
                        break;
                }

                if (isValid) return true;
            }

            return false;
        }

        public void SetFilters(AFilters filters) => _filters = filters;

        public override string ToString() => _filters?.ToString() ?? "NONE";
    }
}
