using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan_Npc : MonoBehaviour
{
    public GameObject questStartIcon;
    public GameObject questGoingIcon;
    public GameObject questClearIcon;

    public QuestManager questManager;

    public UnityEngine.Events.UnityEvent onTalk;

    public UIManager uiManager;

    private void Start()
    {
        questStartIcon.SetActive(true);
    }

    private void Update()
    {
        QuestIconController();
        QuestHuntMonster();
    }

    public void QuestIconController()
    {
        if(questManager.questId == 20)
        {
            questStartIcon.SetActive(false);
            questGoingIcon.SetActive(true);
        }
    }

    public void QuestHuntMonster()
    {
        if(questManager.questId == 30)
        {
            questClearIcon.SetActive(true);
            questGoingIcon.SetActive(false);
        }
        else if (questManager.questId > 30)
        {
            questClearIcon.SetActive(false);
            return;
        }
    }

    public void TestOnTalk()
    {
        if (uiManager.talkQuestIndex == 0)
        {
            Debug.Log("talkQuestIndex: 1");
            onTalk.Invoke();
        }
        if (uiManager.talkQuestIndex == 2)
        {
            Debug.Log("talkQuestIndex: 3");
            onTalk.Invoke();
        }
    }
}
