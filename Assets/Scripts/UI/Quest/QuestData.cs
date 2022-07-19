using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcId;

    public QuestData(string _name, int[] npc)
    {
        questName = _name;
        npcId = npc;
    }
}
