using System.Collections.Generic;

namespace Queris.ExceptionNotifier.Common.ExtMethods
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// If key exist the value will be increment
        /// </summary>
        public static void Increment<T>(this Dictionary<T, int> dictionary, T key)
        {
            if (dictionary.ContainsKey(key))
            {
                int count;
                dictionary.TryGetValue(key, out count);
                dictionary[key] = ++count;
            }
            else
                throw new KeyNotFoundException($"The Key: {key} was not found.");
        }

        /// <summary>
        /// If no exist add new key to dictionary with value (default is "0") else if key exist
        /// the value will be increment
        /// </summary>
        public static void AddOrIncrement<T>(this Dictionary<T, int> dictionary, T key, int value = 0)
        {
            if (dictionary.ContainsKey(key))
                dictionary.Increment(key);
            else
                dictionary.Add(key, value);
        }
    }
}
