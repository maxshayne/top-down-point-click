using System;
using UnityEngine;

namespace Game.PlayerInput
{
    public class WaypointChecker : MonoBehaviour
    {
        private Action _callback;
        private string _colTag;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_colTag)) return;
            _callback?.Invoke();
            Debug.Log(other);
            Destroy(gameObject); //todo: pooling
        }
        
        public void Configure(Vector3 createPosition, Action callback, string colTag)
        {
            _colTag = colTag;
            _callback = callback;
            transform.position = createPosition;
            var col = gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }
}