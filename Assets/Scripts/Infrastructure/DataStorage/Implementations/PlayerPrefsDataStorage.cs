using Cysharp.Threading.Tasks;
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
        
        public UniTask Save(T data)
        {
            var serializedData = _serializer.Serialize(data);
            PlayerPrefs.SetString(SaveDataKey, serializedData);
            PlayerPrefs.Save();
            return new UniTask();
        }

        public UniTask<T> Load()
        {
            var stringData = PlayerPrefs.GetString(SaveDataKey, string.Empty);
            if (string.IsNullOrEmpty(stringData))
            {
                return default;
            }
            var saveData = _serializer.Deserialize<T>(stringData);
            return new UniTask<T>(saveData);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}