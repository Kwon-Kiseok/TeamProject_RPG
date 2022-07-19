using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillBtnB : MonoBehaviour
{
    public Image skillFilter;
    public Text coolTimeCounter;

    public float coolTime;

    private float currentCoolTime;

    private bool canUseSkill0 = true;
    private bool canUseSkill = true;
    private bool canUseSkill1 = true;
    private bool canUseSkill2 = true;
    private bool canUseSkill3 = true;

    public bool basicFireBall;
    public bool middlesunder;
    public bool blizzard;
    public bool plazmaBall;
    public bool meteor;

    public GameObject basicFireBallTime;
    public GameObject middlesunderTime;
    public GameObject blizzardTime;
    public GameObject plazmaBallTime;
    public GameObject meteorTime;

    public GameObject basicFireBallBTN;
    public GameObject middlesunderBTN;
    public GameObject blizzardBTN;
    public GameObject plazmaBallBTN;
    public GameObject meteorBTN;

    private void Start()
    {
        skillFilter.fillAmount = 0;
        basicFireBall = true;
        middlesunder = true;
        blizzard = true;
        plazmaBall = true;
        meteor = true;

        basicFireBallTime.gameObject.SetActive(false);
        middlesunderTime.gameObject.SetActive(false);
        blizzardTime.gameObject.SetActive(false);
        plazmaBallTime.gameObject.SetActive(false);
        meteorTime.gameObject.SetActive(false);

        basicFireBallBTN.GetComponent<Button>().enabled = true;
        middlesunderBTN.GetComponent<Button>().enabled = true;
        blizzardBTN.GetComponent<Button>().enabled = true;
        plazmaBallBTN.GetComponent<Button>().enabled = true;
        meteorBTN.GetComponent<Button>().enabled = true;
    }

    public void BasicFireBall()
    {
        if (canUseSkill0)
        {
            basicFireBallTime.gameObject.SetActive(true);
            basicFireBallBTN.GetComponent<Button>().enabled = false;

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

    public void Middlesunder()
    {
        if (canUseSkill)
        {
            middlesunderTime.gameObject.SetActive(true);
            middlesunderBTN.GetComponent<Button>().enabled = false;

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

    public void Blizzard()
    {
        if (canUseSkill1)
        {
            blizzardTime.gameObject.SetActive(true);
            blizzardBTN.GetComponent<Button>().enabled = false;

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

    public void PlazmaBall()
    {
        if (canUseSkill2)
        {
            plazmaBallTime.gameObject.SetActive(true);
            plazmaBallBTN.GetComponent<Button>().enabled = false;

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

    public void Meteor()
    {
        if (canUseSkill3)
        {
            meteorTime.gameObject.SetActive(true);
            meteorBTN.GetComponent<Button>().enabled = false;

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
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
            basicFireBall = false;

        }
        canUseSkill = true;
        basicFireBall = true;

        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
            canUseSkill = false;
        }
        basicFireBallTime.gameObject.SetActive(false);
        basicFireBallBTN.GetComponent<Button>().enabled = true;
        canUseSkill = true;
        yield break;
    }

    IEnumerator Cooltime1()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;

            middlesunder = false;

        }
        canUseSkill1 = true;
        middlesunder = true;

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
        middlesunderTime.gameObject.SetActive(false);
        middlesunderBTN.GetComponent<Button>().enabled = true;
        canUseSkill1 = true;
        yield break;
    }

    IEnumerator Cooltime2()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;

            blizzard = false;

        }
        canUseSkill2 = true;
        blizzard = true;

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
        blizzardTime.gameObject.SetActive(false);
        blizzardBTN.GetComponent<Button>().enabled = true;
        canUseSkill2 = true;
        yield break;
    }

    IEnumerator Cooltime3()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;

            plazmaBall = false;

        }
        canUseSkill3 = true;
        plazmaBall = true;

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
        plazmaBallTime.gameObject.SetActive(false);
        plazmaBallBTN.GetComponent<Button>().enabled = true;
        canUseSkill3 = true;
        yield break;
    }

    IEnumerator Cooltime4()
    {
        while (skillFilter.fillAmount > 0)
        {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            yield return null;

            meteor = false;

        }
        canUseSkill3 = true;
        meteor = true;

        yield break;
    }

    IEnumerator CoolTimeCounter4()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
            canUseSkill3 = false;
        }
        meteorTime.gameObject.SetActive(false);
        meteorBTN.GetComponent<Button>().enabled = true;
        canUseSkill3 = true;
        yield break;
    }
}