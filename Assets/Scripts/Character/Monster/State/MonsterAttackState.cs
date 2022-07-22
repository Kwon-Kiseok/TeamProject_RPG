using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.State
{
    public class MonsterAttackState : IState
    {
        private readonly MonsterBase monster;
        public MonsterAttackState(MonsterBase monster)
        {
            this.monster = monster;
        }

        public void StateEnter()
        {
            monster.animator.SetBool("IsAttack", true);
            monster.Attack();
        }

        public void StateExit()
        {
            monster.animator.SetBool("IsAttack", false);
        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if (monster.targetDistance > monster.monsterAgent.stoppingDistance)
            {
                monster.stateMachine.SetState(monster.dicState[EnemyState.Idle]);
            }
        }
    }
}