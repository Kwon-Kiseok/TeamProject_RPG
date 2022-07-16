using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public SkillSlot dragSlot;

    public Image Skill_Image;

    private void Awake()
    {
        instance = this;
    }

    public void DragSetImage(Image _ImageSkill)
    {
        Skill_Image.sprite = _ImageSkill.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = Skill_Image.color;
        color.a = alpha;
        Skill_Image.color = color;
    }
}
