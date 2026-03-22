using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

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

    private TowerData ausgewaehlterTower = null;
    private GameObject ausgewaehltesPrefab = null;

    private Dictionary<Vector3Int, GameObject> platzierteTower
        = new Dictionary<Vector3Int, GameObject>();

    #endregion

    #region Unity Lifecycle

    private void Update()
    {
        if (ausgewaehlterTower == null) return;
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 weltPosition = hauptKamera.ScreenToWorldPoint(Input.mousePosition);
        weltPosition.z = 0f;

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

    #endregion
}