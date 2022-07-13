using HOGUS.Scripts.Interface;

namespace HOGUS.Scripts.DP
{
    public class StateDie : IState
    {
        public void StateEnter()
        {
            Debug.Log("죽음");
        }

        public void StateUpdate()
        {
            Debug.Log("진행중임");
        }
        public void StateExit()
        {
            Debug.Log("사라짐");
        }
    }
}