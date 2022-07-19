using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_Npc : MonoBehaviour
{
    public GameObject questStartIcon;
    public GameObject questGoingIcon;
    public GameObject questClearIcon;

    public QuestManager questManager;

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
        if (questManager.monsterHuntCount >= 10)
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
}
