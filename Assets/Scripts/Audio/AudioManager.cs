// AudioManager.cs
// Zweck: Zentraler Manager für alle Audio-Ausgaben im Spiel.
// Sinn: Ein einziger Ort für Musik und SFX. Alle anderen Scripts rufen nur
//       AudioManager.Instance.SpieleSFX(...) auf — kein Script muss selbst
//       AudioSources verwalten.
// Wird verwendet von: TitleScreenManager, LevelKarteUI, UIManager, GameManager,
//                     ResourceManager, TowerPlacer, TowerUpgrader, DropHandler

using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    // ===================================================================
    // AUDIO SOURCES
    // ===================================================================

    #region Audio Sources

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musikSource;   // Loop = true  im Inspector
    [SerializeField] private AudioSource sfxSource;     // Loop = false im Inspector

    #endregion

    // ===================================================================
    // BEREICH 1 — TITELSCREEN & PANELS
    // ===================================================================

    #region TitleScreen — Musik

    [Header("-- TITELSCREEN MUSIK --")]
    [SerializeField] private AudioClip musik_TitleScreen;       // Läuft auf allen Panels außer Dungeons

    #endregion

    #region TitleScreen — Button SFX

    [Header("-- TITELSCREEN BUTTONS --")]
    [SerializeField] private AudioClip sfx_ButtonKlick;         // Alle normalen Buttons auf allen Panels
    [SerializeField] private AudioClip sfx_ButtonOhneFunktion;  // News, Events, Coming Soon Buttons

    #endregion

    #region TitleScreen — UI Elemente SFX

    [Header("-- TITELSCREEN UI ELEMENTE --")]
    [SerializeField] private AudioClip sfx_ToggleUmschalten;    // Toggle Ein/Aus
    [SerializeField] private AudioClip sfx_SliderBewegen;       // Slider ziehen

    #endregion

    #region TitleScreen — Dungeon Auswahl SFX

    [Header("-- DUNGEON AUSWAHL --")]
    [SerializeField] private AudioClip musik_DungeonAuswahl;    // Musik speziell für Dungeon-Panel
    [SerializeField] private AudioClip sfx_LevelAuswahlKlick;   // Klick auf freigeschaltete Level-Karte
    [SerializeField] private AudioClip sfx_LevelGesperrtKlick;  // Klick auf gesperrte Level-Karte

    #endregion

    // ===================================================================
    // BEREICH 2 — LEVEL
    // ===================================================================

    #region Level — Hintergrundmusik

    [Header("-- LEVEL MUSIK --")]
    [SerializeField] private AudioClip musik_Level_00;  // Level_00 Leicht = Normal = Schwer
    [SerializeField] private AudioClip musik_Level_01;  // Level_01 Leicht = Normal = Schwer
    [SerializeField] private AudioClip musik_Level_02;  // Level_02 Leicht = Normal = Schwer

    #endregion

    #region Level — Charakter SFX

    [Header("-- LEVEL CHARAKTER --")]
    [SerializeField] private AudioClip sfx_CharakterAuswahl;    // Turm in der Bauliste angeklickt
    [SerializeField] private AudioClip sfx_CharakterSetzen;     // Turm auf Baufeld platziert
    [SerializeField] private AudioClip sfx_CharakterUpgrade;    // Turm erfolgreich upgraded

    #endregion

    #region Level — Gameplay SFX

    [Header("-- LEVEL GAMEPLAY --")]
    [SerializeField] private AudioClip sfx_LebenVerlust;        // Gegner erreicht Basis — Leben -1
    [SerializeField] private AudioClip sfx_GegnerTod;           // Gegner stirbt
    [SerializeField] private AudioClip sfx_WaveStarten;         // Wave-Start Button gedrückt
    [SerializeField] private AudioClip sfx_Einstellungen;       // Einstellungs-Button im HUD
    [SerializeField] private AudioClip sfx_Belohnung;           // Belohnung nach Gegner-Tod
    [SerializeField] private AudioClip sfx_Victory;              // Victory Screen erscheint
    [SerializeField] private AudioClip sfx_GameOver;             // GameOver Screen erscheint

    #endregion

    // ===================================================================
    // UNITY LIFECYCLE
    // ===================================================================

    #region Unity Lifecycle

    private void Start()
    {
        // Szenen-Wechsel abonnieren damit Musik automatisch wechselt
        SceneManager.sceneLoaded += OnSzeneGeladen;

        // Musik für aktuelle Szene starten
        StarteMusikFuerAktiveSzene();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSzeneGeladen;
    }

    private void OnSzeneGeladen(Scene szene, LoadSceneMode mode)
    {
        StarteMusikFuerAktiveSzene();
    }

    private void StarteMusikFuerAktiveSzene()
    {
        string szenenName = SceneManager.GetActiveScene().name;

        switch (szenenName)
        {
            case "00_TitleScreen":
                SpieleMusik(musik_TitleScreen);
                break;
            case "Level_00":
                SpieleMusik(musik_Level_00);
                break;
            case "Level_01":
                SpieleMusik(musik_Level_01);
                break;
            case "Level_02":
                SpieleMusik(musik_Level_02);
                break;
        }
    }

    #endregion

    // ===================================================================
    // ÖFFENTLICHE METHODEN — TITELSCREEN
    // ===================================================================

    #region TitleScreen — Öffentliche Methoden

    /// <summary>
    /// Wird von TitleScreenManager aufgerufen wenn Dungeons-Panel geöffnet wird.
    /// Wechselt Musik zu Dungeon-Auswahl-Musik.
    /// </summary>
    public void ZeigeDungeonMusik()
    {
        SpieleMusik(musik_DungeonAuswahl);
    }

    /// <summary>
    /// Wird von TitleScreenManager aufgerufen wenn ein anderes Panel (nicht Dungeons) geöffnet wird.
    /// Wechselt zurück zur normalen TitleScreen-Musik.
    /// </summary>
    public void ZeigeTitleScreenMusik()
    {
        SpieleMusik(musik_TitleScreen);
    }

    public void StoppeMusik()
    {
        if (musikSource != null)
            musikSource.Stop();
    }

    public void SpieleSFX_ButtonKlick() => SpieleSFX(sfx_ButtonKlick);
    public void SpieleSFX_ButtonOhneFunktion() => SpieleSFX(sfx_ButtonOhneFunktion);
    public void SpieleSFX_Toggle() => SpieleSFX(sfx_ToggleUmschalten);
    public void SpieleSFX_Slider() => SpieleSFX(sfx_SliderBewegen);
    public void SpieleSFX_LevelAuswahl() => SpieleSFX(sfx_LevelAuswahlKlick);
    public void SpieleSFX_LevelGesperrt() => SpieleSFX(sfx_LevelGesperrtKlick);

    #endregion

    // ===================================================================
    // ÖFFENTLICHE METHODEN — LEVEL
    // ===================================================================

    #region Level — Öffentliche Methoden

    public void SpieleSFX_CharakterAuswahl() => SpieleSFX(sfx_CharakterAuswahl);
    public void SpieleSFX_CharakterSetzen() => SpieleSFX(sfx_CharakterSetzen);
    public void SpieleSFX_CharakterUpgrade() => SpieleSFX(sfx_CharakterUpgrade);
    public void SpieleSFX_LebenVerlust() => SpieleSFX(sfx_LebenVerlust);
    public void SpieleSFX_GegnerTod() => SpieleSFX(sfx_GegnerTod);
    public void SpieleSFX_WaveStarten() => SpieleSFX(sfx_WaveStarten);
    public void SpieleSFX_Einstellungen() => SpieleSFX(sfx_Einstellungen);
    public void SpieleSFX_Belohnung() => SpieleSFX(sfx_Belohnung);
    public void SpieleSFX_Victory() => SpieleSFX(sfx_Victory);
    public void SpieleSFX_GameOver() => SpieleSFX(sfx_GameOver);

    #endregion

    // ===================================================================
    // LAUTSTÄRKE
    // ===================================================================

    #region Lautstärke

    public void SetzeMusikLautstaerke(float wert)
    {
        if (musikSource != null)
            musikSource.volume = wert;
    }

    public void SetzeSFXLautstaerke(float wert)
    {
        if (sfxSource != null)
            sfxSource.volume = wert;
    }

    #endregion

    // ===================================================================
    // PRIVATE HILFSMETHODEN
    // ===================================================================

    #region Private Hilfsmethoden

    private void SpieleMusik(AudioClip clip)
    {
        if (clip == null) return;
        if (musikSource.clip == clip && musikSource.isPlaying) return; // Gleiche Musik läuft schon

        musikSource.clip = clip;
        musikSource.loop = true;
        musikSource.Play();
    }

    private void SpieleSFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    #endregion
}