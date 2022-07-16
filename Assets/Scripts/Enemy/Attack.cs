using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class Attack
    {
        public int Damage { get; private set; }
        public bool IsCritical { get; private set; }


        public Attack(int damage, bool critical)
        {
            Damage = damage;
            IsCritical = critical;
        }
    }
}

