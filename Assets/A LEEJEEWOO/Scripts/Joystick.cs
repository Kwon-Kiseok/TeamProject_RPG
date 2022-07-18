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

    private Vector3 originalPos;

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
        if (originalPos == Vector3.zero)
        {
            originalPos = leverTr.position;
        }

        ControlJoyStickLever(eventData);

        Debug.Log(eventData.position.x);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector2.zero;
        leverTr.position = originalPos;
    }

    public void DodgeButton()
    {
        player.Dodge();
    }

    private void ControlJoyStickLever(PointerEventData eventData)
    {
        var canvas = GetComponentInParent<Canvas>();
        Vector3 screenPos = eventData.position;
        screenPos.z = Mathf.Abs(canvas.worldCamera.transform.position.z - leverTr.position.z);
        var worldPos = canvas.worldCamera.ScreenToWorldPoint(screenPos);
        // direction 방향 수정해야 함
        direction = screenPos - originalPos;

        leverTr.position = worldPos;
    }
}