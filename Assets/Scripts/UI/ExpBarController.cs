using UnityEngine;
using UnityEngine.UI;

public class ExpBarController : MonoBehaviour
{
    public Slider ExpSlider;
    // Start is called before the first frame update
    
    private Player player;

    void Start()
    {
        ExpSlider = GetComponent<Slider>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        ExpSlider.value += Time.deltaTime;

        if (ExpSlider.value >= 100)
        {
            Debug.Log("Level UP");
            ExpSlider.value = 0;
            ExpSlider.maxValue += 100;
        }
    }
}
