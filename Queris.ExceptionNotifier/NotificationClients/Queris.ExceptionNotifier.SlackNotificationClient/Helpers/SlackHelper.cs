using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.Common.Resources;
using Queris.ExceptionNotifier.SlackNotificationClient.Models;
using System.Collections.Generic;

namespace Queris.ExceptionNotifier.SlackNotificationClient.Helpers
{
    internal static class SlackHelper
    {
        internal static Payload CreatePayload(NotificationMessage message, bool extractTheHeader, string color, string channel = null, string username = null)
        {
            return new Payload
            {
                Channel = channel,
                Username = username,
                Attachments = new List<Attachments> { CreateAttachments(message, extractTheHeader, color) }
            };
        }

        private static Fields CreateFields(string title, string value, bool shortmessage = false)
        {
            return new Fields { Title = title, Value = value, Short = shortmessage };
        }

        private static Attachments CreateAttachments(NotificationMessage message, bool extractTheHeader, string color, string title = null, string value = null)
        {
            var attachments = new Attachments
            {
                Title = title,
                Text = value,
                Pretext = PrepareHeader(message, extractTheHeader),
                Mrkdwn_in = new [] { "pretext" },
                Color = color,
                Fields = new List<Fields>()
            };

            foreach (var field in message.Fields) { attachments.Fields.Add(CreateFields(field.Name, field.Value)); }

            return attachments;
        }

        private static string PrepareHeader(NotificationMessage message, bool extractTheHeader)
        {
            string text;

            if (extractTheHeader)
            {
                text = $">>>Notyfikacja od *{ message.LogicalStorage }*";
            }
            else
            {
                text = message.LogicalStorage;
            }

            if (message.AggregatedMessagesCount > 0)
            {
                if (extractTheHeader)
                {
                    var aggregatedCountFormatted = $"\n{ string.Format(Constant.AggregatedCountFormatString, message.AggregatedMessagesCount) }";

                    return string.Concat(text, aggregatedCountFormatted);
                }
                else
                {
                    message.Fields.Insert(0, new FieldInfo { Name = "AggregatedMessagesCount", Value = message.AggregatedMessagesCount.ToString() });
                }
            }

            return text;
        }
    }
}
