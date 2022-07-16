using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Image point;

    public float radius;

    public Player player;

    private Vector2 originalPoint = Vector2.zero;
    private RectTransform rectTr;

    private Vector2 direction;

    private void Awake()
    {

    }

    private void Start()
    {
        rectTr = GetComponent<RectTransform>();
        originalPoint = rectTr.position;
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
        var newPos = eventData.position;
        direction = newPos - originalPoint;

        if (direction.magnitude > radius)
        {
            newPos = originalPoint + direction.normalized * radius;
        }

        point.rectTransform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector2.zero;
        point.rectTransform.position = originalPoint;
    }

    public void DodgeButton()
    {
        player.Dodge();
    }
}