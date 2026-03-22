// TowerShooter.cs
// Zweck: Diese Klasse ist verantwortlich für die Schusslogik der Türme. Sie enthält die Methode "Schiesse", die den Schaden an einem Ziel berechnet und anwendet.
// Sinn: Durch die Trennung der Schusslogik in eine eigene Klasse wird die Wartbarkeit und Erweiterbarkeit des Codes verbessert. Türme können unterschiedliche Schussverhalten implementieren, indem sie verschiedene Shooter-Komponenten verwenden, ohne die grundlegende Zielerfassung und Angriffslogik zu verändern.
// 
// wird verwendet von: TowerBase.cs

using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    #region Schuss-Logik

    public void Schiesse(EnemyBase ziel, float schaden)
    {
        if (ziel == null) return; // Sicherheitscheck, falls kein Ziel vorhanden ist
        if (ziel.IstTot) return; // Überprüfen, ob das Ziel bereits tot ist, um unnötige Aktionen zu vermeiden

        ziel.TakeDamage(schaden);
        //Debug.Log($"[TowerShooter] {schaden} Schaden an {ziel.Data.enemyName}.");
    }

    #endregion
}