using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPf;
    
    public void Spawn()
    {
        enemyPf.SetActive(true);
    }
}
