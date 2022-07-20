using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class TestPlayer : MonoBehaviour, IUpdatableObject
    {
        public Animator animator;
        public float runSpeed;
        public float hAxis;
        public float vAxis;
        public Vector3 moveVec;

        public StateMachine stateMachine;

        //상태 보관
        public readonly Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

        private void Start()
        {
            // 상태 생성
            var idle = new PlayerStateIdle(this);
            var move = new PlayerStateMove(this);
            // 딕셔너리에 보관
            dicState.Add(PlayerState.Idle, idle);
            dicState.Add(PlayerState.Move, move);

            stateMachine = new StateMachine(idle);
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
            transform.position += moveVec * runSpeed * Time.deltaTime;
            animator.SetBool("isMove", moveVec != Vector3.zero);

            transform.LookAt(transform.position + moveVec);
        }

        public void OnUpdate(float deltaTime)
        {
            if (stateMachine.CurrentState == dicState[PlayerState.Idle])
            {
                if (hAxis != 0 || vAxis != 0)
                {
                    stateMachine.SetState(dicState[PlayerState.Move]);
                }
            }
            else if (stateMachine.CurrentState == dicState[PlayerState.Move])
            {
                if (hAxis == 0 && vAxis == 0)
                {
                    stateMachine.SetState(dicState[PlayerState.Idle]);
                }
            }
            Movement();
            stateMachine.DoStateUpdate();
        }

        public void Movement()
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        }

        //public void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "Enemy")
        //    {
        //        stateMachine.SetState(dicState[PlayerState.Die]);
        //    }
        //}
    }
}