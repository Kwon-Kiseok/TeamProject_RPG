using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;

public class FPS_Check : MonoBehaviour, IUpdatableObject
{
    float deltaTime = 0.0f;

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0}?ms?({1:0.}?fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
    }

    public void OnDisable()
    {
        UpdateManager.Instance.DeregisterUpdatableObject(this);
    }

    public void OnUpdate()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
}
