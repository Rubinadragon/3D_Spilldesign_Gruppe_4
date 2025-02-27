using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    // Definerer tastene som styrer de forskjellige tidseffektene
    // 'slowDownKey' for å bremse tiden, 'stopTimeKey' for å stoppe tiden, 'resetTimeKey' for å tilbakestille tiden til normal
    public KeyCode slowDownKey = KeyCode.Alpha1; 
    public KeyCode stopTimeKey = KeyCode.Alpha2; 
    public KeyCode resetTimeKey = KeyCode.Alpha3; 

    // Tidsskala for å bremse tiden. Verdien kan justeres mellom 0 (stoppet) og 1 (normal hastighet).
    [Range(0f, 1f)]
    public float slowTimeScale = 0.2f; 

    // Normal tidsskala, der 1 er den vanlige hastigheten
    private float normalTimeScale = 1f; 

    // Spilleren starter med 3 liv
    public int playerLives = 3; 

    // Hvor raskt spilleren mister liv når tiden bremses eller stoppes
    public float slowLifeLossRate = 0.1f; //bremses
    public float stopLifeLossRate = 0.5f; //stoppes

    // Startpunktet til spilleren og referanse til spilleren som objekt
    public Transform startPoint;
    public Transform player; 
    private Rigidbody playerRigidbody;

    // Variabler for å kontrollere om tiden er bremset eller stoppet
    private bool isSlowingDown = false;
    private bool isTimeStopped = false;

    // Hendelser som trigges når tiden stoppes eller resettes
    public event Action OnTimeSlowedDown;
    public event Action OnTimeStopped;
    public event Action OnTimeReset;

    // Start-metoden kjører når scriptet starter
    void Start()
    {
        // Setter tidsskala til normal ved start
        Time.timeScale = normalTimeScale;
        // Justerer fast oppdatering av fysikk i forhold til tidsskala
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 
        
        // Henter Rigidbody fra spilleren om tilgjengelig
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody>();
        }
    }

    // Update-metoden kjører hvert frame og håndterer tastetrykk og livstap
    void Update()
    {
        // Sjekker om spilleren trykker på tastene for tidseffektene
        if (Input.GetKeyDown(slowDownKey) && playerLives > 0)
        {
            // Hvis 'slowDownKey' trykkes, bremser vi tiden
            SlowDownTime(); 
        }
        else if (Input.GetKeyDown(stopTimeKey) && playerLives > 0)
        {
            // Hvis 'stopTimeKey' trykkes, stopper vi tiden
            StopTime(); 
        }
        else if (Input.GetKeyDown(resetTimeKey))
        {
            // Hvis 'resetTimeKey' trykkes, tilbakestiller vi tiden til normal
            ResetTime(); 
        }

        // Når tiden er bremset eller stoppet, mister spilleren liv over tid
        if (isSlowingDown)
        {
            playerLives -= Mathf.RoundToInt(slowLifeLossRate * Time.unscaledDeltaTime);
        }
        else if (isTimeStopped)
        {
            playerLives -= Mathf.RoundToInt(stopLifeLossRate * Time.unscaledDeltaTime);
        }

        // Hvis spilleren har 0 liv, trigges GameOver
        if (playerLives <= 0)
        {
            // Sørger for at liv ikke går under null
            playerLives = 0; 
            // Kaller GameOver-funksjonen
            GameOver(); 
        }
    }

    // Funksjon for å bremse tiden
    void SlowDownTime()
    {
        Debug.Log("Tiden bremses!"); 
        // Endrer tidsskala til den angitte verdien for å bremse tiden
        Time.timeScale = slowTimeScale; 
        // Justerer fysikkens oppdateringstid
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 
        // Setter flagget for bremset tid til true
        isSlowingDown = true; 
        // Forsikrer at tiden ikke er stoppet samtidig
        isTimeStopped = false; 
        OnTimeSlowedDown?.Invoke();
    }

    // Funksjon for å stoppe tiden helt
    void StopTime()
    {
        Debug.Log("Tiden stoppes!");
        // Setter stoppet tid til true
        isTimeStopped = true; 
        // Forsikrer at tiden ikke bremses samtidig
        isSlowingDown = false; 

        // Kaller hendelsen for at alle objekter skal vite at tiden er stoppet
        OnTimeStopped?.Invoke();
    }

    // Funksjon for å tilbakestille tiden tilbake til normal
    void ResetTime()
    {
        Debug.Log("Tiden resettes til normal!"); 
        // Setter tidsskala tilbake til normal
        Time.timeScale = normalTimeScale; 
        // Justerer fysikkens oppdateringstid tilbake til normal
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 
        isSlowingDown = false; 
        isTimeStopped = false; 

        // Kaller hendelsen for at alle objekter skal vite at tiden er tilbake til normal
        OnTimeReset?.Invoke();
    }

    // Funksjon som håndterer Game Over-logikken når spilleren har mistet alle livene
    void GameOver()
    {
        // Vist i konsollen når spillet er over
        Debug.Log("Game Over! Ingen liv igjen."); 
        // Tilbakestiller spilleren til startpunktet
        ResetPlayerPosition(); 
    }

    // Funksjon som tilbakestiller spilleren til startpunktet og gjenoppretter livene
    void ResetPlayerPosition()
    {
        if (startPoint != null && player != null)
        {
            // Setter spillerens posisjon tilbake til startpunktet
            player.position = startPoint.position; 
            // Gjenoppretter spilleren sine liv
            playerLives = 3; 
            Debug.Log("Spilleren resettes til startpunktet.");
        }
    }

    // Funksjon som oppdaterer fysikkhastigheten til spilleren som gjør at spilleren ikke beveger seg for fort når tiden er endret.
    void FixedUpdate()
    {
        if (playerRigidbody != null)
        {
            if (Time.timeScale != 0f) 
            {
                // Bruker linearVelocity i stedet for velocity for å unngå advarselen
                Vector3 linearVelocity = playerRigidbody.linearVelocity;
                // Justerer hastigheten etter tidsskalaen
                playerRigidbody.linearVelocity = linearVelocity / Time.timeScale; 
            }
        }
    }


    // Funksjon som håndterer spillerens bevegelse med tastaturinput
    void MovePlayer()
    {
        // Bevegelseshastighet for spilleren
        float moveSpeed = 5f; 

        // Henter input fra tastaturet for horisontal og vertikal bevegelse
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Beregner bevegelse basert på tastetrykk og tidsskala
        Vector3 move = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.unscaledDeltaTime; 

        // Oppdaterer spillerens posisjon med den beregnede bevegelsen
        player.position += move; 
    }
}
