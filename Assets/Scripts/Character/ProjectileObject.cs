using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileObject : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;         // 해당 투사체의 속도
    public int damage;          // 해당 투사체의 공격력 생성할 때 설정
    public int lifeTime;
    public Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.AddForce(dir * speed, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어에게 닿았을 경우
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            var player = other.gameObject.GetComponent<Player>();
            player.Damaged(damage);       // 플레이어가 설정된 만큼의 데미지를 받음
            Destroy(gameObject);
        }

        Debug.Log(other.gameObject.name + " " + other.gameObject.tag);
    }
}
