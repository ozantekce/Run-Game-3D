using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{


    private GameObject[] goldCoins;
    private GameObject[] silverCoins;
    private GameObject[] copperCoins;

    private Vector3 stackPos = new Vector3(100f, 100f, 100f);



    private static CoinManager instance;

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

    private void Awake()
    {

        Transform goldCoins_ = transform.GetChild(0);
        goldCoins = new GameObject[goldCoins_.childCount];
        int i = 0;
        foreach (Transform child in goldCoins_)
        {
            goldCoins[i] = child.gameObject;
            i++;
        }


        Transform silverCoins_ = transform.GetChild(1);
        silverCoins = new GameObject[silverCoins_.childCount];
        i = 0;
        foreach (Transform child in silverCoins_)
        {
            silverCoins[i] = child.gameObject;
            i++;
        }


        Transform copperCoins_ = transform.GetChild(2);
        copperCoins = new GameObject[copperCoins_.childCount];
        i = 0;
        foreach (Transform child in copperCoins_)
        {
            copperCoins[i] = child.gameObject;
            i++;
        }

        MakeSingleton();

    }

    private float[] Xposes = { -2, 0, 2 };
    private float[] Yposes = { 1.3f,1.3f,1.3f,1.3f,3.5f };


    private float delayGoldCoin = 1, delaySilverCoin=1, delayCopperCoin=1;


    private float farBehindControl = 5;
    public static CoinManager Instance { get => instance; set => instance = value; }

    private void Update()
    {
        delayGoldCoin -= Time.deltaTime;
        if (delayGoldCoin <= 0)
        {
            PushCoin(goldCoins);
            delayGoldCoin = 3;
        }
        delaySilverCoin -= Time.deltaTime;
        if (delaySilverCoin <= 0)
        {
            PushCoin(silverCoins);
            delaySilverCoin = 2;
        }
        delayCopperCoin -= Time.deltaTime;
        if (delayCopperCoin <= 0)
        {
            PushCoin(copperCoins);
            delayCopperCoin = 1f;
        }


        farBehindControl -= Time.deltaTime;
        if (farBehindControl <= 0)
        {
            FarBehind(goldCoins);
            FarBehind(silverCoins);
            FarBehind(copperCoins);
            farBehindControl = 5;
        }

    }



    private void FarBehind(GameObject[] coins)
    {
        foreach (GameObject t in coins)
        {
            if (t.transform.position.z < Player.Instance.transform.position.z
                && Player.Instance.transform.position.z - t.transform.position.z > 3f)
            {
                StackCoin(t);
            }
        }
    }


    private void PushCoin(GameObject[] coins)
    {
        //Debug.Log("hi1");
        GameObject coin = null;
        foreach(GameObject c in coins)
        {
            if (c.transform.position.Equals(stackPos))
            {
                coin = c;
                break;
            }
        }
        if (coin == null)
            return;

        int rX = Random.Range(0, Xposes.Length);
        int rY = Random.Range(0, Yposes.Length);

        Vector3 pushPos = new Vector3(Xposes[rX],Yposes[rY], Player.Instance.transform.position.z+ 50 +Random.Range(0,5f));

        coin.transform.position = pushPos;
        //Debug.Log("hi2");

    }

    public void StackCoin(GameObject coin)
    {
        coin.transform.position = stackPos;
    }



}
