using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class SkillSlot : MonoBehaviour
{
    public TextMeshProUGUI skill_Name;
    public TextMeshProUGUI skill_Description;
    public TextMeshProUGUI skill_Level;
    public Image skill_Image;
    public void SetData(string _SkillName, string _SkillDec, string _Level, Sprite _SkillImage)
    {
        skill_Name.text = _SkillName;
        skill_Description.text = _SkillDec;
        //skill_Cost = _Cost;
        //skill_CoolTime = _CoolTime;
        skill_Level.text = _Level; // + " " + Player.Lv;
        //skill_CastTime = _CastTime;
        skill_Image.sprite = _SkillImage;
    }
}