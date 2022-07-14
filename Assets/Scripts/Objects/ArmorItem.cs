using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.Object.Item
{
    [CreateAssetMenu(fileName ="New ArmorItem", menuName = "Scriptable Item Asset/Armor Item")]
    public class ArmorItem : EquipmentItem
    {
        [Header("방어구 종류")]
        public ArmorType type;

        [Header("방어력")]
        public int defense;

        public override void ApplyAbility()
        {
            throw new System.NotImplementedException();
        }

        public override string GetDescription()
        {
            return this.itemDescription;
        }
    }
}