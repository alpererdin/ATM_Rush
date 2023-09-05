using System;
using Unity.Mathematics;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public float2 HorizontalInputClampNegativeSides;
        public float HorizontalInputClampStopValue;
    }
}