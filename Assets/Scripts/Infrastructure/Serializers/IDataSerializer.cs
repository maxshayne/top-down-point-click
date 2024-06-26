namespace Infrastructure.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string stringData);
    }
}