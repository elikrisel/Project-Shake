using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArenaBattle : MonoBehaviour
{
  [SerializeField] TriggerArea triggerArea;
  [SerializeField] GameObject spawnEnemy;

  
  public bool enemiesLeft;

  public event EventHandler OnBattleStarted;
  public event EventHandler OnBattleEnded;
  
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
    //enemiesLeft = true;
    OnBattleStarted?.Invoke(this, EventArgs.Empty);
    spawnEnemy.GetComponent<EnemySpawn>().Spawn();

    if (enemiesLeft)
    {
      EndBattle();
      
    }

  }

  public void EndBattle()
  {
         
    state = StateOfGame.End; 
      OnBattleEnded?.Invoke(this,EventArgs.Empty);
      

  }
  
}
