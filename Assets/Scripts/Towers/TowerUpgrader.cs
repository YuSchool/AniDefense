// TowerUpgrader.cs
// Zweck: Verantwortlich für die Upgrade-Logik der Türme. Diese Komponente prüft, ob ein Turm aufgerüstet werden kann, und führt das Upgrade durch, wenn die Bedingungen erfüllt sind.
// Sinn: Ermöglicht es den Spielern, ihre Türme zu verbessern, um stärkere Angriffe oder zusätzliche Fähigkeiten freizuschalten, was die strategische Tiefe des Spiels erhöht.
// Hinweis: Diese Komponente sollte an denselben GameObject wie die TowerBase-Komponente angehängt werden, damit sie auf die Turmdaten zugreifen kann.
// wird verwendet von: TowerBase, ResourceManager

using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    #region Komponenten

    private TowerBase towerBase;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        towerBase = GetComponent<TowerBase>();
    }

    #endregion

    #region Upgrade-Logik

    public bool KannUpgraden()
    {
        if (towerBase.Data.upgradeTo == null) return false; // Kein Upgrade verfügbar
        if (ResourceManager.Instance.Seelen < towerBase.Data.seelenKosten) return false; // Nicht genug Ressourcen
        if (ResourceManager.Instance.Gold < towerBase.Data.goldKosten) return false;
        if (ResourceManager.Instance.Aura < towerBase.Data.auraKosten) return false;
        return true;
    }

    public void Upgrade()
    {
        if (!KannUpgraden()) return; // Sicherheitshalber nochmal prüfen

        ResourceManager.Instance.SpendSeelen(towerBase.Data.seelenKosten); // Kosten abziehen
        ResourceManager.Instance.SpendGold(towerBase.Data.goldKosten); 
        ResourceManager.Instance.SpendAura(towerBase.Data.auraKosten);

        towerBase.SetzeData(towerBase.Data.upgradeTo); // Upgrade durchführen

        Debug.Log($"[TowerUpgrader] Upgrade auf Stufe {towerBase.Data.stufe}.");
    }

    #endregion
}