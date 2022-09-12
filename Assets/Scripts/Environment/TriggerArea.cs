using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    
    public event EventHandler OnPlayerTrigger;
    
    private void OnTriggerEnter(Collider other)
    {

        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null)
        {
            OnPlayerTrigger?.Invoke(this, EventArgs.Empty);
            

        }
            
    }
}
