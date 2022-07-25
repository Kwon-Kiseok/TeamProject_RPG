using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;
using HOGUS.Scripts.Character;
using UnityEngine.UI;
using TMPro;

public class PlayerMP_Bar : MonoBehaviour, IUpdatableObject
{
    private Player player;
    private Slider slider;
    public TextMeshProUGUI mpTMP;

    public void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdatableObject(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        slider = GetComponent<Slider>();
    }

    public void OnFixedUpdate(float deltaTime)
    {

    }

    public void OnUpdate(float deltaTime)
    {
        slider.value = (float)player.GetCurrentStatus().CurMP / player.GetCurrentStatus().MaxMP;
        mpTMP.text = $"{player.GetCurrentStatus().CurMP} / {player.GetCurrentStatus().MaxMP}";
    }
}
