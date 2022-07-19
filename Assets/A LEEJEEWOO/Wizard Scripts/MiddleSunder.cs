using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleSunder : MonoBehaviour
{
    public GameObject middleSunder;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(middleSunder);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
