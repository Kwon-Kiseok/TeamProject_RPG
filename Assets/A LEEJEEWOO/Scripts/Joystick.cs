using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform leverBackTr;
    public RectTransform leverTr;
    private float radius;

    public Player player;

    [SerializeField]
    private Vector2 direction;

    private void Start()
    {
        leverBackTr = GetComponent<RectTransform>();
        radius = leverBackTr.rect.width * 0.2f;
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
        ControlJoyStickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector2.zero;
        leverTr.anchoredPosition = Vector2.zero;
    }

    public void DodgeButton()
    {
        player.Dodge();
    }

    private void ControlJoyStickLever(PointerEventData eventData)
    {
        direction = eventData.position - leverBackTr.anchoredPosition;
        leverTr.anchoredPosition = direction.magnitude < radius ? direction : direction.normalized * radius;
    }
}