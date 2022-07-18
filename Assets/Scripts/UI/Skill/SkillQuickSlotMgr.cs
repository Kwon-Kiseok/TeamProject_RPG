using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQuickSlotMgr : MonoBehaviour
{
    public SkillQuickSlot[] skillQuickSlots = new SkillQuickSlot[4];

    [SerializeField]
    private TestPlayerLv playerLv;

    //public Player player;

    private void Awake()
    {
        //player = GetComponent<Player>();
    }

    public void OnClick_AvoidSkill()
    {
        Debug.Log("AvoidSkill");
    }

    public void OnClick_TripleSkill()
    {
        //if (player.player_Lv >= 3)
        //{
        //    skillQuickSlots[1].skillRock = false;
        //    skillQuickSlots[1].rockImage.enabled = false;
        //}
        if (playerLv.Lv >= 2)
        {
            skillQuickSlots[1].skillLock = false;
            skillQuickSlots[1].lockImage.enabled = false;
            skillQuickSlots[1].lock_Text.enabled = false;
        }
    }

    public void OnClick_WaterSkill()
    {
        //if (player.player_Lv >= 4)
        //{
        //    skillQuickSlots[2].skillRock = false;
        //    skillQuickSlots[2].rockImage.enabled = false;
        //}
        if (playerLv.Lv >= 3)
        {
            skillQuickSlots[2].skillLock = false;
            skillQuickSlots[2].lockImage.enabled = false;
            skillQuickSlots[2].lock_Text.enabled = false;
        }
    }

    public void OnClick_HealSkill()
    {
        //if (player.player_Lv >= 5)
        //{
        //    skillQuickSlots[3].skillRock = false;
        //    skillQuickSlots[3].rockImage.enabled = false;
        //}
        if (playerLv.Lv >= 4)
        {
            skillQuickSlots[3].skillLock = false;
            skillQuickSlots[3].lockImage.enabled = false;
            skillQuickSlots[3].lock_Text.enabled = false;
        }
    }

}
