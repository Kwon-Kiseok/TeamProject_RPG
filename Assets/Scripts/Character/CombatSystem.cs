using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.Data;

namespace HOGUS.Scripts.CustomSystem
{
    /// <summary>
    /// 캐릭터의 근접 공격 시스템
    /// </summary>
    public class CombatSystem : MonoBehaviour
    {
        public GameObject attackCollider;

        // 근접 공격을 수행하는 타이밍의 애니메이션에 이벤트로 등록
        public void OnAttackCollision()
        {
            attackCollider.SetActive(true);
        }

        public bool CheckTargetHit(Stat targetStat)
        {
            if (targetStat == null)
                return false;

            return true;
        }
    }
}