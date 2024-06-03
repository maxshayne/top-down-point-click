using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Infrastructure.Serializers
{
    [UsedImplicitly]
    public class NewtonsoftJsonDataSerializer : IDataSerializer
    {
        public string Serialize<T>(T data)
        {
            try
            {
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }

        public T Deserialize<T>(string stringData)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(stringData);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }
    }
}