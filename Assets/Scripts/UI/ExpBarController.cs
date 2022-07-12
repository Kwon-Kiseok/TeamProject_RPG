using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarController : MonoBehaviour
{
    public Slider ExpSlider;
    // Start is called before the first frame update
    void Start()
    {
        ExpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        ExpSlider.value += Time.deltaTime;

        if(ExpSlider.value >= 100 )
        {
            Debug.Log("Level UP");

            ExpSlider.value = 0;
            ExpSlider.maxValue += 100;
        }
    }
}
