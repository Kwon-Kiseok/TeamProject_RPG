using UnityEngine;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.State
{
    public class IdleState : IState
    {
        private readonly Player player;
        public IdleState(Player player)
        {
            this.player = player;
        }

        public void StateEnter()
        {
        }

        public void StateExit()
        {
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if((player.moveDir != Vector3.zero) && !player.IsSkill)
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Move]);
            }
        }
    }
}