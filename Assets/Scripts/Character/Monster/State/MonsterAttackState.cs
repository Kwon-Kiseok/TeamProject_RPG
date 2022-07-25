using System;
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
        private float timer;

        public MonsterAttackState(MonsterBase monster)
        {
            this.monster = monster;
        }

        public void StateEnter()
        {
            monster.animator.SetTrigger("DoAttack");
            monster.Attack();
        }

        public void StateExit()
        {

        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if (monster.targetDistance > monster.monsterAgent.stoppingDistance)
            {
                monster.stateMachine.SetState(monster.dicState[EnemyState.Idle]);
                return;
            }

            timer += Time.deltaTime;
            if(monster.attackCooltime <= timer)
            {
                monster.animator.SetTrigger("DoAttack");
                monster.Attack();
                timer = 0f;
            }
        }
    }
}