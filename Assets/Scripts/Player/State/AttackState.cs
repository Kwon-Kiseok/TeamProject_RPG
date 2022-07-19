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
            if (player.IsSkill)
                return;

            player.stateMachine.SetState(player.dicState[PlayerState.Idle]);
        }
    }
}