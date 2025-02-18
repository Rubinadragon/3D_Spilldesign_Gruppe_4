using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private bool isGrounded;
    private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        cameraTransform = Camera.main.transform;

        playerControls = new PlayerControls();
        playerControls.PlayerMovement.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.PlayerMovement.Movement.canceled += ctx => moveInput = Vector2.zero;
        playerControls.PlayerMovement.Jump.performed += ctx => Jump();
    }

    private void OnEnable() => playerControls.Enable();
    private void OnDisable() => playerControls.Disable();

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void CheckGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(origin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);

        Debug.Log("Raycast treff: " + isGrounded);

    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 finalDirection = (forward * moveDirection.z + right * moveDirection.x).normalized;

            float currentMoveSpeed = playerControls.PlayerMovement.Run.IsPressed() ? runSpeed : moveSpeed;

            Quaternion targetRotation = Quaternion.LookRotation(finalDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.MovePosition(rb.position + finalDirection * currentMoveSpeed * Time.fixedDeltaTime);

            Debug.Log("Run Input Value: " + playerControls.PlayerMovement.Run.IsPressed());
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
