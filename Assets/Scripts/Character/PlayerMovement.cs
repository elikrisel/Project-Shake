using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [Header("Components")]
  private PlayerHandler input;
  private Rigidbody rb;
  //[SerializeField] private Transform playerArt;
  
  [Header("Adjustable Variables")]
  [SerializeField] private float moveSpeed = 10f;

  [SerializeField] private float rotateSpeed = 50f;
  

  private void Awake()
  {
    input = GetComponent<PlayerHandler>();
    rb = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    var playerMove = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
    
    //Move
    var moveDirection = MoveTowardsTarget(playerMove);

    //Rotate
    Rotation(moveDirection);


  }

  private void Rotation(Vector3 moveDirection)
  {
    if (moveDirection != Vector3.zero)
    {
      var rotation = Quaternion.LookRotation(moveDirection);
      transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
  
    }
    
  }

  private Vector3 MoveTowardsTarget(Vector3 direction)
  {
    var speed = moveSpeed * Time.deltaTime;
    
    var targetPosition = transform.position + direction * speed;
    transform.position = targetPosition;
    targetPosition = direction.normalized;

    
    rb.AddForce(targetPosition * speed, ForceMode.Impulse);

    return direction;


  }
  
}
