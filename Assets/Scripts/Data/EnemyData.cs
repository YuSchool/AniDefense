// EnemyData.cs
// Zweck: Definiert die Datenstruktur für Feinde im Spiel.
// Sinn: Diese Klasse speichert alle relevanten Informationen über Feinde, die im Spiel verwendet werden.
// Kein Verhalten oder Logik, sondern nur Daten.

using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "AniDefense/Enemy Data")]
public class EnemyData : ScriptableObject // Erstellen eines ScriptableObject, eine EnemyData-Datei
{
    [Header("Identität")]
    public string enemyName = "Unbekannt"; // Name des Feindes
    public Rarity seltenheit = Rarity.R; // Seltenheit des Feindes
    public RarityClass seltenheitsKlasse = RarityClass.Rare; // Seltenheitsklasse des Feindes
    public EnemyType enemyTyp = EnemyType.Standard; // Typ des Feindes
    public Sprite enemySprite; // Sprite des Feindes
    [Header("Kampfwerte")]
    public float leben = 100f; // Leben des Feindes
    public float geschwindigkeit = 1f; // Geschwindigkeit des Feindes

    [Header("Belohnungen")]
    public int goldBelohnung = 10; // Goldbelohnung für das Besiegen des Feindes
    public int seelenBelohnung = 5; // Seelenbelohnung für das Besiegen des Feindes
    public int auraBelohnung = 10; // Aura-Belohnung für das Besiegen des Feindes

    [Header("Spezialfähigkeiten")]
    public bool hatSpezialfähigkeit = false; // Gibt an, ob der Feind eine Spezialfähigkeit hat

    [Header("Besonderheiten")]
    public bool istBoss = false; // Gibt an, ob der Feind ein Boss ist
    public bool istFliegend = false; // Gibt an, ob der Feind fliegend ist
}