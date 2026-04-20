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
        if (gegner.gameObject != this.gameObject) return;

        EnemyData data = gegner.Data;

        float mult = LevelDifficultyManager.Instance != null
            ? LevelDifficultyManager.Instance.Belohnung_Multiplikator : 1f;
                
        ResourceManager.Instance.AddGold(Mathf.RoundToInt(data.goldBelohnung * mult));
        ResourceManager.Instance.AddSeelen(Mathf.RoundToInt(data.seelenBelohnung * mult));
        ResourceManager.Instance.AddAura(data.auraBelohnung * mult);

        Debug.Log($"[DropHandler] Drops vergeben: " +
                  $"{Mathf.RoundToInt(data.goldBelohnung * mult)} Gold, " +
                  $"{Mathf.RoundToInt(data.seelenBelohnung * mult)} Seelen, " +
                  $"{data.auraBelohnung * mult} Aura.");
    }
    #endregion

}