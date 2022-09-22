using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Bindings;


public class PlayerController : MonoBehaviour
{
    [Header("Private Components")]
    //private PlayerControls playerControls;
    private Rigidbody rb;
    private Vector3 moveDirection;
    
    [SerializeField] Transform characterArt;
    
    //private Vector2 moveInput = Vector2.zero;
    private float moveX;
    private float moveY;

    [Header("Adjustable Variables")] 
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private LayerMask layerMask;

    
    
    private void Awake()
    {
       // playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        
    }
    
    #region Need to read up on New Input System
    /*
    private void OnEnable()
    {
        playerControls.Player.Movement.performed += DoMove;

    }


    private void OnDisable()
    {
        playerControls.Player.Movement.canceled -= DoMove;

    }
    
    
    private void DoMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

       
        

    }
    */
    #endregion

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        
      
    }
    
    
    
    
    
    private void FixedUpdate()
    {
        
        moveDirection = new Vector3(moveX, moveDirection.y, moveY) * Time.fixedDeltaTime;

        
        Movement(moveDirection);
        
    }

    private void Movement(Vector3 move)
    {

        rb.AddForce(new Vector3(move.x * moveSpeed, move.y, move.z * moveSpeed), ForceMode.Impulse);
    

    }

    private void Rotation()
    {
        
        
        
    }

}
