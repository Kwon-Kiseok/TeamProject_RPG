using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;
using HOGUS.Scripts.Character;
using UnityEngine.UI;
using TMPro;

public class PlayerHP_Bar : MonoBehaviour, IUpdatableObject
{
    private Player player;
    private Slider slider;
    public TextMeshProUGUI hpTMP;

    public void OnDisable()
    {
        if(UpdateManager.Instance != null)
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
        slider.value = (float)player.GetCurrentStatus().CurHP / player.GetCurrentStatus().MaxHP;
        hpTMP.text = $"{player.GetCurrentStatus().CurHP} / {player.GetCurrentStatus().MaxHP}";
    }
}
