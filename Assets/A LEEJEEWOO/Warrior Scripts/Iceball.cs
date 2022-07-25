using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;

public class Iceball : MonoBehaviour
{
    public GameObject iceball;
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall") || collision.gameObject.layer == 20)
        {
            GameObject eff = Instantiate(iceball);
            eff.transform.position = transform.position;
            Destroy(gameObject);

            if(collision.gameObject.CompareTag("Enemy"))
            {
                var enemy = collision.gameObject.GetComponent<MonsterBase>();
                enemy.GetCurrentStat().TakeDamage(damage);
            }
        }
    }
}
