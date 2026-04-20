// HUDController.cs
// Zweck: Steuert die Anzeige des HUD (Head-Up Display) im Spiel, einschließlich der Lebenspunkte, Ressourcen und anderer wichtiger Informationen für den Spieler.
// Sinn: Das HUD ist ein wichtiger Bestandteil der Benutzeroberfläche, da es dem Spieler wichtige Informationen über den Spielstatus liefert. Der HUDController ist verantwortlich für die Aktualisierung und Verwaltung dieser Informationen, um sicherzustellen, dass der Spieler immer über den aktuellen Zustand des Spiels informiert ist.
// Wird verwendet von: RessourceManager, GameManager


using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    #region Referenzen

    [Header("Ressourcen Texte")]
    [SerializeField] private TextMeshProUGUI text_Gold;
    [SerializeField] private TextMeshProUGUI text_Seelen;
    [SerializeField] private TextMeshProUGUI text_Aura;

    [Header("Status Texte")]
    [SerializeField] private TextMeshProUGUI text_Leben;
    [SerializeField] private TextMeshProUGUI text_Wave;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        ResourceManager.OnGoldGeaendert += AktualisierGold;
        ResourceManager.OnSeelenGeaendert += AktualisierSeelen;
        ResourceManager.OnAuraGeaendert += AktualisierAura;
        ResourceManager.OnLebenGeaendert += AktualisierLeben;
        GameManager.OnWaveGestartet += AktualisierWave;
    }

    private void OnDisable()
    {
        ResourceManager.OnGoldGeaendert -= AktualisierGold;
        ResourceManager.OnSeelenGeaendert -= AktualisierSeelen;
        ResourceManager.OnAuraGeaendert -= AktualisierAura;
        ResourceManager.OnLebenGeaendert -= AktualisierLeben;
        GameManager.OnWaveGestartet -= AktualisierWave;
    }

    private void Start()
    {
        // Startwerte sofort anzeigen
        AktualisierGold(ResourceManager.Instance.Gold);
        AktualisierSeelen(ResourceManager.Instance.Seelen);
        AktualisierAura(ResourceManager.Instance.Aura);
        AktualisierLeben(ResourceManager.Instance.Leben);
        AktualisierWave(GameManager.Instance.AktuelleWave);
    }

    #endregion

    #region Aktualisierungsmethoden

    private void AktualisierGold(int wert)
    {
        text_Gold.text = $"Gold: {wert}";
    }

    private void AktualisierSeelen(int wert)
    {
        text_Seelen.text = $"Seelen: {wert}";
    }

    private void AktualisierAura(float wert)
    {
        text_Aura.text = $"Aura: {wert:F0}";
    }

    private void AktualisierLeben(int wert)
    {
        text_Leben.text = $"Leben: {wert}";
    }

    private void AktualisierWave(int wert)
    {
        text_Wave.text = $"Wave: {wert} / {GameManager.Instance.MaxWaves}";
    }

    #endregion
}