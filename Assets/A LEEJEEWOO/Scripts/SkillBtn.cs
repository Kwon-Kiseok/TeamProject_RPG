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
    private bool canUseSkill1 = true;
    private bool canUseSkill2 = true;
    private bool canUseSkill3 = true;

    public bool cool;
    public bool heal;
    public bool combo;
    public bool dash;

    public GameObject masicTime;
    public GameObject healTime;
    public GameObject comboTime;
    public GameObject dashTime;

    public GameObject masicBTN;
    public GameObject comboBTN;
    public GameObject dashBTN;
    public GameObject healBTN;

    private void Start()
    {
        skillFilter.fillAmount = 0;
        cool = true;
        heal = true;
        combo = true;
        dash = true;

        masicTime.gameObject.SetActive(false);
        healTime.gameObject.SetActive(false);
        comboTime.gameObject.SetActive(false);
        dashTime.gameObject.SetActive(false);

        masicBTN.GetComponent<Button>().enabled = true;
        healBTN.GetComponent<Button>().enabled = true;
        comboBTN.GetComponent<Button>().enabled = true;
        dashBTN.GetComponent<Button>().enabled = true;
    }

    public void UseSkill()
    {
        if (canUseSkill)
        {
            masicTime.gameObject.SetActive(true);
            masicBTN.GetComponent<Button>().enabled = false;

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
        if (canUseSkill1)
        {
            healTime.gameObject.SetActive(true);
            healBTN.GetComponent<Button>().enabled = false;

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
        if (canUseSkill2)
        {
            comboTime.gameObject.SetActive(true);
            comboBTN.GetComponent<Button>().enabled = false;

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
        if (canUseSkill3)
        {
            dashTime.gameObject.SetActive(true);
            dashBTN.GetComponent<Button>().enabled = false;

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
        masicTime.gameObject.SetActive(false);
        masicBTN.GetComponent<Button>().enabled = true;
        canUseSkill = true;
        yield break;
    }

    IEnumerator Cooltime1()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
            
            heal = false;
            
        }
        canUseSkill1 = true;        
        heal = true;        

        yield break;
    }

    IEnumerator CoolTimeCounter1()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
            canUseSkill1 = false;
        }
        healTime.gameObject.SetActive(false);
        healBTN.GetComponent<Button>().enabled = true;
        canUseSkill1 = true;
        yield break;
    }

    IEnumerator Cooltime2()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;

            combo = false;

        }        
        canUseSkill2 = true;
        combo = true;

        yield break;
    }

    IEnumerator CoolTimeCounter2()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
            canUseSkill2 = false;
        }
        comboTime.gameObject.SetActive(false);
        comboBTN.GetComponent<Button>().enabled = true;
        canUseSkill2 = true;
        yield break;
    }

    IEnumerator Cooltime3()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;

            dash = false;

        }
        canUseSkill3 = true;
        dash = true;

        yield break;
    }

    IEnumerator CoolTimeCounter3()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
            canUseSkill3 = false;
        }
        dashTime.gameObject.SetActive(false);
        dashBTN.GetComponent<Button>().enabled = true;
        canUseSkill3 = true;
        yield break;
    }
}