using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private GameObject entranceDoor;
    [SerializeField] private GameObject endDoor;
    [SerializeField] private ArenaBattle arenaBattle;

    
    
    
    private void Start()
    {
        arenaBattle.OnBattleStarted += ArenaBattleOnOnBattleStarted;
        arenaBattle.OnBattleEnded += ArenaBattleOnOnBattleEnded; 
    }

    private void ArenaBattleOnOnBattleEnded(object sender, EventArgs e)
    {
        endDoor.SetActive(false);
        
    }

    private void ArenaBattleOnOnBattleStarted(object sender, EventArgs e)
    {
        entranceDoor.SetActive(true);
        
        
    }
}
