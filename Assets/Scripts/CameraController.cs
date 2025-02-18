using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float rotationalSpeed = 200f;
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float bottomClamp = -40f;
    [SerializeField] private float topClamp = 70f;

    private PlayerControls playerControls;
    private Vector2 lookInput;
    private Vector2 panInput;
    private float yaw;
    private float pitch;

    private void Awake()
    {
        playerControls = new PlayerControls();

        // Rotasjon (Look)
        playerControls.Camera.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Camera.Look.canceled += ctx => lookInput = Vector2.zero;

        // Panorering (Pan) - Piltaster
        playerControls.Camera.Pan.performed += ctx => panInput = ctx.ReadValue<Vector2>();
        playerControls.Camera.Pan.canceled += ctx => panInput = Vector2.zero;
    }

    private void OnEnable() => playerControls.Enable();
    private void OnDisable() => playerControls.Disable();

    private void LateUpdate()
    {
        // Rotasjon
        if (lookInput.magnitude > 0.1f)
        {
            yaw += lookInput.x * rotationalSpeed * Time.deltaTime;
            pitch -= lookInput.y * rotationalSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, bottomClamp, topClamp);

            followTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        // Panorering
        if (panInput.magnitude > 0.1f)
        {
            Vector3 panMovement = transform.right * panInput.x + transform.forward * panInput.y;
            followTarget.position += panMovement * panSpeed * Time.deltaTime;
        }
    }
}