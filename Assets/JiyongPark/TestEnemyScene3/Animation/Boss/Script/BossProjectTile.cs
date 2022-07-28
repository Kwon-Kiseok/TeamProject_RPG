using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HOGUS.Scripts.Character;

[RequireComponent(typeof(Rigidbody))]
public class BossProjectTile : MonoBehaviour
{
    private Rigidbody rb;   
    public int damage;         
    private NavMeshAgent navi;
    public Transform target;
    public ParticleSystem explosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        navi = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        navi.SetDestination(target.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            var player = other.gameObject.GetComponent<Player>();
            player.Damaged(damage);
           //Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        Debug.Log(other.gameObject.name + " " + other.gameObject.tag);
    }
}
