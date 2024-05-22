using System;
using Infrastructure.DataStorage;
using UnityEngine;

namespace Infrastructure.Serializers.Implementations
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize<T>(T data)
        {
            try
            {
                return JsonUtility.ToJson(data);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }

        public T Deserialize<T>(string data)
        {
            try
            {
                return JsonUtility.FromJson<T>(data);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }
    }
}