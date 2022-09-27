using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawn : MonoBehaviour
{
        [SerializeField] private GameObject startDoor;
        [SerializeField] private GameObject endDoor;


        


        public void StartDoor()
        {
                startDoor.SetActive(true);
                
                
        }

        public void EndDoor()
        {
                
                endDoor.SetActive(false);
                
                
        }
        
        
}
