using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Helpers;

namespace Queris.ExceptionNotifier.Common.UnitTests.Helpers
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class RowsHelperTests
    {
        [Test]
        [Category("RowsHelperTests.MaxRowsToGet.UnitTests")]
        public void MaxRowsToGet_InitialCounterGreaterThanReadedRowsWhereReadedIsZero_ReturnZero()
        {
            var initialCounter = 250;

            var result = RowsHelper.MaxRowsToGet(0, ref initialCounter);

            result.Should().Be(0);
            initialCounter.Should().Be(0);
        }

        [Test]
        [Category("RowsHelperTests.MaxRowsToGet.UnitTests")]
        public void MaxRowsToGet_InitialCounterGreaterThanReadedRows_ReturnZero()
        {
            var initialCounter = 250;

            var result = RowsHelper.MaxRowsToGet(100, ref initialCounter);

            result.Should().Be(0);
            initialCounter.Should().Be(100);
        }

        [Test]
        [Category("RowsHelperTests.MaxRowsToGet.UnitTests")]
        public void MaxRowsToGet_InitialCounterEqualsReadedRows_ReturnZero()
        {
            var initialCounter = 250;

            var result = RowsHelper.MaxRowsToGet(250, ref initialCounter);

            result.Should().Be(0);
            initialCounter.Should().Be(250);
        }

        [Test]
        [Category("RowsHelperTests.MaxRowsToGet.UnitTests")]
        public void MaxRowsToGet_ReadedRowsGreaterThanInitialCounter_ReturnRecsToTake()
        {
            var initialCounter = 250;

            var result = RowsHelper.MaxRowsToGet(300, ref initialCounter);

            result.Should().Be(50);
            initialCounter.Should().Be(300);
        }
    }
}
