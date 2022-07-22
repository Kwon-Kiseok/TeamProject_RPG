using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Data;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.Character
{
    /// <summary>
    /// ĳ������ ���̽� Ŭ����
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public abstract class Character : MonoBehaviour, IUpdatableObject
    {
        public string Name;         // ĳ������ �̸�
        public Vector3 moveDir;     // ĳ������ �̵� ����
        private bool immune;        // ĳ������ ���� ����
        private bool isDead;        // ĳ������ ��� ����

        public bool Immune { get { return immune; } set { immune = value; } }
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        // �⺻ �پ� ���� ������Ʈ
        #region Base Component
        [HideInInspector]
        public Animator animator;  // ĳ������ �ִϸ�����
        protected Rigidbody rigid;    // ĳ������ rigidbody
        #endregion

        public StateMachine stateMachine;  // ���� �ӽ�
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
        public abstract void Attack();  // ���� �Լ�
        public abstract void Damaged(int damage);        // �ǰ� �Լ�
        public abstract void Die();                    // ��� �Լ�

        // �����ð� �ڷ�ƾ
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
