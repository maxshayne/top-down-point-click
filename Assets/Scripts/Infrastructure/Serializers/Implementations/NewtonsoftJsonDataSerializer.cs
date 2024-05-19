using Infrastructure.DataStorage.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.Serializers.Implementations
{
    public class NewtonsoftJsonDataSerializer : IDataSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        public T Deserialize<T>(string stringData)
        {
            return JsonConvert.DeserializeObject<T>(stringData);
        }
    }
}