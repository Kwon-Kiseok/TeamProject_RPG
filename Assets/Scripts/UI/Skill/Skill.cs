using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill/ Skill")]
public class Skill : ScriptableObject
{
    public string skill_Name;           //스킬 이름
    public string skill_Description;    //스킬 설명
    public float skill_Cost;            //스킬 소모 자원(MP)
    public float skill_CoolTime;        //스킬 쿨타임
    public bool skill_IsCoolTime;       //스킬 쿨타임 체크
    public string skill_Level;           //스킬 레벨
    public Sprite skill_Image;          //스킬 이미지
    public static bool isSkillCheck;    //스킬 사용 중인지 체크
    public float skill_CastTime;        //스킬 시전시간
    public bool skill_Rock;

    public Skill()
    {
        skill_Name = "";
        skill_Description = "";
        skill_Cost = 0f;
        skill_CoolTime = 0f;
        skill_IsCoolTime = false;
        skill_Rock = false;
        skill_Level = "";
    }
}
