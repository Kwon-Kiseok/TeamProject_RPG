using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.DP;
using HOGUS.Scripts.Data;
using HOGUS.Scripts.Enums;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.CustomSystem;
using HOGUS.Scripts.Interface;

public class Player : Character
{
    public Joystick joy;
    public PlayerStat baseStat;        // 기초 베이스 스탯
    PlayerStat currentStat;     // 현재 상태를 나타내는 사용될 스탯

    public WeaponItem weaponPrefab;
    private EquipmentSystem equipmentSystem;
    private CombatSystem combatSystem;
    public Transform weaponEquipPos;
    
    float hAxis;
    float vAxis;

    public float HorizontalAxis { get { return hAxis; } set { hAxis = value; } }
    public float VerticalAxis { get { return vAxis; } set { vAxis = value; } }

    public readonly Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();


    bool isDodge;
    bool isSkill;

    public bool IsSkill { get { return isSkill; } set { isSkill = value; } }

    public GameObject skillPosition;
    public GameObject IceFactory;
    public float throwPower = 15f;

    public SkillBtn magicSkill;
    public SkillBtn dodgeSkill;
    public SkillBtn combatSkill;
    public SkillBtn buffSkill;

    private void Awake()
    {
        isSkill = false;
        equipmentSystem = GetComponent<EquipmentSystem>();
        combatSystem = GetComponent<CombatSystem>();
    }

    private void Start()
    {
        var state_idle = new IdleState(this);
        var state_move = new MoveState(this);
        var state_attack = new AttackState(this);

        dicState.Add(PlayerState.Idle, state_idle);
        dicState.Add(PlayerState.Move, state_move);
        dicState.Add(PlayerState.Attack, state_attack);

        stateMachine = new StateMachine(state_idle);
        // base Player Stat deep copy
        currentStat = Instantiate(baseStat);
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInput();
        Move(deltaTime);
        Turn();
        
        stateMachine.DoStateUpdate();
    }

    public override void OnFixedUpdate(float deltaTime)
    {
        transform.position += currentStat.Speed * deltaTime * moveDir;
    }

    public PlayerStat GetCurrentStatus()
    {
        return currentStat;
    }

    public void LevelUp()
    {
        Debug.Log("Player Level UP" + currentStat.Level);
        currentStat.Level += 1;
        currentStat.CurrentEXP = 0;
        currentStat.EXP += 100;
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
            currentStat.Speed *= 2f;
            animator.SetTrigger("doDodge");
            Invoke("DodgeOut", 0.4f);
        }
    }

    void DodgeOut()
    {
        currentStat.Speed *= 0.5f;
    }

    public void ComboAttack()
    {
        isSkill = true;
        if (combatSkill.combo && isSkill)
        {
            animator.SetTrigger("doAttack");
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
        
        if (buffSkill.heal && isSkill)
        {            
            animator.SetTrigger("doHeal");
        }
    }

    public void EndSkillAnim()
    {
        isSkill = false;
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
        if(equipmentSystem.equipWeapon == null)
        {
            return;
        }

        if (equipmentSystem.equipWeapon.attackType == AttackType.MELEE)
        {
            combatSystem.Attack();
            // 좀 그런데; 연타하면 
            equipmentSystem.combatWeapon.Use();
        }
    }

    public override void Hit(int damage)
    {
        currentStat.CurHP -= damage;
        if (currentStat.CurHP < 0)
        {
            currentStat.CurHP = 0;
        }
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
            currentStat.MinDamage = baseStat.MinDamage;
            currentStat.MaxDamage = baseStat.MaxDamage;
            currentStat.AttackSpeed = baseStat.AttackSpeed;
        }
        // 장착된 무기가 있다면 캐릭터의 베이스 스탯 + 현재 장착된 장비의 능력치로 설정
        else
        {
            currentStat.MinDamage = baseStat.MinDamage + equipmentSystem.equipWeapon.minDamage;
            currentStat.MaxDamage = baseStat.MaxDamage + equipmentSystem.equipWeapon.maxDamage;
            currentStat.AttackSpeed = baseStat.AttackSpeed + equipmentSystem.equipWeapon.attackSpeed;
        }
    }
}
