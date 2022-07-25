using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("늙은 마을 사람과 대화하기.", new int[] { 1000 }));
        questList.Add(20, new QuestData("몬스터 0 / 5 잡기", new int[] { 1000 }));
        questList.Add(30, new QuestData("늙은 이와 다시 대화하기", new int[] { 1000 }));
        questList.Add(40, new QuestData("남자 고블린을 둘러싼 몬스터 0 / 10 잡기", new int[] { 2000 }));
        questList.Add(50, new QuestData("남자 고블린 구출하기.", new int[] { 2000 }));
        questList.Add(60, new QuestData("여자 고블린과 대화하기.", new int[] { 3000 }));
        questList.Add(70, new QuestData("마왕 사냥하기 0 / 1", new int[] { 4000 }));
        questList.Add(80, new QuestData("혹시 모를 위험에 대비하기.", new int[] { 4000 }));
    }

    public int GetQuestTalkIndex(int _id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int _id)
    {
        // 다음 퀘스트로 연계
        if (_id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // 퀘스트에 저장해놓은 npc들과 대화를 다 나눴는지
        if(questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        //quest Name
        return questList[questId].questName;
    }

    public void AddQuestAction()
    {
        questActionIndex++;
    }


    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
