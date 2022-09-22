using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private Camera ortoCamera;
    public event EventHandler OnPlayerTrigger;

    private void Awake()
    {
        ortoCamera = Camera.main;
    }

    void Start()
    {

        ortoCamera.orthographicSize = 5;


    }

    void Update()
    {
        
        Debug.Log("Ortographic size: " + ortoCamera.orthographicSize);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerHandler player = other.GetComponent<PlayerHandler>();
        
        if (player != null)
        {
            OnPlayerTrigger?.Invoke(this, EventArgs.Empty);
            ortoCamera.orthographicSize = 10;

        }
            
    }
}
