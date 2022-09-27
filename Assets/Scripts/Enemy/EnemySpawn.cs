using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemySpawn : MonoBehaviour, ISpawn
{
    [SerializeField] private GameObject enemyPf;

    public void Spawn()
    {
        Vector3 position = Vector3.zero;

        Instantiate(enemyPf, new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }
}
