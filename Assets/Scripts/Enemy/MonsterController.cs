using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HOGUS.Scripts.Character
{
    public class MonsterController : MonoBehaviour, IUpdatableObject
    {
        public Status CurrentStats
        {
            get
            {
                return currentStatus;
            }
            set
            {
                currentStatus = value;
                switch (currentStatus)
                {
                    case Status.Idle:
                        timer = idleWaitTime;
                        monster.isStopped = true;
                        break;

                    case Status.Trace:
                        monster.destination = target.transform.position;
                        monster.speed = monsterSpeed;
                        monster.isStopped = false;
                        break;

                    case Status.Attack:
                        timer = 0f;
                        monster.isStopped = true;
                        break;
                    case Status.GameOver:
                        monster.isStopped = true;
                        break;
                    default:
                        break;
                }
            }
        }
        public Animator animator;
        private Status currentStatus;
        //public StateMachine stateMachine;
        public GameObject target;
        public float idleWaitTime = 5f;
        private float timer;
        private Transform player;
        private float monsterSpeed;
        public float targetDistance;
        public int attackDamage = 10;
        public float aggroRange;
        public NavMeshAgent monster;
        public AttackDefinition weapon;
        public int attackPower = 3;
        public int hp = 20;
        public bool isLooking;
        public bool isChasing;
        //private Rigidbody rigid;

        //상태 보관
        //public Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();
        //private void Start()
        //{
        //    rigid = GetComponent<Rigidbody>();
        //    player = GameObject.FindWithTag("Player").transform;
        //    // 상태 생성
        //    var idle = new EnemyStateIdle(this);
        //    var move = new EnemyStateMove(this);
        //    var attack = new EnemyStateBaseAttack(this);
        //    // 딕셔너리에 보관
        //    dicState.Add(EnemyState.Idle, idle);
        //    dicState.Add(EnemyState.Move, move);
        //    dicState.Add(EnemyState.Attack, attack);

        //    stateMachine = new StateMachine(idle);
        //}

        void Awake()
        {
            animator = GetComponent<Animator>();
            monster = GetComponent<NavMeshAgent>();
            if (monster != null)
            {
                monsterSpeed = monster.speed;
            }
            player = GameObject.FindWithTag("Player").transform;
        }


        private void Start()
        {
            CurrentStats = Status.Idle;
        }
        public void OnEnable()
        {
            UpdateManager.Instance.RegisterUpdatableObject(this);
        }

        public void OnDisable()
        {
            if (UpdateManager.Instance != null)
            {
                UpdateManager.Instance.DeregisterUpdatableObject(this);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {

        }

        public void OnUpdate(float deltaTime)
        {
            ChasePlayer();
            switch (currentStatus)
            {
                case Status.Idle:
                    UpdateIdle();
                    break;

                case Status.Trace:
                    UpdateTrace();
                    break;
                case Status.Attack:
                    UpdateAttack();
                    break;
            }
        }

        public void LookTarget()
        {
            isLooking = true;
            Quaternion lookAt = Quaternion.identity;
            Vector3 lookAtVec = (target.transform.position - transform.position).normalized;
            lookAt.SetLookRotation(lookAtVec);
            transform.rotation = lookAt;
        }

        public void ChasePlayer()
        {
            animator.SetFloat("Speed", monster.velocity.magnitude);

            if (target != null)
            {
                targetDistance = Vector3.Distance(transform.position, target.transform.position);
            }
            switch (currentStatus)
            {
                case Status.Idle:
                    UpdateIdle();
                    break;
                case Status.Trace:
                    UpdateTrace();
                    break;
                case Status.Attack:
                    UpdateAttack();
                    break;
            }
        }
        private void UpdateAttack()
        {
            if (targetDistance > weapon.range)
            {
                CurrentStats = Status.Trace;
                return;
            }

            //쿨다운 주기에 맞춰서 공격 애니메이션 재생
            var pos = player.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);

            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                timer = weapon.cooldown;
                animator.SetTrigger("Attack");
            }
        }

        private void UpdateIdle()
        {

        }
        private void UpdateTrace()
        {
            if (targetDistance < weapon.range)
            {
                CurrentStats = Status.Attack;
            }
            else if (targetDistance > aggroRange)
            {
                CurrentStats = Status.Idle;
            }
        }
    }
}