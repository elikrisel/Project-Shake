using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArenaBattle : MonoBehaviour
{
  [SerializeField] TriggerArea triggerArea;
  

  [SerializeField] ShowGun showGun;

  [SerializeField]private List<Vector3> enemySpawnList;
  
  [SerializeField] private EnemySpawn[] spawnEnemy;
  
  
  public bool enemiesLeft;
  public bool arenaActivated = false;

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
    enemySpawnList = new List<Vector3>();
    foreach (Vector3 es in enemySpawnList)
    {
      enemySpawnList.Add(es);
      
    }
    
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

    
    //Debug.Log("Arena commencing");
    arenaActivated = true;
    state = StateOfGame.Active;

    foreach (EnemySpawn e in spawnEnemy)
    {
      if (e != null)
      {
        e.Spawn();
        

      }
      else
      {
        EndBattle();
      }
       
      //Debug.Log(e);
      
      
    }
    
    
    
    OnBattleStarted?.Invoke(this, EventArgs.Empty);
    if (showGun.gunSelected)
    {
      return;
    }
    showGun.gunState = ShowGun.GunList.RightGun;  
    

  }

  public void EndBattle()
  {
         
    state = StateOfGame.End;
    showGun.gunState = ShowGun.GunList.NoGun;
    OnBattleEnded?.Invoke(this,EventArgs.Empty);
    


  }

  

  
}
