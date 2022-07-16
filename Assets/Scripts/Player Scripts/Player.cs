using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private static Player instance;
    private void Awake()
    {

        MakeSingleton();

    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private float leftX=-2, midX=0, rightX=2;

    private float left_rightTime = 0.2f;
    private float forwardSpeed = 10f;


    private Road currentRoad = Road.mid;

    public Direction _direction;


    private bool ready = true;

    public static Player Instance { get => instance; set => instance = value; }

    private void Update()
    {

        MoveZ();
        if (Input.GetMouseButtonDown(0))
        {
            //run = false;
            float t = Touch.TouchDistanceToTransform(transform);
            Debug.Log(t);
            if(t > 0.5f)
            {
                MoveX(Direction.right);
            }
            else if (t < -0.5f)
            {
                MoveX(Direction.left);
            }
            else
            {

            }
        }

        if(TakenRoad() >= 4f)
        {
            Roads.Instance.PushForward();
            lastZ = transform.position.z;
        }

    }



    private void MoveZ()
    {
        transform.Translate(new Vector3(0,0,forwardSpeed*Time.deltaTime));

    }


    private void MoveX(Direction direction)
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
                MoveX_Part2(midX,Road.mid);
            }

        }
        else if(currentRoad == Road.mid)
        {
            if (direction == Direction.left)
            {
                MoveX_Part2(leftX,Road.left);
            }
            else if (direction == Direction.right)
            {
                MoveX_Part2(rightX,Road.right);
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
                MoveX_Part2(midX,Road.mid);
            }
        }
        

    }

    private void MoveX_Part2(float x,Road road)
    {
        ready = false;
        transform.DOMoveX(x, left_rightTime).OnComplete(() =>
        {
            currentRoad = road;
            ready = true;
        });

    }


    private float lastZ;

    private float TakenRoad()
    {
        return transform.position.z - lastZ;
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