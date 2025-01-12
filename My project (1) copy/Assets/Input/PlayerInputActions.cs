//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerMenu"",
            ""id"": ""cc5d6ee3-0f01-499e-96ac-eb1261cd1ed9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0ca6e50d-950f-4df0-b0d3-8dbb1a177ebf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""8056fe2c-0bb9-433b-82b3-928575fb9a93"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""3e34c502-222b-4ece-8a5d-e3bc90077650"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DebugUI"",
                    ""type"": ""Button"",
                    ""id"": ""d8ac05fc-bebf-4181-b0ab-03edbc49b01c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6c2bdd52-a23d-4974-b082-762652cbc70d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5687cafa-6c49-4779-9582-1bf500d5c0ca"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""70d8ca52-0ee9-4737-a9da-b8da7c961d86"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""599d964d-c7cd-4188-9b76-b30cfac4b06b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d7980cdd-b896-4f34-b7f3-e1d230b5f274"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7ed071ca-38f8-46cb-bc10-1fe5a5ad8a81"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48d54c50-e4a5-4ebc-8a53-cfa1639370ba"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1ac7809-e426-4f29-814c-64a00fc1173b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22ff8037-b686-47fc-92ec-0df81c12fc94"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b49199cd-0667-472a-a23a-8ad74dc3646a"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb9aeb16-d523-4de8-b301-0c6708d8c509"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DebugUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMenu
        m_PlayerMenu = asset.FindActionMap("PlayerMenu", throwIfNotFound: true);
        m_PlayerMenu_Move = m_PlayerMenu.FindAction("Move", throwIfNotFound: true);
        m_PlayerMenu_Select = m_PlayerMenu.FindAction("Select", throwIfNotFound: true);
        m_PlayerMenu_Back = m_PlayerMenu.FindAction("Back", throwIfNotFound: true);
        m_PlayerMenu_DebugUI = m_PlayerMenu.FindAction("DebugUI", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMenu
    private readonly InputActionMap m_PlayerMenu;
    private List<IPlayerMenuActions> m_PlayerMenuActionsCallbackInterfaces = new List<IPlayerMenuActions>();
    private readonly InputAction m_PlayerMenu_Move;
    private readonly InputAction m_PlayerMenu_Select;
    private readonly InputAction m_PlayerMenu_Back;
    private readonly InputAction m_PlayerMenu_DebugUI;
    public struct PlayerMenuActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerMenuActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMenu_Move;
        public InputAction @Select => m_Wrapper.m_PlayerMenu_Select;
        public InputAction @Back => m_Wrapper.m_PlayerMenu_Back;
        public InputAction @DebugUI => m_Wrapper.m_PlayerMenu_DebugUI;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMenuActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMenuActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Select.started += instance.OnSelect;
            @Select.performed += instance.OnSelect;
            @Select.canceled += instance.OnSelect;
            @Back.started += instance.OnBack;
            @Back.performed += instance.OnBack;
            @Back.canceled += instance.OnBack;
            @DebugUI.started += instance.OnDebugUI;
            @DebugUI.performed += instance.OnDebugUI;
            @DebugUI.canceled += instance.OnDebugUI;
        }

        private void UnregisterCallbacks(IPlayerMenuActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Select.started -= instance.OnSelect;
            @Select.performed -= instance.OnSelect;
            @Select.canceled -= instance.OnSelect;
            @Back.started -= instance.OnBack;
            @Back.performed -= instance.OnBack;
            @Back.canceled -= instance.OnBack;
            @DebugUI.started -= instance.OnDebugUI;
            @DebugUI.performed -= instance.OnDebugUI;
            @DebugUI.canceled -= instance.OnDebugUI;
        }

        public void RemoveCallbacks(IPlayerMenuActions instance)
        {
            if (m_Wrapper.m_PlayerMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMenuActions @PlayerMenu => new PlayerMenuActions(this);
    public interface IPlayerMenuActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnDebugUI(InputAction.CallbackContext context);
    }
}