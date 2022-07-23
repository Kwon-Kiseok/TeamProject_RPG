using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReader : MonoBehaviour
{
    private void Start()
    {
        List<Dictionary<string, object>> quest_Dialog = CSVReader.Read("QuestChapter/QuestChapter");
        for (int i = 0; i < quest_Dialog.Count; i++)
        {
            print(quest_Dialog[i]["QuestDec"].ToString());
        }
    }
}
