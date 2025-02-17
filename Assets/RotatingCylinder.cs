using UnityEngine;

public class SelfRotatingCylinder : MonoBehaviour
{
    // Rotasjonshastighet i grader per sekund
    public float rotationSpeed = 50f;

    private void Update()
    {
        // Roter kontinuerlig rundt Y-aksen (sylinderens "vertikale" akse)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
