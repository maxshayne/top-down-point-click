using JetBrains.Annotations;

namespace Game
{
    [UsedImplicitly]
    public class DataBuilder<T> where T : new()
    {
        public T Build()
        {
            return _state;
        }

        public DataBuilder<T> CreateEmptyState()
        {
            _state = new T();
            return this;
        }

        public DataBuilder<T> UpdateState(IBuilderAgent<T> builderAgent)
        {
            _state = builderAgent.UpdateState(_state);
            return this;
        }

        private T _state;
    }

    public interface IBuilderAgent<T>
    {
        T UpdateState(T state);
    }
}