using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerInput
{
    public class ScreenClicker : MonoBehaviour
    {
        [SerializeField] private GameObject m_Player;
        [SerializeField] private float m_Speed = 5f;
        
        private Controls _controls;

        private Queue<Vector3> _pointsQueue = new();
        private Vector3? _nextTarget;

        private void Start()
        {
            _controls = new Controls();
            _controls.Main.Move.performed += MoveOnperformed;
            _controls.Enable();
        }
        
        private void MoveOnperformed(InputAction.CallbackContext obj)
        {
            Debug.Log($"cilck");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var newPos = hit.point;
                //var go = Instantiate(m_Player, newPos, Quaternion.identity);
                _pointsQueue.Enqueue(newPos);
            }

            if (_nextTarget.HasValue) return;
            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            while (_pointsQueue.Count > 0 || _nextTarget.HasValue)
            {
                if (!_nextTarget.HasValue)
                {
                    if (_pointsQueue.Count == 0)
                    {
                        yield break;
                    }
                    _pointsQueue.TryDequeue(out var point);
                    _nextTarget = point;
                }
                var pos = Vector3.MoveTowards(m_Player.transform.position, _nextTarget.Value, m_Speed * Time.deltaTime);
                m_Player.transform.position = pos;
                var dist = Vector3.Distance(m_Player.transform.position, _nextTarget.Value);
                Debug.Log(dist);
                if (dist < 0.1f)
                {
             
                        _pointsQueue.TryDequeue(out var point);
                        _nextTarget = point;
                }
                yield return null;
            }
        }
    }
}