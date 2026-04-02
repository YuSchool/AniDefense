// SonGoku.cs
// Zweck: Definieren der SonGoku-Tower-Klasse, die von TowerBase erbt. Aktuell enthält sie keine zusätzlichen Eigenschaften oder Methoden, kann aber in Zukunft erweitert werden, um spezifische Fähigkeiten oder Verhaltensweisen für den SonGoku-Tower zu implementieren.
// Sinn: Diese Klasse dient als Grundlage für die Implementierung eines speziellen Turms im Spiel, der möglicherweise einzigartige Angriffe oder Fähigkeiten haben könnte, die von der Basis-Tower-Klasse abweichen.

using UnityEngine;

public class SonGoku : TowerBase
{
    public override void SpezialAngriff()
    {
        // Hier könnte die spezifische Logik für den Spezialangriff von SonGoku implementiert werden.
        // Zum Beispiel könnte dies ein mächtiger Angriff sein, der mehrere Gegner gleichzeitig trifft oder zusätzlichen Schaden verursacht.
        Debug.Log("SonGoku führt seinen Spezialangriff aus!");
    }

}
