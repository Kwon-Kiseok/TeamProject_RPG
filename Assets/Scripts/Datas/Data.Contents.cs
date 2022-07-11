using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;

// Reference by : https://velog.io/@starkshn/Data-Manager

namespace HOGUS.Scripts.Data
{

    #region Item
    [Serializable]
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite_path { get; set; }
        public string Desc { get; set; }
    }

    [Serializable]
    public class ItemData : ILoader<int, Item>
    {
        public List<Item> items = new();            // json 파일에서 담길 리스트

        public Dictionary<int, Item> MakeDict()
        {
            Dictionary<int, Item> dict = new();
            foreach (Item item in items)           // 리스트에서 Dictionary로 옮김
            {
                dict.Add(item.Id, item);           // ID를 Key로 사용
            }
            return dict;
        }
    }
    #endregion

}