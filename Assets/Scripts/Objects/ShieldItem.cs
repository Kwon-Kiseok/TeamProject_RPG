using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Object.Item
{
    [CreateAssetMenu(fileName ="New ShieldItem", menuName ="Scriptable Item Asset/Shield Item")]
    public class ShieldItem : EquipmentItem
    {
        [Header("방어력")]
        public int defense;

        [Header("막기 확률")]
        public float blockChance;

        public void CopyValue(ShieldItem item)
        {
            base.CopyValue(item);
            defense = item.defense;
            blockChance = item.blockChance;
        }

        public override void ApplyAbility(int add)
        {
            defense += add;
        }

        public override string GetDescription()
        {
            return this.itemDescription;
        }
    }
}