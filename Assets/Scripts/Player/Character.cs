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
    public abstract class Character : MonoBehaviour, IUpdatableObject
    {
        public string Name;         // 캐릭터의 이름
        public Vector3 moveDir;     // 캐릭터의 이동 방향

        // 기본 붙어 있을 컴포넌트
        #region Base Component
        protected Animator animator;  // 캐릭터의 애니메이터
        protected Rigidbody rigid;    // 캐릭터의 rigidbody
        #endregion

        public StateMachine stateMachine;  // 상태 머신

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

        public abstract void OnFixedUpdate();
        public abstract void OnUpdate();
        public abstract void OnUpdate(float deltaTime);
        #endregion

        protected void ComponenetSet()
        {
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
        }

        #region Base Function
        public abstract void Move();    // 이동 함수
        #endregion
    }
}
