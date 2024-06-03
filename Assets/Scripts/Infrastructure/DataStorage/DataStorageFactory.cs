using System;
using Game.Root.Configuration;
using Infrastructure.Auth;
using Infrastructure.Serializers;
using JetBrains.Annotations;

namespace Infrastructure.DataStorage
{
    [UsedImplicitly]
    public class DataStorageFactory<T> where T : new()
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly IDataSerializer _dataSerializer;
        private readonly AuthService _authService;

        public DataStorageFactory(
            GameConfiguration gameConfiguration, 
            IDataSerializer dataSerializer,
            AuthService authService)
        {
            _gameConfiguration = gameConfiguration;
            _dataSerializer = dataSerializer;
            _authService = authService;
        }

        public IDataStorage<T> Create()
        {
            return _gameConfiguration.GetDataStorageType() switch
            {
                DataStorageType.Local => new PlayerPrefsDataStorage<T>(_dataSerializer),
                DataStorageType.Cloud => new CloudSaveDataStorage<T>(_authService, _dataSerializer),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}