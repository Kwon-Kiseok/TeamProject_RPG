using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;
using HOGUS.Scripts.Object.Item;

namespace HOGUS.Scripts.CustomSystem
{
    /// <summary>
    /// 캐릭터의 근접 공격 시스템
    /// </summary>
    public class CombatSystem : MonoBehaviour
    {
        public GameObject attackCollider;

        public void OnAttackCollision()
        {
            attackCollider.SetActive(true);
        }
    }
}