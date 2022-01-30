using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "RD_PlayerData", menuName = "Data/PlayerData", order = 0)]
    public class RD_PlayerData : ScriptableObject
    {
        public PlayerVO vo;
    }
}