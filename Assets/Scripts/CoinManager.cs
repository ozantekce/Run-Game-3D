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


    private float delayGoldCoin = 1, delaySilverCoin=1, delayCopperCoin=1;

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

        int r = Random.Range(0, 3);

        Vector3 pushPos = new Vector3(Xposes[r],1.3f, Player.Instance.transform.position.z+ 50 +Random.Range(0,5f));

        coin.transform.position = pushPos;
        //Debug.Log("hi2");

    }

    public void StackCoin(GameObject coin)
    {
        coin.transform.position = stackPos;
    }



}
