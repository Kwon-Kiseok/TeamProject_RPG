using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;
namespace HOGUS.Scripts.State
{
    public class PlayerStateIdle : IState
    {
        private TestPlayer player;
        public PlayerStateIdle(TestPlayer p)
        {
            player = p;
        }
        public void StateEnter()
        {

        }
        public void StateUpdate()
        {
            if (player.vAxis != 0 || player.hAxis != 0)
            {
                player.stateMachine.SetState(player.dicState[PlayerState.Move]);
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