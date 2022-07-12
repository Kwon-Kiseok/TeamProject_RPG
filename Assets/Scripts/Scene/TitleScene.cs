using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void OnClickNewGameButton()
    {
        Debug.Log("게임 시작");
        LoadingSceneController.LoadScene("MainGame");
    }
    public void OnClickContinueButton()
    {
        Debug.Log("게임 계속 진행");
        //SceneManager.LoadScene();
    }
    public void OnClickExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
