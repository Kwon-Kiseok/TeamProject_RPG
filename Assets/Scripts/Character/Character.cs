using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Data;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.Character
{
    /// <summary>
    /// 캐릭터의 베이스 클래스
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public abstract class Character : MonoBehaviour, IUpdatableObject
    {
        public string Name;         // 캐릭터의 이름
        public Vector3 moveDir;     // 캐릭터의 이동 방향
        private bool immune;        // 캐릭터의 무적 여부
        private bool isDead;        // 캐릭터의 사망 여부

        public bool Immune { get { return immune; } set { immune = value; } }
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        // 기본 붙어 있을 컴포넌트
        #region Base Component
        [HideInInspector]
        public Animator animator;  // 캐릭터의 애니메이터
        protected Rigidbody rigid;    // 캐릭터의 rigidbody
        #endregion

        public StateMachine stateMachine;  // 상태 머신
        protected CapsuleCollider characterCollider;

        #region IUpdatableObject Interface
        public void OnEnable()
        {
            ComponenetSet();
            UpdateManager.Instance.RegisterUpdatableObject(this);
        }

        public void OnDisable()
        {
            if(UpdateManager.Instance != null)
                UpdateManager.Instance.DeregisterUpdatableObject(this);
        }

        public abstract void OnFixedUpdate(float deltaTime);
        public abstract void OnUpdate(float deltaTime);
        #endregion

        protected void ComponenetSet()
        {
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            characterCollider = GetComponent<CapsuleCollider>();
        }

        #region Base Function
        public abstract void Attack(Stat targetStat);  // 공격 함수
        public abstract void Damaged();        // 피격 함수
        public abstract void Die();                    // 사망 함수

        // 무적시간 코루틴
        protected IEnumerator coImmune(float immuneTime)
        {
            Immune = true;
            float timer = 0f;

            while (timer < immuneTime)
            {
                yield return null;
                timer += Time.deltaTime;
            }

            Immune = false;
        }

        #endregion
    }
}
