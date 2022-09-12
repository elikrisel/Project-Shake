using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawn : MonoBehaviour, ISpawn
{

    [SerializeField] private GameObject enemyPf;
    
        
    public void Spawn()
    {
        
        Instantiate(enemyPf, new Vector3(0,0,0), Quaternion.identity);


    }

  
}
