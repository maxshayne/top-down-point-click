using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;

namespace Infrastructure.DataStorage.Implementations
{
    public class CloudSaveDataStorage<T> : IDataStorage<T>
    {
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

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        private const string SaveDataKey = "saveData";
    }
}