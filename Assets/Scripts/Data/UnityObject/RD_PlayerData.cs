using Assets.Scripts.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Data.UnityObject
{
    [CreateAssetMenu(fileName = "RD_PlayerData", menuName = "Data/PlayerData", order = 0)]
    public class RD_PlayerData : ScriptableObject
    {
        public PlayerVO vo;
    }
}