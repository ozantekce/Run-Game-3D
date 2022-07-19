using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    private GameObject[] obstacles;

    private Vector3 stackPos = new Vector3(100f, 100f, 100f);


    private static ObstacleManager instance;

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

    private float[] Xposes = { -2, 0, 2 };


    private void Awake()
    {

        Transform obstacles_ = transform.GetChild(0);
        obstacles = new GameObject[obstacles_.childCount];
        int i = 0;
        foreach (Transform child in obstacles_)
        {
            obstacles[i] = child.gameObject;
            i++;
        }


        MakeSingleton();

    }

    private float delayObstacle = 3;

    private float farBehindControl = 5;

    private void Update()
    {
        delayObstacle -= Time.deltaTime;
        if (delayObstacle <= 0)
        {
            PushTrap(obstacles);
            delayObstacle = 3;
        }


        farBehindControl -= Time.deltaTime;
        if(farBehindControl <= 0)
        {
            FarBehind(obstacles);
            farBehindControl = 5;
        }
        


    }


    private void FarBehind(GameObject[] traps)
    {
        foreach (GameObject t in traps)
        {
            if(t.transform.position.z<Player.Instance.transform.position.z 
                && Player.Instance.transform.position.z - t.transform.position.z > 3f)
            {
                StackTrap(t);
            }
        }
    }



    private void PushTrap(GameObject[] traps)
    {
        //Debug.Log("hi1");
        GameObject trap = null;
        foreach (GameObject t in traps)
        {
            if (t.transform.position.Equals(stackPos))
            {
                trap = t;
                break;
            }
        }
        if (trap == null)
            return;

        int rX = Random.Range(0, Xposes.Length);

        Vector3 pushPos = new Vector3(Xposes[rX], 0.8f, Player.Instance.transform.position.z + 50 + Random.Range(0, 5f));

        trap.transform.position = pushPos;
        //Debug.Log("hi2");

    }

    public void StackTrap(GameObject trap)
    {
        trap.transform.position = stackPos;
    }


}
