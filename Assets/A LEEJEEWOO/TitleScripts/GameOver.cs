using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void GoSavePoint()
    {
        //LoadingSceneController.LoadScene("");
    }

    public void GoTitleScreen()
    {
        LoadingSceneController.LoadScene("TitleManager");
    }
}
