using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    #region Singleton

    public static WaveManager Instance { get; private set; }

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

    [Header("Waves")]
    [SerializeField] private WaveData[] waves;

    [Header("Einstellungen")]
    [SerializeField] private GameObject[] gegnerPrefabs;
    [SerializeField] private float zeitZwischenWaves = 3f;

    #endregion

    #region Zustand

    private int aktuelleWaveIndex = 0;
    private int verbleibendeGegner = 0;
    private bool waveAktiv = false;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        EnemyBase.OnEnemyGestorben += OnGegnerGestorben;
        GameManager.OnWaveGestartet += OnWaveGestartet;
    }

    private void OnDisable()
    {
        EnemyBase.OnEnemyGestorben -= OnGegnerGestorben;
        GameManager.OnWaveGestartet -= OnWaveGestartet;
    }

    #endregion

    #region Wave-Logik

    private void OnWaveGestartet(int waveNummer)
    {
        aktuelleWaveIndex = waveNummer - 1;

        if (aktuelleWaveIndex >= waves.Length)
        {
            Debug.LogWarning("[WaveManager] Kein WaveData für diese Wave vorhanden.");
            return;
        }

        StartCoroutine(StarteWave(waves[aktuelleWaveIndex]));
    }

    private IEnumerator StarteWave(WaveData waveData)
    {
        waveAktiv = true;

        // Erst alle Gegner zählen
        verbleibendeGegner = 0;
        foreach (WaveEntry eintrag in waveData.waveEintraege)
        {
            verbleibendeGegner += eintrag.anzahl;
        }

        // Dann spawnen (so dass die Anzeige der verbleibenden Gegner von Anfang an korrekt ist)
        SpawnPoint[] alleSpawnPoints = FindObjectsOfType<SpawnPoint>();

        foreach (WaveEntry eintrag in waveData.waveEintraege)
        {
            for (int i = 0; i < eintrag.anzahl; i++)
            {
                SpawnPoint spawnPoint = alleSpawnPoints[Random.Range(0, alleSpawnPoints.Length)];
                SpawneGegner(eintrag, spawnPoint);

                yield return new WaitForSeconds(eintrag.spawnAbstand);
            }
        }
    }

    private void SpawneGegner(WaveEntry eintrag, SpawnPoint spawnPoint)
    {
        GameObject prefab = HolePrefabFuerTyp(eintrag.enemyData);

        if (prefab == null)
        {
            Debug.LogWarning($"[WaveManager] Kein Prefab für {eintrag.enemyData.enemyName} gefunden.");
            return;
        }

        GameObject gegnerObjekt = Instantiate(
            prefab,
            spawnPoint.Wegpunkte[0].position, // Gegner wird am ersten Wegpunkt des SpawnPoints gespawnt
            Quaternion.identity
        );

        PathFollower pf = gegnerObjekt.GetComponent<PathFollower>();
        pf.Initialisiere(spawnPoint.Wegpunkte, eintrag.enemyData.geschwindigkeit);
    }

    private GameObject HolePrefabFuerTyp(EnemyData data)
    {
        foreach (GameObject prefab in gegnerPrefabs)
        {
            EnemyBase eb = prefab.GetComponent<EnemyBase>();
            if (eb != null && eb.Data == data)
                return prefab;
        }
        return null;
    }

    private void OnGegnerGestorben(EnemyBase gegner)
    {
        verbleibendeGegner--;
        Debug.Log($"[WaveManager] Gegner gestorben. Verbleibend: {verbleibendeGegner}");

        if (verbleibendeGegner <= 0 && waveAktiv)
        {
            waveAktiv = false;
            StartCoroutine(WaveAbgeschlossenNachDelay());
        }
    }

    private IEnumerator WaveAbgeschlossenNachDelay()
    {
        yield return new WaitForSeconds(zeitZwischenWaves);
        GameManager.Instance.WaveAbgeschlossen();
    }

    #endregion
}