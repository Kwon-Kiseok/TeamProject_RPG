using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAttack : MonoBehaviour
{
    public Animator animator;
    bool nomalComboPossible;
    int nomalComboStep;

    public void NomalAttack1()
    {
        if (nomalComboStep == 0)
        {
            animator.Play("Attack_02");
            nomalComboStep = 1;
            return;
        }
        if (nomalComboStep != 0)
        {
            if (nomalComboPossible)
            {
                nomalComboPossible = false;
                nomalComboStep += 1;
            }
            Debug.Log("NomalAttack1");
        }
    }

    public void ComboPossible()
    {
        nomalComboPossible = true;
        Debug.Log("nomalCombo Possible");
    }

    public void Combo()
    {
        if (nomalComboStep == 2)
        {
            animator.Play("Attack_03");
        }
        if (nomalComboStep == 3)
        {
            animator.Play("Attack_17");
        }
    }

    public void ComboReset()
    {
        nomalComboPossible = false;
        nomalComboStep = 0;
        Debug.Log("nomalCombo imPossible");
    }
}
