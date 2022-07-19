using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private static Player instance;


    public GameObject MenuScreen,GameScreen;

    private GameObject currentScreen;

    private void Awake()
    {

        MakeSingleton();
        currentScreen = MenuScreen;
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


    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = 0;
    }

    private float leftX=-2, midX=0, rightX=2;

    private float left_rightTime = 0.2f;
    private float forwardSpeed = 10f;

    [SerializeField]
    private Road currentRoad = Road.mid;

    public Direction _direction;


    private bool ready = true;

    public static Player Instance { get => instance; set => instance = value; }

    private float delay = 1.2f;

    private void Update()
    {

        if(currentScreen == MenuScreen)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentScreen.SetActive(false);
                currentScreen = GameScreen;
                currentScreen.SetActive(true);
                delay = 0.2f;
                animator.speed = 1;
            }

        }
        else if(currentScreen == GameScreen)
        {
            delay -= Time.deltaTime;
            if (delay > 0)
            {
                
                return;
            }
                

            MoveZ();
            if (Input.GetKeyUp(KeyCode.D))
            {
                MoveX(Direction.right);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                MoveX(Direction.left);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Jump();
            }

            if (TakenRoad() >= 4f)
            {
                Roads.Instance.PushForward();
                lastZ = transform.position.z;
            }

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
            //Debug.Log("Coin");
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


        if (other.CompareTag("Trap"))
        {
            currentScreen.SetActive(false);
            currentScreen = MenuScreen;
            currentScreen.SetActive(true);
            animator.speed = 0;
            //transform.position = new Vector3(0,1.33f,0);
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