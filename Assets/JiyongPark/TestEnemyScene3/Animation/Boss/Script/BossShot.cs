using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOGUS.Scripts.Manager;
using UnityEngine.AI;

namespace HOGUS.Scripts.Character
{

    public class BossShot : Spell, IUpdatableObject
    {
        public Transform target;
        private NavMeshAgent navi;

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


        private void Awake()
        {
            navi = GetComponent<NavMeshAgent>();
        }
        public void OnUpdate(float deltaTime)
        {
            navi.SetDestination(target.position);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            
        }

    }
}