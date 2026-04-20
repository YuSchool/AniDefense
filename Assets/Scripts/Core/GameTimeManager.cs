// GameTimeManager.cs
// Zweck: Verwaltet die Gesamtspielzeit – lädt sie beim Start, zählt hoch,
// zeigt sie im HUD an und speichert sie regelmäßig über den SaveManager.
// Wird verwendet von: Panel_Home → Text_Spielzeit im 00_TitleScreen

using UnityEngine;
using TMPro;

public class GameTimeManager : MonoBehaviour
{
    #region Referenzen

    [Header("Anzeige")]
    [SerializeField] private TextMeshProUGUI text_Spielzeit;

    [Header("Einstellungen")]
    [SerializeField] private float speicherIntervall = 30f; // Alle 30 Sekunden speichern

    #endregion

    #region Zustand

    private float _gesamtSpielzeit = 0f;
    private float _zeitSeitLetztemSpeichern = 0f;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        // Gespeicherte Spielzeit laden
        if (SaveManager.Instance != null)
        {
            _gesamtSpielzeit = SaveManager.Instance.Daten.gesamtSpielzeit;
            Debug.Log($"[SpielzeitManager] Spielzeit geladen: {FormatierteZeit(_gesamtSpielzeit)}");
        }

        AktualisierAnzeige();
    }

    private void Update()
    {
        _gesamtSpielzeit += Time.deltaTime;
        _zeitSeitLetztemSpeichern += Time.deltaTime;

        AktualisierAnzeige();

        // Automatisch speichern alle X Sekunden
        if (_zeitSeitLetztemSpeichern >= speicherIntervall)
        {
            Speichern();
            _zeitSeitLetztemSpeichern = 0f;
        }
    }

    private void OnApplicationQuit()
    {
        // Beim Beenden immer speichern
        Speichern();
    }

    private void OnApplicationPause(bool pause)
    {
        // Auf Mobile beim Pausieren speichern
        if (pause) Speichern();
    }

    #endregion

    #region Anzeige

    private void AktualisierAnzeige()
    {
        if (text_Spielzeit != null)
            text_Spielzeit.text = $"Spielzeit: {FormatierteZeit(_gesamtSpielzeit)}";
    }

    private string FormatierteZeit(float sekunden)
    {
        int h = Mathf.FloorToInt(sekunden / 3600f);
        int m = Mathf.FloorToInt((sekunden % 3600f) / 60f);
        int s = Mathf.FloorToInt(sekunden % 60f);

        if (h > 0)
            return $"{h:00}:{m:00}:{s:00}";
        else
            return $"{m:00}:{s:00}";
    }

    #endregion

    #region Speichern

    private void Speichern()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SpielzeitSpeichern(_gesamtSpielzeit);
            Debug.Log($"[SpielzeitManager] Spielzeit gespeichert: {FormatierteZeit(_gesamtSpielzeit)}");
        }
    }

    #endregion
}