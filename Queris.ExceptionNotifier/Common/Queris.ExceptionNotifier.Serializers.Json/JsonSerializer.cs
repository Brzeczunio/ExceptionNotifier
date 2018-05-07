namespace Queris.ExceptionNotifier.Serializers.Json
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T o) => JsonHelper.Serialize(o);

        public T Deserialize<T>(string data) => JsonHelper.Deserialize<T>(data);
    }
}