using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerArea : MonoBehaviour
{
    [HideInInspector]
    public Camera orthoCamera;
    public event EventHandler OnPlayerTrigger;
    public GameObject environmentText;
    [SerializeField] private float timeBeforeDisable = 10f;
    
    
    [SerializeField] private ArenaBattle arenaBattle;

   

    private void Awake()
    {
        orthoCamera = Camera.main;
        environmentText.SetActive(false);
    }
    
    void Update()
    {
        Debug.Log("Time before disable: " + timeBeforeDisable);
        //Debug.Log("Orthographic size: " + orthoCamera.orthographicSize);

        if (arenaBattle.arenaActivated)
        {
            if (timeBeforeDisable > 0)
            {
                timeBeforeDisable -= Time.deltaTime;
            }
            else
            {
                
                environmentText.SetActive(false);
                
            }
            
            
            
        }
            
         

    }
        
        
        
        
    


    private void OnTriggerEnter(Collider other)
    {

        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null)
        {
            OnPlayerTrigger?.Invoke(this, EventArgs.Empty);
            orthoCamera.orthographicSize = 70;
            environmentText.SetActive(true);

        }
            
    }
    
    
    
}
