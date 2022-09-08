using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    [Header("Private Components")]
    //private PlayerControls playerControls;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private GameObject characterArt;
    
    //private Vector2 moveInput = Vector2.zero;

    [Header("Adjustable Variables")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerMask;


    private void Awake()
    {
        //playerControls = GetComponent<PlayerControls>();
        rb = GetComponent<Rigidbody>();
        characterArt = transform.Find("Art").gameObject;

    }

    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");

        
        moveDirection = new Vector3(moveX, moveDirection.y, moveZ) * Time.deltaTime;  
        

    }

    private void FixedUpdate()
    {
        Movement(moveDirection);
        
    }

    private void Movement(Vector3 move)
    {

        rb.AddForce(new Vector3(move.x * moveSpeed, move.y, move.z * moveSpeed), ForceMode.Impulse);


    }
    

    #region Updating when implementing new Input System
    // private void OnEnable()
    // {
    //     playerControls.Player.Movement.enabled += DoMove;
    // }
    //
    //
    //
    // private void OnDisable()
    // {
    //     
    // }
    #endregion
    
}
