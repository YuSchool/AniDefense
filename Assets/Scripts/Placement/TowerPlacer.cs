using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    #region Singleton

    public static TowerPlacer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion

    #region Daten

    [Header("Tilemap")]
    [SerializeField] private Tilemap baufelderTilemap;

    [Header("Tower")]
    [SerializeField] private Camera hauptKamera;

    [Header("Upgrade UI")]
    [SerializeField] private UpgradeUI upgradeUI;

    private TowerBase ausgewaehlterPlatzierterTower = null;
    private TowerData ausgewaehlterTower = null;
    private GameObject ausgewaehltesPrefab = null;

    private Dictionary<Vector3Int, GameObject> platzierteTower
        = new Dictionary<Vector3Int, GameObject>();

    #endregion

    #region Unity Lifecycle

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        // Prüfen ob ein platzierter Tower ausgewählt ist
        
        Vector3 weltPosition = hauptKamera.ScreenToWorldPoint(Input.mousePosition);
        weltPosition.z = 0f;

        // Prüfen ob ein platzierter Tower angeklickt wurde
        RaycastHit2D hit = Physics2D.Raycast(weltPosition, Vector2.zero);
        if (hit.collider != null)
        {
            TowerBase tower = hit.collider.GetComponent<TowerBase>();
            if (tower != null)
            {
                // Tower auswählen und Upgrade-UI anzeigen
                WaehlePlatzierterTower(tower);
                return; // Früher Rückkehr, damit kein neuer Tower platziert wird
            }
        }

        // Wenn nichts getroffen wurde durch den Raycast, Auswahl aufheben
        if (upgradeUI != null)
            upgradeUI.SchliesseUpgrade();

        // Sonst normalen Platzierungsprozess fortsetzen

        if (ausgewaehlterTower == null) return;
        

        Vector3Int tilePosition = baufelderTilemap.WorldToCell(weltPosition);

        if (!baufelderTilemap.HasTile(tilePosition))
        {
            Debug.Log("[TowerPlacer] Kein Baufeld an dieser Position.");
            return;
        }

        if (platzierteTower.ContainsKey(tilePosition))
        {
            Debug.Log("[TowerPlacer] Dieses Feld ist bereits belegt.");
            return;
        }

        if (!ResourceManager.Instance.KannGoldAusgeben(ausgewaehlterTower.goldKosten))
        {
            Debug.Log("[TowerPlacer] Nicht genug Gold.");
            return;
        }

        BaueTower(tilePosition);
    }

    #endregion

    #region Öffentliche Methoden

    public void WaehleTower(TowerData data, GameObject prefab)
    {
        ausgewaehlterTower = data;
        ausgewaehltesPrefab = prefab;
        Debug.Log($"[TowerPlacer] Tower ausgewählt: {data.charakterName}");
    }

    public void AuswahlAufheben()
    {
        ausgewaehlterTower = null;
        ausgewaehltesPrefab = null;
    }

    #endregion

    #region Private Methoden

    private void BaueTower(Vector3Int tilePosition)
    {
        Vector3 weltMitte = baufelderTilemap.GetCellCenterWorld(tilePosition);

        GameObject neuerTower = Instantiate(
            ausgewaehltesPrefab,
            weltMitte,
            Quaternion.identity
        );

        ResourceManager.Instance.SpendGold(ausgewaehlterTower.goldKosten);
        platzierteTower.Add(tilePosition, neuerTower);

        Debug.Log($"[TowerPlacer] Tower gebaut bei {tilePosition}. " +
                  $"Gold ausgegeben: {ausgewaehlterTower.goldKosten}");
    }

    private void WaehlePlatzierterTower(TowerBase tower)
    {
        ausgewaehlterPlatzierterTower = tower;
        AuswahlAufheben(); // Deselektiere den Tower, damit kein neuer gebaut wird
        if (upgradeUI != null)
            upgradeUI.ZeigeUpgrade(tower);

        Debug.Log($"[TowerPlacer] Platzierter Tower ausgewählt: {tower.Data.charakterName}");


    }   
    

    #endregion
}