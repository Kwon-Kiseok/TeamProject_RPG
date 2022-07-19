using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan_Npc : MonoBehaviour
{
    public GameObject questStartIcon;
    public GameObject questGoingIcon;
    public GameObject questClearIcon;

    public QuestManager questManager;

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
        if(questManager.monsterHuntCount >= 5)
        {
            questClearIcon.SetActive(true);
            questGoingIcon.SetActive(false);
        }
        else if (questManager.questId >= 40)
        {
            questClearIcon.SetActive(false);
            return;
        }
    }
}
