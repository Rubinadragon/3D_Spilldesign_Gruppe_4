using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Hastigheten på bevegelsen
    public float moveSpeed = 2f;

    // Hvor langt plattformen skal bevege seg fra startposisjon
    public float moveDistance = 3f;

    // Retningen plattformen beveger seg (f.eks. høyre/venstre: Vector3.right)
    public Vector3 moveDirection = Vector3.right;

    // Startposisjonen til plattformen
    private Vector3 startPosition;

    private void Start()
    {
        // Lagre plattformens startposisjon
        startPosition = transform.position;
    }

    private void Update()
    {
        // Kalkuler bevegelsen frem og tilbake ved hjelp av sinuskurve
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + moveDirection * offset;
    }
}
