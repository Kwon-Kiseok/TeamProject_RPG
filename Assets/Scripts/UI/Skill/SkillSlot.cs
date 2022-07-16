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

    //private void SetColor(float alpha)
    //{
    //    Color color = skill_Image.color;
    //    color.a = alpha;
    //    skill_Image.color = color;
    //}

    //public void AddSkill(Skill skill)
    //{
    //    this.skill = skill;
    //    skill_Image.sprite = skill.skill_Image;
    //    SetColor(1);
    //}

    //private void ClearSlot()
    //{
    //    skill = null;
    //    skill_Image.sprite = null;
    //    SetColor(0);
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (skill != null)
    //    {
    //        DragSlot.instance.dragSlot = this;
    //        DragSlot.instance.DragSetImage(skill_Image);

    //        DragSlot.instance.transform.position = eventData.position;
    //    }

    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if(skill != null)
    //    {
    //        DragSlot.instance.transform.position = eventData.position;
    //    }
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    if(DragSlot.instance.dragSlot != null)
    //    {
    //        ChangeSlot();
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    DragSlot.instance.SetColor(0);
    //    DragSlot.instance.dragSlot = null;
    //}

    //private void ChangeSlot()
    //{
    //    Skill tempSkill = skill;
    //    AddSkill(DragSlot.instance.dragSlot.skill);

    //    if(tempSkill != null)
    //    {
    //        DragSlot.instance.dragSlot.AddSkill(tempSkill);
    //    }
    //    else
    //    {
    //        DragSlot.instance.dragSlot.ClearSlot();
    //    }

    //}

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