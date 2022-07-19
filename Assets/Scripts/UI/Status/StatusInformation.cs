using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HOGUS.Scripts.UI
{
    public class StatusInformation : MonoBehaviour
    {
        public List<TextMeshProUGUI> informationList = new();

        private void OnEnable()
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

            var player_stat = player.GetCurrentStatus();

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
        }
    }
}
