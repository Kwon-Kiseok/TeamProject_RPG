using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using HOGUS.Scripts.Character;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform leverBackTr;
    public RectTransform leverTr;
    private float radius;

    public GameObject takeButtonGO;

    [SerializeField]
    private Vector3 direction;
    private Vector3 originalPos;

    private float hAxis;
    private float vAxis;

    public float HorizontalAxis { get { return hAxis; } set { hAxis = value; } }
    public float VerticalAxis { get { return vAxis; } set { vAxis = value; } }

    private void Start()
    {
        leverBackTr = GetComponent<RectTransform>();
        radius = leverBackTr.rect.width * 0.5f;

    }

    public void GetMove()
    {
        hAxis = GetAxisRaw("Horizontal");
        vAxis = GetAxisRaw("Vertical");
    }

    public float GetAxis(string axis)
    {
        var dir = direction.normalized;

        switch (axis)
        {
            case "Horizontal":
                return dir.x;

            case "Vertical":
                return dir.y;
        }

        return 0f;
    }

    public float GetAxisRaw(string axis)
    {
        var dir = direction.normalized;

        switch (axis)
        {
            case "Horizontal":
                return dir.x;

            case "Vertical":
                return dir.y;
        }

        return 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (originalPos == Vector3.zero)
        {
            originalPos = leverTr.position;
        }

        ControlJoyStickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector2.zero;
        leverTr.position = originalPos;
    }

    private void ControlJoyStickLever(PointerEventData eventData)
    {
        var canvas = GetComponentInParent<Canvas>();
        Vector3 screenPos = eventData.position;
        screenPos.z = Mathf.Abs(canvas.worldCamera.transform.position.z - leverTr.position.z);
        var worldPos = canvas.worldCamera.ScreenToWorldPoint(screenPos);

        // 실제 이동을 시켜주는 좌표
        var inputDir = worldPos - leverBackTr.position;
        direction = inputDir.magnitude < radius ? inputDir : inputDir.normalized * radius;

        // 보여지는 레버의 좌표는 중심좌표 기준으로 강제로 100씩
        var displayLeverPos = direction.normalized * 100;
        leverTr.localPosition = displayLeverPos;
    }
}