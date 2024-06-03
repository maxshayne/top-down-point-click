using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Auth;
using Infrastructure.Serializers;
using Unity.Services.CloudSave;

namespace Infrastructure.DataStorage
{
    public class CloudSaveDataStorage<T> : IDataStorage<T>
    {
        private const string SaveDataKey = "saveData";
        
        private readonly AuthService _authService;
        private readonly IDataSerializer _dataSerializer;
        
        public CloudSaveDataStorage(AuthService authService, IDataSerializer dataSerializer)
        {
            _authService = authService;
            _dataSerializer = dataSerializer;
            _authService.Initialize();
        }

        public async UniTask Save(T data)
        {
            var stringData = _dataSerializer.Serialize(data);
            var dict = new Dictionary<string, object> { { SaveDataKey, stringData } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(dict);
        }

        public async UniTask<T> Load()
        {
            await UniTask.WaitUntil(() => _authService.IsInitialized);
            var playerData =
                await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>() { SaveDataKey });
            if (playerData.TryGetValue(SaveDataKey, out var data))
            {
                var stringData =  data.Value.GetAs<string>();
                return _dataSerializer.Deserialize<T>(stringData);
            }

            return default;
        }

        public async void Clear()
        {
            await CloudSaveService.Instance.Data.Player.DeleteAllAsync();
        }
    }
}