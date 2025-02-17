using UnityEngine;

public class InfiniteOneWayRotation : MonoBehaviour
{
    // Hastigheten på rotasjonen (grader per sekund)
    public float rotationSpeed = 30f;

    // Retning for rotasjon (endres om du vil snu rotasjonen)
    public Vector3 rotationAxis = Vector3.right;

    private void Update()
    {
        // Utfør kontinuerlig rotasjon i én retning
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
