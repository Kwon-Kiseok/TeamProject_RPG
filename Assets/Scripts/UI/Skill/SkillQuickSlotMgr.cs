using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQuickSlotMgr : MonoBehaviour
{
    public SkillQuickSlot[] skillQuickSlots = new SkillQuickSlot[4];
    public Player player;

    public void OnClick_AvoidSkill()
    {
        if (player.GetStat().Level >= 2)
        {
            skillQuickSlots[0].isLocked = false;
            skillQuickSlots[0].lockImage.enabled = false;
            skillQuickSlots[0].lockText.enabled = false;
        }
    }

    public void OnClick_TripleSkill()
    {
        if (player.GetStat().Level >= 3)
        {
            skillQuickSlots[1].isLocked = false;
            skillQuickSlots[1].lockImage.enabled = false;
        }
    }

    public void OnClick_WaterSkill()
    {
        if (player.GetStat().Level >= 4)
        {
            skillQuickSlots[2].isLocked = false;
            skillQuickSlots[2].lockImage.enabled = false;
        }
    }

    public void OnClick_HealSkill()
    {
        if (player.GetStat().Level >= 5)
        {
            skillQuickSlots[3].isLocked = false;
            skillQuickSlots[3].lockImage.enabled = false;
        }
    }

}
