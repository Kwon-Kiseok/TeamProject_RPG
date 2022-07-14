using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.Object.Item
{
    [CreateAssetMenu(fileName ="New BasicItem", menuName = "Scriptable Item Asset/Basic Item")]
    public class BaseItem : ScriptableObject
    {
        [Header("아이템 종류")]
        public ItemQuality quality;

        [Header("아이템 희귀도")]
        public ItemRarity rarity;

        [Header("아이템 이름")]
        public string itemName;

        [Header("아이템 크기")]
        public int itemWidth;
        public int itemHeight;
    }
}
