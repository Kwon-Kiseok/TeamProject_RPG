using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.State
{
    public class BossAttackState : IState
    {
        private readonly BossBase boss;
        private float timer;

        public BossAttackState(BossBase boss)
        {
            this.boss = boss;
        }

        public void StateEnter()
        {
            boss.Attack();
        }

        public void StateExit()
        {

        }

        public void StateFixedUpdate()
        {
        }

        public void StateUpdate()
        {
            if (boss.targetDistance > boss.bossAgent.stoppingDistance)
            {
                boss.stateMachine.SetState(boss.dicState[EnemyState.Idle]);
                return;
            }

            timer += Time.deltaTime;
            if (boss.attackCooltime <= timer)
            {
                boss.animator.SetTrigger("DoAttack");
                boss.Attack();
                timer = 0f;
            }
        }
    }
}