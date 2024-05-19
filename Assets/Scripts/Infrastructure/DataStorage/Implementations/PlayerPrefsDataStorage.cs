using Infrastructure.DataStorage.Interfaces;
using UnityEngine;

namespace Infrastructure.DataStorage.Implementations
{
    public class PlayerPrefsDataStorage<T> : IDataStorage<T>
    {
        private readonly IDataSerializer _serializer;
        private const string SaveDataKey = "savedata";

        public PlayerPrefsDataStorage(IDataSerializer serializer)
        {
            _serializer = serializer;
        }
        
        public void Save(T data)
        {
            var serializedData = _serializer.Serialize(data);
            PlayerPrefs.SetString(SaveDataKey, serializedData);
            PlayerPrefs.Save();
        }

        public T Load()
        {
            var stringData = PlayerPrefs.GetString(SaveDataKey, string.Empty);
            if (string.IsNullOrEmpty(stringData))
            {
                return default;
            }
            var saveData = _serializer.Deserialize<T>(stringData);
            return saveData;
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}