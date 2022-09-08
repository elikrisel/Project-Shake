using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum ChangeState
    {
        Start,
        MainMenu,
        Game,
        GameOver
        
        
    }

    private ChangeState changeState;

    private void Awake()
    {
        if (instance == null)
        {
            
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    private void Start()
    {
         changeState = ChangeState.Start;
    }

    void Update()
    {
        
        
    }

    public void State(ChangeState newState)
    {
        switch (newState)
        {
            case ChangeState.Start:
                Debug.Log("Starting the game");
                
                break;
            case ChangeState.MainMenu:
                Debug.Log("Entering main menu");
                break;
            case ChangeState.Game:
                Debug.Log("Starting the game");
                break;
            case ChangeState.GameOver:
                break;

        }
        
        
        
    }
    
    
}

