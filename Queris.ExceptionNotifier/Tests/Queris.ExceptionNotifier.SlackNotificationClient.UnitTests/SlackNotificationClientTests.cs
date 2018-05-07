using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.Serializers.Json;
using Queris.ExceptionNotifier.SlackNotificationClient.Entities;

namespace Queris.ExceptionNotifier.SlackNotificationClient.UnitTests
{
    [TestFixture]
    [Category("Queris.ExceptionNotifier.SlackNotificationClient.UnitTests")]
    public class SlackNotificationClientTests
    {
        private NotificationMessage _message;

        [OneTimeSetUp]
        public void Init()
        {
            _message = new NotificationMessage
            {
                ReaderId = 0,
                LogicalStorage = "TEST",
                MessageType = "ERROR",
                AggregatedMessagesCount = 1,
                Fields = new List<FieldInfo>
                {
                    new FieldInfo
                    {
                        Name = "Message",
                        Value = "Exception"
                    }
                }
            };
        }

        [Test]
        [Category("SlackNotificationClientTests.SlackNotificationClient.UnitTests")]
        public void SlackNotificationClient_EmptyUrl_ThrowArgumentNullException()
        {
            Action result = () => new SlackNotificationClient(new SlackInitParams(), 0, new JsonSerializer());
            result.Should().Throw<ArgumentNullException>().WithMessage("Wartość nie może być zerowa.\r\nNazwa parametru: Url is empty!");
        }

        [Test]
        [Category("SlackNotificationClientTests.SlackNotificationClient.UnitTests")]
        public void SlackNotificationClient_WithWrongUrl_ThrowWebException()
        {
            var slackInitParam = new SlackInitParams()
            {
                Channel = null,
                Username = null,
                Url = "https://hooks.slack.com/services/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"
            };

            var slackClient = new SlackNotificationClient(slackInitParam, 0, new JsonSerializer());
            Action result = () => slackClient.Send(_message);

            result.Should().Throw<WebException>().WithMessage("Serwer zdalny zwrócił błąd: (404) Nie znaleziono.");
        }

        [Test]
        [Category("SlackNotificationClientTests.SlackNotificationClient.UnitTests")]
        public void SlackNotificationClient_ExtractAggregateCount_ExtractCount()
        {
            var slackInitParam = new SlackInitParams()
            {
                Channel = null,
                Username = null,
                ExtractTheHeader = true,
                Url = "https://hooks.slack.com/services/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"
            };

            var slackClient = new SlackNotificationClient(slackInitParam, 0, new JsonSerializer());
            Action result = () => slackClient.Send(_message);

            result.Should().Throw<WebException>().WithMessage("Serwer zdalny zwrócił błąd: (404) Nie znaleziono.");
        }

        [Test]
        [Category("SlackNotificationClientTests.SlackNotificationClient.UnitTests")]
        public void SlackNotificationClient_CheckSlackConfig_ChangeColor()
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SlackConfig.json";

            var slackInitParam = new SlackInitParams()
            {
                Channel = null,
                Username = null,
                ExtractTheHeader = true,
                Url = "https://hooks.slack.com/services/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",
                ConfigPath = path
            };

            var slackClient = new SlackNotificationClient(slackInitParam, 0, new JsonSerializer());
            Action result = () => slackClient.Send(_message);

            result.Should().Throw<WebException>().WithMessage("Serwer zdalny zwrócił błąd: (404) Nie znaleziono.");
        }

        [Test]
        [Category("SlackNotificationClientTests.SlackNotificationClient.UnitTests")]
        public void SlackNotificationClient_CheckDefaultColorInConfig_DefaultColor()
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\DefaultColor.json";

            var slackInitParam = new SlackInitParams()
            {
                Channel = null,
                Username = null,
                ExtractTheHeader = true,
                Url = "https://hooks.slack.com/services/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",
                ConfigPath = path
            };

            var slackClient = new SlackNotificationClient(slackInitParam, 0, new JsonSerializer());
            Action result = () => slackClient.Send(_message);

            result.Should().Throw<WebException>().WithMessage("Serwer zdalny zwrócił błąd: (404) Nie znaleziono.");
        }
    }
}
