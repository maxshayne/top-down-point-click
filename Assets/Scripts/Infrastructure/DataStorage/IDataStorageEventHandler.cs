using EventBusSystem;

namespace Infrastructure.DataStorage
{
    internal interface IDataStorageEventHandler : IGlobalSubscriber
    {
        void ClearStorage();
    }
}