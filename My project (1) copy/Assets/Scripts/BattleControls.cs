using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BattleControls : MonoBehaviour
{
    private PlayerInputActions inputActions;
    [SerializeField] private BattleUI battleUI;
    void Awake(){
        inputActions = new PlayerInputActions(); 
    }

    void OnEnable()
    {
        inputActions.PlayerMenu.DebugUI.performed += debugDisplay; // Register listener
        inputActions.PlayerMenu.Select.performed += SelectPressed;
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.PlayerMenu.DebugUI.performed -= debugDisplay; // Deregister listener
        inputActions.PlayerMenu.Select.performed -= SelectPressed;
        inputActions.Disable();
    }

    void debugDisplay(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        battleUI.ToggleUI();
    }

    void
    SelectPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //battleUI.SelectPressed();
    }
}
