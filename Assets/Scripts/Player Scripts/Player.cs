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

    [SerializeField]
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
            //Debug.Log(t);
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

        if (Input.GetAxis("Jump")>0)
        {
            Jump();
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

    private bool jumpReady = true;
    private void Jump()
    {

        if (!jumpReady)
            return;
        jumpReady = false;
        transform.DOMoveY(3f, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.DOMoveY(1.33f, 0.5f).SetEase(Ease.InSine).OnComplete(() => { jumpReady = true; });
        });
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



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Coin"))
        {
            Debug.Log("Coin");
            CoinManager.Instance.StackCoin(other.gameObject);

            if (other.name.Equals("Gold"))
            {
                ScoreManager.Instance.Score += 3;
            }
            else if (other.name.Equals("Silver"))
            {
                ScoreManager.Instance.Score += 2;
            }
            else
            {
                ScoreManager.Instance.Score += 1;
            }
        }

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