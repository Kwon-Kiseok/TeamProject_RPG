using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearScene : MonoBehaviour
{
    public float Endtime = 5.0f;
    public float Starttime = 0f;

    private void Update()
    {
        Starttime += Time.deltaTime;
        if (Starttime > Endtime)
        {
            LoadingSceneController.LoadScene("ClearScene2");
        }
    }
}
