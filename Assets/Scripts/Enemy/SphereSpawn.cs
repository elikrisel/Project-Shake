using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{

    private GameObject enemyPf;

    private void Start()
    {
        enemyPf = GameObject.Find("Test");
    }

    public void Spawn()
    {
        Instantiate(enemyPf, new Vector3(0, 0, 0), Quaternion.identity);

    }
    
}
