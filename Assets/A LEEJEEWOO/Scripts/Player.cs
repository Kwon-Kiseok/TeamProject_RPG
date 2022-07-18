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

public class Player : Character
{
    public Joystick joy;
    PlayerStat stat;
    PlayerStat baseStat;

    public WeaponItem weaponPrefab;
    private EquipmentSystem equipmentSystem;
    public Transform weaponEquipPos;
    
    float hAxis;
    float vAxis;

    bool isDodge;
    bool isSkill;

    public GameObject skillPosition;
    public GameObject IceFactory;
    public float throwPower = 15f;

    //public GameObject comboPTC;
    //public GameObject hillPTC;

    public SkillBtn magicSkill;
    public SkillBtn dodgeSkill;
    public SkillBtn combatSkill;
    public SkillBtn buffSkill;

    private void Awake()
    {
        isSkill = false;
        //comboPTC.gameObject.SetActive(false);
        //hillPTC.gameObject.SetActive(false);
        stat = GetComponent<PlayerStat>();
        baseStat = new(stat);

        equipmentSystem = GetComponent<EquipmentSystem>();
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInput();
        Move(deltaTime);
        Turn();        
    }

    public override void OnFixedUpdate(float deltaTime)
    {
        transform.position += stat.Speed * deltaTime * moveDir;
    }

    public PlayerStat GetStat()
    {
        return stat;
    }

    public void LevelUp()
    {
        Debug.Log("Player Level UP" + stat.Level);
        stat.Level += 1;
        stat.CurrentEXP = 0;
        stat.EXP += 100;
    }

    void GetInput()
    {
        if (!isSkill)
        {
            hAxis = joy.GetAxisRaw("Horizontal");
            vAxis = joy.GetAxisRaw("Vertical");
        }
        else
        {
            moveDir = new Vector3(0, 0, 0);
        }
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveDir);
    }

    public void Dodge()
    {    
        if (dodgeSkill.dash && moveDir != Vector3.zero)
        {
            stat.Speed *= 2f;
            animator.SetTrigger("doDodge");
            Invoke("DodgeOut", 0.4f);
        }
    }

    void DodgeOut()
    {
        stat.Speed *= 0.5f;
    }

    public void ComboAttack()
    {
        isSkill = true;
        //comboPTC.gameObject.SetActive(true);
        
        if (combatSkill.combo && isSkill)
        {
            animator.SetTrigger("doAttack");
            //comboPTC.gameObject.SetActive(true);
        }
    }

    public void IceBall()
    {
        isSkill = true;
        
        if (magicSkill.cool && isSkill)
        {
            animator.SetTrigger("doMasic");

            GameObject iceSkill = Instantiate(IceFactory);
            iceSkill.transform.position = skillPosition.transform.position;
            Rigidbody rb = iceSkill.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        }      
    }

    public void SkilHeal()
    {
        isSkill = true;
        //hillPTC.gameObject.SetActive(true);
        
        if (buffSkill.heal && isSkill)
        {            
            animator.SetTrigger("doHeal");
            //hillPTC.gameObject.SetActive(true);
        }
    }

    public void EndSkillAnim()
    {
        isSkill = false;
        //comboPTC.gameObject.SetActive(false);
        //hillPTC.gameObject.SetActive(false);
    }

    public override void Move(float deltaTime)
    {
        if (!isSkill)
        {
            if (hAxis != 0 || vAxis != 0)
            {
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }

            moveDir = new Vector3(hAxis, 0, vAxis).normalized;
        }
    }

    public override void Attack(float deltaTime)
    {
    }

    public override void Die()
    {
    }

    public void TestEquip()
    {
        if (equipmentSystem.equipWeapon == null)
        {
            var weapon = ScriptableObject.CreateInstance<WeaponItem>();
            weapon.CopyValue(weaponPrefab);
            equipmentSystem.DoEquip(EquipPart.WEAPON, weapon);

            UpdateStat();
        }
        else
        {
            equipmentSystem.DoUnequip(EquipPart.WEAPON);

            UpdateStat();
        }
    }

    private void UpdateStat()
    {
        // 현재 장착한 무기가 없다면 캐릭터의 기본 베이스 스탯으로 설정해줌
        if (equipmentSystem.equipWeapon == null)
        {
            stat.MinDamage = baseStat.MinDamage;
            stat.MaxDamage = baseStat.MaxDamage;
            stat.AttackSpeed = baseStat.AttackSpeed;
        }
        // 장착된 무기가 있다면 캐릭터의 베이스 스탯 + 현재 장착된 장비의 능력치로 설정
        else
        {
            stat.MinDamage = baseStat.MinDamage + equipmentSystem.equipWeapon.minDamage;
            stat.MaxDamage = baseStat.MaxDamage + equipmentSystem.equipWeapon.maxDamage;
            stat.AttackSpeed = baseStat.AttackSpeed + equipmentSystem.equipWeapon.attackSpeed;
        }
    }
}
