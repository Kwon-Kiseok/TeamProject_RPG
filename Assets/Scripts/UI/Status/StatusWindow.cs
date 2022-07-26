using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatusWindow : MonoBehaviour, IPointerClickHandler
{
    public GameObject User_Status;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(User_Status.activeSelf)
        {
            OnClickCloseButton();
            return;
        }

        User_Status.SetActive(true);
    }

    public void OnClickCloseButton()
    {
        User_Status.SetActive(false);
    }
}
