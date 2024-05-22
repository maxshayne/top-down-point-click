using System;
using EventBusSystem;
using UnityEngine;

namespace Game.PlayerMovement
{
    public class WaypointChecker : MonoBehaviour
    {
        public void Configure(Vector3 createPosition, string colTag)
        {
            _colTag = colTag;
            createPosition.y = 0;
            transform.position = createPosition;
            var col = gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_colTag)) return;
            EventBus.RaiseEvent<IWaypointReachHandler>(h=>h.WaypointReached());
            Debug.Log(other);
            Destroy(gameObject); //todo: pooling
        }
        
        private Action _callback;
        private string _colTag;
    }
}