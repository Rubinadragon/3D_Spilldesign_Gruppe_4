using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask groundLayer; // Lagmaske for bakken
    [SerializeField] private float raycastDistance = 0.5f; // Juster denne verdien
    [SerializeField] private InputActionReference jumpControl;
    [SerializeField] private InputActionReference movementControl;

    private Vector2 moveInput;
    private CharacterController controller;
    private PlayerControls playerControls;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;

    private void Awake()
    {
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        jumpControl.action.Disable();
    }

    private void Update()
    {
        // Raycast for å sjekke om vi er på bakken
        RaycastHit hit;
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer);

        Debug.Log("Grounded: " + groundedPlayer);
        Debug.Log("Raycast hit: " + hit.collider); // Sjekk hva raycasten treffer

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Hent inputbevegelse
        Vector2 inputVector = playerControls.PlayerMovement.Movement.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);

        playerVelocity.y += gravity * Time.deltaTime;

        // Hvis det er noen inputbevegelser
        if (movementVector.magnitude >= 0.1f)
        {
            // Beregn ønsket rotasjonsretning basert på input
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Rotere karakteren jevnt til ønsket retning
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Beveg karakteren i den nåværende rotasjonen
            Vector3 moveDirection = transform.forward * movementVector.magnitude; // Bruker transform.forward for riktig bevegelsesretning
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }


    }
}