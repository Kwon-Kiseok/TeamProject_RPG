using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Data;

namespace HOGUS.Scripts.Object.Item
{
    [CreateAssetMenu(fileName ="New BasicItem", menuName = "Scriptable Item Asset/Basic Item")]
    public class BaseItem : ScriptableObject
    {
        /// <summary>
        /// Number 1 = 무기(오른손)
        /// Number 2 = 장비
        /// Number 3 = 보석
        /// Number 4 = 소모품
        /// </summary>

        [Header("아이템 번호")]
        public int id;

        [Header("아이템 이미지")]
        public Sprite sprite;

        [Header("아이템 종류")]
        public ItemQuality quality;

        [Header("아이템 희귀도")]
        public ItemRarity rarity;

        [Header("아이템 이름")]
        public string itemName;

        [Header("아이템 설명")]
        public string itemDescription;

        [Header("아이템 크기")]
        public int itemWidth;
        public int itemHeight;

        protected void CopyValue(BaseItem item)
        {
            quality = item.quality;
            rarity = item.rarity;
            itemName = item.itemName;
            itemDescription = item.itemDescription;
            itemWidth = item.itemWidth;
            itemHeight = item.itemHeight;
        }
    }
}
