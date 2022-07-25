using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearScene2 : MonoBehaviour
{
    public void CleartextNext()
    {
        LoadingSceneController.LoadScene("EnddingScene");
    }
}
