namespace Queris.ExceptionNotifier.Serializers
{
    public interface ISerializer
    {
        string Serialize<T>(T o);
        T Deserialize<T>(string data);
    }
}