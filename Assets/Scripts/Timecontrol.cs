using UnityEngine;

public class TimeController : MonoBehaviour
{
    public KeyCode slowDownKey = KeyCode.Alpha1;
    public KeyCode stopTimeKey = KeyCode.Alpha2; 
    public KeyCode resetTimeKey = KeyCode.Alpha3;

    [Range(0f, 1f)]
    // Hastighet når tiden sakses
    public float slowTimeScale = 0.2f; 

    private float normalTimeScale = 1f;

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
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
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

