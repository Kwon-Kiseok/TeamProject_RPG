using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;
    public TextMeshProUGUI questText;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("늙은 마을 사람과 대화하기.", new int[] { 1000 }));
        questList.Add(20, new QuestData("남자 고블린 구출하기.", new int[] { 2000 }));
        questList.Add(30, new QuestData("여자 고블린과 대화하기.", new int[] { 3000 }));
    }

    public int GetQuestTalkIndex(int _id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int _id)
    {
        // 다음 퀘스트로 연계
        if(_id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // 퀘스트에 저장해놓은 npc들과 대화를 다 나눴는지
        if(questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        //quest Name
        questText.text = questList[questId].questName;
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //quest Name
        questText.text = questList[questId].questName;
        return questList[questId].questName;
    }

    private void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
