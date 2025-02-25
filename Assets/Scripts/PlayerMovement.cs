using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
[SerializeField] private float movementSpeed;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        inputVector = inputVector.normalized;
        Vector3 movementVector = new Vector3(inputVector.x, 0f, inputVector.y);

        // Roterer karakteren vår
        //transform.forward = Vector3.Slerp(transform.forward, movementVector, Time.deltaTime * rotationSpeed);

        transform.position += movementVector * movementSpeed * Time.deltaTime;
    }
}
