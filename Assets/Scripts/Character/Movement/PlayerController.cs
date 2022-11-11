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
   
   [SerializeField]
   private Transform orientation;

   [SerializeField] private Transform playerArt;
   
   
   
   [SerializeField] private Camera mainCamera;
   
   [Header("Input")]
   private Vector2 readInput;
   private Vector2 lookInput;
   private Vector2 moveInput;
   private InputAction buttonPrompt;
   
   
   [Header("Adjustable Variables")]
    public float moveSpeed = 1.0f;
    public float maxSpeed = 20f;
    public float dampenForce = 4f;
    public float maxDampen = 4f;
    public float controllerDeadZone = 0.1f;
    public float rotateSmoothness = 1000f;

    #region State
    public enum ControllerState
   {
      Keyboard,
      Gamepad
      
   }

   public ControllerState state;
    #endregion


    

   [Header("Conditions")]
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
      // Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      // //set orientation
      // Vector3 viewDir = transform.position - new Vector3(mouseWorldPos.x, transform.position.y, mouseWorldPos.y);
      // orientation.forward = viewDir.normalized;
      
   }

   
   private void FixedUpdate()
   {
      if (readInput != Vector2.zero)
      {
         Movement(moveInput);
         

      }
      
      
   }
    #region DrawGizmos
    private void OnDrawGizmos()
   {
      Gizmos.color = Color.blue;
      Gizmos.DrawLine(transform.position, orientation.forward);
      
      Ray ray = mainCamera.ScreenPointToRay(readInput);

      if (Physics.Raycast(ray, out RaycastHit hit, 100f))
      {
         Gizmos.DrawLine(mainCamera.transform.position, hit.point);
      }
   }
    #endregion

    #region Movement interface
    void PlayerControls.IPlayer_GamepadActions.OnMove(InputAction.CallbackContext ctx)
   {
      
         readInput = ctx.ReadValue<Vector2>();
         moveInput = readInput;

         state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;
         
         
   }

   void PlayerControls.IPlayer_KBMActions.OnMove(InputAction.CallbackContext ctx)
   {
      readInput = ctx.ReadValue<Vector2>();
      moveInput = readInput;

      state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;
      

   }

   private void Movement(Vector2 moveDirection)
   {
        this.moveInput = moveDirection;
        var horizontalDampener = Mathf.Clamp(rb.velocity.x * dampenForce, -maxDampen, maxDampen);
        var verticalDampener = Mathf.Clamp(rb.velocity.z * dampenForce, -maxDampen, maxDampen);
        rb.AddForce(new Vector3((moveDirection.x * moveSpeed) - horizontalDampener, 0, (moveDirection.y * moveSpeed) - verticalDampener));
      
    }
   
   #endregion
   
   #region Look Interface
   void PlayerControls.IPlayer_KBMActions.OnLook(InputAction.CallbackContext ctx)
   {

      readInput = ctx.ReadValue<Vector2>();
      lookInput = readInput;
      Ray ray = mainCamera.ScreenPointToRay(readInput);

      Vector3 targetDirection = Vector3.zero;
      if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
      {
         targetDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
         print("look target: " + targetDirection);
      }
      
      state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;

      LookRotation(targetDirection);

   }

   void PlayerControls.IPlayer_GamepadActions.OnLook(InputAction.CallbackContext ctx)
   {
      readInput = ctx.ReadValue<Vector2>();
      lookInput = readInput;
      
       if(Mathf.Abs(readInput.x) > controllerDeadZone || Mathf.Abs(readInput.y) > controllerDeadZone)
       {

            Vector3 targetDirection = Vector3.right * readInput.x + Vector3.forward * readInput.y;
            if(targetDirection.sqrMagnitude > 0.0f)
            {
                Quaternion newRot = Quaternion.LookRotation(targetDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, rotateSmoothness * Time.deltaTime);
            }

       }
        


      
      state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;
      
      

}

   private void LookRotation(Vector3 direction)
   {

      transform.LookAt(direction);

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
   
   void PlayerControls.IPlayer_KBMActions.OnSwitchWeapon(InputAction.CallbackContext ctx)
   {
      state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;
   
      if (ctx.action.actionMap.name == "Player_KBM")
      {
         state = ControllerState.Keyboard;

      }
      else
      {
         state = ControllerState.Gamepad;
      }
      
      Interact();
   }
   void PlayerControls.IPlayer_GamepadActions.OnSwitchWeapon(InputAction.CallbackContext ctx)
   {

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
