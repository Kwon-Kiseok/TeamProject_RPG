using UnityEngine;
using System.Collections.Generic;
 
/// <summary>
/// 카메라 해상도 고정 레터박스
/// </summary>
public class CameraResolution : MonoBehaviour
{
    #region ScreenSize
    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;
    #endregion


    private void RescaleCamera()
    {
        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;
 
        float targetaspect = 16.0f / 10.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;
 
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
 
             camera.rect = rect;
        }
        else // add letterbox
        {
            float scalewidth = 1.0f / scaleheight;
 
            Rect rect = camera.rect;
 
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
 
             camera.rect = rect;
        }
 
        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }

    void OnPreCull()
    {
        // UI 카메라의 경우 검정색으로 날려주면 안됨
        if (gameObject.CompareTag("UICamera"))
            return;

        GL.Clear(true, true, Color.black);
    }
 
    void Start () 
    {
        RescaleCamera();
    }
}
 