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
            if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }

        }
    }
}