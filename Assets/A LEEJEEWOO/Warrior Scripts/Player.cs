using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.DP;
using HOGUS.Scripts.Data;
using HOGUS.Scripts.Enums;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.CustomSystem;
using HOGUS.Scripts.Interface;

namespace HOGUS.Scripts.Character
{
    public class Player : Character
    {
        public Joystick joystick;
        public PlayerStat baseStat;        // ���� ���̽� ����
        PlayerStat currentStat;     // ���� ���¸� ��Ÿ���� ���� ����

        public WeaponItem weaponPrefab;
        private EquipmentSystem equipmentSystem;
        private CombatSystem combatSystem;
        public Transform weaponEquipPos;

        public readonly Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

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
            var state_Damaged = new DamagedState(this);

            dicState.Add(PlayerState.Idle, state_idle);
            dicState.Add(PlayerState.Move, state_move);
            dicState.Add(PlayerState.Attack, state_attack);
            dicState.Add(PlayerState.Damaged, state_Damaged);

            stateMachine = new StateMachine(state_idle);
            // base Player Stat deep copy
            currentStat = Instantiate(baseStat);
        }

        public override void OnUpdate(float deltaTime)
        {
            joystick.GetMove();
            Turn();
            stateMachine.DoStateUpdate();
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            Move(deltaTime);
        }

        public PlayerStat GetCurrentStatus()
        {
            return currentStat;
        }

        readonly float nextLevelNeedEXP = 100f;
        public void LevelUp()
        {
            Debug.Log("Player Level UP" + currentStat.Level);
            currentStat.Level++;
            currentStat.CurrentEXP = 0;
            currentStat.EXP += nextLevelNeedEXP;
        }

        void Turn()
        {
            moveDir = new Vector3(joystick.HorizontalAxis, 0, joystick.VerticalAxis).normalized;

            if (joystick.HorizontalAxis != 0 && joystick.VerticalAxis != 0)
            {
                Quaternion lookAt = Quaternion.LookRotation(moveDir);
                transform.rotation = lookAt;
            }
        }

        public void Dodge()
        {
            StopCoroutine(coImmune(0));
            StartCoroutine(coImmune(1));

            if (dodgeSkill.dash && moveDir != Vector3.zero)
            {
                currentStat.Speed *= 2f;
                animator.SetTrigger("doDodge");
                Invoke(nameof(DodgeOut), 0.4f);
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

        public void Move(float deltaTime)
        {
            if (stateMachine.CurrentState != dicState[PlayerState.Attack])
            {
                transform.position += currentStat.Speed * deltaTime * moveDir;
            }
        }

        public override void Attack()
        {
            if (equipmentSystem.equipWeapon == null)
            {
                return;
            }

            if (equipmentSystem.equipWeapon.attackType == AttackType.MELEE)
            {
                // ó�� �ٸ� ���¿��� �������� ���� ���
                if (stateMachine.CurrentState != dicState[PlayerState.Attack])
                {
                    stateMachine.SetState(dicState[PlayerState.Attack]);
                    return;
                }
                else if (stateMachine.CurrentState == dicState[PlayerState.Attack])
                {
                    animator.SetTrigger("doWeaponAttack");
                }
            }
        }
        
        readonly float PlayerHitImmuneTime = 2f;
        public override void Damaged(int damage)
        {
            // �������� �̰ų� �̹� �׾��ٸ� �������� ���� ����
            if (Immune || IsDead)
            {
                return;
            }
            // ���� ȸ������ ���� Ȯ�������� ���� �� ����
            else if(Random.Range(1, 100) <= currentStat.DodgeChance)
            {
                Debug.Log("Guard");
                return;
            }

            stateMachine.SetState(dicState[PlayerState.Damaged]);
            StopCoroutine(nameof(coImmune));
            StartCoroutine(coImmune(PlayerHitImmuneTime));

            currentStat.CurHP -= damage;
            if (currentStat.CurHP < 0)
            {
                currentStat.CurHP = 0;
                Die();
            }
            Debug.Log("CurrentHP " + currentStat.CurHP);
        }

        public override void Die()
        {
            IsDead = true;

            // ���� ���� ���� ��...
            // DeadCheck�ʹ� ���еǾ� �ϱ� �ҵ�

            Debug.Log("Player Dead");
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
            // ���� ������ ���Ⱑ ���ٸ� ĳ������ �⺻ ���̽� �������� ��������
            if (equipmentSystem.equipWeapon == null)
            {
                currentStat.MinDamage = baseStat.MinDamage;
                currentStat.MaxDamage = baseStat.MaxDamage;
                currentStat.AttackSpeed = baseStat.AttackSpeed;
            }
            // ������ ���Ⱑ �ִٸ� ĳ������ ���̽� ���� + ���� ������ ����� �ɷ�ġ�� ����
            else
            {
                currentStat.MinDamage = baseStat.MinDamage + equipmentSystem.equipWeapon.minDamage;
                currentStat.MaxDamage = baseStat.MaxDamage + equipmentSystem.equipWeapon.maxDamage;
                currentStat.AttackSpeed = baseStat.AttackSpeed + equipmentSystem.equipWeapon.attackSpeed;
            }
        }
    }
}