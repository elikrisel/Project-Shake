using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowGun : MonoBehaviour
{
        [Header("Gun GameObjects")] 
        public GameObject gun_1 , gun_2, backPack_L,backPack_R;

        public bool gunSelected;

        public Transform firePoint;

        public enum GunList
        {
                NoGun,
                LeftGun,
                RightGun
        }
        

        public GunList gunState;
        
        private void Start()
        {
                gunState = GunList.NoGun;
        }

        private void Update()
        {
                
                
                switch (gunState)
                {
                        case GunList.NoGun:
                                gun_1.SetActive(false);
                                gun_2.SetActive(false);
                                break;
                        case GunList.LeftGun:
                                gunSelected = true;
                                Left();
                                break;
                        case GunList.RightGun:
                                gunSelected = true;
                                Right();
                                break;
                }
                
                
        }
        public void Shooting()
        {
            
            Ray ray = new Ray(firePoint.position, firePoint.forward);
            RaycastHit hit;

            float distance = 20f;

            if(Physics.Raycast(ray,out hit, distance))
            {
                distance = hit.distance;

            }
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 1);

        }

        public void Left()
        {
                gun_1.SetActive(true);
                gun_2.SetActive(false);
                backPack_L.SetActive(false);
                backPack_R.SetActive(true);
        }

        public void Right()
        {
                gun_1.SetActive(false);
                gun_2.SetActive(true);
                backPack_L.SetActive(true);
                backPack_R.SetActive(false);
        }
        
        
}
