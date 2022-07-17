using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{


    private static ScoreManager instance;

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

    private int score;
    private Text scoreText;
    public int Score {
        get => score;
        set { 
            score = value;
            if(scoreText == null)
            {
                scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            }
            scoreText.text = score.ToString();
        }
    }

    public static ScoreManager Instance { get => instance; set => instance = value; }
}
