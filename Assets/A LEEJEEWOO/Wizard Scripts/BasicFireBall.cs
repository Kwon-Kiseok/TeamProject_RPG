using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFireBall : MonoBehaviour
{
    public GameObject basicFireBall;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(basicFireBall);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
