using UnityEngine;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.State
{
    public class MoveState : IState
    {
        private readonly Player player;
        public MoveState(Player player)
        {
            this.player = player;
        }

        public void StateEnter()
        {
            player.animator.SetBool("isMove", true);
        }

        public void StateExit()
        {
            player.animator.SetBool("isMove", false);
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if((player.moveDir == Vector3.zero) && player.stateMachine.CurrentState != player.dicState[PlayerState.Attack])
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Idle]);
                return;
            }
        }
    }
}
