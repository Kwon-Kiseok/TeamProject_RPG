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

    public GameObject timeGO;
    public GameObject buttonGO;

    private void Start()
    {
        skillFilter.fillAmount = 0;
        cool = true;

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