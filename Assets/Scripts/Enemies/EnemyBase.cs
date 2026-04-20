//  EnemyBase.cs
//  Zweck: Basisklasse für alle Gegnertypen im Spiel. Enthält gemeinsame Eigenschaften und Methoden wie HP, Schaden nehmen, sterben und angreifen. 
//  Sinn: Erlaubt es, verschiedene Gegnertypen zu erstellen, die von dieser Basisklasse erben und spezifische Angriffsverhalten implementieren können, während sie gleichzeitig gemeinsame Funktionalitäten teilen.
//  Verantwortlichkeiten:
//  Wird verwendet von: Alle spezifischen Gegnertypen (z.B. Goblin, Ork, Boss) erben von EnemyBase und implementieren die Angreifen-Methode entsprechend ihrem Verhalten.

using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    #region DATEN
    [Header("Daten")]
    [SerializeField] private EnemyData data; // Referenz auf die ScriptableObject-Daten des Gegners

    public EnemyData Data => data; // Öffentlicher Zugriff auf die Daten

    #endregion

    #region ZUSTAND
    public float AktuelleHP { get; private set; } // Aktuelle HP des Gegners, private Setter erzwingen die Verwendung von Methoden, die Logik enthalten
    public bool IstTot { get; private set; } = false; // Grundeinstellung ist false, wird auf true gesetzt, wenn der Gegner stirbt, um weitere Aktionen zu verhindern.
    public float FortschrittAufPfad { get; private set; } = 0f; // Fortschritt des Gegners auf dem Pfad, kann von Bewegungsskripten aktualisiert werden, um die Position des Gegners zu verfolgen.
    #endregion

    #region EVENTS
    public static event System.Action<EnemyBase> OnEnemyGestorben; // Event, das ausgelöst wird, wenn ein Gegner stirbt, übergibt eine Referenz auf den gestorbenen Gegner
    #endregion

    #region Unity Lifecycle

    protected virtual void Start()
    {
        // Nur setzen wenn SetzeHP() noch nicht aufgerufen wurde
        if (AktuelleHP == 0f)
            AktuelleHP = data.leben;
    }

    #endregion

    #region ÖFFENTLICHE METHODEN
    public void TakeDamage(float schaden) // Methode, um Schaden zu nehmen, wird von Türmen oder anderen Schadensquellen aufgerufen
    {
        if (IstTot) return; // Wenn der Gegner bereits tot ist, wird kein weiterer Schaden mehr verarbeitet.

        AktuelleHP -= schaden; // Schaden von den aktuellen HP abziehen
        Debug.Log($"[{data.enemyName}] {schaden} Schaden erhalten. HP: {AktuelleHP}"); // Debug-Ausgabe

        if (AktuelleHP <= 0) // Wenn die HP auf 0 oder darunter fallen, stirbt der Gegner
        {
            Die();
        }
    }

    public void SetzeFortschritt(float wert)
    {
        FortschrittAufPfad = Mathf.Clamp01(wert);
    }

    public void ErreichteBase()
    {
        if (IstTot) return; // Wenn der Gegner bereits tot ist, wird keine weitere Aktion ausgeführt.
        IstTot = true; // Gegner als tot markieren, um weitere Aktionen zu verhindern.
        OnEnemyGestorben?.Invoke(this); // Event auslösen, um andere Systeme zu informieren, dass dieser Gegner gestorben ist
    }

    // Funktionen für LevelSystem
    public void SetzeHP(float multiplikator)
    {
        AktuelleHP = data.leben * multiplikator;
    }

    #endregion

    #region VIRTUELLE UND ABSTRAKTE METHODEN
    protected virtual void Die()
    {
        if (IstTot) return;
        IstTot = true;

        //Debug.Log($"[{data.enemyName}] gestorben.");
        OnEnemyGestorben?.Invoke(this); // Event auslösen, um andere Systeme zu informieren, dass dieser Gegner gestorben ist
        Destroy(gameObject);
    }

    public abstract void Angreifen(); // Abstrakte Methode, die von spezifischen Gegnertypen implementiert werden muss, um ihren Angriff zu definieren.

    #endregion
}