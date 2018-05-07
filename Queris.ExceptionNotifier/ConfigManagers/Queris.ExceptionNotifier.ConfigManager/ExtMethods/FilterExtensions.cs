using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.ConfigManager.Models;
using System;
using System.Linq;

namespace Queris.ExceptionNotifier.ConfigManager.ExtMethods
{
    public static class FilterExtensions
    {
        public static FilterInfo[] CopyToFilterInfo(this Filter[] utilArray)
        {
            /*if (utilArray == null || utilArray.Length == 0) return Array.Empty<FilterInfo>();
            var outArray = new FilterInfo[utilArray.Length];
            for (var i = 0; i < utilArray.Length; i++)
            {
                outArray[i] = new FilterInfo
                    {
                        ColumnName = utilArray[i].ColumnName,
                        ColumnFilters = utilArray[i].ColumnFilters
                    };
            }
            return outArray;*/

            return utilArray.Any()
                ? utilArray.Select(x => new FilterInfo {ColumnName = x.ColumnName, ColumnFilters = x.ColumnFilters}).ToArray()
                : Array.Empty<FilterInfo>();
        }
    }
}
