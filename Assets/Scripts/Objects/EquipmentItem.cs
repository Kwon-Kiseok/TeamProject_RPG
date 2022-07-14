using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Object.Item
{
    public class EquipmentItem : BaseItem
    {
        [Header("내구도")]
        public int durability;

        [Header("소켓 갯수")]
        public int socket;

        [Header("요구 능력치")]
        public int requireLevel;
        public int requireStrength;
        public int requireAgility;
    }
}
