using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;
namespace HOGUS.Scripts.State
{
    public class EnemyStateIdle : IState
    {
        private HandMonster enemy;
        public EnemyStateIdle(HandMonster e)
        {
            enemy = e;
        }
        public void StateEnter()
        {

        }
        public void StateUpdate()
        {
            
        }
        public void StateExit()
        {

        }

        public void StateFixedUpdate()
        {

        }
    }
}