using UnityEngine;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.State
{
    public class DamagedState : IState
    {
        private readonly Player player;

        public DamagedState(Player player)
        {
            this.player = player;
        }

        public void StateEnter()
        {
            Debug.Log("피격 상태");
        }

        public void StateExit()
        {
            Debug.Log("피격 상태 종료");
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if(player.moveDir != Vector3.zero)
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Move]);
            }
            // 피격 상태는 코루틴이 종료됨과 동시에 종료 되어야 함
            if(!player.Immune)
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Idle]);
            }
        }
    }
}