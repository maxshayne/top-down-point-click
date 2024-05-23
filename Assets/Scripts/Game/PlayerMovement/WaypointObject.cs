using System;
using EventBusSystem;
using UnityEngine;

namespace Game.PlayerMovement
{
    public class WaypointObject : MonoBehaviour
    {
        public void Configure(string colTag)
        {
            _colTag = colTag;
            var col = gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
        
        public void MoveToPoint(Vector3 position)
        {
            position.y = 0;
            transform.position = position;
            gameObject.SetActive(true);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_colTag)) return;
            gameObject.SetActive(false);
            EventBus.RaiseEvent<IWaypointReachHandler>(handler=>handler.WaypointReached());
        }
        
        private Action _callback;
        private string _colTag;
    }
}