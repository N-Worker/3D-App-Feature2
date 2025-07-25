//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input Action/Input.inputactions
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

public partial class @Input: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""CameraInput"",
            ""id"": ""f2cbfc2e-0f1f-4cb8-b7a2-f358cc96d874"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""39d23249-beab-4180-af8d-5dacfbdbef46"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleRotateMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""233b00f4-10d3-4de9-8be6-4b0e899cc77b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleCameraMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""54e4b1e2-310b-489c-8092-cfef88cccd06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e973a102-d705-44f9-abe9-67ac1c2ec926"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0f550e43-68c7-4cd7-90d9-878c6043b6d7"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9fa559da-b6d3-46d2-9bcb-729e79d67fe2"",
                    ""path"": ""<Mouse>/delta/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ddaed2c9-a44d-4621-9022-ae063a9bcb6e"",
                    ""path"": ""<Mouse>/delta/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fefc1d88-799c-4c46-b829-7994afcc2d85"",
                    ""path"": ""<Mouse>/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1024360f-1798-4df8-9a36-5ab9493baf05"",
                    ""path"": ""<Mouse>/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""247853d5-dad0-4dac-be29-478b05824039"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleRotateMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d000b924-f4a5-4fbb-b152-216e29a05a9d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea3eddd8-f359-4226-b01d-93457649d499"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Hold(duration=0.15)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CameraInput
        m_CameraInput = asset.FindActionMap("CameraInput", throwIfNotFound: true);
        m_CameraInput_Movement = m_CameraInput.FindAction("Movement", throwIfNotFound: true);
        m_CameraInput_ToggleRotateMovement = m_CameraInput.FindAction("ToggleRotateMovement", throwIfNotFound: true);
        m_CameraInput_ToggleCameraMovement = m_CameraInput.FindAction("ToggleCameraMovement", throwIfNotFound: true);
        m_CameraInput_Zoom = m_CameraInput.FindAction("Zoom", throwIfNotFound: true);
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

    // CameraInput
    private readonly InputActionMap m_CameraInput;
    private List<ICameraInputActions> m_CameraInputActionsCallbackInterfaces = new List<ICameraInputActions>();
    private readonly InputAction m_CameraInput_Movement;
    private readonly InputAction m_CameraInput_ToggleRotateMovement;
    private readonly InputAction m_CameraInput_ToggleCameraMovement;
    private readonly InputAction m_CameraInput_Zoom;
    public struct CameraInputActions
    {
        private @Input m_Wrapper;
        public CameraInputActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CameraInput_Movement;
        public InputAction @ToggleRotateMovement => m_Wrapper.m_CameraInput_ToggleRotateMovement;
        public InputAction @ToggleCameraMovement => m_Wrapper.m_CameraInput_ToggleCameraMovement;
        public InputAction @Zoom => m_Wrapper.m_CameraInput_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_CameraInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraInputActions set) { return set.Get(); }
        public void AddCallbacks(ICameraInputActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraInputActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraInputActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @ToggleRotateMovement.started += instance.OnToggleRotateMovement;
            @ToggleRotateMovement.performed += instance.OnToggleRotateMovement;
            @ToggleRotateMovement.canceled += instance.OnToggleRotateMovement;
            @ToggleCameraMovement.started += instance.OnToggleCameraMovement;
            @ToggleCameraMovement.performed += instance.OnToggleCameraMovement;
            @ToggleCameraMovement.canceled += instance.OnToggleCameraMovement;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(ICameraInputActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @ToggleRotateMovement.started -= instance.OnToggleRotateMovement;
            @ToggleRotateMovement.performed -= instance.OnToggleRotateMovement;
            @ToggleRotateMovement.canceled -= instance.OnToggleRotateMovement;
            @ToggleCameraMovement.started -= instance.OnToggleCameraMovement;
            @ToggleCameraMovement.performed -= instance.OnToggleCameraMovement;
            @ToggleCameraMovement.canceled -= instance.OnToggleCameraMovement;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(ICameraInputActions instance)
        {
            if (m_Wrapper.m_CameraInputActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraInputActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraInputActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraInputActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraInputActions @CameraInput => new CameraInputActions(this);
    public interface ICameraInputActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnToggleRotateMovement(InputAction.CallbackContext context);
        void OnToggleCameraMovement(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
