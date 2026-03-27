// TitleScreenManager.cs
// Zweck: Ste
// Sinn: Der TitleScreenManager ist verantwortlich für die Verwaltung und Steuerung des Titelbildschirms des Spiels. 
// 


using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private string spielSzeneName;

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
        // Hier könntest du die Optionen-UI aktivieren oder eine neue Szene laden
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
