using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private Camera _camera;

    private Vector2 _delta;
    private Vector2 _scrollInput;
    private float x_rotation;

    private bool _isMoving;
    private bool _isRotating;
    private bool _isZooming;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float zoomSpeed = 1f;

    private void Awake()
    {
        x_rotation = transform.rotation.eulerAngles.x;
        _camera = GetComponent<Camera>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _delta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _isMoving = context.started || context.performed;
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        _scrollInput = context.ReadValue<Vector2>();
        _isZooming = context.started || context.performed;
    }

    private void LateUpdate()
    {
        if (_isMoving && !_isRotating && !_isZooming)
        {
            var position = transform.right * (_delta.x * -movementSpeed);
            position += transform.up * (_delta.y * -movementSpeed);
            transform.position += position * Time.deltaTime;
        }

        if (_isRotating && !_isMoving && !_isZooming)
        {
            transform.Rotate(new Vector3(x_rotation, -_delta.x * rotationSpeed, 0.0f));
            transform.rotation = Quaternion.Euler(x_rotation, transform.rotation.eulerAngles.y, 0.0f);
        }

        if(_isZooming && !_isMoving && !_isRotating)
        {
            _camera.orthographicSize -= _scrollInput.y * zoomSpeed;

            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 4, 10);
        }
    }
}
