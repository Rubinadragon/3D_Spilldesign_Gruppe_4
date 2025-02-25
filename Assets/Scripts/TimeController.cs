using UnityEngine;
using System.Collections.Generic;

public class TimeController : MonoBehaviour
{
    public KeyCode slowDownKey = KeyCode.Alpha1;
    public KeyCode stopTimeKey = KeyCode.Alpha2;
    public KeyCode resetTimeKey = KeyCode.Alpha3; 

    [Range(0f, 1f)]
    public float slowTimeScale = 0.2f; 
    private float normalTimeScale = 1f; 

    public GameObject player; 
    public Transform startPoint; 

    private List<Rigidbody> affectedObjects = new List<Rigidbody>();

    public int maxLives = 5;
    private int currentLives;
    public float slowTimeLifeLossRate = 0.5f;
    public float stopTimeLifeLossRate = 2f;   
    private bool isLosingLife = false;

    void Start()
    {
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        currentLives = maxLives;

        
        foreach (Rigidbody rb in FindObjectsOfType<Rigidbody>())
        {
            if (rb.gameObject != player)
            {
                affectedObjects.Add(rb);
            }
        }
    }

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

        
        if (isLosingLife)
        {
            float lifeLossRate = (Time.timeScale == slowTimeScale) ? slowTimeLifeLossRate : stopTimeLifeLossRate;
            LoseLifeOverTime(lifeLossRate);
        }
    }

    void SlowDownTime()
    {
        Debug.Log("Slowing down time!");
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isLosingLife = true;

        foreach (Rigidbody obj in affectedObjects)
        {
            if (!obj.isKinematic) 
            {
                obj.linearVelocity *= slowTimeScale;
                obj.angularVelocity *= slowTimeScale;
            }
        }
    }

    void StopTime()
    {
        Debug.Log("Stopping time!");
        Time.timeScale = 0f; 
        isLosingLife = true;
    }

    void ResetTime()
    {
        Debug.Log("Resetting time to normal!");
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isLosingLife = false;
    }

    void LoseLifeOverTime(float rate)
    {
        int lifeLoss = Mathf.RoundToInt(rate * Time.unscaledDeltaTime);
        if (lifeLoss > 0) 
        {
            currentLives -= lifeLoss;
            Debug.Log("Life lost! Remaining: " + currentLives);
            RespawnPlayer(); 
        }

        if (currentLives <= 0)
        {
            currentLives = 0;
            GameOver();
        }
    }
}
