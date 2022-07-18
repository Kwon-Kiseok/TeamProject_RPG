using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Data
{
    public class PlayerStat : Stat
    {
        #region Player Stat Value
        [Header("플레이어 고유 스탯")]
        [SerializeField]
        protected string characterClass; // 직업
        [SerializeField]
        protected int strength;     // 힘 스탯     -> 10당 최소~최대 데미지 1씩 증가
        [SerializeField]
        protected int magic;        // 마법 스탯   -> 10당 마법 공격력 3증가
        [SerializeField]
        protected int dexterity;    // 민첩 스탯   -> 15당 회피율 1퍼 증가
        [SerializeField]
        protected int vitality;     // 활력 스탯   -> 1당 최대체력 1증가
        [SerializeField]
        protected float exp;        // 레벨업 시 필요한 경험치
        [SerializeField]
        protected float currExp;    // 현재 경험치
        [SerializeField]
        protected int gold;         // 현재 골드
        [SerializeField]
        protected int statPoint;    // 남아있는 스탯 포인트

        #endregion

        #region Player Stat Property
        public string CharacterClass { get { return characterClass; } set { characterClass = value; } }
        public int Strength { get { return strength; } set { strength = value; } }
        public int Magic { get { return magic; } set { magic = value; } }
        public int Dexterity { get { return dexterity; } set { dexterity = value; } }
        public int Vitality { get { return vitality; } set { vitality = value;} }
        public float EXP { get { return exp; } set { exp = value; } }
        public float CurrentEXP { get { return currExp; } set { currExp = value; } }
        public int Gold { get { return gold; } set { gold = value; } }
        public int StatPoint { get { return statPoint; } set { statPoint = value; } }
        #endregion

        public PlayerStat(PlayerStat copy)
        {
            strength = copy.strength;
            magic = copy.magic;
            Dexterity = copy.dexterity;
            Vitality = copy.vitality;
            minDamage = copy.minDamage;
            maxDamage = copy.maxDamage;
            defense = copy.defense;
            dodgeChance = copy.dodgeChance;
            speed = copy.speed;
            attackSpeed = copy.attackSpeed;
        }

#if UNITY_EDITOR
        public void PrintDebugStat()
        {
            Debug.Log($"level: {level}\nmaxHP: {maxHP}\ncurrHP: {currHP}\nminDamage: {minDamage}\n" +
                $"maxDamage: {maxDamage}\ndefense: {defense}\ndodgeChance: {dodgeChance}\nspeed: {speed}\n" +
                $"attackSpeed: {attackSpeed}\nexp: {exp}\ngole: {gold}");
        }
#endif
    }
}