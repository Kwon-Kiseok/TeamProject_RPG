using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMinimap : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MapName;
    private void Start()
    {
        MapName.text = SceneManager.GetActiveScene().name;
    }
}
