using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;

public class SkillQuickSlotMgr : MonoBehaviour
{
    public SkillQuickSlot[] skillQuickSlots = new SkillQuickSlot[4];
    public SkillBtn[] skillBtns = new SkillBtn[4];
    public Player player;

    #region skill index
    readonly int dodgeIndex = 0;
    readonly int comboIndex = 1;
    readonly int iceballIndex = 2;
    readonly int healIndex = 3;
    #endregion

    readonly int dodgeUnlockLevel = 2;
    public void OnClick_AvoidSkill()
    {
        if (skillQuickSlots[dodgeIndex].isLocked && player.GetCurrentStatus().Level >= dodgeUnlockLevel)
        {
            skillQuickSlots[dodgeIndex].isLocked = false;
            skillQuickSlots[dodgeIndex].lockImage.enabled = false;
            skillQuickSlots[dodgeIndex].lockText.enabled = false;
        }
        else if(!skillQuickSlots[dodgeIndex].isLocked)
        {
            if (player.GetCurrentStatus().CurMP < player.DodgeRequireMP)
            {
                return;
            }

            player.Dodge();
            skillBtns[dodgeIndex].UseSkill();
        }
    }

    readonly int comboUnlockLevel = 3;
    public void OnClick_TripleSkill()
    {
        if (skillQuickSlots[comboIndex].isLocked && player.GetCurrentStatus().Level >= comboUnlockLevel)
        {
            skillQuickSlots[comboIndex].isLocked = false;
            skillQuickSlots[comboIndex].lockImage.enabled = false;
        }
        else if (!skillQuickSlots[comboIndex].isLocked)
        {
            if (player.GetCurrentStatus().CurMP < player.ComboRequireMP)
            {
                return;
            }

            player.ComboAttack();
            skillBtns[comboIndex].UseSkill();
        }
    }

    readonly int iceballUnlockLevel = 4;
    public void OnClick_WaterSkill()
    {
        if (skillQuickSlots[iceballIndex].isLocked && player.GetCurrentStatus().Level >= iceballUnlockLevel)
        {
            skillQuickSlots[iceballIndex].isLocked = false;
            skillQuickSlots[iceballIndex].lockImage.enabled = false;
        }
        else if (!skillQuickSlots[iceballIndex].isLocked)
        {
            if (player.GetCurrentStatus().CurMP < player.IceBallRequireMP)
            {
                return;
            }

            player.IceBall();
            skillBtns[iceballIndex].UseSkill();
        }
    }

    readonly int healUnlockLevel = 5;
    public void OnClick_HealSkill()
    {
        if (skillQuickSlots[healIndex].isLocked && player.GetCurrentStatus().Level >= healUnlockLevel)
        {
            skillQuickSlots[healIndex].isLocked = false;
            skillQuickSlots[healIndex].lockImage.enabled = false;
        }
        else if (!skillQuickSlots[healIndex].isLocked)
        {
            player.SkilHeal();
            skillBtns[healIndex].UseSkill();
        }
    }

}
