using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace Queris.ExceptionNotifier.Serializers.Json
{
    public static class JsonHelper
    {
        public static string Serialize<T>(T o) => JsonConvert.SerializeObject(o,
            new JsonSerializerSettings
            {
                Culture = new CultureInfo("pl-PL"),
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

        public static T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json,
            new JsonSerializerSettings
            {
                Culture = new CultureInfo("pl-PL"),
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
    }
}