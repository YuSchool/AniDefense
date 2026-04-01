// TitleScreenManager.cs
// Zweck: Ste
// Sinn: Der TitleScreenManager ist verantwortlich für die Verwaltung und Steuerung des Titelbildschirms des Spiels. 
// 


using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private string spielSzeneName;
    [SerializeField] private GameObject panel_Options;

    [Header("Auflösungen")]
    [SerializeField] private TMPro.TextMeshProUGUI text_Aufloesung;

    private int[] breiten = { 1280, 1920, 2560, 3440 };
    private int[] hoehen = { 720, 1080, 1440, 1440 };
    private string[] namen = { "1280x720", "1920x1080", "2560x1440", "3440x1440" };
    private int aktuellerIndex = 1;

    private void Start()
    {
        AktualisierAufloesung();
    }

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

    public void NeuesSpielStarten()
    {
        Debug.Log($"{Pressed("Neues Spiel")}");
    
        if (string.IsNullOrEmpty(spielSzeneName))
        {
            Debug.LogError("[TitleScreenManager] Kein Szenenname für das Spiel festgelegt!");
            return;
        }

        SceneManager.LoadScene(spielSzeneName);
    }

    public void OptionenStarten()
    {
        Debug.Log($"{Pressed("Optionen")}");
        panel_Options.SetActive(!panel_Options.activeSelf); // Toggle-Logik für das Optionspanel
    }

    public void SpielBeenden()
    {
        Debug.Log($"{Pressed("Spiel Beenden")}");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();

#endif
    }

    private string Pressed(string buttonName) => $"Button '{buttonName}' gedrückt";

}
