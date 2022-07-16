using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Data
{
    [System.Serializable]
    public class Stat : MonoBehaviour
    {
        #region Base Stat value
        [Header("캐릭터 기본 스탯")]
        [SerializeField]
        protected int level;           // 캐릭터의 레벨
        [SerializeField]
        protected int maxHP;           // 캐릭터의 최대 체력
        [SerializeField]
        protected int currHP;          // 캐릭터의 현재 체력
        [SerializeField]
        protected int minDamage;       // 캐릭터의 최소 공격력
        [SerializeField]
        protected int maxDamage;       // 캐릭터의 최대 공격력
        [SerializeField]
        protected int defense;         // 캐릭터의 방어력
        [SerializeField]
        protected float dodgeChance;   // 캐릭터의 회피율
        [SerializeField]
        protected float speed;         // 캐릭터의 이동 속도
        [SerializeField]
        protected float attackSpeed;   // 캐릭터의 공격 속도
        #endregion

        #region Base Stat Property
        public int Level { get { return level; } set { level = value; } }
        public int MaxHP { get { return maxHP; } set { maxHP = value; } }
        public int CurHP { get { return currHP; } set { currHP = value; } }
        public int MinDamage { get { return minDamage; } set { minDamage = value; } }
        public int MaxDamage { get { return maxDamage; } set { maxDamage = value; } }
        public int Defense { get { return defense; } set { defense = value; } }
        public float DodgeChance { get { return dodgeChance; } set { dodgeChance = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        #endregion
    }
}
