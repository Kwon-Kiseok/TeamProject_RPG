using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.Object.Item
{
    public class CombatWeapon : MonoBehaviour
    {
        public BoxCollider meleeAreaCollider;

        private void Start()
        {
            meleeAreaCollider = GetComponent<BoxCollider>();
        }

        public void Use()
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }

        private IEnumerator Swing()
        {
            yield return new WaitForSeconds(0.1f);
            meleeAreaCollider.enabled = true;
            yield return new WaitForSeconds(1f);
            meleeAreaCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            //
            if(other.CompareTag("Enemy"))
            {
                Debug.Log("Àû È÷Æ®");
            }
        }
    }
}