using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HOGUS.Scripts.Character
{
    public interface IAttackable
    {
        void OnAttack(GameObject attacker, Attack attack);
    }
}
