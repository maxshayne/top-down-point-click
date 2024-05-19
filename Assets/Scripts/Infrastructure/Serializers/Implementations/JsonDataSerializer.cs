using System;
using Infrastructure.DataStorage.Interfaces;
using UnityEngine;

namespace Infrastructure.Serializers.Implementations
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonUtility.ToJson(data);
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