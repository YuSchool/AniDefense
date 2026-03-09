// WaveEntry.cs
// Zweck: Definiert die Datenstruktur für Einträge in einer Welle von Feinden.
// Sinn: Diese Struktur speichert Informationen über die Anzahl und den Typ der Feinde, die in einer Welle erscheinen sollen.
// Kein Verhalten oder Logik, sondern nur Daten.
// Wird verwendet von: WaveData.cs, um die Feindtypen und deren Anzahl in einer Welle zu definieren.

using UnityEngine;

[System.Serializable]
public struct WaveEntry
{
    public EnemyData enemyData; // Referenz auf die EnemyData, die den Typ des Feindes definiert
    public int anzahl; // Anzahl der Feinde dieses Typs, die in der Welle erscheinen sollen
    public float spawnAbstand; // Zeitabstand zwischen dem Spawnen der einzelnen Feinde dieses Typs in der Welle
}
