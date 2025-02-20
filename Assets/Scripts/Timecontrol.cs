using UnityEngine;

public class TimeController : MonoBehaviour
{
    public KeyCode slowDownKey = KeyCode.Alpha1; // Tast 1 for å sakke ned
    public KeyCode stopTimeKey = KeyCode.Alpha2; // Tast 2 for å stoppe tiden
    public KeyCode resetTimeKey = KeyCode.Alpha3; // Tast 3 for å nullstille tiden

    [Range(0f, 1f)]
    public float slowTimeScale = 0.2f; // Hastighet når tiden sakses

    private float normalTimeScale = 1f; // Normal hastighet

    void Update()
    {
        if (Input.GetKeyDown(slowDownKey))
        {
            SlowDownTime();
        }
        else if (Input.GetKeyDown(stopTimeKey))
        {
            StopTime();
        }
        else if (Input.GetKeyDown(resetTimeKey))
        {
            ResetTime();
        }
    }

    void SlowDownTime()
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Sikrer riktig fysikksimulering
    }

    void StopTime()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void ResetTime()
    {
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}

