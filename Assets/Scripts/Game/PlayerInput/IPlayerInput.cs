namespace Game.PlayerInput
{
    public interface IPlayerInput
    {
        void Subscribe(IInputListener listener);
        void Unsubscribe(IInputListener listener);
    }
}