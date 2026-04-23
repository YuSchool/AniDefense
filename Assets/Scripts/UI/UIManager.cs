// UIManager.cs
// Zweck: Steuert die Anzeige der Benutzeroberfläche im Spiel, einschließlich des HUD, des Game Over-Bildschirms und des Victory-Bildschirms. Es aktualisiert auch die Lebensanzeige (Herzen) basierend auf den aktuellen Lebenspunkten des Spielers.
// Sinn: Der UIManager ist ein zentraler Bestandteil der Benutzeroberfläche, da er dafür verantwortlich ist, wichtige Informationen und Statusänderungen an den Spieler zu kommunizieren. Durch die Verwaltung der verschiedenen Panels und der Lebensanzeige trägt der UIManager dazu bei, dass der Spieler
// Wird verwendet von: GameManager, ResourceManager
// Hat verbindungen zu: GameManager.OnGameOver, GameManager.OnVictory, ResourceManager.OnLebenGeaendert

using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance { get; private set; }

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

    #region Referenzen

    [Header("Panels")]
    [SerializeField] private GameObject panel_Oben;
    [SerializeField] private GameObject panel_Leben;
    [SerializeField] private GameObject panel_Rechts;
    [SerializeField] private GameObject panel_Bauliste;
    [SerializeField] private GameObject panel_Upgrade;
    [SerializeField] private GameObject panel_GameOver;
    [SerializeField] private GameObject panel_Victory;

    [Header("Herzen")]
    [SerializeField] private GameObject herz_Eins;
    [SerializeField] private GameObject herz_Zwei;
    [SerializeField] private GameObject herz_Drei;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        GameManager.OnGameOver += ZeigeGameOver;
        GameManager.OnVictory += ZeigeVictory;
        ResourceManager.OnLebenGeaendert += AktualisierHerzen;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ZeigeGameOver;
        GameManager.OnVictory -= ZeigeVictory;
        ResourceManager.OnLebenGeaendert -= AktualisierHerzen;
    }

    private void Start()
    {
        panel_GameOver.SetActive(false);
        panel_Victory.SetActive(false);
        AktualisierHerzen(ResourceManager.Instance.Leben);
    }

    #endregion

    #region Panel Steuerung

    private void ZeigeGameOver()
    {
        AudioManager.Instance?.StoppeMusik();
        AudioManager.Instance?.SpieleSFX_GameOver();
        panel_Oben.SetActive(false);
        panel_Leben.SetActive(false);
        panel_Rechts.SetActive(false);
        panel_GameOver.SetActive(true);
        Time.timeScale = 0f;
        TowerPlacer.Instance.AuswahlAufheben();
    }

    private void ZeigeVictory()
    {
        AudioManager.Instance?.StoppeMusik();
        AudioManager.Instance?.SpieleSFX_Victory();
        panel_Oben.SetActive(false);
        panel_Leben.SetActive(false);
        panel_Rechts.SetActive(false);
        panel_Victory.SetActive(true);
        Time.timeScale = 0f;
        TowerPlacer.Instance.AuswahlAufheben();
    }

    #endregion

    #region Herzen

    private void AktualisierHerzen(int leben)
    {
        herz_Eins.SetActive(leben >= 1);
        herz_Zwei.SetActive(leben >= 2);
        herz_Drei.SetActive(leben >= 3);
    }

    #endregion

    #region BUTTON FUNKTIONEN

    public void NeuStarten()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager
            .LoadScene(UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name);
    }

    public void WeiterZumMenue()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("00_TitleScreen");
    }

    public void Pausieren()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            Debug.Log("[UIManager] Spiel fortgesetzt.");
        }
        else
        {
            Time.timeScale = 0f;
            Debug.Log("[UIManager] Spiel pausiert.");
        }
    }

    public void ZumMenue()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("00_TitleScreen");
    }

    public void ZeigeBauliste()
    {
        // Logik zum Anzeigen der Bauliste implementieren
        panel_Upgrade.SetActive(false);
        panel_Bauliste.SetActive(true);
        Debug.Log("[UIManager] Bauliste-Panel angezeigt.");

    }

    public void ZeigeUpgrade()
    {
        // Logik zum Anzeigen der Upgrade-Optionen implementieren
        panel_Bauliste.SetActive(false);
        panel_Upgrade.SetActive(true);
        Debug.Log("[UIManager] Upgrade-Panel angezeigt.");
    }

    #endregion

}