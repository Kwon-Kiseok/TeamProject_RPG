using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.CustomSystem;

public class Tester : MonoBehaviour, IUpdatableObject
{
    //
    public WeaponItem weaponPrefab;
    public JewelItem jewelPrefab;

    //
    private EquipmentSystem equipmentSystem;
    public Transform weaponEquipPos;

    //
    private JewelItem hasJewel;

    // tester stat
    public int minDamage;
    public int maxDamage;
    public int defense;
    public float dodgeChance;
    public float attackSpeed;

    //
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

    private void Awake()
    {
        equipmentSystem = GetComponent<EquipmentSystem>();
    }

    public void OnFixedUpdate()
    {
        if (equipmentSystem.dictEquipmets.ContainsKey(EquipPart.WEAPON))
        {
            WeaponItem weaponPart = (WeaponItem)equipmentSystem.dictEquipmets[EquipPart.WEAPON];

            if (weaponPart != null)
            {
                testerDataUI.text =
                    "Name: " + weaponPart.name + "\n" +
                    string.Format("Damage: {0} ~ {1}\n", weaponPart.minDamage, weaponPart.maxDamage) +
                    "Desc: " + weaponPart.GetDescription();
            }
        }
    }

    public void OnUpdate()
    {
        // 무기 장착 테스트
        if(Input.GetKeyDown(KeyCode.E))
        {
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.CopyValue(weaponPrefab);
            equipmentSystem.DoEquip(EquipPart.WEAPON, weapon);
        }
        // 무기 해제 테스트
        else if(Input.GetKeyDown(KeyCode.U))
        {
            equipmentSystem.DoUnequip(EquipPart.WEAPON);
            testerDataUI.text = null;
        }
        // 보석 삽입 테스트 
        else if(Input.GetKeyDown(KeyCode.R))
        {
            if (hasJewel == null)
            {
                hasJewel = ScriptableObject.CreateInstance<JewelItem>();
                hasJewel.CopyValue(jewelPrefab);
            }
            hasJewel.Attach(equipmentSystem.dictEquipmets[EquipPart.WEAPON]);

            hasJewel = null;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            // 로그 재확인
            Debug.Log(string.Format("{0}, {1}, {2}, {3]", minDamage, maxDamage, defense, attackSpeed));
        }
    }

    public void OnUpdate(float deltaTime)
    {
    }
}
