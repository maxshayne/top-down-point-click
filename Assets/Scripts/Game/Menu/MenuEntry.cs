using System;
using EventBusSystem;
using Game.Data;
using Infrastructure.DataStorage;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game.Menu
{
    [UsedImplicitly]
    public class MenuEntry : IStartable, IDataStorageEventHandler, IDisposable
    {
        public MenuEntry(IDataStorage<SaveData> dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        public void Start()
        {
            EventBus.Subscribe(this);
        }

        public void ClearStorage()
        {
            _dataStorage.Clear();
        }
        
        public void Dispose()
        {
            EventBus.Unsubscribe(this);
        }
        
        private readonly IDataStorage<SaveData> _dataStorage;
    }
}