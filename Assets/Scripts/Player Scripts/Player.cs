using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    private float leftX=-2, midX=0, rightX=2;


    private Road currentRoad = Road.mid;

    public Direction _direction;


    private bool ready = true;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //run = false;
            float t = Touch.TouchDistanceToTransform(transform);
            Debug.Log(t);
            if(t > 5)
            {
                Move(Direction.right);
            }
            else if (t < -5)
            {
                Move(Direction.left);
            }
            else
            {

            }
        }

    }

    private void Move(Direction direction)
    {
        if (!ready)
            return;

        if(currentRoad == Road.left)
        {

            if(direction == Direction.left)
            {
                return;
            }
            else if(direction == Direction.right)
            {
                Move_(midX,Road.mid);
            }

        }
        else if(currentRoad == Road.mid)
        {
            if (direction == Direction.left)
            {
                Move_(leftX,Road.left);
            }
            else if (direction == Direction.right)
            {
                Move_(rightX,Road.right);
            }
        }
        else if(currentRoad == Road.right)
        {
            if (direction == Direction.right)
            {
                return;
            }
            else if (direction == Direction.left)
            {
                Move_(midX,Road.mid);
            }
        }
        

    }

    private void Move_(float x,Road road)
    {
        ready = false;
        transform.DOMoveX(x, 0.5f).OnComplete(() =>
        {
            currentRoad = road;
            ready = true;
        });

    }




}

public enum Road
{
    left,mid,right
}

public enum Direction
{
    none,left,right
}