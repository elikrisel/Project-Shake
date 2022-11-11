using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
   public int gunId;

   public enum GunType
   {
      Shotgun,
      Rifle
      
      
   }

   public GunType gunType;
   
   [SerializeField] private Transform firePoint;
   [SerializeField] private float distance = 20f;
   
   
   public void Shooting()
   {
          
      Ray ray = new Ray(firePoint.position, firePoint.forward);
      RaycastHit hit;

      if(Physics.Raycast(ray,out hit, distance))
      {
         distance = hit.distance;

      }
      Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 1);

   }
   
   
   
   
   
   
}
