// LevelKarteUI.cs
// Zweck: Steuert eine einzelne Level-Karte im DungeonScreen.
// Sinn: Jede Karte kennt ihre eigene Map und Schwierigkeit, zeigt ob sie
//       freigeschaltet ist, und startet beim Klick die richtige Szene.
// Wird verwendet von: DungeonScreenManager — wird pro Karte instanziiert und befüllt.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelKarteUI : MonoBehaviour
{
    #region Referenzen

    [SerializeField] private TextMeshProUGUI text_MapName;
    [SerializeField] private TextMeshProUGUI text_Schwierigkeit;
    [SerializeField] private Button button;
    [SerializeField] private Image hintergrundImage;

    #endregion

    #region Farben

    [Header("Farben")]
    [SerializeField] private Color farbe_Freigeschaltet = Color.white;
    [SerializeField] private Color farbe_Gesperrt = new Color(0.4f, 0.4f, 0.4f, 1f);

    #endregion

    #region Daten

    private string _mapName;
    private Schwierigkeit _schwierigkeit;

    #endregion

    #region Initialisierung

    /// <summary>
    /// Wird von DungeonScreenManager aufgerufen um die Karte zu befüllen.
    /// </summary>
    public void Initialisiere(string mapName, Schwierigkeit schwierigkeit, bool freigeschaltet)
    {
        _mapName = mapName;
        _schwierigkeit = schwierigkeit;

        text_MapName.text = mapName;
        text_Schwierigkeit.text = schwierigkeit.ToString();

        SetzeSperrStatus(freigeschaltet);
    }

    #endregion

    #region Sperr-Status

    private void SetzeSperrStatus(bool freigeschaltet)
    {
        button.interactable = freigeschaltet;
        hintergrundImage.color = freigeschaltet ? farbe_Freigeschaltet : farbe_Gesperrt;

        if (freigeschaltet)
        {
            button.onClick.AddListener(OnKarteGeklickt);
        }
    }

    #endregion

    #region Klick

    private void OnKarteGeklickt()
    {
        SaveManager.AktuelleMap = _mapName;
        SaveManager.AktuelleSchwierigkeit = _schwierigkeit;

        Debug.Log($"[LevelKarteUI] Starte {_mapName} auf {_schwierigkeit}");

        UnityEngine.SceneManagement.SceneManager.LoadScene(_mapName);
    }

    #endregion
}