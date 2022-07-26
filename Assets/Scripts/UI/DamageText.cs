using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float speed;
    private float destroyTime;
    public int damage;

    void Start()
    {
        speed = 2.0f;
        destroyTime = 2.0f;
        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
