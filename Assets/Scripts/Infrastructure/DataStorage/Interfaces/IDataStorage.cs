namespace Infrastructure.DataStorage.Interfaces
{
    public interface IDataStorage<T>
    {
        void Save(T data);
        T Load();
        void Clear();
    }
}