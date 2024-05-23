using EventBusSystem;

namespace Game.PlayerMovement
{
    public interface IWaypointReachHandler : IGlobalSubscriber
    {
        void WaypointReached();
    }
}