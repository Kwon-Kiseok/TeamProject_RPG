using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    public UIManager uIManager;
    public GameObject scanObject;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            //?싱글?터치.
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos;
            //Vector3 touchPosUI;
            Ray ray;
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    Vector3 touchPosToVector3 = new Vector3(touch.position.x, touch.position.y, 100);
                    touchPos = Camera.main.ScreenToWorldPoint(touchPosToVector3);
                    ray = Camera.main.ScreenPointToRay(touchPosToVector3);

                    Vector3 touchPosToUiVector3 = new Vector3(touch.position.x, touch.position.y, 100);
                    // ui카메라를 찾아서 ray를 받고 새로운 physics.raycast를 하여 새로운 hit인 TalkPanel을 찾아주자

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        if (hit.collider.tag == "NPC")
                        {
                            scanObject = hit.collider.gameObject;
                            uIManager.Action(scanObject);
                        }
                        //if(hit.collider.tag == "TalkPanel")
                        //{
                        //    scanObject = hit.collider.gameObject;
                        //    uIManager.Action(scanObject);

                        //    Debug.Log(hit.collider.gameObject.name);
                        //} 
                        else
                        {
                            scanObject = null;
                        }
                    }
                    break;
            }
        }
    }
}
