//  PathFollower.cs
//  Zweck: Steuert die Bewegung eines Gegners entlang eines vorgegebenen Pfades. Verwendet Waypoints, um die Route zu definieren
//  Sinn: Ermöglicht es Gegnern, sich dynamisch entlang eines Pfades zu bewegen, was für die meisten Tower-Defense-Spiele typisch ist.



using UnityEngine;

public class PathFollower : MonoBehaviour
{
    #region Zustand

    private Transform[] wegpunkte;
    private int aktuellerIndex = 0;
    private float geschwindigkeit;
    private bool istAktiv = false;

    #endregion

    #region INITIALISIERUNG

    public void Initialisiere(Transform[] pfad, float speed)
    {
        wegpunkte = pfad;
        geschwindigkeit = speed;
        aktuellerIndex = 0;
        istAktiv = true;
    }

    #endregion

    #region Unity Lifecycle

    private void Update()
    {
        if (!istAktiv || wegpunkte == null || wegpunkte.Length == 0) return;

        BewegZuNaechstemPunkt();
    }

    #endregion

    #region BEWEGUNGSLOGIK

    private void BewegZuNaechstemPunkt()
    {
        Transform ziel = wegpunkte[aktuellerIndex];
        float schritt = geschwindigkeit * Time.deltaTime;

        transform.position = Vector3.MoveTowards(
            transform.position,
            ziel.position,
            schritt
        );

        if (Vector3.Distance(transform.position, ziel.position) < 0.05f)
        {
            PunktErreicht();
        }
    }

    private void PunktErreicht()
    {
        aktuellerIndex++;

        if (aktuellerIndex >= wegpunkte.Length)
        {
            BaseErreicht();
        }
    }

    private void BaseErreicht()
    {
        istAktiv = false;
        Debug.Log("[PathFollower] Base erreicht.");
        ResourceManager.Instance.LoseLeben();
        Destroy(gameObject);
    }

    #endregion
}
