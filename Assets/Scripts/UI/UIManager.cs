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

    public UnityEngine.Events.UnityEvent onTalk;

    //private void Start()
    //{
    //    Debug.Log(questManager.CheckQuest());
    //}

    // player�� �ٶ󺸴� raycast���� layer object�� ������ �κп��� �� �Լ� ȣ��
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
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(_id));
            onTalk.Invoke();
            return;
        }
        if(_isNpc)
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
