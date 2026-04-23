// OptionScreenManager.cs
// Zweck: Der OptionScreenManager ist verantwortlich für die Verwaltung und Steuerung des Optionsbildschirms des Spiels.
// Sinn: Der OptionScreenManager ermöglicht es den Spielern, verschiedene Einstellungen des Spiels anzupassen, wie z.B. die Auflösung, die Lautstärke oder andere spielbezogene Optionen. Er sorgt dafür, dass die Änderungen der Spieler korrekt angewendet und gespeichert werden, um ein personalisiertes Spielerlebnis zu bieten.
// Wird verwendet von: TitleScreenManager, um die Optionen zu verwalten und anzupassen, bevor das Spiel gestartet wird. Er könnte auch von anderen Teilen des Spiels aufgerufen werden, um die Einstellungen während des Spiels zu ändern oder zu überprüfen.


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreenManager : MonoBehaviour
{
    #region Navigation

    [Header("Navigation")]
    [SerializeField] private TextMeshProUGUI text_ReiterName;
    [SerializeField] private GameObject[] subPanels;

    private string[] reiterNamen = { "Auflösung", "Musik", "Steuerung", "Tutorial", "Credits" };
    private int aktuellerIndex = 0;

    public void NavigiereRechts()
    {
        aktuellerIndex = (aktuellerIndex + 1) % subPanels.Length;
        AktualisiereReiter();
    }

    public void NavigiereLinks()
    {
        aktuellerIndex = (aktuellerIndex - 1 + subPanels.Length) % subPanels.Length;
        AktualisiereReiter();
    }

    private void AktualisiereReiter()
    {
        foreach (var panel in subPanels)
        {
            panel.SetActive(false);
        }
        subPanels[aktuellerIndex].SetActive(true);
        text_ReiterName.text = reiterNamen[aktuellerIndex];
    }
    #endregion

    #region Auflösung

    [Header("SubPanelAuflösung")]
    [SerializeField] private TextMeshProUGUI text_Aufloesung;
    [SerializeField] private Toggle toggle_Vollbild;
    [SerializeField] private TextMeshProUGUI text_Grafik;
    [SerializeField] private TextMeshProUGUI text_FPS;
    [SerializeField] private Slider slider_Helligkeit;

    private string[] aufloesung_Werte = { "1280x720", "1920x1080", "2560x1440", "3440x1440" };
    private string[] grafik_Werte = { "Low", "Medium", "High" };
    private string[] fps_Werte = { "30 FPS", "60 FPS", "120 FPS" };

    private int aufloesung_Index = 1;
    private int grafik_Index = 1;
    private int fps_Index = 1;

    public void Aufloesung_Rechts() { aufloesung_Index = (aufloesung_Index + 1) % aufloesung_Werte.Length; text_Aufloesung.text = aufloesung_Werte[aufloesung_Index]; }
    public void Aufloesung_Links() { aufloesung_Index = (aufloesung_Index - 1 + aufloesung_Werte.Length) % aufloesung_Werte.Length; text_Aufloesung.text = aufloesung_Werte[aufloesung_Index]; }

    public void Grafik_Rechts() { grafik_Index = (grafik_Index + 1) % grafik_Werte.Length; text_Grafik.text = grafik_Werte[grafik_Index]; }
    public void Grafik_Links() { grafik_Index = (grafik_Index - 1 + grafik_Werte.Length) % grafik_Werte.Length; text_Grafik.text = grafik_Werte[grafik_Index]; }

    public void FPS_Rechts() { fps_Index = (fps_Index + 1) % fps_Werte.Length; text_FPS.text = fps_Werte[fps_Index]; }
    public void FPS_Links() { fps_Index = (fps_Index - 1 + fps_Werte.Length) % fps_Werte.Length; text_FPS.text = fps_Werte[fps_Index]; }

    #endregion

    #region Musik

    [Header("SubPanel — Musik")]
    [SerializeField] private Toggle toggle_Musik;
    [SerializeField] private Toggle toggle_SFX;
    [SerializeField] private Slider slider_MusicLautstaerke;
    [SerializeField] private Slider slider_SFXLautstaerke;
    [SerializeField] private Toggle toggle_StilleModus;

    public void OnMusikToggleGeaendert(bool aktiv)
    {
        AudioManager.Instance?.SetzeMusikLautstaerke(aktiv ? slider_MusicLautstaerke.value : 0f);
        Debug.Log($"[OptionScreenManager] Musik: {(aktiv ? "AN" : "AUS")}");
    }

    public void OnSFXToggleGeaendert(bool aktiv)
    {
        AudioManager.Instance?.SetzeSFXLautstaerke(aktiv ? slider_SFXLautstaerke.value : 0f);
        Debug.Log($"[OptionScreenManager] SFX: {(aktiv ? "AN" : "AUS")}");
    }

    public void OnMusikLautstaerkeGeaendert(float wert)
    {
        AudioManager.Instance?.SetzeMusikLautstaerke(wert);
        if (toggle_Musik != null && wert == 0f)
            toggle_Musik.isOn = false;
    }

    public void OnSFXLautstaerkeGeaendert(float wert)
    {
        AudioManager.Instance?.SetzeSFXLautstaerke(wert);
        if (toggle_SFX != null && wert == 0f)
            toggle_SFX.isOn = false;
    }

    public void OnStilleModusGeaendert(bool aktiv)
    {
        if (aktiv)
        {
            AudioManager.Instance?.SetzeMusikLautstaerke(0f);
            AudioManager.Instance?.SetzeSFXLautstaerke(0f);
            if (toggle_Musik != null) toggle_Musik.isOn = false;
            if (toggle_SFX != null) toggle_SFX.isOn = false;
        }
        else
        {
            AudioManager.Instance?.SetzeMusikLautstaerke(slider_MusicLautstaerke.value);
            AudioManager.Instance?.SetzeSFXLautstaerke(slider_SFXLautstaerke.value);
            if (toggle_Musik != null) toggle_Musik.isOn = true;
            if (toggle_SFX != null) toggle_SFX.isOn = true;
        }
        Debug.Log($"[OptionScreenManager] Stille-Modus: {(aktiv ? "AN" : "AUS")}");
    }

    #endregion

    #region Steuerung

    [Header("SubPanel — Steuerung")]
    [SerializeField] private Slider slider_Mausempfindlichkeit;
    [SerializeField] private Toggle toggle_Tooltips;
    [SerializeField] private Toggle toggle_Tutorial;
    [SerializeField] private Toggle toggle_Bestaetigung;
    [SerializeField] private TextMeshProUGUI text_Sprache;

    private string[] sprache_Werte = { "Deutsch", "English" };
    private int sprache_Index = 0;

    public void Sprache_Rechts() { sprache_Index = (sprache_Index + 1) % sprache_Werte.Length; text_Sprache.text = sprache_Werte[sprache_Index]; }
    public void Sprache_Links() { sprache_Index = (sprache_Index - 1 + sprache_Werte.Length) % sprache_Werte.Length; text_Sprache.text = sprache_Werte[sprache_Index]; }

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        // Startwerte setzen
        text_Aufloesung.text = aufloesung_Werte[aufloesung_Index];
        text_Grafik.text = grafik_Werte[grafik_Index];
        text_FPS.text = fps_Werte[fps_Index];
        text_Sprache.text = sprache_Werte[sprache_Index];

        // Ersten Reiter anzeigen
        aktuellerIndex = 0;
        AktualisiereReiter();


        // Startwerte für Musik-Optionen setzen
        slider_MusicLautstaerke.value = 1f;
        slider_SFXLautstaerke.value = 1f;

        // Musik-Optionen initialisieren
        slider_MusicLautstaerke.onValueChanged.AddListener(OnMusikLautstaerkeGeaendert);
        slider_SFXLautstaerke.onValueChanged.AddListener(OnSFXLautstaerkeGeaendert);
        toggle_Musik.onValueChanged.AddListener(OnMusikToggleGeaendert);
        toggle_SFX.onValueChanged.AddListener(OnSFXToggleGeaendert);
        toggle_StilleModus.onValueChanged.AddListener(OnStilleModusGeaendert);
    }

    #endregion

    #region Speichern & Schliessen

    public void Speichern()
    {
        // Vorerst nur optisch — PlayerPrefs später ergänzen
        Debug.Log("[OptionScreenManager] Einstellungen gespeichert (visuell).");
    }

    public void Schliessen()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
