using System;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public float ClampSpeed;
        public Vector2 ClampValues;
    }
}