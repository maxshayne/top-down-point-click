using Cysharp.Threading.Tasks;

namespace Infrastructure.DataStorage
{
    public interface IDataStorage<T>
    {
        UniTask Save(T data);
        UniTask<T> Load();
        void Clear();
    }
}