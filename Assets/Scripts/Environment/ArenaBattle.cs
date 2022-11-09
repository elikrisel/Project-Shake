using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;

public class ArenaBattle : MonoBehaviour
{
  [SerializeField] TriggerArea triggerArea;
  [SerializeField] EnemySpawn spawnEnemyPf;
  [SerializeField] ShowGun showGun;

  //public List<Vector3> spawnPositionList;
  
  public bool enemiesLeft;

  public event EventHandler OnBattleStarted;
  public event EventHandler OnBattleEnded;

  public int timerToSpawn;
  
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
    /*spawnPositionList = new List<Vector3>();
    foreach (Transform spawnPosition in transform.Find("SpawnPositions"))
    {
      spawnPositionList.Add(spawnPosition.position);
    }
    */
  }


  void Start()
  {
    
    triggerArea.OnPlayerTrigger += TriggerAreaOnOnPlayerTrigger;
    

  }

  private void Update()
  {
    Debug.Log(state);
    //Debug.Log("Spawn: " + spawnPositionList);
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
    //Vector3 spawnPosition = spawnPositionList[Random.Range(0, spawnPositionList.Count)];
    var spawnPosition = transform.position + new Vector3(0,0,32);
    
    EnemySpawn enemySpawner = Instantiate(spawnEnemyPf, spawnPosition, Quaternion.identity);
    enemySpawner.Spawn();

    

  }
  
}
