using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class CharacterStats : MonoBehaviour
    {
        public int maxHp;
        public int damage;
        public float armor;

        public int Hp
        {
            get;
            set;
        }

        private void Awake()
        {
            Hp = maxHp;
        }
    }
}