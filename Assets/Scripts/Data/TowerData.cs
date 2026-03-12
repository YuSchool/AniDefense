// TowerData.cs
// Zweck: Definiert die Daten für Türme im Spiel.
// Sinn: Diese Klasse speichert alle relevanten Informationen über Türme, die im Spiel verwendet werden.
// Kein Verhalten oder Logik, sondern nur Daten.

using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData", menuName = "AniDefense/Tower Data")]
public class  TowerData : ScriptableObject // Erstellen eines ScriptableObject, eine TowerData-Datei
{
    [Header("Identität")]
    public string charakterName = "Unbekannt"; // Name des Charakters
    public Rarity seltenheit = Rarity.R; // Seltenheit des Turms
    public RarityClass seltenheitsKlasse = RarityClass.Rare; // Seltenheitsklasse des Turms
    public TowerType towerTyp = TowerType.Melee; // Typ des Turms
    public Sprite charakterSprite; // Sprite des Charakters

    [Header("Stufe")]
    public int stufe = 1; // Aktuelle Stufe des Turms
    public TowerData upgradeTo; // Referenz auf die Daten des nächsten Upgrades

    [Header("Kampfwerte")]
    public float schaden = 10f; // Schaden, den der Turm verursacht
    public float reichweite = 3f; // Reichweite des Turms
    public float angriffsgeschwindigkeit = 1f; // Angriffsgeschwindigkeit des Turms
    public TargetPriority zielPrioritaet = TargetPriority.First; // Priorität bei der Zielerfassung (z.B. First, Last, Strongest, etc.)

    [Header("Kosten")]
    public int goldKosten = 100; // Kosten in Gold, um den Turm zu bauen
    public int seelenKosten = 50; // Kosten in Seelen, um den Turm zu bauen
}