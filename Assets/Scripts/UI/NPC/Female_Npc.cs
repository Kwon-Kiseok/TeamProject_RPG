using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Female_Npc : MonoBehaviour
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
        if (questManager.questId == 70)
        {
            questStartIcon.SetActive(false);
            questGoingIcon.SetActive(true);
        }
    }

    public void QuestHuntMonster()
    {
        if (questManager.monsterHuntCount >= 1)
        {
            questGoingIcon.SetActive(false);
        }
        else if (questManager.questId >= 80)
        {
            return;
        }
    }
}
