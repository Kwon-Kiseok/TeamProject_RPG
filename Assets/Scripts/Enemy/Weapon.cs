using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    [CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")] // 에셋에 아이템 추가 누를때마다 프로젝트에 추가
    public class Weapon : AttackDefinition
    {
        public GameObject prefab;

        public void ExecuteAttack(GameObject attacker, GameObject defender)
        {
            if (defender == null)
            {
                return;
            }

            // 거리
            if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
            {
                return;
            }

            // 방향
            var dir = defender.transform.position - attacker.transform.position;
            dir.Normalize();
            var dot = Vector3.Dot(attacker.transform.forward, dir);

            if (dot < 0.5f)
            {
                return;
            }

            // 공격
            var aStates = attacker.GetComponent<CharacterStats>();
            var dStates = defender.GetComponent<CharacterStats>();
            var attack = CreateAttack(aStates, dStates);

            var attackables = defender.GetComponentsInChildren<IAttackable>();
            foreach (var attackable in attackables)
            {
                attackable.OnAttack(attacker, attack);
            }

        }
    }
}