using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Interface;

namespace HOGUS.Scripts.DP
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float walkSpeed;
        private float hAxis;
        private float vAxis;
        private bool RDown;
        private Vector3 moveVec;
        private enum PlayerState
        {
            Idle,
            Move,
            Attack,
            Damaged,
            Die
        }

        private StateMachine stateMachine;

        //상태 보관
        private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

        private void Start()
        {
            // 상태 생성
            IState idle = new StateIdle();
            IState move = new StateMove();
            IState die = new StateDie();

            // 딕셔너리에 보관
            dicState.Add(PlayerState.Idle, idle);
            dicState.Add(PlayerState.Move, move);
            dicState.Add(PlayerState.Die, die);

            stateMachine = new StateMachine(idle);
        }

        private void Update()
        {
            // 매프레임 실행해야하는 동작 호출
            stateMachine.DoStateUpdate();

            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * walkSpeed * Time.deltaTime;
            
            animator.SetBool("isWalking", moveVec != Vector3.zero);

            transform.LookAt(transform.position + moveVec);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                stateMachine.SetState(dicState[PlayerState.Die]);
            }
        }
    }
}