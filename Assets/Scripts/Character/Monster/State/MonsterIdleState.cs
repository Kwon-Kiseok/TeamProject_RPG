using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.State
{
    public class MonsterIdleState : IState
    {
        private readonly MonsterBase monster;
        public MonsterIdleState(MonsterBase monster)
        {
            this.monster = monster;
        }

        public void StateEnter()
        {
            monster.monsterAgent.isStopped = true;
        }

        public void StateExit()
        {
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if (monster.IsLooking && monster.targetDistance <= monster.targetRadius
                && monster.targetDistance >= monster.monsterAgent.stoppingDistance)
            {
                monster.stateMachine.SetState(monster.dicState[EnemyState.Chase]);
            }
        }
    }
}