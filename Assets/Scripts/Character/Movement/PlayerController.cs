using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, PlayerControls.IPlayer_GamepadActions, PlayerControls.IPlayer_KBMActions
{
   [Header("Components")]
   private PlayerControls input;
   private Rigidbody rb;
   [SerializeField] private Transform playerArt;

   
   [Header("Input")]
   private Vector2 readInput;
   private Vector3 lookDirection = Vector3.zero;
   private Vector2 moveDirection = Vector2.zero;
   private InputAction buttonPrompt;

    public float lookSpeed = 180f;
    public float moveSpeed = 1.0f;
    public float maxSpeed = 20f;
    public float dampenForce = 4f;
    public float maxDampen = 4f;


    #region State
    public enum ControllerState
   {
      Keyboard,
      Gamepad
      
   }

   public ControllerState state;
    #endregion


    

   [Header("Acceleration")]
   private Vector3 acceleration;
   
   public bool usingGamepad;
   

   #region Enable and Disable
   private void OnEnable()
   {
      if (input == null)
      {
         input = new PlayerControls();
         input.Player_Gamepad.SetCallbacks(this);
         input.Player_KBM.SetCallbacks(this);
         input.Enable();
      }
   }

   private void OnDisable()
   {
      if (input != null)
      {
         input.Disable();
         input = null;

      }
   }
   #endregion


   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      switch (state)
      {
         case ControllerState.Keyboard:
            usingGamepad = false;
            break;
         case ControllerState.Gamepad:
            usingGamepad = true;
            break;
         
         
      }
      
      LookRotation();
      

   }

   
   private void FixedUpdate()
   {
      if (readInput != Vector2.zero)
      {
         Movement(moveDirection);
         

      }
      
      
   }


   #region Movement interface
   void PlayerControls.IPlayer_GamepadActions.OnMove(InputAction.CallbackContext ctx)
   {
      
         readInput = ctx.ReadValue<Vector2>();
         moveDirection = readInput;

         state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;
         
         
   }

   void PlayerControls.IPlayer_KBMActions.OnMove(InputAction.CallbackContext ctx)
   {
      readInput = ctx.ReadValue<Vector2>();
      moveDirection = readInput;

      state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;
      

   }

   private void Movement(Vector2 moveDirection)
   {
        this.moveDirection = moveDirection;
        var dampener = Mathf.Clamp(rb.velocity.x * dampenForce, -maxDampen, maxDampen);
        var verticalforce = Mathf.Clamp(rb.velocity.z * dampenForce, -maxDampen, maxDampen);
        rb.AddForce(new Vector3((moveDirection.x * moveSpeed) - dampener, 0, (moveDirection.y * moveSpeed) - verticalforce));

        Debug.Log(dampener);
        Debug.Log(verticalforce);

    }
   
   #endregion
   
   #region Look Interface
   void PlayerControls.IPlayer_KBMActions.OnLook(InputAction.CallbackContext ctx)
   {
      readInput = ctx.ReadValue<Vector2>();
      lookDirection = readInput;

      state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;

      

   }
   
   void PlayerControls.IPlayer_GamepadActions.OnLook(InputAction.CallbackContext ctx)
   {
      readInput = ctx.ReadValue<Vector2>();
      lookDirection = readInput;

      state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;

    

   }

   private void LookRotation()
   {
      
     

      

   }
   
   #endregion
   
   #region Shoot Interface
   void PlayerControls.IPlayer_KBMActions.OnShoot(InputAction.CallbackContext ctx)
   {
      var buttonPress = input.Player_Gamepad.Shoot;
      buttonPrompt = buttonPress;
      
      if (ctx.action.actionMap.name == "Player_KBM")
      {
         state = ControllerState.Keyboard;

      }
      else
      {
         state = ControllerState.Gamepad;
      }
      Shoot();
   }
   void PlayerControls.IPlayer_GamepadActions.OnShoot(InputAction.CallbackContext ctx)
   {
      state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;

      Shoot();
   }

   private void Shoot()
   {
      
   }
   
   #endregion
   
   #region Interact interface
   
   void PlayerControls.IPlayer_KBMActions.OnInteract(InputAction.CallbackContext ctx)
   {
      state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;

      Interact();
   }
   void PlayerControls.IPlayer_GamepadActions.OnInteract(InputAction.CallbackContext ctx)
   {
      var buttonPress = input.Player_Gamepad.Interact;
      buttonPrompt = buttonPress;
      
      if (ctx.action.actionMap.name == "Player_Gamepad")
      {
         state = ControllerState.Gamepad;

      }
      else
      {
         state = ControllerState.Keyboard;
      }
      Interact();
      
   }

   void Interact()
   {
      
      
   }
   #endregion
   
}
