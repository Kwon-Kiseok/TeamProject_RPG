using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.Manager;
using HOGUS.Scripts.Object.Item;

public class Tester : MonoBehaviour, IUpdatableObject
{
    public WeaponItem weaponPrefab;
    public JewelItem jewelPrefab;

    private WeaponItem myWeapon;
    private JewelItem myJewel;

    public TextMeshProUGUI testerDataUI;

    public void OnDisable()
    {
        if(UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdatableObject(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
    }

    private void Start()
    {
        myWeapon = new WeaponItem(weaponPrefab);
        myJewel = new JewelItem(jewelPrefab);
    }

    public void OnFixedUpdate()
    {
        testerDataUI.text =
            "Name: " + myWeapon.name + "\n" +
            string.Format("Damage: {0} ~ {1}\n", myWeapon.minDamage, myWeapon.maxDamage) +
            "Desc: " + myWeapon.GetDescription();
    }

    public void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            myJewel.Attach(myWeapon);
        }
    }

    public void OnUpdate(float deltaTime)
    {
    }
}
