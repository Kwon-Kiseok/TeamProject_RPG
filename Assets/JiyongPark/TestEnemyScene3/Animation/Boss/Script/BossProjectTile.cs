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
        navi.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            var player = collision.gameObject.GetComponent<Player>();
            player.GetCurrentStatus().TakeDamage(damage);       
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        Debug.Log(collision.gameObject.name + " " + collision.gameObject.tag);
    }
}
