using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;
using HOGUS.Scripts.Character;
using UnityEngine.UI;
using TMPro;

public class Player_Level : MonoBehaviour, IUpdatableObject
{
    private Player player;
    public TextMeshProUGUI lvTMP;

    public void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdatableObject(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void OnFixedUpdate(float deltaTime)
    {
        lvTMP.text = $"Lv. {player.GetCurrentStatus().Level}";
    }

    public void OnUpdate(float deltaTime)
    {
    }

}
