using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject meteor;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(meteor);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
