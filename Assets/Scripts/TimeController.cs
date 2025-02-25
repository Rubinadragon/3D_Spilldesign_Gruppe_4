using UnityEngine;

public class TimeController : MonoBehaviour
{
    public KeyCode slowDownKey = KeyCode.Alpha1; 
    public KeyCode stopTimeKey = KeyCode.Alpha2; 
    public KeyCode resetTimeKey = KeyCode.Alpha3; 

    [Range(0f, 1f)]
    public float slowTimeScale = 0.2f; 

    private float normalTimeScale = 1f; 

    public int playerLives = 3; 

    public float slowLifeLossRate = 0.1f; 
    public float stopLifeLossRate = 0.5f; 

    public Transform startPoint;
    public Transform player; 
    private Rigidbody playerRigidbody;

    private bool isSlowingDown = false;
    private bool isTimeStopped = false;

    void Start()
    {
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(slowDownKey) && playerLives > 0)
        {
            SlowDownTime();
        }
        else if (Input.GetKeyDown(stopTimeKey) && playerLives > 0)
        {
            StopTime();
        }
        else if (Input.GetKeyDown(resetTimeKey))
        {
            ResetTime();
        }

        if (isSlowingDown)
        {
            playerLives -= Mathf.RoundToInt(slowLifeLossRate * Time.unscaledDeltaTime);
        }
        else if (isTimeStopped)
        {
            playerLives -= Mathf.RoundToInt(stopLifeLossRate * Time.unscaledDeltaTime);
        }

        if (playerLives <= 0)
        {
            playerLives = 0;
            GameOver();
        }
    }

    void SlowDownTime()
    {
        Debug.Log("Slowing down time!");
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isSlowingDown = true;
        isTimeStopped = false;
    }

    void StopTime()
    {
        Debug.Log("Stopping time!");
        Time.timeScale = 0f; 
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isTimeStopped = true;
        isSlowingDown = false;
    }

    void ResetTime()
    {
        Debug.Log("Resetting time to normal!");
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isSlowingDown = false;
        isTimeStopped = false;
    }

    void GameOver()
    {
        Debug.Log("Game Over! No lives remaining.");
        ResetPlayerPosition();
    }

    void ResetPlayerPosition()
    {
        if (startPoint != null && player != null)
        {
            player.position = startPoint.position;
            playerLives = 3;
            Debug.Log("Player reset to start point.");
        }
    }

    void FixedUpdate()
    {
        if (playerRigidbody != null)
        {
            if (Time.timeScale != 0f) 
            {
                Vector3 velocity = playerRigidbody.linearVelocity;
                playerRigidbody.linearVelocity = velocity / Time.timeScale; 
            }
        }
    }

    void MovePlayer()
    {
        float moveSpeed = 5f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.unscaledDeltaTime; // Use unscaledDeltaTime for consistent movement

        player.position += move;
    }
}
