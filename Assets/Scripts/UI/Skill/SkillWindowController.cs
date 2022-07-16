using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillWindowController : MonoBehaviour
{
    public SkillSlot[] skillsSlotArray = new SkillSlot[10];

    public Skill[] skillDatas = new Skill[5];

    //private Skill skill;

    //public Button equipButton;

    //private void Awake()
    //{
    //    skill = GetComponent<Skill>();
    //}
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < skillsSlotArray.Length; ++i)
        {
            SetSkillData(i, skillDatas[i]);
        }
    }

    public void SetSkillData(int idx, Skill data)
    {
        skillsSlotArray[idx].SetData(data.skill_Name, data.skill_Description, data.skill_Level, data.skill_Image);
    }

    //public void OnClickButton()
    //{
    //    if(skill.skill_Rock)
    //    {

    //    }
    //}
}
