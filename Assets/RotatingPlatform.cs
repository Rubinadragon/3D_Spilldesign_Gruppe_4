using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Hastigheten på rotasjonen (grader per sekund)
    public float rotationSpeed = 50f;

    // Rotasjonsakse (f.eks. Vector3.up for å rotere rundt Y-aksen)
    public Vector3 rotationAxis = Vector3.up;

    // Oppdater rotasjonen i hver frame
    void Update()
    {
        // Roter plattformen rundt aksen
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
