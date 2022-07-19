using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;

namespace HOGUS.Scripts.CustomSystem
{
    /// <summary>
    /// 캐릭터의 근접 공격 시스템
    /// </summary>
    public class CombatSystem : MonoBehaviour
    {
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask targetLayer;

        public void Attack()
        {
            // Detect specific LayerMask target in attackRange
            var hitTargets = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayer);

            foreach (var target in hitTargets)
            {
                // 공유하려면 Character나 공통된 특성을 가져와서 해야 하지 않을까?
                
            }
        }
    }
}