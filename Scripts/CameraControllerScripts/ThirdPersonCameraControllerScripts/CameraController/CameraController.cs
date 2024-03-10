using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _followTarget; // The target the camera will follow
    [SerializeField] private float _rotationSpeed = 2f; // Speed of camera rotation
    [SerializeField] private float _distance = 5f; // Initial distance of the camera from the target

    private const string MOUSE_X = "Mouse X"; // Input axis for horizontal mouse movement
    private const string MOUSE_Y = "Mouse Y"; // Input axis for vertical mouse movement
    private const string MOUSE_SCROLLWHEEL = "Mouse ScrollWheel"; // Input axis for mouse scroll wheel

    [SerializeField] private float _minVerticalAngle = -45f; // Minimum vertical angle for camera rotation
    [SerializeField] private float _maxVerticalAngle = 45f; // Maximum vertical angle for camera rotation

    [SerializeField] private Vector2 _framingOffset; // Offset from the target for framing

    [SerializeField] private bool _invertX; // Flag to invert horizontal mouse movement
    [SerializeField] private bool _invertY; // Flag to invert vertical mouse movement

    private float _invertXVal; // Value to invert horizontal mouse movement
    private float _invertYVal; // Value to invert vertical mouse movement

    private float _rotationX; // Current rotation around the X-axis
    private float _rotationY; // Current rotation around the Y-axis

    [SerializeField] private float _minZoomDistance = 3f; // Minimum distance the camera can zoom in
    [SerializeField] private float _maxZoomDistance = 10f; // Maximum distance the camera can zoom out

    [SerializeField] private float _zoomSpeed = 5f; // Speed of camera zoom

    #endregion Fields

    #region Methods
    private void Start()
    {
        // Hide and lock the cursor at the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Update camera rotation and zoom
        CameraRotationControls();
        ZoomControl();
    }

    private void CameraRotationControls()
    {
        // Calculate inversion values based on user settings
        _invertXVal = (_invertX) ? -1 : 1;
        _invertYVal = (_invertY) ? -1 : 1;

        // Update rotation around the X-axis (vertical)
        _rotationX += Input.GetAxis(MOUSE_Y) * _invertYVal * _rotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);

        // Update rotation around the Y-axis (horizontal)
        _rotationY += Input.GetAxis(MOUSE_X) * _invertXVal * _rotationSpeed;

        // Calculate the target rotation based on current rotations
        var targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        // Calculate the position to focus the camera on
        var focusPosition = _followTarget.position + new Vector3(_framingOffset.x, _framingOffset.y);

        // Update camera position and rotation based on focus position and target rotation
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, _distance);
        transform.rotation = targetRotation;
    }

    private void ZoomControl()
    {
        // Adjust camera distance based on mouse scroll input
        _distance -= Input.GetAxis(MOUSE_SCROLLWHEEL) * _zoomSpeed;
        // Clamp camera distance within min and max zoom distances
        _distance = Mathf.Clamp(_distance, _minZoomDistance, _maxZoomDistance);
    }
    #endregion Methods
}
