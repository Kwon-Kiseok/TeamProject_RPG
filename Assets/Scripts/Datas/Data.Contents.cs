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
        public List<Item> items = new();            // json ���Ͽ��� ��� ����Ʈ

        public Dictionary<int, Item> MakeDict()
        {
            Dictionary<int, Item> dict = new();
            foreach (Item item in items)           // ����Ʈ���� Dictionary�� �ű�
            {
                dict.Add(item.Id, item);           // ID�� Key�� ���
            }
            return dict;
        }
    }
    #endregion

}