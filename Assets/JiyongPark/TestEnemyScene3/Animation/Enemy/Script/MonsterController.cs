using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace HOGUS.Scripts.Character
{
    public class MonsterController : MonoBehaviour, IUpdatableObject
    {
        public Animator animator;
        public Transform player;
        public NavMeshAgent monster;
        public bool isLooking;
        private Rigidbody rigid;
        private BoxCollider boxCollider;
        public bool isChase;
        public bool isAttack;
        public BoxCollider attackArea;
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

        //void Awake()
        //{
        //    animator = GetComponent<Animator>();
        //    monster = GetComponent<NavMeshAgent>();
        //    if (monster != null)
        //    {
        //        monsterSpeed = monster.speed;
        //    }
        //    player = GameObject.FindWithTag("Player").transform;
        //}


        private void Awake()
        {
            monster = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();

            Invoke("ChaseStart", 2);
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
            LookTarget();
            Targeting();
            FreezeVelocity();
        }

        public void OnUpdate(float deltaTime)
        {
            ChasePlayer();
        }

        public void LookTarget()
        {
            isLooking = true;
            Quaternion lookAt = Quaternion.identity;
            Vector3 lookAtVec = (player.transform.position - transform.position).normalized;
            lookAt.SetLookRotation(lookAtVec);
            transform.rotation = lookAt;
        }
        private void ChaseStart()
        {
            isChase = true;
            animator.SetBool("IsRunning", true);
        }

        public void ChasePlayer()
        { 
            if (monster.enabled)
            {
                monster.SetDestination(player.position);
                monster.isStopped = !isChase;
            }
        }

        private void FreezeVelocity()
        {
            if (isChase)
            {
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
            }
        }

        private void Targeting()
        {
            float targetRadius = 1.5f;
            float targetRange = 2f;

            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
        IEnumerator Attack()
        {
            isChase = false;
            isAttack = true;
            animator.SetBool("IsAttack", true);

            yield return new WaitForSeconds(0.3f);
            attackArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            attackArea.enabled = false;

            isChase = true;
            isAttack = false;
            animator.SetBool("IsAttack", false);
        }
    }
}