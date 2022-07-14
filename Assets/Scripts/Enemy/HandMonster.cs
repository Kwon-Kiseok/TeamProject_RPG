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
    public class HandMonster : MonoBehaviour, IUpdatableObject
    {
        public Animator animator;
        public float monsterSpeed;
        public Vector3 moveVec;
        public StateMachine stateMachine;
        public GameObject player;

        public float attackInterval = 0.5f;
        public int attackDamage = 10;

        private bool playerInRange;
        private float accumTime = 0f;

        NavMeshAgent monster;
        BoxCollider boxCollider;

        //상태 보관
        public Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();
        private void Start()
        {
            var find = GameObject.FindWithTag("Player");
            monster = GetComponent<NavMeshAgent>();
            boxCollider = GetComponent<BoxCollider>();
            // 상태 생성
            var idle = new EnemyStateIdle(this);
            var move = new EnemyStateMove(this);
            var attack = new EnemyStateBaseAttack(this);
            // 딕셔너리에 보관
            dicState.Add(EnemyState.Idle, idle);
            dicState.Add(EnemyState.Move, move);
            dicState.Add(EnemyState.Attack, attack);

            stateMachine = new StateMachine(idle);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                accumTime = attackInterval;
                playerInRange = true;
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

        public void OnFixedUpdate()
        {
            animator.SetBool("isRunning", monster.velocity.sqrMagnitude > 0f);
            if (player != null)
            {
                monster.destination = player.transform.position;
            }
            transform.LookAt(player.transform.position);
        }

        public void OnUpdate()
        {
            stateMachine.DoStateUpdate();
        }

        public void OnUpdate(float deltaTime)
        {

        }
    }
}