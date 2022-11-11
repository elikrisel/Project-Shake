using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;

public class ArenaBattle : MonoBehaviour
{
  [SerializeField] TriggerArea triggerArea;
  
  [SerializeField] ShowGun showGun;

  [SerializeField] private EnemySpawn enemyPf;
  
  public bool enemiesLeft;

  public event EventHandler OnBattleStarted;
  public event EventHandler OnBattleEnded;

  //public int timerToSpawn;
  
  private enum StateOfGame
  {
    Idle,
    Active,
    End,
    
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
    enemiesLeft = true;
    OnBattleStarted?.Invoke(this, EventArgs.Empty);
    if (showGun.gunSelected)
    {
      return;
    }
    showGun.gunState = ShowGun.GunList.RightGun;  

        
      
    
    
    SpawnEnemy();  
     
    
    if (enemiesLeft)
    {
      EndBattle();
      
    }

  }

  public void EndBattle()
  {
         
    state = StateOfGame.End;
    showGun.gunState = ShowGun.GunList.NoGun;
    OnBattleEnded?.Invoke(this,EventArgs.Empty);
      

  }


  private void SpawnEnemy()
  {
    Vector3 spawnPosition = new Vector3(0, 0, -103);
    
    enemyPf = Instantiate(enemyPf, spawnPosition, Quaternion.identity);



  }
  
}
