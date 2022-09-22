using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    
    public event EventHandler OnPlayerTrigger;
    
    private void OnTriggerEnter(Collider other)
    {

        PlayerHandler player = other.GetComponent<PlayerHandler>();
        
        if (player != null)
        {
            OnPlayerTrigger?.Invoke(this, EventArgs.Empty);
            

        }
            
    }
}
