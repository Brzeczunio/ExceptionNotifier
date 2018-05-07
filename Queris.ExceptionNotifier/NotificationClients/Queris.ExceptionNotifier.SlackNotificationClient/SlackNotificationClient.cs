using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.Common.Entities;
using Queris.ExceptionNotifier.SlackNotificationClient.Helpers;
using Queris.ExceptionNotifier.SlackNotificationClient.Models;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Queris.ExceptionNotifier.Serializers;
using Queris.ExceptionNotifier.SlackNotificationClient.Entities;
using Queris.ExceptionNotifier.SlackNotificationClient.Enums;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Queris.ExceptionNotifier.SlackNotificationClient
{
    public class SlackNotificationClient : AClient, INotificationClient
    {
        private readonly SlackInitParams _params;
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();
        private readonly ISerializer _serializer;
        private readonly Dictionary<string, string> _color;

        public SlackNotificationClient(SlackInitParams initParams, int id, ISerializer serializer) : base(id)
        {
            if(string.IsNullOrEmpty(initParams.Url)) throw new ArgumentNullException($"{nameof(initParams.Url)} is empty!");
            _params = initParams;
            _serializer = serializer;
            _uri = new Uri(initParams.Url);

            if (!string.IsNullOrEmpty(initParams.ConfigPath))
            {
                var json = File.ReadAllText(initParams.ConfigPath, Encoding.Default);
                _color = _serializer.Deserialize<Dictionary<string, string>>(json);
            }
        }

        public bool Send(NotificationMessage message)
        {
            var payload = SlackHelper.CreatePayload(message, _params.ExtractTheHeader, GetColor(message.MessageType), _params.Channel, _params.Username);

            return SendPostMessage(payload);
        }

        private bool SendPostMessage(Payload payload)
        {
            var payloadJson = _serializer.Serialize(payload);

            using (var client = new WebClient())
            {
                var data = new NameValueCollection {["payload"] = payloadJson};
                client.Proxy = WebRequest.DefaultWebProxy;
                client.Credentials = CredentialCache.DefaultCredentials;
                client.Proxy.Credentials = CredentialCache.DefaultCredentials;
                var response = client.UploadValues(_uri, RequestType.Post.ToString().ToUpper(), data);
                if (_encoding.GetString(response).Equals(ResponseStatus.Ok.ToString(), StringComparison.OrdinalIgnoreCase)) return true;

                throw new WebException($"Error: problem with send notification to slack: {_encoding.GetString(response)}");
            }
        }

        private string GetColor(string messageType)
        {
            if (_color is null || _color.Count == 0 || messageType is null) return "#439FE0";

            return _color.Keys.Contains(messageType) ? 
                _color.FirstOrDefault(x => x.Key.Equals(messageType)).Value : _color.FirstOrDefault(x => x.Key.Equals("DEFAULT")).Value;
        }
    }
}