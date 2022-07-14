using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.Object.Item
{
    [CreateAssetMenu(fileName ="New WeaponItem", menuName = "Scriptable Item Asset/Weapon Item")]
    public class WeaponItem : EquipmentItem
    {
        [Header("무기 종류")]
        public WeaponType type;

        [Header("최소-최대 데미지")]
        public int minDamage;
        public int maxDamage;

        [Header("공격속도")]
        public int attackSpeed;
    }
}
