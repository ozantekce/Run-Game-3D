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

        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);

        v3Pos = v3Pos - transform.position;

        return v3Pos.x;

    }



}