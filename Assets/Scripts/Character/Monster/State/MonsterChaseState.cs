using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.State
{
    public class MonsterChaseState : IState
    {
        private readonly MonsterBase monster;
        public MonsterChaseState(MonsterBase monster)
        {
            this.monster = monster;
        }

        public void StateEnter()
        {
            if(monster.enemyType == EnemyType.MagicMonster)
            {
                monster.animator.SetBool("IsWalking", true);
            }
            else
                monster.animator.SetBool("IsRunning", true);

            monster.monsterAgent.isStopped = false;
        }

        public void StateExit()
        {
            if (monster.enemyType == EnemyType.MagicMonster)
            {
                monster.animator.SetBool("IsWalking", false);
            }
            else
                monster.animator.SetBool("IsRunning", false);
        }

        public void StateFixedUpdate()
        {
            monster.FreezeVelocity();
        }

        public void StateUpdate()
        {
            if (monster.targetDistance <= monster.monsterAgent.stoppingDistance)
            {
                // Attack target
                monster.stateMachine.SetState(monster.dicState[EnemyState.Attack]);
            }
            else if (monster.targetDistance >= monster.targetRadius)
            {
                monster.stateMachine.SetState(monster.dicState[EnemyState.Idle]);
            }
            else
            {
                monster.monsterAgent.SetDestination(monster.targetTr.position);
                
                if (monster.enemyType != EnemyType.MagicMonster)
                {
                    if (!monster.animator.GetBool("IsRunning"))
                    {
                        monster.animator.SetBool("IsRunning", true);
                    }
                }
                else
                {
                    if (!monster.animator.GetBool("IsWalking"))
                    {
                        monster.animator.SetBool("IsWalking", true);
                    }
                }
            }
        }
    }
}