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
        private Animator animator;
        bool normalComboPossible;
        int normalComboStep;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            if(0 == normalComboStep)
            {
                animator.Play("Attack_02");
                normalComboStep = 1;
                return;
            }
            if(0 != normalComboStep)
            {
                if(normalComboPossible)
                {
                    normalComboPossible = false;
                    normalComboStep += 1;
                }
            }
        }

        public void ComboPossible()
        {
            normalComboPossible = true;
        }

        public void Combo()
        {
            if(2 == normalComboStep)
            {
                animator.Play("Attack_03");
            }
            if(3 == normalComboStep)
            {
                animator.Play("Attack_17");
            }
        }

        public void ComboReset()
        {
            normalComboPossible = false;
            normalComboStep = 0;
        }
    }
}