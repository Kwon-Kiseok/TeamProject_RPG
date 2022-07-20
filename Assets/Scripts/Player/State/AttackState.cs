using UnityEngine;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.State
{
    public class AttackState : IState
    {
        private readonly Player player;
        public AttackState(Player player)
        {
            this.player = player;
        }

        public void StateEnter()
        {
            Debug.Log("플레이어 공격 시전");
            player.animator.SetTrigger("doWeaponAttack");
        }

        public void StateExit()
        {
            Debug.Log("플레이어 공격 종료");
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if(player.animator.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Sword-Combo-Attack"))
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Idle]);
            }
        }
    }
}