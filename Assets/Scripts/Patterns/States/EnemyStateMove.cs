using HOGUS.Scripts.Character;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Enums;
namespace HOGUS.Scripts.State
{
    public class EnemyStateMove : IState
    {
        private HandMonster enemy;
        public EnemyStateMove(HandMonster e)
        {
            enemy = e;
        }
        public void StateEnter()
        {

        }
        public void StateUpdate()
        {
            // 이동량이 없고 공격중이 아닐 때
            
            // 이동량이 
        }
        public void StateExit()
        {

        }

        public void StateFixedUpdate()
        {

        }
    }
}