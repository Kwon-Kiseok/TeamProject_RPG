using UnityEngine;

using HOGUS.Scripts.Interface;

namespace HOGUS.Scripts.DP
{
    public class StateIdle : IState
    {
        private Player player;
        private Animator animator;
        
        public void StateEnter()
        { 
            animator = player.gameObject.GetComponent<Animator>();
        }
        public void StateUpdate()
        {
            //if (Input.GetAxisRaw("Horizontal") != 0)
            //{
            //    animator.SetBool("isWalking", true);
            //}
            //else
            //{
            //    animator.SetBool("isWalking", false);
            //}
        }
        public void StateExit()
        {
            Debug.Log("서있는거 끝남");
        }

    }
}