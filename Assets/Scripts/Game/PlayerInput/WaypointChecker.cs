using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.PlayerInput
{
    [RequireComponent(typeof (MeshFilter))]
    [RequireComponent(typeof (MeshRenderer))]
    public class WaypointChecker : MonoBehaviour
    {
        private Action _callback;
        private string _colTag;

        private void Start()
        {
            Vector3[] vertices = {
                new Vector3 (0, 0, 0),
                new Vector3 (1, 0, 0),
                new Vector3 (1, 1, 0),
                new Vector3 (0, 1, 0),
                new Vector3 (0, 1, 1),
                new Vector3 (1, 1, 1),
                new Vector3 (1, 0, 1),
                new Vector3 (0, 0, 1),
            };

            int[] triangles = {
                0, 2, 1, //face front
                0, 3, 2,
                2, 3, 4, //face top
                2, 4, 5,
                1, 2, 5, //face right
                1, 5, 6,
                0, 7, 4, //face left
                0, 4, 3,
                5, 4, 7, //face back
                5, 7, 6,
                0, 6, 7, //face bottom
                0, 1, 6
            };
			
            Mesh mesh = GetComponent<MeshFilter> ().mesh;
            mesh.Clear ();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.Optimize ();
            mesh.RecalculateNormals ();
        }

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
            createPosition.y = 0;
            transform.position = createPosition;
            var col = gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }
}