using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Data;
using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;

namespace HOGUS.Scripts.Character
{
    public class MonsterBase : Character
    {
        private bool isLooking = false;

        public UnityEngine.Events.UnityEvent onDead;

        public bool IsLooking { get { return isLooking; } set { isLooking = value; } }

        public float targetRadius = 0f;
        public float targetRange = 0f;
        public float targetDistance = 0f;

        public readonly Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

        public EnemyType enemyType;
        public Stat stat;
        public Transform targetTr;
        public NavMeshAgent monsterAgent;
        public MeshRenderer[] meshes;
        public GameObject weaponGO;
        public GameObject fireballGO;

        private void Start()
        {
            var state_Idle = new MonsterIdleState(this);
            var state_Chase = new MonsterChaseState(this);
            var state_Attack = new MonsterAttackState(this);

            dicState.Add(EnemyState.Idle, state_Idle);
            dicState.Add(EnemyState.Chase, state_Chase);
            dicState.Add(EnemyState.Attack, state_Attack);

            stateMachine = new StateMachine(state_Idle);

            targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
            monsterAgent = GetComponent<NavMeshAgent>();
            monsterAgent.speed = stat.Speed;
            meshes = GetComponentsInChildren<MeshRenderer>();
        }

        public override void Attack(Stat targetStat)
        {
        }

        public override void Die()
        {
            IsDead = true;

            onDead.Invoke();
        }

        public override void Damaged()
        {
            StartCoroutine(OnDamage());
            
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            Targeting(deltaTime);

            stateMachine.DoStateFixedUpdate();
        }

        public override void OnUpdate(float deltaTime)
        {
            stateMachine.DoStateUpdate();
        }

        public void LookTarget(float deltaTime)
        {
            IsLooking = true;
            Vector3 direction = (targetTr.position - transform.position).normalized;
            Quaternion lookAt = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, deltaTime * 5f);
        }

        // NavMeshAgent의 이동을 rigid의 물리력이 방해하는 것을 멈추기 위한 함수
        public void FreezeVelocity()
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }

        // 타겟과의 거리 계산
        private void Targeting(float deltaTime)
        {
            targetDistance = Vector3.Distance(transform.position, targetTr.position);

            if (targetDistance <= targetRadius)
            {
                LookTarget(deltaTime);
            }
            else
                IsLooking = false;
        }

        IEnumerator OnDamage()
        {
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.material.color = Color.red;
            }
            yield return new WaitForSeconds(0.1f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetRadius);
        }
    }
#endif
}