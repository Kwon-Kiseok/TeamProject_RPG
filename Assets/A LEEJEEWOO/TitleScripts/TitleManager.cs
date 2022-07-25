using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{    
    public void StartGame()
    {
        Debug.Log("게임시작");
        LoadingSceneController.LoadScene("GameClearScene1");
    }

    public void LoadGame()
    {
        Debug.Log("블러오기");
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Debug.Log("게임종료");        
        Application.Quit();
    }
}
