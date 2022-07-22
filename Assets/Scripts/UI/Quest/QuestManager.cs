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
    public int monsterHuntCount = 0;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("���� ���� ����� ��ȭ�ϱ�.", new int[] { 1000 }));
        questList.Add(20, new QuestData("���� " + monsterHuntCount + " / 5 ���", new int[] { 1000 }));
        questList.Add(30, new QuestData("���� �̿� �ٽ� ��ȭ�ϱ�", new int[] { 1000 }));
        questList.Add(40, new QuestData("���� ����� �ѷ��� ���� " + monsterHuntCount + " / 10 ���", new int[] { 2000 }));
        questList.Add(50, new QuestData("���� ��� �����ϱ�.", new int[] { 2000 }));
        questList.Add(60, new QuestData("���� ����� ��ȭ�ϱ�.", new int[] { 3000 }));
        questList.Add(70, new QuestData("���� ����ϱ�" + monsterHuntCount + " / 1", new int[] { 4000 }));
        questList.Add(80, new QuestData("Ȥ�� �� ���迡 ����ϱ�.", new int[] { 4000 }));
    }

    private void Update()
    {
        MonsterHuntQuestController();
    }

    public void MonsterHuntQuestController()
    {
        // ���� ������ monsterHuntCount ī��Ʈ ������Ű��
        if (questId == 20)
        {
            questText.text = "���� " + monsterHuntCount + " / 5 ���";
            if (monsterHuntCount == 5)
            {
                NextQuest();
                CheckQuest();
            }
        }
        if (questId == 40)
        {
            questText.text = "���� " + monsterHuntCount + " / 10 ���";
            if (monsterHuntCount == 10)
            {
                NextQuest();
                CheckQuest();
            }
        }
        if (questId == 70)
        {
            questText.text = "���� ����ϱ�" + monsterHuntCount + " / 1";
            if (monsterHuntCount == 1)
            {
                NextQuest();
                CheckQuest();
            }
        }
    }

    public int GetQuestTalkIndex(int _id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int _id)
    {
        // ���� ����Ʈ�� ����
        if (_id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        // ����Ʈ�� �����س��� npc��� ��ȭ�� �� ��������
        if(questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        //quest Name
        questText.text = questList[questId].questName;
        return questList[questId].questName;
    }

    public void AddQuestAction()
    {
        questActionIndex++;
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
        monsterHuntCount = 0;
        Debug.Log(questId);
        Debug.Log("Next Quest");
        questActionIndex = 0;
    }
}
