using EventBusSystem;

namespace Game.PlayerInput
{
    public interface IWaypointReachHandler : IGlobalSubscriber
    {
        void WaypointReached();
    }
}