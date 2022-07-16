using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class RoadBuilder : MonoBehaviour
{

    void Start()
    {
        
    }


    public bool build=false;

    void Update()
    {
        if (build)
        {
            Build(40);
            build = false;
        }
    }


    public GameObject roadPrefab;

    private float firstZ=-40;
    private void Build(int num)
    {

        float x=0, y =0, z = firstZ;

        for (int i = 0; i < num; i++)
        {

            GameObject road = Instantiate(roadPrefab);
            
            road.transform.position = new Vector3(x,y,z);
            z += 4;
            road.transform.parent = transform;
            road.name = "Road "+i;
        }

    }




}
