// DungeonScreenManager.cs
// Zweck: Verwaltet den DungeonScreen — baut die Level-Karten auf und
//        liest den SaveManager aus um gesperrte Karten auszugrauen.
// Sinn: Zentralisiert die Logik der Levelauswahl. Neue Maps können einfach
//       durch Erweiterung des mapNamen-Arrays ergänzt werden.
// Wird verwendet von: Panel_Dungeons im 00_TitleScreen

using UnityEngine;

public class DungeonScreenManager : MonoBehaviour
{
    #region Referenzen

    [Header("Karten")]
    [SerializeField] private GameObject levelKartePrefab;
    [SerializeField] private Transform kartenContainer;

    #endregion

    #region Daten

    // Hier alle Maps eintragen — Reihenfolge bestimmt die Anzeige
    private readonly string[] mapNamen = { "Level_00", "Level_01", "Level_02" };

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        // Karten neu aufbauen wenn Panel geöffnet wird
        // (damit nach einem Spielerfolg die neu freigeschalteten Karten sofort sichtbar sind)
        BaueKartenAuf();
    }

    #endregion

    #region Karten aufbauen

    private void BaueKartenAuf()
    {
        // Alte Karten löschen
        foreach (Transform kind in kartenContainer)
            Destroy(kind.gameObject);

        // Für jede Map alle 3 Schwierigkeiten anlegen
        foreach (string mapName in mapNamen)
        {
            ErstelleKarte(mapName, Schwierigkeit.Leicht);
            ErstelleKarte(mapName, Schwierigkeit.Normal);
            ErstelleKarte(mapName, Schwierigkeit.Schwer);
        }
    }

    private void ErstelleKarte(string mapName, Schwierigkeit schwierigkeit)
    {
        bool freigeschaltet = true;

        // SaveManager prüfen falls vorhanden
        if (SaveManager.Instance != null)
        {
            freigeschaltet = SaveManager.Instance.IstFreigeschaltet(mapName, schwierigkeit);
        }
        else
        {
            // Kein SaveManager — nur Leicht spielbar (Fallback für Tests ohne TitleScreen)
            freigeschaltet = schwierigkeit == Schwierigkeit.Leicht;
            Debug.LogWarning("[DungeonScreenManager] Kein SaveManager gefunden – Fallback aktiv.");
        }

        GameObject karteObj = Instantiate(levelKartePrefab, kartenContainer);

        LevelKarteUI karte = karteObj.GetComponent<LevelKarteUI>();
        if (karte != null)
        {
            karte.Initialisiere(mapName, schwierigkeit, freigeschaltet);
        }
        else
        {
            Debug.LogError("[DungeonScreenManager] LevelKartePrefab hat kein LevelKarteUI-Script!");
        }
    }

    #endregion
}