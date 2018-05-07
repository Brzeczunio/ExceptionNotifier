using System;

namespace Queris.ExceptionNotifier.Common.ExtMethods
{
    public static class StringExtensions
    {
        public static TEnum ToEnum<TEnum>(this string _this) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), _this, true);
        }
    }
}