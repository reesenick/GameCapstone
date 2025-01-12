// using UnityEngine;
// using UnityEngine.InputSystem;

// using System.Collections;
// using System.Collections.Generic;

// public class PlayerController : MonoBehaviour
// {
//     #region Variables

//     [Header("Player Input")]
//     private PlayerInputActions playerInputActions; // This is the object that listens for inputs at the hardware level
//     private Vector2 movementInput;



//     [Header("Component / Object References")]
//     [SerializeField] private BattleUI BattleUI;

//     #endregion

//     #region Unity Functions

//     // Awake is called before Start() when an object is created or when the level is loaded
//     private void Awake()
//     {
//         // Set up our player actions in code
//         // This class name is based on what you named your .inputactions asset
//         playerInputActions = new PlayerInputActions();
//     }

//     private void Start()
//     {
//         // GameState callback listeners
//         GameState.Instance.OnGamePaused.AddListener(OnGamePausedReceived);
//         GameState.Instance.OnGameResumed.AddListener(OnGameResumedReceived);
        
//         GameState.Instance.OnPlayerLost.AddListener(ReceivedOnPlayerLost);
//         GameState.Instance.OnPlayerWon.AddListener(ReceivedOnPlayerWon);
//     }

//     private void OnEnable()
//     {
//         // Here we can subscribe functions to our
//         // input actions to make code occur when
//         // our input actions occur
//         SubscribeInputActions();

//         // We need to enable our "Player" action map so Unity will listen for our input
//         SwitchActionMap("Player");
//     }

//     private void OnDisable()
//     {
//         // Here we can unsubscribe our functions
//         // from our input actions so our object
//         // doesn't try to call functions after
//         // it is destroyed
//         UnsubscribeInputActions();

//         // Disable all action maps
//         SwitchActionMap();
//     }

//     private void OnEnable()
//     {
//         inputActions.PlayerMenu.Enable();

//         inputActions.PlayerMenu.Move.performed += ctx => HandleMove(ctx);
//         inputActions.PlayerMenu.Move.canceled += ctx => HandleMove(ctx);
//         inputActions.PlayerMenu.Select.performed += ctx => HandleSelect();
//         inputActions.PlayerMenu.Back.performed += ctx => HandleBack();
//         inputActions.PlayerMenu.DebugUI.performed += ctx => HandleDebugUI();
//     }

//     private void OnDisable()
//     {
//         inputActions.PlayerMenu.Disable();

//         inputActions.PlayerMenu.Move.performed -= ctx => HandleMove(ctx);
//         inputActions.PlayerMenu.Move.canceled -= ctx => HandleMove(ctx);
//         inputActions.PlayerMenu.Select.performed -= ctx => HandleSelect();
//         inputActions.PlayerMenu.Back.performed -= ctx => HandleBack();
//         inputActions.PlayerMenu.DebugUI.performed -= ctx => HandleDebugUI();
//     }

//     private void HandleMove(InputAction.CallbackContext context)
//     {
//         OnMove?.Invoke(context.ReadValue<Vector2>());
//     }

//     private void HandleSelect()
//     {
//         OnSelect?.Invoke();
//     }

//     private void HandleBack()
//     {
//         OnBack?.Invoke();
//     }

//     private void HandleDebugUI()
//     {
//         OnDebugUI?.Invoke();
//     }
// }

