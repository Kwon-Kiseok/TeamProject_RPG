//using HOGUS.Scripts.Character;
//using HOGUS.Scripts.Interface;
//using HOGUS.Scripts.Enums;
//using UnityEngine;
//namespace HOGUS.Scripts.State
//{
//    public class EnemyStateIdle : IState
//    {
//        float timer;
//        private MonsterController enemy;
//        private float chaseRange = 8;
//        public EnemyStateIdle(MonsterController e)
//        {
//            enemy = e;
//        }
//        public void StateEnter()
//        {
//            timer = 0;
//        }
//        public void StateUpdate()
//        {
//            timer += Time.deltaTime;

//            float distance = Vector3.Distance(enemy.target.transform.position, enemy.transform.position);
//            if (distance < chaseRange)
//            {
//                enemy.animator.SetBool("IsChasing", true); 
//            }
//        }
//        public void StateExit()
//        {

//        }

//        public void StateFixedUpdate()
//        {

//        }
//    }
//}