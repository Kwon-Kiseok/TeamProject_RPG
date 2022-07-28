using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    public OldMan_Npc oldNpc;
    public Man_Npc manNpc;
    public Female_Npc femaleNpc;

    public int talkQuestIndex = 0;
    public int killQuestIndex = 0;

    // player가 바라보는 raycast에서 layer object를 만나는 부분에서 이 함수 호출
    public void Action(GameObject _scanObj)
    {
        scanObject = _scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int _id, bool _isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(_id);
        string talkData = talkManager.GetTalk(_id + questTalkIndex, talkIndex);
        if (talkData == null)
        {
            isAction = false;
            Debug.Log("대화 끝남");
            oldNpc.TestOnTalk();
            manNpc.TestOnTalk();
            femaleNpc.TestOnTalk();
            talkIndex = 0;
            return;
        }
        if (_isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true;
        talkIndex++;
    }
}
