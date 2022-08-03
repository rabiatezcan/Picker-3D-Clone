using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameHelper : MonoBehaviour
{ 
    [SerializeField] Camera screenCamera;
    [SerializeField] Camera gameCamera;
    [SerializeField] Canvas screenCanvas;
    public static Camera ScreenCamera;
    public static Camera GameCamera;
    public static Canvas ScreenCanvas;

    public static Vector3 WorldToScreenPoint(Vector3 worldPos)
    {
        Vector3 screenPos;
        Vector3 canvasPos;
        Vector2 posRect2D;
        screenPos = GameCamera.WorldToScreenPoint(worldPos);
        //Canvas positioning
        if (ScreenCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            canvasPos = screenPos;
        }
        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ScreenCanvas.transform as RectTransform, screenPos, ScreenCanvas.worldCamera, out posRect2D);
            canvasPos = ScreenCanvas.transform.TransformPoint(posRect2D);
        }
        return canvasPos;
    }

}