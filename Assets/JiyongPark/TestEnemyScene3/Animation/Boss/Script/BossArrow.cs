using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOGUS.Scripts.Manager;

namespace HOGUS.Scripts.Character
{
    public class BossArrow : MonoBehaviour
    {
        Rigidbody rigid;
        public int damage;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0, -20f, 0);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
