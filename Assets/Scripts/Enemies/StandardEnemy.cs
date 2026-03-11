// StandardEnemy.cs
// Zweck: Repräsentiert einen Standardgegner im Spiel, der grundlegende Angriffsverhalten aufweist. Diese Klasse erbt von EnemyBase und implementiert die spezifische Logik für den Angriff eines Standardgegners.
// Sinn: Ermöglicht es, verschiedene Gegnertypen mit unterschiedlichen Verhaltensweisen zu
// wird verwendet von: 
// Greift auf: EnemyBase, EnemyData, ResourceManager

using UnityEngine;

public class StandardEnemy : EnemyBase
{
    protected override void Start()
    {
        base.Start(); // Grundlegende Initialisierung aus EnemyBase
    }

    public override void Angreifen()
    {
        //
    }
}
