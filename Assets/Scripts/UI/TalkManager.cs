using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public UIManager uIManager;
    public QuestManager questManager;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        //Talk Data
        //NPC OldMan: 1000, NPC MAN: 2000, NPC Female: 3000
        // npc id를 받아서 해당 npc의 대사 출력
        talkData.Add(1000, new string[] { "어서오시게, 용사여.." });
        talkData.Add(2000, new string[] { "도와주세요.. 으윽.." });
        talkData.Add(3000, new string[] { "조심해!" });

        // 늙은 고블린 퀘스트
        talkData.Add(10 + 1000, new string[] { "어서오시게, 용사여..", "강해보이는군.", "내 부탁 하나 들어주겠는가?", "근처의 몬스터 2마리만 잡아주게" });
        talkData.Add(21 + 1000, new string[] { "2마리만 부탁하네"});

        talkData.Add(30 + 1000, new string[] { "고맙다네. 도움 받고 염치없지만..", "혹시 괜찮다면 마을사람들을 구출해줄 수 있겠나?" });

        // 마을 남자 퀘스트
        talkData.Add(40 + 1000, new string[] { "가장 가까운 마을 사람은 바로 이 앞 동굴에 있네." });
        talkData.Add(50 + 2000, new string[] { "감사합니다!", "정말 감사합니다!", "하지만 아직 구출이 안 된 마을 사람이 있습니다.", "부탁드립니다." });

        // 마을 여자 퀘스트
        talkData.Add(60 + 3000, new string[] { "너도 마왕을 잡으러 온 거야?", "특별히 너한테 먼저 양보해줄게" });
    }

    public string GetTalk(int _id, int _talkIndex)
    {
        if(!talkData.ContainsKey(_id))
        {
            if(!talkData.ContainsKey(_id - _id %10))
            {
                // 퀘스트 맨 처음 대사마저 없다면 기본 대사만 출력
               return GetTalk(_id - _id %100, _talkIndex);  
            }
            else
            {
                // 해당 퀘스트 진행 순서 대사가 없을 때
                // 퀘스트 맨 처음 대사를 가져온다.
                return GetTalk(_id - _id % 10, _talkIndex);
            }
        }
        if(_talkIndex == talkData[_id].Length)
        {
            return null;
        }
        else
        {
            return talkData[_id][_talkIndex];
        }
    }
}
