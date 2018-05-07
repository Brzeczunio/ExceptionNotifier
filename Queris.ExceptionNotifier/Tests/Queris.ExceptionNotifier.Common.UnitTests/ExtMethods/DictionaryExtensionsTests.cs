using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.ExtMethods;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.Common.UnitTests.ExtMethods
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class DictionaryExtensionsTests
    {
        [Test]
        [Category("DictionaryExtensionsTests.AddOrIncrement.UnitTests")]
        public void AddOrIncrement_AddingNewKey_ReturnNewlyAddedElement()
        {
            const string key = "New";
            var dictionary = new Dictionary<string, int>();

            dictionary.AddOrIncrement(key);

            dictionary.Should().Contain(key, 0);
        }

        [Test]
        [Category("DictionaryExtensionsTests.AddOrIncrement.UnitTests")]
        public void AddOrIncrement_AddingNewKeyWithValue_ReturnNewlyAddedElementWithValue()
        {
            const string key = "New";
            var dictionary = new Dictionary<string, int>();

            dictionary.AddOrIncrement(key, 1);

            dictionary.Should().Contain(key, 1);
        }

        [Test]
        [Category("DictionaryExtensionsTests.AddOrIncrement.UnitTests")]
        public void AddOrIncrement_IncrementingValueForExistKey_ReturnIncrementedValueForKey()
        {
            const string key = "New";
            var dictionary = new Dictionary<string, int>() { { key, 2 } };

            dictionary.AddOrIncrement(key);

            dictionary.Should().Contain(key, 3);
        }

        [Test]
        [Category("DictionaryExtensionsTests.AddOrIncrement.UnitTests")]
        public void AddOrIncrement_IncrementingValueForExistKeyWithGivenValue_ReturnIncrementedValueForKeyWithoutGivenValue()
        {
            const string key = "New";
            var dictionary = new Dictionary<string, int>() { { key, 2 } };

            dictionary.AddOrIncrement(key, 50);

            dictionary.Should().Contain(key, 3);
        }

        [Test]
        [Category("DictionaryExtensionsTests.Increment.UnitTests")]
        public void Increment_IncrementingValueForEmptyDictionary_ReturnException()
        {
            const string key = "New";
            var dictionary = new Dictionary<string, int>();

            dictionary.Invoking(x => x.Increment(key)).Should().Throw<KeyNotFoundException>().WithMessage($"The Key: {key} was not found.");
        }
    }
}
