using UnityEngine;
using UnityEngine.UI;

using HOGUS.Scripts.Character;

public class ExpBarController : MonoBehaviour
{
    public Slider ExpSlider;
    public Player player;

    void Start()
    {
        ExpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //player.GetStat().CurrentEXP += Time.deltaTime * 10;
        //ExpSlider.value = player.GetStat().CurrentEXP / player.GetStat().EXP;

        if (ExpSlider.value >= 1)
        {
            player.LevelUp();
            ExpSlider.value = 0;
        }
    }
}
