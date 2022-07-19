using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HOGUS.Scripts.CustomSystem
{
    public class InputHandler : MonoBehaviour
    {
        public Joystick joystick;

        private float hAxis;
        private float vAxis;

        public float HorizontalAxis { get { return hAxis; } set { hAxis = value; } }
        public float VerticalAxis { get { return vAxis; } set { vAxis = value; } }


        public void GetMove()
        {
            hAxis = joystick.GetAxisRaw("Horizontal");
            vAxis = joystick.GetAxisRaw("Vertical");
        }
    }
}