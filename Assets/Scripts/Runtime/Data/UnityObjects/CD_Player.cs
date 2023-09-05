using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "ATM_Rush/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;

    }
}