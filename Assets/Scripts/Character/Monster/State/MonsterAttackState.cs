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
            Debug.Log("몬스터 공격 수행");
        }

        public void StateExit()
        {
            Debug.Log("몬스터 공격 수행 종료");
        }

        public void StateFixedUpdate()
        {            
        }

        public void StateUpdate()
        {
            
        }
    }
}