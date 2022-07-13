using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Interface;

namespace HOGUS.Scripts.DP
{
    public class Enemy : MonoBehaviour
    {
        private enum EnemyState
        {
            Idle,
            Move,
            Attack,
            Damaged,
            Die
        }

        private StateMachine stateMachine;
        //상태 보관
        private Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

        private void Start()
        {
            // 상태 생성
            IState idle = new StateIdle();
            IState move = new StateMove();
            IState die = new StateDie();

            // 딕셔너리에 보관
            dicState.Add(EnemyState.Idle, idle);
            dicState.Add(EnemyState.Move, move);
            dicState.Add(EnemyState.Die, die);

            stateMachine = new StateMachine(idle);
        }

        private void Update()
        {
            // 매프레임 실행해야하는 동작 호출
            stateMachine.DoStateUpdate();
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                stateMachine.SetState(dicState[EnemyState.Die]);
            }
        }
    }
}