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
            Physics.gravity = new Vector3(0, -15f, 0);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
                var player = other.gameObject.GetComponent<Player>();
                player.Damaged(damage);
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Floor"))
            {
                Destroy(gameObject);
            }
        }
    }
}
