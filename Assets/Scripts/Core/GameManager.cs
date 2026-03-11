using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton-Instanz für globalen Zugriff 


    #region SPIELZUSTAND
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
    #endregion

    #region LEVELINFO
    [Header("Wave Info")]
    public int AktuelleWave { get; private set; } = 0;
    public int MaxWaves = 5;
    #endregion

    #region EVENTS

    public static event System.Action<GameState> OnZustandGeaendert;
    public static event System.Action<int> OnWaveGestartet;
    public static event System.Action OnGameOver;
    public static event System.Action OnVictory;

    #endregion

    #region UNITY LEBENSZYKLUS
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion

    #region ÖFFENTLICHE METHODEN 

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

    #endregion

    #region PRIVATE METHODEN

    private void SetzeZustand(GameState neuerZustand)
    {
        AktuellerZustand = neuerZustand;
        OnZustandGeaendert?.Invoke(neuerZustand);
    }

    #endregion
}