using FluentAssertions;
using Moq;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;

namespace Queris.ExceptionNotifier.Common.UnitTests.Entities
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.Common.UnitTests")]
    public class NotificationDataReaderDecoratorTests
    {
        [Test]
        [Category("NotificationDataReaderDecoratorTests.GetId.UnitTests")]
        public void GetId_CreateObjectWithId_ReturnId()
        {
            var dataReaderMock = new Mock<INotificationDataReader>();

            var notificationDataReaderDecorator = new NotificationDataReaderDecorator(dataReaderMock.Object, 1);

            notificationDataReaderDecorator.GetId().Should().Be(1);
        }
    }
}
