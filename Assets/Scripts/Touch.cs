using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private static bool _pressing;

    private static bool _active;

    public static bool Active { get => _active; set => _active = value; }
    public static bool Pressing { get => _pressing; set => _pressing = value; }

    public void OnPointerDown(PointerEventData eventData)
    {

        _pressing = true;


    }

    public void OnPointerUp(PointerEventData eventData)
    {

        _pressing = false;


    }

    public static float TouchDistanceToTransform(Transform transform)
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.x -=  Screen.width / 2;
        return mousePos.x;
        //Debug.Log(mousePos.x + " " + Screen.width/2);
        float dis = Mathf.Abs(mousePos.x);
        return Mathf.Clamp(dis, 0, Screen.width / 2);
    }



}