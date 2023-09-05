using System;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;

    }

    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;
    }
}