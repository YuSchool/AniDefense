using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [Header("Gegner")]
    [SerializeField] private GameObject gegnerPrefab;
    [SerializeField] private Transform[] route;

    [Header("Tower")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform towerPosition;

    private void Start()
    {
        // Tower spawnen
        Instantiate(towerPrefab, towerPosition.position, Quaternion.identity);
        // Nächste Wave starten
        GameManager.Instance.StarteNaechsteWave();
    }
}