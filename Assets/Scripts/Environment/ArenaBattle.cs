using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArenaBattle : MonoBehaviour
{
  [SerializeField] TriggerArea triggerArea;
  [SerializeField] GameObject enemySpawn;
  

  private enum StateOfGame
  {
    Idle,
    Active,
    End,
    Complete
    
  }

  private StateOfGame state;

  private void Awake()
  {
    state = StateOfGame.Idle;
    
  }


  void Start()
  {
    
    triggerArea.OnPlayerTrigger += TriggerAreaOnOnPlayerTrigger;
    

  }

  private void Update()
  {
    Debug.Log(state);
    
  }

  private void TriggerAreaOnOnPlayerTrigger(object sender, EventArgs e)
  {
    if (state == StateOfGame.Idle)
    {
      
      StartBattle();
      triggerArea.OnPlayerTrigger -= TriggerAreaOnOnPlayerTrigger; 

      
    }

  }

  void StartBattle()
  {
    
    Debug.Log("Arena commencing");
    state = StateOfGame.Active;
    enemySpawn.GetComponent<CubeSpawn>().Spawn();
   

  }
  
  
}
