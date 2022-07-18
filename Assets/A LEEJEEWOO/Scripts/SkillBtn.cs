using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillBtn : MonoBehaviour
{
    public Image skillFilter;
    public Text coolTimeCounter;

    public float coolTime;
    private float currentCoolTime;
    private bool canUseSkill = true;

    public bool cool;
    public bool heal;
    public bool combo;
    public bool dash;

    public GameObject timeGO;
    public GameObject buttonGO;

    private void Start()
    {
        skillFilter.fillAmount = 0;
        cool = true;
        heal = true;
        combo = true;
        dash = true;

        timeGO.SetActive(false);
        buttonGO.GetComponent<Button>().enabled = true;
    }

    public void UseSkill()
    {
        if (canUseSkill)
        {
            timeGO.SetActive(true);
            buttonGO.GetComponent<Button>().enabled = false;

            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter");
        }
        else
        {
            Debug.Log("스킬사용불가");
        }
    }

    public void UseHeal()
    {
        if (canUseSkill)
        {
            timeGO.SetActive(true);
            buttonGO.GetComponent<Button>().enabled = false;

            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime1");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter1");
        }
        else
        {
            Debug.Log("스킬사용불가");
        }
    }

    public void UseCombo()
    {
        if (canUseSkill)
        {
            timeGO.SetActive(true);
            buttonGO.GetComponent<Button>().enabled = false;

            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime2");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter2");
        }
        else
        {
            Debug.Log("스킬사용불가");
        }
    }

    public void UseDash()
    {
        if (canUseSkill)
        {
            timeGO.SetActive(true);
            buttonGO.GetComponent<Button>().enabled = false;

            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime3");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter3");
        }
        else
        {
            Debug.Log("스킬사용불가");
        }
    }

    IEnumerator Cooltime()
    {
        while(skillFilter.fillAmount > 0)
        {
            
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
            cool = false;

        }
        canUseSkill = true;
        cool = true;

        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        while(currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
            canUseSkill=false;
        }
        timeGO.SetActive(false);
        buttonGO.GetComponent<Button>().enabled = true;
        canUseSkill = true;
        yield break;
    }    
}