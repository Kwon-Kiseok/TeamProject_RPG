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
            throw new System.NotImplementedException();
        }

        public void StateExit()
        {
            throw new System.NotImplementedException();
        }

        public void StateFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void StateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}