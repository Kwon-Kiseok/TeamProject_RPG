using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Manager;

namespace HOGUS.Scripts.UI
{
    public class StatusInformation : MonoBehaviour, IUpdatableObject
    {
        public List<TextMeshProUGUI> informationList = new();
        private Player player;
        

        public void OnDisable()
        {
            if(UpdateManager.Instance != null)
                UpdateManager.Instance.DeregisterUpdatableObject(this);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            var player_stat = player.GetCurrentStatus();
            StatUpdate(player_stat);
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnEnable()
        {
            UpdateManager.Instance.RegisterUpdatableObject(this);

            player = GameObject.FindWithTag("Player").GetComponent<Player>();            
        }

        private void StatUpdate(Data.PlayerStat player_stat)
        {
            informationList[0].text = player_stat.CharacterClass;
            informationList[1].text = player_stat.Level.ToString();
            informationList[2].text = player_stat.Strength.ToString();
            informationList[3].text = player_stat.Magic.ToString();
            informationList[4].text = player_stat.Dexterity.ToString();
            informationList[5].text = player_stat.Vitality.ToString();
            informationList[6].text = player_stat.StatPoint.ToString();

            informationList[7].text = player_stat.CurHP.ToString();
            informationList[8].text = player_stat.MaxHP.ToString();
            informationList[9].text = player_stat.CurrentEXP.ToString();
            informationList[10].text = player_stat.EXP.ToString();
            informationList[11].text = player_stat.Gold.ToString();
            informationList[12].text = player_stat.Defense.ToString();
            informationList[13].text = player_stat.DodgeChance.ToString();
            informationList[14].text = $"{player_stat.MinDamage} ~ {player_stat.MaxDamage}";
            informationList[15].text = player_stat.CurMP.ToString();
            informationList[16].text = player_stat.MaxMP.ToString();
        }

        public void OnClickUpgradeStat(int stat)
        {
            if (player == null)
                return;
            if (player.GetCurrentStatus().StatPoint <= 0)
                return;

            player.GetCurrentStatus().StatPoint--;
            player.GetCurrentStatus().AddStat(stat);
        }
    }
}
