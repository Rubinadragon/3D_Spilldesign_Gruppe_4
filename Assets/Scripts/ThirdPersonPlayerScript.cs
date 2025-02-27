using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Normal bevegelseshastighet
    [SerializeField] private float runSpeed = 10f; // L�pehastighet
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody rb;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private bool isRunning;
    private bool isGrounded;
    private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate; // S�rger for jevn bevegelse
        cameraTransform = Camera.main.transform;

        playerControls = new PlayerControls();

        // H�ndterer input
        playerControls.PlayerMovement.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.PlayerMovement.Movement.canceled += ctx => moveInput = Vector2.zero;
        playerControls.PlayerMovement.Jump.performed += ctx => Jump();
        playerControls.PlayerMovement.Run.started += ctx => isRunning = true;
        playerControls.PlayerMovement.Run.canceled += ctx => isRunning = false;
    }

    private void OnEnable() => playerControls.Enable();
    private void OnDisable() => playerControls.Disable();

    private void Update()
    {
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckGrounded()
    {
        // Sjekker om spilleren er p� bakken ved hjelp av en Raycast
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, 0.3f);
        Debug.Log(isGrounded);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (moveDirection.magnitude >= 0.1f)
        {
            // Finner fremover- og h�yre-retningen basert p� kameraet
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Beregner bevegelsesretning
            Vector3 finalDirection = forward * moveDirection.z + right * moveDirection.x;

            // Velger hastighet basert p� om spilleren l�per
            float currentSpeed = isRunning ? runSpeed : moveSpeed;

            // Beveger spilleren
            rb.MovePosition(rb.position + finalDirection * currentSpeed * Time.fixedDeltaTime);

            // Rotasjon: Spilleren skal vende i bevegelsesretningen
            Quaternion targetRotation = Quaternion.LookRotation(finalDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }

    private void Jump()
    {
        if (!isGrounded) // Bare hopp hvis spilleren er p� bakken
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }
}
