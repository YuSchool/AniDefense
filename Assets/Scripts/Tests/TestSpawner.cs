using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [Header("Test Einstellungen")]
    [SerializeField] private GameObject gegnerPrefab;
    [SerializeField] private Transform[] route;

    private void Start()
    {
        GameObject gegner = Instantiate(
            gegnerPrefab,
            route[0].position,
            Quaternion.identity
        );

        PathFollower pf = gegner.GetComponent<PathFollower>();
        pf.Initialisiere(route, 3f);
    }
}