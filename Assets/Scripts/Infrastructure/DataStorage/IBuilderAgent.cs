namespace Infrastructure.DataStorage
{
    public interface IBuilderAgent<T>
    {
        T UpdateState(T state);
    }
}