using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Object.Item
{
    public abstract class EquipmentItem : BaseItem
    {
        [Header("내구도")]
        public int durability;

        [Header("소켓 갯수")]
        public int socket;

        [Header("요구 능력치")]
        public int requireLevel;
        public int requireStrength;
        public int requireAgility;

        protected void CopyValue(EquipmentItem item)
        {
            base.CopyValue(item);

            this.durability = item.durability;
            this.socket = item.socket;
            this.requireLevel = item.requireLevel;
            this.requireStrength = item.requireStrength;
            this.requireAgility = item.requireAgility;
        }

        public abstract string GetDescription();
        public abstract void ApplyAbility(int add);
    }
}
