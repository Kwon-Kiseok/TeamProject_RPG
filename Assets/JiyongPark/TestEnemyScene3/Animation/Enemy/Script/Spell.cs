using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class Spell : MonoBehaviour
    {
        public int damage;
        public bool isMelee;
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Floor")
            {
                Destroy(gameObject, 3);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}