using System;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, PlayerControls.IPlayer_GamepadActions, PlayerControls.IPlayer_KBMActions
{
   [Header("Components")]
   private PlayerControls input;
   private Rigidbody rb;
   public ShowGun showGun;
   public GunScript gun;
   

   [Header("UI")]
   public GameObject environmentTextKBM;
   public GameObject environmentTextGamepad;
   
   
   [SerializeField] private LayerMask clippingLayer;
   [SerializeField] private Transform orientation;
   [SerializeField] private Camera mainCamera;
   
   [Header("Input")]
   private Vector2 readInput;
   private Vector2 lookInput;
   private Vector2 moveInput;
   private InputAction buttonPrompt;


   [Header("Adjustable Variables")] public float moveSpeed = 1.0f;
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
    public bool buttonPressed;
   

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
            environmentTextKBM.SetActive(true);
            environmentTextGamepad.SetActive(false);
            usingGamepad = false;
            break;
         case ControllerState.Gamepad:
            environmentTextKBM.SetActive(false);
            environmentTextGamepad.SetActive(true);
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
      if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clippingLayer, QueryTriggerInteraction.Collide))
      {
         targetDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
         
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

        state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;

        Shoot();
   }
   void PlayerControls.IPlayer_GamepadActions.OnShoot(InputAction.CallbackContext ctx)
   {
      state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;

      Shoot();
   }

   private void Shoot()
   {
      

        if(showGun.gunSelected)
        {
            gun.Shooting();
        }
        
   }
   
   #endregion
   
   #region Interact interface
   
   void PlayerControls.IPlayer_KBMActions.OnSwitchWeapon(InputAction.CallbackContext ctx)
   {

        var buttonPress = input.Player_KBM.SwitchWeapon;
        buttonPrompt = buttonPress;

        state = ctx.action.actionMap.name == "Player_KBM" ? ControllerState.Keyboard : ControllerState.Gamepad;

      
      Interact();
   }
   void PlayerControls.IPlayer_GamepadActions.OnSwitchWeapon(InputAction.CallbackContext ctx)
   {


        var buttonPress = input.Player_Gamepad.SwitchWeapon;
        buttonPrompt = buttonPress;


        state = ctx.action.actionMap.name == "Player_Gamepad" ? ControllerState.Gamepad : ControllerState.Keyboard;

        Interact();
      
   }

   void Interact()
   {
        if(!buttonPressed)
        {
           
            showGun.gunState = ShowGun.GunList.LeftGun;
            gun.gunType = GunScript.GunType.Shotgun;
            buttonPressed = true;
        }
        else
        {
            showGun.gunState = ShowGun.GunList.RightGun;
            gun.gunType = GunScript.GunType.Rifle;
            buttonPressed = false;
        }

   }


   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Saboteur"))
      {
         CameraShake.instance.ShakeCamera();
      }
   }

   #endregion
   
}
