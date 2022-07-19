using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

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


    private GameStatus lastStatus;
    private GameStatus gameStatus;


    private void Start()
    {
       
        GameStatus = GameStatus.menu;
        lastStatus = gameStatus;
    
    }

    private void Update()
    {
        
    }


    private void StatusChange()
    {

        if(lastStatus != gameStatus)
        {
            if(gameStatus == GameStatus.menu)
            {

            }


        }

    }



    public GameStatus GameStatus { get => gameStatus; set 
        {
            gameStatus = value;
            StatusChange();
        } 
    }


}

public enum GameStatus
{
    menu,preparing,running,paused,died
}
