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

    #region base stat
    // ĳ������ �⺻ ������ ����
    public int minDamage = 0;
    public int maxDamage = 0;
    public int defense = 0;
    public float dodgeChance = 0f;
    public float attackSpeed = 0f;
    #endregion

    #region current equipments stat
    // ĳ���Ͱ� �����ϰ� �ִ� ������ ������ ������
    public int equipedDefense = 0;

    public float equipedDodgeChance = 0f;
    #endregion

    #region result stat
    // ĳ������ �⺻ ������ ����
    public int resMinDamage = 0;
    public int resMaxDamage = 0;
    public int resDefense = 0;
    public float resDodgeChance = 0f;
    public float resAttackSpeed = 0f;
    #endregion

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
        if (equipmentSystem.equipWeapon != null)
        {
            WeaponItem weaponPart = equipmentSystem.equipWeapon;

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
        // ���� ���� �׽�Ʈ
        if(Input.GetKeyDown(KeyCode.E))
        {
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.CopyValue(weaponPrefab);
            equipmentSystem.DoEquip(EquipPart.WEAPON, weapon);

            UpdateStat();
        }
        // ���� ���� �׽�Ʈ
        else if(Input.GetKeyDown(KeyCode.U))
        {
            equipmentSystem.DoUnequip(EquipPart.WEAPON);
            testerDataUI.text = null;

            UpdateStat();
        }
        // ���� ���� �׽�Ʈ 
        else if(Input.GetKeyDown(KeyCode.R))
        {
            if (hasJewel == null)
            {
                hasJewel = ScriptableObject.CreateInstance<JewelItem>();
                hasJewel.CopyValue(jewelPrefab);
            }
            hasJewel.Attach(equipmentSystem.equipWeapon);

            hasJewel = null;

            UpdateStat();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {            
            // �α� ��Ȯ��
            Debug.Log(resMinDamage+", "+ resMaxDamage);
        }
    }

    public void OnUpdate(float deltaTime)
    {
    }

    private void UpdateStat()
    {
        // ���� ������ ���Ⱑ ���ٸ� ĳ������ �⺻ ���̽� �������� ��������
        if (equipmentSystem.equipWeapon == null)
        {
            resMinDamage = minDamage;
            resMaxDamage = maxDamage;
            resAttackSpeed = attackSpeed;
        }
        // ������ ���Ⱑ �ִٸ� ĳ������ ���̽� ���� + ���� ������ ����� �ɷ�ġ�� ����
        else
        {
            resMinDamage = minDamage + equipmentSystem.equipWeapon.minDamage;
            resMaxDamage = maxDamage + equipmentSystem.equipWeapon.maxDamage;
            resAttackSpeed = attackSpeed + equipmentSystem.equipWeapon.attackSpeed;
        }
        resDefense = defense + equipedDefense;
        resDodgeChance = dodgeChance + equipedDodgeChance;
    }
}