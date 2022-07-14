using HOGUS.Scripts.Interface;
using UnityEngine;

namespace HOGUS.Scripts.DP
{
    [HelpURL("https://glikmakesworld.tistory.com/10?category=797136")]
    public class StateMachine
    {
        // 현재 상태 프로퍼티
        public IState CurrentState { get; private set; }

        // 기본 상태를 생성자를 통해 생성과 동시에 설정
        public StateMachine(IState defaultState)
        {
            CurrentState = defaultState;
        }

        // 상태 설정
        public void SetState(IState state)
        {
            // 상태가 중복으로 들어가지 않음
            if (CurrentState == state)
            {
                return;
            }

            // 현재 상태 우선 해제
            CurrentState.StateExit();
            // 현재 상태 변경
            CurrentState = state;
            // 상태 진입
            CurrentState.StateEnter();
        }

        // 매 프레임 실행해야 할 상태 기능 수행
        public void DoStateUpdate()
        {
            CurrentState.StateUpdate();
        }

        public void DoStateFixedUpdate()
        {
            CurrentState.StateFixedUpdate();
        }
    }
}
