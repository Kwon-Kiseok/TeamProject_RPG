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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어에게 닿았을 경우
        if(collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.GetCurrentStatus().TakeDamage(damage);       // 플레이어가 설정된 만큼의 데미지를 받음
            Debug.Log("Player Hit");
        }
    }
}
