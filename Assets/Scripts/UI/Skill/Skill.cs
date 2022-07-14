using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill
{
    public string skill_Name;           //스킬 이름
    public string skill_Description;    //스킬 설명
    public float skill_Cost;            //스킬 소모 자원(MP)
    public float skill_CoolTime;        //스킬 쿨타임
    public bool skill_IsCoolTime;       //스킬 쿨타임 체크
    public float skill_Level;           //스킬 레벨
    public Sprite skill_Image;          //스킬 이미지
    public static bool isSkillCheck;    //스킬 사용 중인지 체크
    public float skill_CastTime;        //스킬 시전시간

    public Skill()
    {
        skill_Name = "";
        skill_Description = "";
        skill_Cost = 0f;
        skill_CoolTime = 0f;
        skill_IsCoolTime = false;
        skill_Level = 1f;
    }

    public void SetData(string _SkillName, string _SkillDec, float _Cost, float _CoolTime, float _Level, float _CastTime, Sprite _SkillImage = null)
    {
        skill_Name = _SkillName;
        skill_Description = _SkillDec;
        skill_Cost = _Cost;
        skill_CoolTime = _CoolTime;
        skill_Level = _Level;
        skill_CastTime = _CastTime;
        skill_Image = _SkillImage;
        skill_IsCoolTime = true;
        isSkillCheck = true;
    }

    public virtual IEnumerator Cast()
    {
        yield return new WaitForSeconds(skill_CastTime);
    }
}
