using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.Object.Item
{
    [CreateAssetMenu(fileName ="New JewelItem", menuName = "Scriptable Item Asset/Jewel Item")]
    public class JewelItem : Decorator
    {
        [Header("추가 능력치")]
        public int additionalAbility;

        [Header("추가 능력치 설명")]
        public string optionDescription;

        [Header("장착 유무")]
        public bool InsertSocket = false;

        public JewelItem(WeaponItem weapon)
        {
            this.equipment = weapon;
        }

        public override void ApplyAbility()
        {
            InsertSocket = true;
        }

        public override string GetDescription()
        {
            return $"{this.equipment.GetDescription()}\n{optionDescription}";
        }
    }
}
