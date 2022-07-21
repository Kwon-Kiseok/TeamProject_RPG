using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArrow : MonoBehaviour
{
    Rigidbody rigid;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
