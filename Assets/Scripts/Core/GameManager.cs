//  GameManager.cs - Zentraler Manager für Spielzustände, Wellenverwaltung und globale Events
//  Zweck: Verwaltung des Spielablaufs, einschließlich Starten von Wellen, Verwalten von Spielzuständen (z.B. Running, Paused, GameOver) und Auslösen von Events für andere Systeme (z.B. HUD, EnemyManager).
//  Sinn: Zentralisiert die Steuerung des Spielablaufs, ermöglicht eine klare Trennung von Logik und Daten, und erleichtert die Kommunikation zwischen verschiedenen Systemen durch Events.
//  Verantwortlichkeiten:
//  Wird verwendet von: Alle Systeme, die den Spielablauf beeinflussen oder darauf reagieren müssen (z.B. HUD, EnemyManager, DropHandler) abonnieren die Events des GameManagers, um auf Änderungen im Spielzustand oder Wellenfortschritt zu reagieren.

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton-Instanz für globalen Zugriff 


    #region SPIELZUSTAND
    
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

        if (AktuellerZustand == GameState.Running)
        {
            Debug.LogWarning("[GameManager] Wave läuft bereits. Bitte warten oder pausieren.");
            return;
        }

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

            // Fortschritt speichern
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.LevelAbgeschlossen(
                    SaveManager.AktuelleMap,
                    SaveManager.AktuelleSchwierigkeit);
            }

            Debug.Log("[GameManager] Alle Waves geschafft — Victory!");
        }
        else
        {
            SetzeZustand(GameState.WaveOver);
            Debug.Log($"[GameManager] Wave {AktuelleWave} abgeschlossen.");
            // StarteNaechsteWave() wurde entfernt - Spieler startet manuell
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