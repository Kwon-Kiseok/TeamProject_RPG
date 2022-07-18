using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConvertWorldToUI : MonoBehaviour
{
    public RectTransform targetRectTr;
    public RectTransform UIRectTr;
    public Camera UICamera;

    public Vector2 screenPoint;

    public void Convert(Vector2 mousePos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTr, mousePos, UICamera, out screenPoint);
        UIRectTr.localPosition = screenPoint;
    }
}
