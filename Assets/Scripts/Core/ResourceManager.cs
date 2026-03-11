//  ResourceManager.cs
//  Zweck: Verwaltung von Ressourcen wie Gold, Seelen, Aura und Leben. Bietet Methoden zum Hinzufügen, Ausgeben und Verlieren von Ressourcen sowie Events für HUD-Updates.
//  Sinn: Zentralisiert die Ressourcenverwaltung, ermöglicht einfache Interaktion mit anderen Systemen (z.B. HUD, GameManager) und sorgt für eine klare Trennung von Daten und Logik.
//  Verantwortlichkeiten: 

using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; } // Singleton-Instanz für globalen Zugriff

    #region RESSOURCEN UND STARTWERTE
    [Header("Startwerte")] // Startwerte für Ressourcen, können im Inspector angepasst werden
    [SerializeField] private int startGold = 150;
    [SerializeField] private int startSeelen = 0;
    [SerializeField] private float startAura = 0f;
    [SerializeField] private int startLeben = 3;
    
    // Aktuelle Werte der Ressourcen, private Setter erzwingen die Verwendung von Methoden, die Events feuern
    public int Gold { get; private set; }
    public int Seelen { get; private set; }
    public float Aura { get; private set; }
    public int Leben { get; private set; }

    [Header("Aura-Maximum")] // Maximalwert für Aura, kann im Inspector angepasst werden
    [SerializeField] private float maxAura = 100f;
    public float MaxAura => maxAura; // Öffentlicher Getter für maxAura, damit andere Klassen den Wert kennen, aber nicht ändern können
    #endregion
    #region EVENTS
    public static event System.Action<int> OnGoldGeaendert;
    public static event System.Action<int> OnSeelenGeaendert;
    public static event System.Action<float> OnAuraGeaendert;
    public static event System.Action<int> OnLebenGeaendert;
    #endregion

    //  Unity erfordert, dass die Singleton-Instanz in Awake gesetzt wird, damit sie vor anderen Start-Methoden verfügbar ist
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Startwerte setzen und HUD sofort informieren
        SetzeGold(startGold);
        SetzeSeelen(startSeelen);
        SetzeAura(startAura);
        SetzeLeben(startLeben);
    }

    #region GOLD
    public void AddGold(int menge)
    {
        SetzeGold(Gold + menge);
        Debug.Log($"[ResourceManager] +{menge} Gold. Gesamt: {Gold}");
    }

    public bool SpendGold(int menge)
    {
        if (Gold < menge)
        {
            Debug.Log($"[ResourceManager] Nicht genug Gold. Habe: {Gold}, Brauche: {menge}");
            return false;
        }
        SetzeGold(Gold - menge);
        return true;
    }
    #endregion

    #region SEELEN
    public void AddSeelen(int menge)
    {
        SetzeSeelen(Seelen + menge);
        Debug.Log($"[ResourceManager] +{menge} Seelen. Gesamt: {Seelen}");
    }

    public bool SpendSeelen(int menge)
    {
        if (Seelen < menge)
        {
            Debug.Log($"[ResourceManager] Nicht genug Seelen. Habe: {Seelen}, Brauche: {menge}");
            return false;
        }
        SetzeSeelen(Seelen - menge);
        return true;
    }
    #endregion

    #region AURA
    public void AddAura(float menge)
    {
        SetzeAura(Mathf.Min(Aura + menge, maxAura));
    }

    public bool SpendAura(float menge)
    {
        if (Aura < menge) return false;
        SetzeAura(Aura - menge);
        return true;
    }
    #endregion

    #region LEBEN
    public void LoseLeben()
    {
        SetzeLeben(Leben - 1);
        Debug.Log($"[ResourceManager] Leben verloren. Verbleibend: {Leben}");

        if (Leben <= 0)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
    #endregion

    #region PRIVATE SETTER — feuern immer das Event

    private void SetzeGold(int wert)
    {
        Gold = Mathf.Max(0, wert);
        OnGoldGeaendert?.Invoke(Gold);
    }

    private void SetzeSeelen(int wert)
    {
        Seelen = Mathf.Max(0, wert);
        OnSeelenGeaendert?.Invoke(Seelen);
    }

    private void SetzeAura(float wert)
    {
        Aura = Mathf.Clamp(wert, 0f, maxAura);
        OnAuraGeaendert?.Invoke(Aura);
    }

    private void SetzeLeben(int wert)
    {
        Leben = Mathf.Max(0, wert);
        OnLebenGeaendert?.Invoke(Leben);
    }
    #endregion
}