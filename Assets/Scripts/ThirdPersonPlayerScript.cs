using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] public float jumpHeight = 250;
    [SerializeField] private float rotationSpeed = 10f;
 

    private Vector2 moveInput;
    private CharacterController controller;
    private PlayerControls playerControls;
    private Vector3 playerVelocity;
    public bool groundedPlayer; //er default false
    public Transform groundChecher;
    public LayerMask _layer;
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
       
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        groundedPlayer = Physics.CheckSphere(groundChecher.position, 0.5f, _layer);

        // Hent inputbevegelse
        Vector2 inputVector = playerControls.PlayerMovement.Movement.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);

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

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
        }


    }
}