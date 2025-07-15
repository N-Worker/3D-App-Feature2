using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    #region Settings
    [Header("Sensitivity")]
    public float cameraSensitivity = 30f;
    public float zoomSpeed = 50f;

    [Header("Distance Limit")]
    public float currentDistance = 1.75f;
    public float minDistance = 1f;
    public float maxDistance = 3.5f;
    #endregion

    #region Variables
    public float vertical { get; set; }
    public float horizontal { get; set; }

    private Quaternion _cameraRotation;
    private Quaternion _returnRotation = Quaternion.identity;
    private Coroutine _resumeRotationCoroutine;

    private bool _getManualRotate;
    private bool _isRotateBack;
    #endregion

    #region References
    private GameManager _gameManager;
    private InputManager _inputManager;
    private TableManager _tableManager;
    private UIManager _uIManager;
    private UI_TurnTableOption _uI_turnTableOption;
    private Transform _cam;
    #endregion

    #region Unity Main Methods
    private void Start()
    {
        _InitializeReferences();
    }

    private void Update()
    {
        _CameraPosition();

        _HandleRotateBack();

        _CameraZoom();

        _CameraRotatePosition();
    }

    private void FixedUpdate()
    {
        _RotateCameraBack();
    }
    #endregion
    private void _InitializeReferences()
    {
        _cam = Camera.main.transform;

        _gameManager = GameManager.Instance;
        _inputManager = _gameManager.inputManager;
        _tableManager = _gameManager.tableManager;
        _uIManager = _gameManager.uiManager;
        _uI_turnTableOption = _uIManager.turnTableOption;
    }

    #region Camera Control Method
    private void _CameraZoom()
    {
        if (_inputManager.zoom == 0) return;

        currentDistance += _inputManager.zoom * zoomSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }
    private void _RotateCamera(Vector2 input)
    {
        horizontal += input.x * cameraSensitivity * Time.deltaTime;
        vertical -= input.y * cameraSensitivity * Time.deltaTime;
    }
    private void _CameraRotatePosition()
    {
        _cameraRotation = Quaternion.Euler(vertical, horizontal, 0f);
        _cam.rotation = _cameraRotation;
        _cam.position = transform.position + _cameraRotation * Vector3.back * currentDistance;
    }
    private void _CameraPosition()
    {
        if (_inputManager.toggleCameraMovement == 1)
        {
            Vector3 right = _cam.right;
            Vector3 up = _cam.up;

            Vector3 move = (right * _inputManager.movement.x + up * _inputManager.movement.y);

            transform.position -= move * Time.deltaTime;
        }
    }
    #endregion

    #region Rotate Back
    private void _HandleRotateBack()
    {
        if ((_inputManager.toggleRotateMovement == 1 || _inputManager.toggleCameraMovement == 1) && _inputManager.movement != Vector2.zero)
        {
            if (_inputManager.toggleRotateMovement == 1)
            {
                _RotateCamera(_inputManager.movement);
                _tableManager.toggleRotate = false;
            }

            if (_resumeRotationCoroutine != null) StopCoroutine(_resumeRotationCoroutine);
            _resumeRotationCoroutine = null;
            _getManualRotate = true;
            _isRotateBack = false;
        }
        else if (_getManualRotate && _resumeRotationCoroutine == null && !_isRotateBack && _uI_turnTableOption.autoResetCamera.isOn)
        {
            _resumeRotationCoroutine = StartCoroutine(_ResumeRotationAfterDelay(3f));
        }
    }
    private IEnumerator _ResumeRotationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isRotateBack = true;
        _resumeRotationCoroutine = null;
    }
    private void _RotateCameraBack()
    {
        if (!_isRotateBack) return;

        _cam.rotation = Quaternion.Slerp(_cam.rotation, _returnRotation, Time.deltaTime * 5f);
        _tableManager.transform.rotation = Quaternion.Slerp(_tableManager.transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 5f);

        transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * 5f);

        if (Vector3.Distance(transform.position, Vector3.zero) < 0.01f)
        {
            transform.position = Vector3.zero;
        }

        if (Quaternion.Angle(_cam.rotation, _returnRotation) < 0.1f && transform.position == Vector3.zero)
        {
            _cam.rotation = _returnRotation;
            _getManualRotate = false;
            _isRotateBack = false;
            _tableManager.toggleRotate = true;
            _tableManager.totalRotation = 0f;
        }

        var euler = _cam.rotation.eulerAngles;
        vertical = euler.x > 180 ? euler.x - 360 : euler.x;
        horizontal = euler.y > 180 ? euler.y - 360 : euler.y;
    }
    #endregion
}
