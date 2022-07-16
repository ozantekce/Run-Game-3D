using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour
{

    private static Roads instance;

    private GameObject[] roads;

    private int firstIndex;
    private int lastIndex;

    private void Awake()
    {

        roads = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            roads[i] = transform.GetChild(i).gameObject;
        }    

        firstIndex = 0;
        lastIndex = roads.Length-1;

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



    /*
    public bool pushIt;

    private void Update()
    {
        if (pushIt)
        {
            pushIt = false;
            PushForward();
        }
    }*/

    public void PushForward()
    {

        roads[firstIndex].transform.position = roads[lastIndex].transform.position+ new Vector3(0,0,4);
        //Debug.Log("first : " + firstIndex);
        //Debug.Log("last  : " + lastIndex);
        lastIndex = firstIndex;
        firstIndex++;
        firstIndex %= roads.Length;
        

    }


}
