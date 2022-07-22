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
            player.animator.SetTrigger("doWeaponAttack");
        }

        public void StateExit()
        {
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            // ¸¾¿¡ ¾Èµê
            if(player.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Sword-Combo-Attack.Attack_02")
                || player.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Sword-Combo-Attack.Attack_03")
                || player.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Sword-Combo-Attack.Attack_05")
                || player.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Sword-Combo-Attack.Attack_17"))
            {
                return;
            }
            else
                player.stateMachine.SetState(player.dicState[PlayerState.Idle]);
        }
    }
}