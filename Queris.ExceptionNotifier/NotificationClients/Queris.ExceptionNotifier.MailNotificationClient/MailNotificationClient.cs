using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.MailNotificationClient.Entities;
using Queris.ExceptionNotifier.MailNotificationClient.Models;
using Queris.ExceptionNotifier.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Queris.ExceptionNotifier.MailNotificationClient
{
    public class MailNotificationClient : AClient, INotificationClient
    {
        private readonly Config _config;
        private readonly ICryptoDecoder _cryptoDecoder;

        public MailNotificationClient(MailInitParams initParams, int id, ISerializer serializer, ICryptoDecoder cryptoDecoder) : base(id)
        {
            if (string.IsNullOrEmpty(initParams.ConfigPath)) throw new ArgumentNullException($"{nameof(initParams.ConfigPath)} is empty!");

            var json = File.ReadAllText(initParams.ConfigPath, Encoding.Default);
            _config = serializer.Deserialize<Config>(json);
            _cryptoDecoder = cryptoDecoder;
        }

        public bool Send(NotificationMessage message)
        {
            if (message.Fields == null || message.Fields.Count == 0) return false;

            var client = new SmtpClient(_config.Host, _config.Port)
            {
                Credentials = new NetworkCredential(_config.UserName, _cryptoDecoder.Decode(_config.Password)),
                EnableSsl = _config.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            client.Send(PrepareMessage(message.Fields));

            return true;
        }

        private MailMessage PrepareMessage(List<FieldInfo> fields)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.From),
                IsBodyHtml = _config.IsBodyHtml,
                Subject = _config.Subject,
                Body = PrepareBody(fields)
            };

            _config.To.ToList().ForEach(x => { if (x != "") mailMessage.To.Add(new MailAddress(x)); });
            _config.CC.ToList().ForEach(x => { if (x != "") mailMessage.CC.Add(new MailAddress(x)); });
            _config.Bcc.ToList().ForEach(x => { if (x != "") mailMessage.Bcc.Add(new MailAddress(x)); });

            return mailMessage;
        }

        private string PrepareBody(List<FieldInfo> fields)
        {
            var reg = @"[$](\w+\s?\w+)\.(\w+)[$]";
            var splitResult = Regex.Split(_config.Body, reg);

            var name = "";
            var columnsValue = new List<string>();

            foreach (var r in splitResult)
            {
                switch (r)
                {
                    case "Name":
                        columnsValue.Add(fields.FirstOrDefault(x => x.Name.Equals(name))?.Name);
                        break;
                    case "Value":
                        columnsValue.Add(fields.FirstOrDefault(x => x.Name.Equals(name))?.Value);
                        break;
                    default:
                        name = r;
                        break;
                }
            }

            reg = @"[$](\w+\s?\w+)\.(\w+)[$]";
            var matchesResult = Regex.Matches(_config.Body, reg);

            var i = 0;
            var body = _config.Body;

            columnsValue.ForEach(x => { body = body.Replace(matchesResult[i].Value, x); i++; });

            return body;
        }
    }
}
