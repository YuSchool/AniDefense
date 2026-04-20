// SaveManager.cs
// Zweck: Verwaltet das Speichern und Laden des Spielstands als JSON-Datei.
// Sinn: Persistenter Spielfortschritt – freigeschaltete Schwierigkeiten, abgeschlossene
//       Level und später Items/Charaktere bleiben nach Spielneustart erhalten.
// Wird verwendet von: DungeonScreenManager (lesen), GameManager (schreiben bei Victory),
//         GameTimeManager (lesen und schreiben der Gesamtspielzeit), TitleScreenManager (Debug: Spielstand zurücksetzen)

using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    #region Singleton

    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _speicherpfad = Path.Combine(Application.persistentDataPath, "spielstand.json");
        LadeSpielstand();
    }

    #endregion

    #region Temporäre Session-Daten

    // Wird von DungeonScreenManager gesetzt, von GameManager beim Start ausgelesen
    public static Schwierigkeit AktuelleSchwierigkeit { get; set; } = Schwierigkeit.Leicht;
    public static string AktuelleMap { get; set; } = "Level_00";

    #endregion

    #region Spielstand

    public Spielstand Daten { get; private set; }

    private string _speicherpfad;

    #endregion

    #region Speichern & Laden

    public void Speichern()
    {
        string json = JsonUtility.ToJson(Daten, prettyPrint: true);
        File.WriteAllText(_speicherpfad, json);
        Debug.Log($"[SaveManager] Spielstand gespeichert: {_speicherpfad}");
    }

    private void LadeSpielstand()
    {
        if (File.Exists(_speicherpfad))
        {
            string json = File.ReadAllText(_speicherpfad);
            Daten = JsonUtility.FromJson<Spielstand>(json);
            Debug.Log("[SaveManager] Spielstand geladen.");
        }
        else
        {
            Daten = new Spielstand();
            Debug.Log("[SaveManager] Kein Spielstand gefunden – neuer Spielstand erstellt.");
        }
    }

    #endregion

    #region Fortschritt

    public void LevelAbgeschlossen(string mapName, Schwierigkeit schwierigkeit)
    {
        bool geaendert = false;

        switch (mapName)
        {
            case "Level_00":
                if (schwierigkeit == Schwierigkeit.Leicht && !Daten.level00_Leicht_Abgeschlossen)
                {
                    Daten.level00_Leicht_Abgeschlossen = true;
                    Daten.level00_Normal_Freigeschaltet = true;
                    Daten.level01_Leicht_Freigeschaltet = true;
                    geaendert = true;
                }
                else if (schwierigkeit == Schwierigkeit.Normal && !Daten.level00_Normal_Abgeschlossen)
                {
                    Daten.level00_Normal_Abgeschlossen = true;
                    Daten.level00_Schwer_Freigeschaltet = true;
                    geaendert = true;
                }
                else if (schwierigkeit == Schwierigkeit.Schwer && !Daten.level00_Schwer_Abgeschlossen)
                {
                    Daten.level00_Schwer_Abgeschlossen = true;
                    Daten.level02_Leicht_Freigeschaltet = true;
                    geaendert = true;
                }
                break;

            case "Level_01":
                if (schwierigkeit == Schwierigkeit.Leicht && !Daten.level01_Leicht_Abgeschlossen)
                {
                    Daten.level01_Leicht_Abgeschlossen = true;
                    Daten.level01_Normal_Freigeschaltet = true;
                    geaendert = true;
                }
                else if (schwierigkeit == Schwierigkeit.Normal && !Daten.level01_Normal_Abgeschlossen)
                {
                    Daten.level01_Normal_Abgeschlossen = true;
                    Daten.level01_Schwer_Freigeschaltet = true;
                    geaendert = true;
                }
                else if (schwierigkeit == Schwierigkeit.Schwer && !Daten.level01_Schwer_Abgeschlossen)
                {
                    Daten.level01_Schwer_Abgeschlossen = true;
                    geaendert = true;
                }
                break;

            case "Level_02":
                if (schwierigkeit == Schwierigkeit.Leicht && !Daten.level02_Leicht_Abgeschlossen)
                {
                    Daten.level02_Leicht_Abgeschlossen = true;
                    Daten.level02_Normal_Freigeschaltet = true;
                    geaendert = true;
                }
                else if (schwierigkeit == Schwierigkeit.Normal && !Daten.level02_Normal_Abgeschlossen)
                {
                    Daten.level02_Normal_Abgeschlossen = true;
                    Daten.level02_Schwer_Freigeschaltet = true;
                    geaendert = true;
                }
                else if (schwierigkeit == Schwierigkeit.Schwer && !Daten.level02_Schwer_Abgeschlossen)
                {
                    Daten.level02_Schwer_Abgeschlossen = true;
                    geaendert = true;
                }
                break;
        }

        if (geaendert)
        {
            Speichern();
            Debug.Log($"[SaveManager] {mapName} – {schwierigkeit} abgeschlossen und gespeichert.");
        }
    }

    public bool IstFreigeschaltet(string mapName, Schwierigkeit schwierigkeit)
    {
        switch (mapName)
        {
            case "Level_00":
                if (schwierigkeit == Schwierigkeit.Leicht) return true;
                if (schwierigkeit == Schwierigkeit.Normal) return Daten.level00_Normal_Freigeschaltet;
                if (schwierigkeit == Schwierigkeit.Schwer) return Daten.level00_Schwer_Freigeschaltet;
                break;

            case "Level_01":
                if (schwierigkeit == Schwierigkeit.Leicht) return Daten.level01_Leicht_Freigeschaltet;
                if (schwierigkeit == Schwierigkeit.Normal) return Daten.level01_Normal_Freigeschaltet;
                if (schwierigkeit == Schwierigkeit.Schwer) return Daten.level01_Schwer_Freigeschaltet;
                break;

            case "Level_02":
                if (schwierigkeit == Schwierigkeit.Leicht) return Daten.level02_Leicht_Freigeschaltet;
                if (schwierigkeit == Schwierigkeit.Normal) return Daten.level02_Normal_Freigeschaltet;
                if (schwierigkeit == Schwierigkeit.Schwer) return Daten.level02_Schwer_Freigeschaltet;
                break;

        }
        return false;
    }

    #endregion

    #region Spielzeit

    public void SpielzeitSpeichern(float sekunden)
    {
        Daten.gesamtSpielzeit = sekunden;
        Speichern();
    }

    #endregion

    #region Debug

    [ContextMenu("Spielstand zurücksetzen")]
    public void SpielsstandZuruecksetzen()
    {
        Daten = new Spielstand();
        Speichern();
        Debug.Log("[SaveManager] Spielstand wurde zurückgesetzt.");
    }

    #endregion
}

[System.Serializable]
public class Spielstand
{
    // Level_00
    public bool level00_Leicht_Abgeschlossen = false;
    public bool level00_Normal_Freigeschaltet = false;
    public bool level00_Normal_Abgeschlossen = false;
    public bool level00_Schwer_Freigeschaltet = false;
    public bool level00_Schwer_Abgeschlossen = false;

    // Level_01
    public bool level01_Leicht_Freigeschaltet = false;
    public bool level01_Leicht_Abgeschlossen = false;
    public bool level01_Normal_Freigeschaltet = false;
    public bool level01_Normal_Abgeschlossen = false;
    public bool level01_Schwer_Freigeschaltet = false;
    public bool level01_Schwer_Abgeschlossen = false;
    
    // Level_02
    public bool level02_Leicht_Freigeschaltet = false;
    public bool level02_Leicht_Abgeschlossen = false;
    public bool level02_Normal_Freigeschaltet = false;
    public bool level02_Normal_Abgeschlossen = false;
    public bool level02_Schwer_Freigeschaltet = false;
    public bool level02_Schwer_Abgeschlossen = false;


    // Spielzeit in Sekunden
    public float gesamtSpielzeit = 0f;

    // Später erweiterbar:
    // public int gold;
    // public List<string> freigeschalteteItems = new List<string>();
    // public int sonGoku_Level = 1;
}