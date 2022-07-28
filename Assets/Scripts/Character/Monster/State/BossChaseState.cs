using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.State
{
    public class BossChaseState : IState
    {
        private readonly BossBase boss;
        public BossChaseState(BossBase boss)
        {
            this.boss = boss;
        }

        public void StateEnter()
        {

        }

        public void StateExit()
        {

        }

        public void StateFixedUpdate()
        {
            boss.FreezeVelocity();
        }

        public void StateUpdate()
        {
            if (boss.targetDistance <= boss.bossAgent.stoppingDistance)
            {
                // Attack target
                boss.stateMachine.SetState(boss.dicState[EnemyState.Attack]);
            }
            else if (boss.targetDistance >= boss.targetRadius)
            {
                boss.stateMachine.SetState(boss.dicState[EnemyState.Idle]);
            }
        }
    }
}