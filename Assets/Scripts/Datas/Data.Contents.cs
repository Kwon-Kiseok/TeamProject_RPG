using HOGUS.Scripts.Manager;
using System;
using System.Collections.Generic;

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