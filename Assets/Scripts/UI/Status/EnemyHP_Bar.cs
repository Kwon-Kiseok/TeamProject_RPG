using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP_Bar : MonoBehaviour
{
    private Camera UICamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHP;

    public Vector3 offset = Vector3.zero;
    public Transform enemyTr;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        UICamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = this.gameObject.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset);
     
        if(screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, UICamera, out localPos);
        
        rectHP.localPosition = localPos;
    }

    public float UpdateGage(float currentHP, float maxHP)
    {
        return currentHP/ maxHP;
    }
}
