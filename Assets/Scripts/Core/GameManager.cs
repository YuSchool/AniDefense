using UnityEngine;

public class GameManager : MonoBehaviour
{
    //
    //  SINGLETON
    // 
    public static GameManager Instance { get; private set; }

    // 
    //  SPIELZUSTAND
    //
    public enum GameState
    {
        Idle,        // Spiel wartet auf Start
        Running,     // Wave läuft
        Paused,      // Pausiert
        WaveOver,    // Wave beendet, kurze Pause bis nächste
        GameOver,    // Alle Leben verloren
        Victory      // Alle Waves geschafft
    }

    public GameState AktuellerZustand { get; private set; } = GameState.Idle;

    [Header("Wave Info")]
    public int AktuelleWave { get; private set; } = 0;
    public int MaxWaves = 5;

    // 
    //  EVENTS
    // 
    public static event System.Action<GameState> OnZustandGeaendert;
    public static event System.Action<int> OnWaveGestartet;
    public static event System.Action OnGameOver;
    public static event System.Action OnVictory;

    // 
    //  UNITY LIFECYCLE
    // 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //
    //  ÖFFENTLICHE METHODEN
    // 

    public void StarteNaechsteWave()
    {
        if (AktuellerZustand == GameState.GameOver ||
            AktuellerZustand == GameState.Victory) return;

        AktuelleWave++;
        SetzeZustand(GameState.Running);
        OnWaveGestartet?.Invoke(AktuelleWave);
        Debug.Log($"[GameManager] Wave {AktuelleWave} gestartet.");
    }

    public void WaveAbgeschlossen()
    {
        if (AktuelleWave >= MaxWaves)
        {
            SetzeZustand(GameState.Victory);
            OnVictory?.Invoke();
            Debug.Log("[GameManager] Alle Waves geschafft — Victory!");
        }
        else
        {
            SetzeZustand(GameState.WaveOver);
            Debug.Log($"[GameManager] Wave {AktuelleWave} abgeschlossen.");
        }
    }

    public void TriggerGameOver()
    {
        SetzeZustand(GameState.GameOver);
        OnGameOver?.Invoke();
        Debug.Log("[GameManager] Game Over.");
    }

    public void Pausieren(bool pause)
    {
        if (pause && AktuellerZustand == GameState.Running)
        {
            SetzeZustand(GameState.Paused);
            Time.timeScale = 0f;
        }
        else if (!pause && AktuellerZustand == GameState.Paused)
        {
            SetzeZustand(GameState.Running);
            Time.timeScale = 1f;
        }
    }

    public void LevelNeuStarten()
    {
        Time.timeScale = 1f;
        AktuelleWave = 0;
        SetzeZustand(GameState.Idle);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //  PRIVAT
  
    private void SetzeZustand(GameState neuerZustand)
    {
        AktuellerZustand = neuerZustand;
        OnZustandGeaendert?.Invoke(neuerZustand);
    }
}