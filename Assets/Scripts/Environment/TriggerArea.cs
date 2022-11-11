using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [HideInInspector]
    public Camera ortoCamera;
    public event EventHandler OnPlayerTrigger;

    [SerializeField] private ArenaBattle arenaBattle;
    

    private void Awake()
    {
        ortoCamera = Camera.main;
    }
    
    void Update()
    {
        
        Debug.Log("Ortographic size: " + ortoCamera.orthographicSize);

        if (arenaBattle.enemiesLeft)
        {
            ortoCamera.orthographicSize = 20;    
            
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {

        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null)
        {
            OnPlayerTrigger?.Invoke(this, EventArgs.Empty);
            ortoCamera.orthographicSize = 20;

        }
            
    }
    
    
    
}
