using UnityEngine;

public class ExampleListener : MonoBehaviour
{
    private Rigidbody rb;

    // Start-metoden kjøres ved oppstart av scenen.
    void Start()
    {
        // Henter Rigidbody-komponenten på objektet dette scriptet er festet til
        rb = GetComponent<Rigidbody>();

        // Finner en referanse til TimeController-objektet i scenen
        TimeController timeController = FindFirstObjectByType<TimeController>();

        // Sjekker om TimeController er funnet, og abonnerer på relevante hendelser
        if (timeController != null)
        {
            // Når tiden stoppes, skal HandleTimeStopped metoden kalles
            timeController.OnTimeStopped += HandleTimeStopped;
            // Når tiden resettes, skal HandleTimeReset metoden kalles
            timeController.OnTimeReset += HandleTimeReset;
            // Hendelsen for når tiden bremses er fjernet (kanskje ønsket å være aktivert)
            timeController.OnTimeSlowedDown -= HandleTimeSlowedDown;
        }
    }

    // Denne funksjonen kalles når tiden bremses
    void HandleTimeSlowedDown()
    {
        // Sørger for at spilleren ikke blir påvirket av langsommere tid
        if (gameObject.CompareTag("Player")) return; 

        // Hvis Rigidbody-komponenten finnes
        if (rb != null)
        {
            // Reduserer objektets hastighet ved å dele den med Time.timeScale
            // Dette gjør at objektet beveger seg langsommere når tiden bremses
            rb.linearVelocity = rb.linearVelocity / Time.timeScale;
        }
    }

    // Denne funksjonen kalles når tiden stoppes
    void HandleTimeStopped()
    {
        // Sørger for at spilleren ikke blir påvirket av stoppet tid
        if (gameObject.CompareTag("Player")) return; // Ikke påvirke spilleren

        // Hvis Rigidbody-komponenten finnes
        if (rb != null)
        {
            // Setter hastigheten til null for å stoppe bevegelsen
            rb.linearVelocity = Vector3.zero;

            // Gjør objektet kinematisk, som betyr at det ikke påvirkes av fysikken
            rb.isKinematic = true;
        }
    }

    // Denne funksjonen kalles når tiden resettes til normal
    void HandleTimeReset()
    {
        // Sørger for at spilleren ikke blir påvirket av reset-tiden
        if (gameObject.CompareTag("Player")) return;

        // Hvis Rigidbody-komponenten finnes
        if (rb != null)
        {
            // Setter isKinematic til false, slik at objektet igjen kan påvirkes av fysikk
            rb.isKinematic = false; 
        }
    }
}
