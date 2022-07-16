using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public interface IDestructable
    {
        void OnDestruction(GameObject attacker);
    }
}
