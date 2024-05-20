using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public class SaveData
    {
        public List<Vector3> GetPoints()
        {
            _cached ??= _points.Select<SerializableVector3, Vector3>(x => x).ToList();
            return _cached;
        }

        public void SetPoints(List<Vector3> points)
        {
            _cached = points;
            _points = points.Select<Vector3, SerializableVector3>(x => x).ToList();
        }
        
        public Vector3 LastPoint
        {
            get => _lastPoint;
            set => _lastPoint = value;
        }
        
        public Vector3 LocalPosition
        {
            get => _localPosition;
            set => _localPosition = value;
        }
        
        public Vector3 LocalEulerRotation
        {
            get => _localEulerRotation;
            set => _localEulerRotation = value;
        }
        
        public Vector3 LocalScale
        {
            get => _localScale;
            set => _localScale = value;
        }
        
        public bool HasLastPoint
        {
            get => _hasLastPoint;
            set => _hasLastPoint = value;
        }
        
        [NonSerialized]
        private List<Vector3> _cached;
        
        private List<SerializableVector3> _points = new();
        private bool _hasLastPoint;
        private SerializableVector3 _lastPoint;
        private SerializableVector3 _localPosition;
        private SerializableVector3 _localEulerRotation;
        private SerializableVector3 _localScale;
    }
}