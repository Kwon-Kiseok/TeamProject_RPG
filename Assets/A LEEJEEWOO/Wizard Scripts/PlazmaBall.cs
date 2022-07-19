using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmaBall : MonoBehaviour
{
    public GameObject plazmaBall;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(plazmaBall);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
