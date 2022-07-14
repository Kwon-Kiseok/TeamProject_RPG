using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;
namespace HOGUS.Scripts.State
{
    public class PlayerStateMove : IState
    {
        private TestPlayer player;

        public PlayerStateMove(TestPlayer p)
        {
            player = p;
        }

        public void StateEnter()
        {

        }

        public void StateUpdate()
        {
            if (player.vAxis == 0 && player.hAxis == 0)
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Idle]);
            }
        }
        public void StateExit()
        {

        }

        public void StateFixedUpdate()
        {

        }
    }
}