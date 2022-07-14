using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSkillWindow;

    [SerializeField]
    private GameObject activeSkillWindow;

    [SerializeField]
    private GameObject passiveSkillWindow;

    private bool ActiveButtonCheck;

    private bool SkillWindowOpen;

    public void OnClickSkillButton()
    {
        ActiveButtonCheck = true;
        playerSkillWindow.SetActive(true);
        activeSkillWindow.SetActive(true);
        passiveSkillWindow.SetActive(false);
    }

    public void OnClickActiveButton()
    {
        if(ActiveButtonCheck)
        {
            return;
        }
        ActiveButtonCheck = true;
        activeSkillWindow.SetActive(true);
        passiveSkillWindow.SetActive(false);
    }
    public void OnClickPassiveButton()
    {
        ActiveButtonCheck = false;
        activeSkillWindow.SetActive(false);
        passiveSkillWindow.SetActive(true);
    }

    public void OnClickCloseButton()
    {
        playerSkillWindow.SetActive(false);
        activeSkillWindow.SetActive(false);
        passiveSkillWindow.SetActive(false);
    }
}
