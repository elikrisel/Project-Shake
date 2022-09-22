using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private float speed = 15f;

 


  private void OnEnable()
  {
    Invoke("Hide",1f);
  }

  private void Update()
  {
    transform.Translate(Vector3.forward * speed);
   

  }

  void Hide()
  {
    
    this.gameObject.SetActive(false);
    
  }
 
}
