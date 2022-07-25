using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Female_Npc : MonoBehaviour
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
        TestOnTalk();
    }

    public void QuestIconController()
    {
        if (questManager.questId == 70)
        {
            questStartIcon.SetActive(false);
            questGoingIcon.SetActive(true);
        }
    }

    public void TestOnTalk()
    {
        if (uiManager.talkQuestIndex == 6)
        {
            onTalk.Invoke();
            questManager.questId = 70;
        }
    }
}
