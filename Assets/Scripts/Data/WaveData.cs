// WaveData.cs
// Zweck: Definiert die Daten für eine Welle von Feinden, die in einem Spiel erscheinen sollen.
// Sinn: Diese Klasse speichert alle relevanten Informationen über eine Welle.
// Kein Verhalten oder Logik, sondern nur Daten.


using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "AniDefense/WaveData")]
public class WaveData : ScriptableObject
{
    public int waveNummer = 1; // Nummer der Welle, z.B. 1, 2, 3, etc.
    public bool istBossWelle = false; // Gibt an, ob diese Welle eine Bosswelle ist
    public WaveEntry[] waveEintraege; // Array von WaveEntry, die die verschiedenen Feindtypen und deren Anzahl in dieser Welle definieren
}
