using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Variables
    public Vector2 movement { get; private set; }
    public float toggleRotateMovement { get; private set; }
    public float toggleCameraMovement { get; private set; }
    public float zoom { get; private set; }
    #endregion

    #region Reference
    private Input input;
    #endregion

    #region Unity Main Methods
    private void OnEnable()
    {
        if (input == null) input = new Input();
        input.Enable();

        input.CameraInput.Movement.performed += _MovementInput;

        input.CameraInput.ToggleRotateMovement.performed += _ToggleCameraRotateInput;
        input.CameraInput.ToggleRotateMovement.canceled += context => toggleRotateMovement = 0f;

        input.CameraInput.ToggleCameraMovement.performed += _ToggleCameraMovementInput;
        input.CameraInput.ToggleCameraMovement.canceled += context => toggleCameraMovement = 0f;

        input.CameraInput.Zoom.performed += _CameraZoomInput;
    }
    #endregion

    #region Input Callbacks
    private void _MovementInput(InputAction.CallbackContext context)
    {
        if (toggleRotateMovement == 1 || toggleCameraMovement == 1) movement = context.ReadValue<Vector2>();
        else movement = Vector2.zero;
    }
    private void _ToggleCameraRotateInput(InputAction.CallbackContext context)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            toggleRotateMovement = 0f;
            return;
        }
        toggleRotateMovement = context.ReadValue<float>();
    }
    private void _ToggleCameraMovementInput(InputAction.CallbackContext context)
    {
        toggleCameraMovement = context.ReadValue<float>();
    }
    private void _CameraZoomInput(InputAction.CallbackContext context)
    {
        zoom = context.ReadValue<float>();
    }
    #endregion
}