using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameStateChanged;
    }


    private void GameStateChanged(GameManager.GameState state)
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GAME");
        GameManager.instance.UpdateState(GameManager.GameState.Game);
        

    }

    public void Quit()
    {
        Debug.Log("Quitting game");
        Application.Quit();


    }
    
    
    
}
