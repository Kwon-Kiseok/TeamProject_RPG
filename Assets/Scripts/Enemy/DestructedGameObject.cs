using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class DestructedGameObject : MonoBehaviour, IDestructable
    {
        public void OnDestruction(GameObject attacker)
        {
            Destroy(gameObject);
        }
    }
}