//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//using HOGUS.Scripts.Data;
//using HOGUS.Scripts.Enums;
//using HOGUS.Scripts.Character;
//using HOGUS.Scripts.Object.Item;
//using HOGUS.Scripts.CustomSystem;

//public class Tester : Character
//{
//    //
//    public WeaponItem weaponPrefab;
//    public JewelItem jewelPrefab;

//    //
//    public Joystick joystick;
//    private EquipmentSystem equipmentSystem;
//    public Transform weaponEquipPos;

//    //
//    private JewelItem hasJewel;

//    #region base stat
//    // ĳ������ �⺻ ������ ����
//    PlayerStat stat;
//    #endregion

//    #region current equipments stat
//    // ĳ���Ͱ� �����ϰ� �ִ� ������ ������ ������
//    public int equipedDefense = 0;

//    public float equipedDodgeChance = 0f;
//    #endregion

//    #region result stat
//    // ĳ������ �⺻ ������ ����
//    public int resMinDamage = 0;
//    public int resMaxDamage = 0;
//    public int resDefense = 0;
//    public float resDodgeChance = 0f;
//    public float resAttackSpeed = 0f;
//    #endregion

//    //
//    public TextMeshProUGUI testerDataUI;

//    private void Awake()
//    {
//        equipmentSystem = GetComponent<EquipmentSystem>();
//        stat = GetComponent<PlayerStat>();
//    }

//    public override void OnFixedUpdate(float deltaTime)
//    {
//        if (equipmentSystem.equipWeapon != null)
//        {
//            WeaponItem weaponPart = equipmentSystem.equipWeapon;

//            if (weaponPart != null)
//            {
//                testerDataUI.text =
//                    "Name: " + weaponPart.name + "\n" +
//                    string.Format("Damage: {0} ~ {1}\n", weaponPart.minDamage, weaponPart.maxDamage) +
//                    "Desc: " + weaponPart.GetDescription();
//            }
//        }
//    }

//    public override void OnUpdate(float deltaTime)
//    {
//        Move(deltaTime);

//        // ���� ���� �׽�Ʈ
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
//            weapon.CopyValue(weaponPrefab);
//            equipmentSystem.DoEquip(EquipPart.WEAPON, weapon);

//            UpdateStat();
//        }
//        // ���� ���� �׽�Ʈ
//        else if (Input.GetKeyDown(KeyCode.U))
//        {
//            equipmentSystem.DoUnequip(EquipPart.WEAPON);
//            testerDataUI.text = null;

//            UpdateStat();
//        }
//        // ���� ���� �׽�Ʈ 
//        else if (Input.GetKeyDown(KeyCode.R))
//        {
//            if (hasJewel == null)
//            {
//                hasJewel = ScriptableObject.CreateInstance<JewelItem>();
//                hasJewel.CopyValue(jewelPrefab);
//            }
//            hasJewel.Attach(equipmentSystem.equipWeapon);

//            hasJewel = null;

//            UpdateStat();
//        }
//        //else if (Input.GetKeyDown(KeyCode.Space))
//        //{
//        //    stat.PrintDebugStat();
//        //    Debug.Log(resMinDamage + ", " + resMaxDamage);
//        //}
//    }

//    private void UpdateStat()
//    {
//        // ���� ������ ���Ⱑ ���ٸ� ĳ������ �⺻ ���̽� �������� ��������
//        if (equipmentSystem.equipWeapon == null)
//        {
//            resMinDamage = stat.MinDamage;
//            resMaxDamage = stat.MaxDamage;
//            resAttackSpeed = stat.AttackSpeed;
//        }
//        // ������ ���Ⱑ �ִٸ� ĳ������ ���̽� ���� + ���� ������ ����� �ɷ�ġ�� ����
//        else
//        {
//            resMinDamage = stat.MinDamage + equipmentSystem.equipWeapon.minDamage;
//            resMaxDamage = stat.MaxDamage + equipmentSystem.equipWeapon.maxDamage;
//            resAttackSpeed = stat.AttackSpeed + equipmentSystem.equipWeapon.attackSpeed;
//        }
//        resDefense = stat.Defense + equipedDefense;
//        resDodgeChance = stat.DodgeChance + equipedDodgeChance;
//    }

//    public override void Move(float deltaTime)
//    {
//        transform.position += stat.Speed * deltaTime * moveDir;
//    }

//    public override void Attack()
//    {
//    }

//    public override void Die()
//    {
//    }

//    public override void Damaged()
//    {
//    }
//}
