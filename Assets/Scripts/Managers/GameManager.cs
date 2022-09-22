using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;       
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    private void Start()
    {
         UpdateState(GameState.Start);
    }

    void Update()
    {
        Debug.Log("Current State: " + gameState);
        
    }
    
    
    public enum GameState
    {
        Start,
        MainMenu,
        Game,
        GameOver
        
        
    }
    

    public void UpdateState(GameState newState)
    {
        gameState = newState;
        
        switch (newState)
        {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.MainMenu:
                HandleMenu();
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;

        }

        OnGameStateChanged?.Invoke(newState);

    }
    

    private void HandleStart()
    {
        SceneManager.LoadSceneAsync("MAINMENU");
        UpdateState(GameState.MainMenu);
    }

    private void HandleMenu()
    {
        
    }

    private void HandleGame()
    {
        
        
    }

    private void HandleGameOver()
    {
        
        
    }
 
    
    
    

}

