using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // in this game player transform
    public Transform target;

    public Vector3 offset;

    void Update()
    {
        transform.position = target.position + offset;
    }
}
