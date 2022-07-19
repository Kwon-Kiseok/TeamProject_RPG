using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    public GameObject iceball;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(iceball);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
