// DropHandler.cs
// Zweck: Handhabt das Ablegen von Gegenständen oder Belohnungen, wenn ein Gegner besiegt wird. Kann verwendet werden, um Power-Ups, Münzen oder andere Belohnungen zu generieren.
// Sinn: Ermöglicht es Gegnern, Belohnungen fallen zu lassen, was ein wichtiger Anreiz für Spieler ist, Gegner zu besiegen und das Spiel voranzutreiben.
// Hinweis: Die eigentliche Logik zum Generieren von Gegenständen oder Belohnungen würde in dieser Klasse implementiert werden, wahrscheinlich in einer Methode, die aufgerufen wird, wenn der Gegner besiegt wird.
// Wird verwendet von: 
// Greift auf: EnemyBase, EnemyData, ResourceManager

using UnityEngine;

public class DropHandler : MonoBehaviour
{
    #region UNITY LEBENSZYKLUS
    private void OnEnable() // Registriere das Ereignis, wenn der Gegner gestorben ist
    {
        EnemyBase.OnEnemyGestorben += OnGegnerGestorben; // += bedeutet, dass die Methode OnGegnerGestorben aufgerufen wird, wenn das Ereignis OnEnemyGestorben ausgelöst wird.
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyGestorben -= OnGegnerGestorben; // -= bedeutet, dass die Methode OnGegnerGestorben nicht mehr aufgerufen wird, wenn das Ereignis OnEnemyGestorben ausgelöst wird.
    }
    #endregion

    #region DROP LOGIK
    private void OnGegnerGestorben(EnemyBase gegner)
    {
        // Überprüfen, ob der gestorbene Gegner dieses GameObject ist, um sicherzustellen, dass die Belohnungen nur für den richtigen Gegner vergeben werden.
        // Wenn der gestorbene Gegner dieses GameObject ist, wird die Methode verlassen, da wir keine Belohnungen vergeben möchten.
        if (gegner.gameObject != this.gameObject) return; 

        EnemyData data = gegner.Data; // Zugriff auf die EnemyData des gestorbenen Gegners, um die Belohnungen zu erhalten.
        ResourceManager.Instance.AddGold(data.goldBelohnung); // Vergeben der Goldbelohnung an den Spieler, indem die AddGold-Methode des ResourceManager aufgerufen wird.
        ResourceManager.Instance.AddSeelen(data.seelenBelohnung); // Vergeben der Seelenbelohnung an den Spieler, indem die AddSeelen-Methode des ResourceManager aufgerufen wird.
        ResourceManager.Instance.AddAura(data.auraBelohnung); // Vergeben der Aurabelohnung an den Spieler, indem die AddAura-Methode des ResourceManager aufgerufen wird.

        Debug.Log($"[DropHandler] Drops vergeben: " +
                  $"{data.goldBelohnung} Gold, " +
                  $"{data.seelenBelohnung} Seelen, " +
                  $"{data.auraBelohnung} Aura.");

    }
    #endregion

}