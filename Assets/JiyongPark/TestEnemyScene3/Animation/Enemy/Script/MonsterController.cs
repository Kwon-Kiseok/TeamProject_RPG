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
        public Rigidbody rigid;
        public BoxCollider boxCollider;
        public MeshRenderer[] meshes;
        public bool isChase;
        public bool isAttack;
        public BoxCollider attackArea;
        public GameObject weapon;
        public GameObject fireball;
        public EnemyType enemyType;

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
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            meshes = GetComponentsInChildren<MeshRenderer>();
            if (enemyType != EnemyType.WarChief)
            {
                Invoke("ChaseStart", 2);
            }
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
            if (enemyType == EnemyType.MagicMonster)
            {
                isChase = true;
                animator.SetBool("IsWalking", true);
            }
            else
            {
                isChase = true;
                animator.SetBool("IsRunning", true);
            }
        }

        public void ChasePlayer()
        { 
            if (monster.enabled && enemyType != EnemyType.WarChief)
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
            float targetRadius = 0;
            float targetRange = 0;

            switch (enemyType)
            {
                case EnemyType.PunchMonster:
                    targetRadius = 1.2f;
                    targetRange = 1.5f;
                    break;
                case EnemyType.SwordMonster:
                    targetRadius = 1f;
                    targetRange = 2f;
                    break;
                case EnemyType.MagicMonster:
                    targetRadius = 0.3f;
                    targetRange = 25f;
                    break;
                case EnemyType.WarChief:
                    targetRadius = 2f;
                    targetRange = 100f;
                    break;
                default:
                    break;

            }

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

            switch (enemyType)
            {
                case EnemyType.PunchMonster:
                    yield return new WaitForSeconds(0.3f);
                    attackArea.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    attackArea.enabled = false;
                    yield return new WaitForSeconds(0.5f);
                    break;
                case EnemyType.SwordMonster:
                    yield return new WaitForSeconds(0.3f);
                    attackArea.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    attackArea.enabled = false;
                    yield return new WaitForSeconds(0.5f);
                    break;
                case EnemyType.MagicMonster:
                    yield return new WaitForSeconds(0.3f);
                    GameObject InstantSpell = Instantiate(fireball, weapon.transform.position, weapon.transform.rotation);
                    Rigidbody rigidSpell = InstantSpell.GetComponent<Rigidbody>();
                    rigidSpell.velocity = transform.forward * 40;
                    yield return new WaitForSeconds(2f);
                    break;
                case EnemyType.WarChief:
                    break;
                default:
                    break;

            }

            isChase = true;
            isAttack = false;
            animator.SetBool("IsAttack", false);
        }


        void OnTriggerEnter(Collider other)
        {
            
        }

        public void HitByPlayer(Vector3 explosionPos)
        {
            Vector3 reactVector = transform.position - explosionPos;
            StartCoroutine(OnDamage(reactVector, true));
        }

        IEnumerator OnDamage(Vector3 reactVector, bool isPlayerAttack)
        {
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.material.color = Color.red;
            }
            yield return new WaitForSeconds(0.1f);

            //현재 체력이 0보다 큰 경우에는 고대로

            // 그 외에는 죽는거 표현.
        }
    }
}