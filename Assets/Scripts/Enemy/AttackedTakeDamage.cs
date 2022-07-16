using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class AttackedTakeDamage : MonoBehaviour
    {
        private CharacterStats stats;

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
        }
        //stat 같은 건 미리 겟컴포넌트로 해놓자.
        public void OnAttack(GameObject attacker, Attack attack)
        {
            stats.Hp -= attack.Damage;
            if (stats.Hp <= 0)
            {
                stats.Hp = 0;
                //어떤 상황에 따라서 죽는경우가 많으므로 인터페이스화로 하는게 좋다.
                var destructibles = GetComponentsInChildren<IDestructable>();  //자식까지 다 가져와야하므로
                foreach (var destructible in destructibles)
                {
                    destructible.OnDestruction(attacker);
                }
            }
        }
    }
}