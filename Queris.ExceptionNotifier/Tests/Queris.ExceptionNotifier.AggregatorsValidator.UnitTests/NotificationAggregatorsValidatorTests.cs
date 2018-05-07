using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Entities;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.AggregatorsValidator.UnitTests
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.AggregatorsValidator.UnitTests")]
    public class NotificationAggregatorsValidatorTests
    {
        private Aggregators _aggregator;
        private FieldsContainer _message;

        [OneTimeSetUp]
        public void Init()
        {
            _aggregator = new Aggregators
            {
                AggregationParams = new[] {
                    new AggregationInfo
                    {
                        MessageAggregateColumnNames = new[] { "Message", "MessageType", "Exception" }
                    }
                }
            };

            _message = new FieldsContainer();
            _message.Add("Message", "Exception");
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.ToString.UnitTests")]
        public void ToString_WithoutAggregators_ReturnNONE()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();

            notificationAggregatorsValidator.ToString().Should().Match("NONE");
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.ToString.UnitTests")]
        public void ToString_WithAggregators_ReturnString()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();

            const string expected = "Id: 0, \r\nAggregators: 1: {\r\n	MessageAggregateColumnNames: [Message,MessageType,Exception]\r\n}\r\n";

            notificationAggregatorsValidator.SetAggregators(_aggregator);

            notificationAggregatorsValidator.ToString().Should().Match(expected);
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_AggregatorsIsNull_ReturnFalse()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();

            notificationAggregatorsValidator.AggregateIfPossible(null, null).Should().BeFalse();
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_MessageFieldsIsNull_ReturnFalse()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();
            var message = new FieldsContainer();

            notificationAggregatorsValidator.SetAggregators(_aggregator);

            notificationAggregatorsValidator.AggregateIfPossible(null, message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_ListMessagesCountIsZero_ReturnFalse()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();
            var listMessages = new List<NotificationMessage>();

            notificationAggregatorsValidator.SetAggregators(_aggregator);

            notificationAggregatorsValidator.AggregateIfPossible(listMessages, _message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_AggregationColumnNameListaggregationColumnNameListIsNull_ReturnFalse()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();
            var listMessages = new List<NotificationMessage> { new NotificationMessage() };
            var aggregator = new Aggregators
            {
                AggregationParams = new[] {
                    new AggregationInfo
                    {
                        MessageAggregateColumnNames = new[] { "ApplicationName", "MessageType", "Exception" }
                    }
                }
            };

            notificationAggregatorsValidator.SetAggregators(aggregator);

            notificationAggregatorsValidator.AggregateIfPossible(listMessages, _message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_NoMessagesToAgggregate_ReturnFalse()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();
            var listMessages = new List<NotificationMessage>
            {
                new NotificationMessage
                {
                    Fields = new List<FieldInfo>
                    {
                        new FieldInfo
                        {
                            Name = "Message",
                            Value = "Lorem ipsum"
                        }
                    }
                }
            };

            notificationAggregatorsValidator.SetAggregators(_aggregator);

            notificationAggregatorsValidator.AggregateIfPossible(listMessages, _message).Should().BeFalse();
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_MessagesToAgggregateAddNewCounter_ReturnTrue()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();
            var listMessages = new List<NotificationMessage>
            {
                new NotificationMessage
                {
                    Fields = new List<FieldInfo>
                    {
                        new FieldInfo
                        {
                            Name = "Message",
                            Value = "Exception"
                        }
                    }
                }
            };

            notificationAggregatorsValidator.SetAggregators(_aggregator);

            notificationAggregatorsValidator.AggregateIfPossible(listMessages, _message).Should().BeTrue();
        }

        [Test]
        [Category("NotificationAggregatorsValidatorTests.Aggregate.UnitTests")]
        public void Aggregate_MessagesToAgggregateIncrementCounter_ReturnTrueAndThreeAggregateMessages()
        {
            var notificationAggregatorsValidator = new NotificationAggregatorsValidator();
            var listMessages = new List<NotificationMessage>
            {
                new NotificationMessage
                {
                    Fields = new List<FieldInfo>
                    {
                        new FieldInfo
                        {
                            Name = "Message",
                            Value = "Exception"
                        }
                    }
                }
            };
            var expected = new List<NotificationMessage>
            {
                new NotificationMessage
                {
                    Fields = new List<FieldInfo>
                    {
                        new FieldInfo
                        {
                            Name = "Message",
                            Value = "Exception"
                        }
                    },
                    AggregatedMessagesCount = 2
                }
            };

            notificationAggregatorsValidator.SetAggregators(_aggregator);

            notificationAggregatorsValidator.AggregateIfPossible(listMessages, _message).Should().BeTrue();

            listMessages.Should().BeEquivalentTo(expected);
        }
    }
}
