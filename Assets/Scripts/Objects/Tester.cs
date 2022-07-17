using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.Data;
using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.CustomSystem;

public class Tester : Character
{
    //
    public WeaponItem weaponPrefab;
    public JewelItem jewelPrefab;

    //
    public Joystick joystick;
    private EquipmentSystem equipmentSystem;
    public Transform weaponEquipPos;

    //
    private JewelItem hasJewel;

    #region base stat
    // 캐릭터의 기본 스탯은 고유
    PlayerStat stat;
    #endregion

    #region current equipments stat
    // 캐릭터가 장착하고 있는 장비들의 스탯은 별도로
    public int equipedDefense = 0;

    public float equipedDodgeChance = 0f;
    #endregion

    #region result stat
    // 캐릭터의 기본 스탯은 고유
    public int resMinDamage = 0;
    public int resMaxDamage = 0;
    public int resDefense = 0;
    public float resDodgeChance = 0f;
    public float resAttackSpeed = 0f;
    #endregion

    //
    public TextMeshProUGUI testerDataUI;

    private void Awake()
    {
        equipmentSystem = GetComponent<EquipmentSystem>();
        stat = GetComponent<PlayerStat>();
    }

    public override void OnFixedUpdate(float deltaTime)
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

    public override void OnUpdate(float deltaTime)
    {
        Move(deltaTime);

        // 무기 장착 테스트
        if (Input.GetKeyDown(KeyCode.E))
        {
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.CopyValue(weaponPrefab);
            equipmentSystem.DoEquip(EquipPart.WEAPON, weapon);

            UpdateStat();
        }
        // 무기 해제 테스트
        else if (Input.GetKeyDown(KeyCode.U))
        {
            equipmentSystem.DoUnequip(EquipPart.WEAPON);
            testerDataUI.text = null;

            UpdateStat();
        }
        // 보석 삽입 테스트 
        else if (Input.GetKeyDown(KeyCode.R))
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            stat.PrintDebugStat();
            Debug.Log(resMinDamage + ", " + resMaxDamage);
        }
    }

    private void UpdateStat()
    {
        // 현재 장착한 무기가 없다면 캐릭터의 기본 베이스 스탯으로 설정해줌
        if (equipmentSystem.equipWeapon == null)
        {
            resMinDamage = stat.MinDamage;
            resMaxDamage = stat.MaxDamage;
            resAttackSpeed = stat.AttackSpeed;
        }
        // 장착된 무기가 있다면 캐릭터의 베이스 스탯 + 현재 장착된 장비의 능력치로 설정
        else
        {
            resMinDamage = stat.MinDamage + equipmentSystem.equipWeapon.minDamage;
            resMaxDamage = stat.MaxDamage + equipmentSystem.equipWeapon.maxDamage;
            resAttackSpeed = stat.AttackSpeed + equipmentSystem.equipWeapon.attackSpeed;
        }
        resDefense = stat.Defense + equipedDefense;
        resDodgeChance = stat.DodgeChance + equipedDodgeChance;
    }

    public override void Move(float deltaTime)
    {
        transform.position += stat.Speed * deltaTime * moveDir;
    }

    public override void Attack(float deltaTime)
    {
    }

    public override void Die()
    {
    }
}
