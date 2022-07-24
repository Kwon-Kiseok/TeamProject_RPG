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
        public PlayerStat baseStat;        // 기초 베이스 스탯
        PlayerStat currentStat;     // 현재 상태를 나타내는 사용될 스탯

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
            UpdateStat();
            InitialSetStatus();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (IsDead)
                return;

            Recover(deltaTime);
            UpdateStat();
            stateMachine.DoStateUpdate();
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            if (IsDead)
                return;

            joystick.GetMove();
            Turn();
            Move(deltaTime);
            LevelUp();
        }

        public PlayerStat GetCurrentStatus()
        {
            return currentStat;
        }

        readonly float nextLevelNeedEXP = 100f;
        readonly int levelUpPoint = 5;
        public void LevelUp()
        {
            if (currentStat.CurrentEXP >= currentStat.EXP)
            {
                currentStat.Level++;
                currentStat.StatPoint += levelUpPoint;
                currentStat.CurrentEXP = 0;
                currentStat.EXP += nextLevelNeedEXP;
            }
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

        readonly float dodgeImmuneTime = 1f;
        readonly float dodgeSpeed = 2f;
        readonly float returnToOriginSpeedVal = 0.5f;
        readonly float InvokeDodgeOutTime = 0.4f;

        readonly int DodgeRequireMP = 10;
        public void Dodge()
        {
            if (GetCurrentStatus().CurMP < DodgeRequireMP)
            {
                return;
            }

            StopCoroutine(coImmune(0));
            StartCoroutine(coImmune(dodgeImmuneTime));

            if (dodgeSkill.cool && moveDir != Vector3.zero)
            {
                GetCurrentStatus().CurMP = Mathf.Clamp(GetCurrentStatus().CurMP - DodgeRequireMP, 0, GetCurrentStatus().MaxMP);
                currentStat.Speed *= dodgeSpeed;
                animator.SetTrigger("doDodge");
                Invoke(nameof(DodgeOut), InvokeDodgeOutTime);
            }
        }

        void DodgeOut()
        {
            currentStat.Speed *= returnToOriginSpeedVal;
        }

        readonly int ComboRequireMP = 30;
        public void ComboAttack()
        {
            if (equipmentSystem.equipWeapon == null)
            {
                return;
            }

            if (GetCurrentStatus().CurMP < ComboRequireMP)
            {
                return;
            }

            isSkill = true;
            if (combatSkill.cool && isSkill)
            {
                GetCurrentStatus().CurMP = Mathf.Clamp(GetCurrentStatus().CurMP - ComboRequireMP, 0, GetCurrentStatus().MaxMP);
                animator.SetTrigger("doAttack");
            }
        }

        readonly int IceBallRequireMP = 40;
        public void IceBall()
        {

            if(GetCurrentStatus().CurMP < IceBallRequireMP)
            {
                return;
            }

            isSkill = true;
            if (magicSkill.cool && isSkill)
            {
                animator.SetTrigger("doMagic");

                GameObject iceSkill = Instantiate(IceFactory);
                iceSkill.transform.position = skillPosition.transform.position;
                iceSkill.GetComponent<Iceball>().damage = GetCurrentStatus().MagicDamage;
                GetCurrentStatus().CurMP = Mathf.Clamp(GetCurrentStatus().CurMP - IceBallRequireMP, 0, GetCurrentStatus().MaxMP);
                Rigidbody rb = iceSkill.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
            }
        }

        public void SkilHeal()
        {
            isSkill = true;

            if (buffSkill.cool && isSkill)
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
            if (IsSkill)
                return;

            if (stateMachine.CurrentState != dicState[PlayerState.Attack])
            {
                transform.position += currentStat.Speed * deltaTime * moveDir;
            }
        }

        public override void Attack()
        {
            if (IsSkill)
                return;

            if (equipmentSystem.equipWeapon == null)
            {
                return;
            }

            if (equipmentSystem.equipWeapon.attackType == AttackType.MELEE)
            {
                // 처음 다른 상태에서 어택으로 들어올 경우
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
            // 무적상태거나 죽으면 데미지를 받지 않음
            if (Immune || IsDead)
            {
                return;
            }
            // 현재 회피율에 따라 확률적으로 피할 수 있음
            else if(Random.Range(1, 100) <= currentStat.DodgeChance)
            {
                Debug.Log("Guard");
                return;
            }

            stateMachine.SetState(dicState[PlayerState.Damaged]);
            StopCoroutine(nameof(coImmune));
            StartCoroutine(coImmune(PlayerHitImmuneTime));

            currentStat.TakeDamage(damage);
            if (currentStat.CurHP == 0)
            {
                Die();
                return;
            }
            animator.SetInteger("DamagedIndex", Random.Range(0, 6));
            animator.SetTrigger("Damaged");
        }

        public override void Die()
        {
            IsDead = true;

            // 죽은 다음 수행 될...
            // DeadCheck와는 구분되야 하긴 할듯
            animator.SetInteger("DeadIndex", Random.Range(0, 4));
            animator.SetTrigger("Dead");

            Debug.Log("Player Dead");
        }

        public void TestEquip()
        {
            if (equipmentSystem.equipWeapon == null)
            {
                var weapon = ScriptableObject.CreateInstance<WeaponItem>();
                weapon.CopyValue(weaponPrefab);
                equipmentSystem.DoEquip(EquipPart.WEAPON, weapon);
            }
            else
            {
                equipmentSystem.DoUnequip(EquipPart.WEAPON);
            }
        }

        public void UpdateStat()
        {
            // 현재 장착한 무기가 없다면 캐릭터의 기본 베이스 스탯으로 설정해줌
            if (equipmentSystem.equipWeapon == null)
            {
                currentStat.MinDamage = baseStat.MinDamage + currentStat.Strength / currentStat.DamagePerStrength;
                currentStat.MaxDamage = baseStat.MaxDamage + currentStat.Strength / currentStat.DamagePerStrength;
                currentStat.AttackSpeed = baseStat.AttackSpeed;
            }
            // 장착된 무기가 있다면 캐릭터의 베이스 스탯 + 현재 장착된 장비의 능력치로 설정
            else
            {
                currentStat.MinDamage = baseStat.MinDamage + equipmentSystem.equipWeapon.minDamage + currentStat.Strength / currentStat.DamagePerStrength;
                currentStat.MaxDamage = baseStat.MaxDamage + equipmentSystem.equipWeapon.maxDamage + currentStat.Strength / currentStat.DamagePerStrength;
                currentStat.AttackSpeed = baseStat.AttackSpeed + equipmentSystem.equipWeapon.attackSpeed;
            }

            currentStat.MagicDamage = baseStat.MagicDamage + currentStat.Magic / currentStat.DamagePerMagic;
            currentStat.DodgeChance = baseStat.DodgeChance + currentStat.Dexterity / currentStat.DodgePerDex;
            currentStat.MaxHP = baseStat.MaxHP + currentStat.Vitality / currentStat.HPPerVital;
            currentStat.MaxMP = baseStat.MaxMP + currentStat.Magic / currentStat.DamagePerMagic;
        }

        private void InitialSetStatus()
        {
            currentStat.CurHP = currentStat.MaxHP;
            currentStat.CurMP = currentStat.MaxMP;
        }


        private float time_current = 0f;
        private float time_recover = 3f;
        private readonly int recoverDiff = 1;
        private void Recover(float deltaTime)
        {            
            if (currentStat.CurHP == currentStat.MaxHP && currentStat.CurMP == currentStat.MaxMP)
                return;

            time_current += deltaTime;
            if (time_current >= time_recover)
            {
                currentStat.CurMP = Mathf.Clamp(currentStat.CurMP + recoverDiff, 0, currentStat.MaxMP);
                currentStat.CurHP = Mathf.Clamp(currentStat.CurHP + recoverDiff, 0, currentStat.MaxHP);
                time_current = 0f;
            }
        }
    }
}