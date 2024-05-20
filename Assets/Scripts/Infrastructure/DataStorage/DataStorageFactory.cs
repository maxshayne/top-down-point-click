using System;
using Game;
using Game.Root.Configuration;
using Infrastructure.DataStorage.Implementations;
using JetBrains.Annotations;

namespace Infrastructure.DataStorage
{
    [UsedImplicitly]
    public class DataStorageFactory
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

        public IDataStorage<SaveData> Create()
        {
            return _gameConfiguration.GetSavingSystem() switch
            {
                DataStorageType.Local => new PlayerPrefsDataStorage<SaveData>(_dataSerializer),
                DataStorageType.Cloud => new CloudSaveDataStorage<SaveData>(_authService, _dataSerializer),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}