﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public class SaveData
    {
        [NonSerialized]
        private List<Vector3> _cached = new();
        
        private List<SerializableVector3> _points = new();
        private SerializableVector3 _localPosition;
        private SerializableVector3 _localEulerRotation;
        private SerializableVector3 _localScale;
        
        public List<Vector3> GetPoints()
        {
            _cached = _points.Select<SerializableVector3, Vector3>(x => x).ToList();
            return _cached;
        }

        public void SetPoints(List<Vector3> points)
        {
            _cached = points;
            _points = points.Select<Vector3, SerializableVector3>(x => x).ToList();
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
    }
}