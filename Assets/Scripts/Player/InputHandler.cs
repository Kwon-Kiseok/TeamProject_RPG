using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.CustomSystem
{
    /// <summary>
    /// 플레이어의 입력들을 담당하는 핸들러
    /// 커맨드 패턴을 사용해야 하는가에 대한 의문점
    /// 각각의 조작에 사용될 UI 버튼 키들은 각각 하나의 기능을 담당하기로 논의되긴 함
    /// NewInputSystem에서 사용되는 것처럼 프레스, 클릭 등의 페이즈를 넣어서 구현이 이상적
    /// </summary>
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

        public void GetButton(string key)
        {
            if(joystick.events.ContainsKey(key))
            {
                joystick.events[key]();
            }
        }
    }
}