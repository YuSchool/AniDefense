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
        // Gegner spawnen
        GameObject gegner = Instantiate(
            gegnerPrefab,
            route[0].position,
            Quaternion.identity
        );
        PathFollower pf = gegner.GetComponent<PathFollower>();
        pf.Initialisiere(route, 3f);

        // Tower spawnen
        Instantiate(towerPrefab, towerPosition.position, Quaternion.identity);
    }
}