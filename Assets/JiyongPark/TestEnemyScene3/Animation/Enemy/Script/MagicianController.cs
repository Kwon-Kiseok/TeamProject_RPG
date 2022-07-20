using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace HOGUS.Scripts.Character
{
    public class MagicianController : MonoBehaviour, IUpdatableObject
    {
        public Animator animator;
        public Transform player;
        public NavMeshAgent monster;
        public GameObject weapon;
        public GameObject fireball;
        public bool isLooking;
        private Rigidbody rigid;
        public bool isChase;
        public bool isAttack;
        public BoxCollider attackArea;
        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();

            Invoke("ChaseStart", 2f);
        }
        public void OnEnable()
        {
            UpdateManager.Instance.RegisterUpdatableObject(this);
        }

        public void OnDisable()
        {
            if (UpdateManager.Instance != null)
            {
                UpdateManager.Instance.DeregisterUpdatableObject(this);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            LookTarget();
            Targeting();
            FreezeVelocity();
        }

        public void OnUpdate(float deltaTime)
        {
            ChasePlayer();
        }

        public void LookTarget()
        {
            isLooking = true;
            Quaternion lookAt = Quaternion.identity;
            Vector3 lookAtVec = (player.transform.position - transform.position).normalized;
            lookAt.SetLookRotation(lookAtVec);
            transform.rotation = lookAt;
        }
        private void ChaseStart()
        {
            isChase = true;
            animator.SetBool("IsWalking", true);
        }

        public void ChasePlayer()
        {
            if (monster.enabled)
            {
                monster.SetDestination(player.position);
                monster.isStopped = !isChase;
            }
        }

        private void FreezeVelocity()
        {
            if (isChase)
            {
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
            }
        }

        private void Targeting()
        {
            float targetRadius = 0.3f;
            float targetRange = 25f;

            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
        IEnumerator Attack()
        {
            isChase = false;
            isAttack = true;
            animator.SetBool("IsAttack", true);

            yield return new WaitForSeconds(0.3f);
            GameObject InstantSpell = Instantiate(fireball, weapon.transform.position, weapon.transform.rotation);
            Rigidbody rigidSpell = InstantSpell.GetComponent<Rigidbody>();
            rigidSpell.velocity = transform.forward * 40;
            yield return new WaitForSeconds(2f);
            isChase = true;
            isAttack = false;
            animator.SetBool("IsAttack", false);
        }
    }
}