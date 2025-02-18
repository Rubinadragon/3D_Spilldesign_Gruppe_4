using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // Karakterens transform
    public float smoothSpeed = 0.125f; // Juster for jevnhet
    public Vector3 offset; // Offset fra karakteren

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}