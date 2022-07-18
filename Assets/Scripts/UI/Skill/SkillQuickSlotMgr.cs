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
        //if (player.player_Lv >= 2)
        //{
        //    skillQuickSlots[0].skillRock = false;
        //    skillQuickSlots[0].rockImage.enabled = false;
        //}

        if (playerLv.Lv >= 2f)
        {
            skillQuickSlots[0].skillRock = false;
            skillQuickSlots[0].rockImage.enabled = false;
            skillQuickSlots[0].rock_Text.enabled = false;
        }
    }

    public void OnClick_TripleSkill()
    {
        //if (player.player_Lv >= 3)
        //{
        //    skillQuickSlots[1].skillRock = false;
        //    skillQuickSlots[1].rockImage.enabled = false;
        //}
        if (playerLv.Lv >= 3)
        {
            skillQuickSlots[1].skillRock = false;
            skillQuickSlots[1].rockImage.enabled = false;
        }
    }

    public void OnClick_WaterSkill()
    {
        //if (player.player_Lv >= 4)
        //{
        //    skillQuickSlots[2].skillRock = false;
        //    skillQuickSlots[2].rockImage.enabled = false;
        //}
        if (playerLv.Lv >= 4)
        {
            skillQuickSlots[2].skillRock = false;
            skillQuickSlots[2].rockImage.enabled = false;
        }
    }

    public void OnClick_HealSkill()
    {
        //if (player.player_Lv >= 5)
        //{
        //    skillQuickSlots[3].skillRock = false;
        //    skillQuickSlots[3].rockImage.enabled = false;
        //}
        if (playerLv.Lv >= 5)
        {
            skillQuickSlots[3].skillRock = false;
            skillQuickSlots[3].rockImage.enabled = false;
        }
    }

}
