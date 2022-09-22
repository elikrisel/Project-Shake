using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectpool : MonoBehaviour
{
    public static Objectpool instance;
    private List<GameObject> objectPooler;
    [SerializeField] private int pooledAmount;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectPooler = new List<GameObject>();
        GameObject bullet;
        for (int i = 0; i < pooledAmount; i++)
        {
            bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            objectPooler.Add(bullet);


        }
    }


    public GameObject GetPooledObject()
    {

        for (int i = 0; i < pooledAmount; i++)
        {

            if (!objectPooler[i].activeInHierarchy)
            {
                return objectPooler[i];
            }
            
            
        }

        return null;

    }
    
    
}
