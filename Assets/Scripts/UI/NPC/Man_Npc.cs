using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_Npc : MonoBehaviour
{
    public GameObject questStartIcon;
    public GameObject questGoingIcon;
    public GameObject questClearIcon;

    public QuestManager questManager;

    public UnityEngine.Events.UnityEvent onTalk;
    public UIManager uiManager;
    private void Start()
    {
        questGoingIcon.SetActive(true);
    }

    private void Update()
    {
        QuestHuntMonster();
    }

    public void QuestHuntMonster()
    {
        if (questManager.questId == 50)
        {
            questClearIcon.SetActive(true);
            questGoingIcon.SetActive(false);
        }
        else if (questManager.questId >= 60)
        {
            questClearIcon.SetActive(false);
            return;
        }
    }

    public void TestOnTalk()
    {
        if (uiManager.talkQuestIndex == 4)
        {
            onTalk.Invoke();
        }
    }
}
