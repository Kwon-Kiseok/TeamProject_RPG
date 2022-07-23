using HOGUS.Scripts.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Object.Item;

// Reference by : https://velog.io/@starkshn/Data-Manager

namespace HOGUS.Scripts.Data
{
    #region PlayerData
    [Serializable]
    public class SavePlayerData
    {
        // 묶어서 플레이어 클래스 객체의 정보로 받아와야함
        public string PlayerName { get; set; }
        public int Stage { get; set; }
        public int LastHP { get; set; }
        public int LastMP { get; set; }
        // 플레이어 인벤토리, 맵 데이터, 퀘스트 상황 등도 저장
    }

    [Serializable]
    public class PlayerData : ILoader<string, SavePlayerData>
    {
        public List<SavePlayerData> savePlayerDatas = new();

        public Dictionary<string, SavePlayerData> MakeDict()
        {
            Dictionary<string, SavePlayerData> dict = new();
            foreach (SavePlayerData playerData in savePlayerDatas)           // 리스트에서 Dictionary로 옮김
            {
                dict.Add(playerData.PlayerName, playerData);           // ID를 Key로 사용
            }
            return dict;
        }
    }
    #endregion

    #region ItemData
    [Serializable]
    public class Item : ScriptableObject
    {
        public int id;
        public ItemType type;
        //public string name;
        public Sprite sprite;
        public string desc;

        public bool Use()
        {
            return false;
        }
    }

    [Serializable]
    public class ItemData : ILoader<int, BaseItem>
    {
        public List<BaseItem> items = new();            // json 파일에서 담길 리스트

        public Dictionary<int, BaseItem> MakeDict()
        {
            Dictionary<int, BaseItem> dict = new();
            foreach (BaseItem item in items)           // 리스트에서 Dictionary로 옮김
            {
                dict.Add(item.id, item);           // ID를 Key로 사용
            }
            return dict;
        }
    }
    #endregion

}