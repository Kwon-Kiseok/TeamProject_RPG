using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HOGUS.Scripts.Character;
using HOGUS.Scripts.Data;

[RequireComponent(typeof(Rigidbody))]
public class BossProjectile : MonoBehaviour
{
    private Rigidbody rb;   
    public int damage;         
    private NavMeshAgent navi;
    public Transform target;
    public ParticleSystem explosion;
    public int lifeTime;
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        navi = GetComponent<NavMeshAgent>();
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        navi.SetDestination(target.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Hit");
            var player = other.gameObject.GetComponent<Player>();
            player.Damaged(damage);
            explosion.Play();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            explosion.Play();
            Destroy(gameObject);
        }

        //Debug.Log(other.gameObject.name + " " + other.gameObject.tag);
    }
}
