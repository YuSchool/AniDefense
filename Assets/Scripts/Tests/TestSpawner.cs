using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [Header("Gegner")]
    [SerializeField] private GameObject gegnerPrefab;
    [SerializeField] private Transform[] route;

    [Header("Tower")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform towerPosition;

    [Header("Platzierung")]
    [SerializeField] private TowerData testTowerData;
    [SerializeField] private GameObject testTowerPrefab;

    private void Start()
    {
        // Nächste Wave starten
        GameManager.Instance.StarteNaechsteWave();

        // Tower für Platzierung vorauswählen
        TowerPlacer.Instance.WaehleTower(testTowerData, testTowerPrefab);
    }
}