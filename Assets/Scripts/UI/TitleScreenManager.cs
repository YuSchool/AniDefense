// TitleScreenManager.cs
// Zweck: Steuert den TitleScreen — Panel-Navigation, Auflösung, Spiel beenden.
// Sinn: Zentralisiert alle Button-Aktionen des TitleScreens. Jeder Nav-Button
//       blendet das zugehörige Panel ein und alle anderen aus.
// Wird verwendet von: Canvas im 00_TitleScreen

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    #region Referenzen

    [Header("Panels")]
    [SerializeField] private GameObject panel_Home;
    [SerializeField] private GameObject panel_User;
    [SerializeField] private GameObject panel_Dungeons;
    [SerializeField] private GameObject panel_Items;
    [SerializeField] private GameObject panel_Summon;
    [SerializeField] private GameObject panel_Options;

    #endregion

    #region Auflösung

    [Header("Auflösung")]
    [SerializeField] private TMPro.TextMeshProUGUI text_Aufloesung;

    private int[] breiten = { 1280, 1920, 2560, 3440 };
    private int[] hoehen = { 720, 1080, 1440, 1440 };
    private string[] namen = { "1280x720", "1920x1080", "2560x1440", "3440x1440" };
    private int aktuellerIndex = 1;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        AktualisierAufloesung();
        ZeigePanel(panel_Home);
    }

    #endregion

    #region Panel Navigation

    public void ZeigeHome() { ZeigePanel(panel_Home); AudioManager.Instance?.ZeigeTitleScreenMusik(); }
    public void ZeigeUser() { ZeigePanel(panel_User); AudioManager.Instance?.ZeigeTitleScreenMusik(); }
    public void ZeigeDungeons() { ZeigePanel(panel_Dungeons); AudioManager.Instance?.ZeigeDungeonMusik(); }
    public void ZeigeItems() { ZeigePanel(panel_Items); AudioManager.Instance?.ZeigeTitleScreenMusik(); }
    public void ZeigeSummon() { ZeigePanel(panel_Summon); AudioManager.Instance?.ZeigeTitleScreenMusik(); }
    public void ZeigeOptionen() { ZeigePanel(panel_Options); AudioManager.Instance?.ZeigeTitleScreenMusik(); }

    private void ZeigePanel(GameObject zielPanel)
    {
        panel_Home.SetActive(false);
        panel_User.SetActive(false);
        panel_Dungeons.SetActive(false);
        panel_Items.SetActive(false);
        panel_Summon.SetActive(false);
        panel_Options.SetActive(false);

        if (zielPanel != null)
            zielPanel.SetActive(true);
    }

    #endregion

    #region Auflösung

    public void AufloeungsWeiter()
    {
        aktuellerIndex = (aktuellerIndex + 1) % breiten.Length;
        AktualisierAufloesung();
    }

    public void AufloeungsZurueck()
    {
        aktuellerIndex = (aktuellerIndex - 1 + breiten.Length) % breiten.Length;
        AktualisierAufloesung();
    }

    private void AktualisierAufloesung()
    {
        Screen.SetResolution(breiten[aktuellerIndex], hoehen[aktuellerIndex], true);
        if (text_Aufloesung != null)
            text_Aufloesung.text = namen[aktuellerIndex];
    }

    #endregion

    #region Spiel

    public void SpielBeenden()
    {
        Debug.Log("[TitleScreenManager] Spiel beenden.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}