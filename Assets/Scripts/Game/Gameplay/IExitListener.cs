using EventBusSystem;

namespace Game.Gameplay
{
    public interface IExitListener : IGlobalSubscriber
    {
        void CallExit();
    }
}