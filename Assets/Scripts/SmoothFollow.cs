using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;  // Tid for å glatte ut bevegelsen
    public Vector3 offset = new Vector3(0, 3, -5);

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position + offset;
    }

}
