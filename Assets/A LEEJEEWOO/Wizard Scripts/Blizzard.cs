using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
    public GameObject blizzard;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(blizzard);
        eff.transform.position = transform.position;
        Destroy(gameObject);
    }
}
