// TowerBase.cs
// Zweck: Basisklasse für alle Türme im Spiel, die grundlegende Funktionen wie Zielerfassung, Schusslogik und Angriffsverhalten bereitstellt. 
// Sinn: Ermöglicht es, verschiedene Turmtypen zu erstellen, die von dieser Basisklasse erben und spezifische Angriffsverhalten implementieren können.
// Hinweis: Diese Klasse ist abstrakt, was bedeutet, dass sie nicht direkt instanziiert werden kann.

using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    #region Daten

    [Header("Daten")]
    [SerializeField] protected TowerData data;

    public TowerData Data => data;

    public void SetzeData(TowerData neueData)
    {
        data = neueData;
        Debug.Log($"[TowerBase] Data getauscht → {data.charakterName} Stufe {data.stufe}");
    }

    #endregion

    #region Zustand

    private float zeitSeitLetztemSchuss = 0f;
    protected EnemyBase aktuellesZiel = null;

    #endregion

    #region Komponenten

    protected TowerShooter shooter;

    #endregion

    #region Unity Lifecycle

    protected virtual void Start()
    {
        shooter = GetComponent<TowerShooter>();
    }

    protected virtual void Update()
    {
        zeitSeitLetztemSchuss += Time.deltaTime;

        SucheZiel();

        if (aktuellesZiel != null && ZeitFuerSchuss())
        {
            Schiesse();
            zeitSeitLetztemSchuss = 0f;
        }
    }

    #endregion

    #region Ziel-Logik

    private void SucheZiel()
    {
        EnemyBase[] alleGegner = FindObjectsOfType<EnemyBase>();
        EnemyBase besterGegner = null;
        float besteWertung = float.MaxValue;

        foreach (EnemyBase gegner in alleGegner)
        {
            float distanz = Vector3.Distance(transform.position, gegner.transform.position);

            if (distanz > data.reichweite) continue;

            float wertung = BerechneWertung(gegner, distanz);

            if (wertung < besteWertung)
            {
                besteWertung = wertung;
                besterGegner = gegner;
            }
        }

        aktuellesZiel = besterGegner;
    }

    private float BerechneWertung(EnemyBase gegner, float distanz)
    {
        switch (data.zielPrioritaet)
        {
            case TargetPriority.First:
                return -gegner.FortschrittAufPfad;
            case TargetPriority.Last:
                return gegner.FortschrittAufPfad;
            case TargetPriority.Strongest:
                return -gegner.AktuelleHP;
            case TargetPriority.Weakest:
                return gegner.AktuelleHP;
            default:
                return distanz;
        }
    }

    #endregion

    #region Schuss-Logik

    private bool ZeitFuerSchuss()
    {
        return zeitSeitLetztemSchuss >= 1f / data.angriffsgeschwindigkeit;
    }

    private void Schiesse()
    {
        if (shooter != null)
            shooter.Schiesse(aktuellesZiel, data.schaden);
    }

    #endregion

    #region Abstrakte Methoden

    public abstract void SpezialAngriff();

    #endregion
}