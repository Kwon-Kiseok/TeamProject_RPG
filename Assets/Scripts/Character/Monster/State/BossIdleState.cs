using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.State
{
    public class BossIdleState : IState
    {
        private readonly BossBase boss;
        public BossIdleState(BossBase boss)
        {
            this.boss = boss;
        }

        public void StateEnter()
        {
            boss.bossAgent.isStopped = true;
        }

        public void StateExit()
        {
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if (boss.IsLooking && boss.targetDistance <= boss.targetRadius
                && boss.targetDistance >= boss.bossAgent.stoppingDistance)
            {
                boss.stateMachine.SetState(boss.dicState[EnemyState.Chase]);
            }
        }
    }
}