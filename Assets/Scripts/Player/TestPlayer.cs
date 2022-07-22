using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.Character
{
    public class TestPlayer : MonoBehaviour
    {
        public float runSpeed;
        public float hAxis;
        public float vAxis;
        public Vector3 moveVec;

        public void FixedUpdate()
        {
            transform.position += moveVec * runSpeed * Time.deltaTime;
            transform.LookAt(transform.position + moveVec);
        }

        public void Update()
        {      
            Movement();
        }

        public void Movement()
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        }
    }
}