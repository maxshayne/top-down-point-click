using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public class SaveData
    {
        public List<Vector3> Points = new();
        public Vector3? LastPoint;
        public Vector3 LocalPosition;
        public Vector3 LocalEulerRotation;
        public Vector3 LocalScale;
    }
}