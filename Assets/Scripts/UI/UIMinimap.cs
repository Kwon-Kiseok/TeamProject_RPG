using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMinimap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;

    [SerializeField]
    private TextMeshProUGUI MapName;
    private void Awake()
    {
        MapName.text = SceneManager.GetActiveScene().name;
    }
}
