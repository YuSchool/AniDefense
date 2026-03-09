using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // 
    //  SINGLETON
    // 
    public static ResourceManager Instance { get; private set; }

    // 
    //  RESSOURCEN
    // 
    [Header("Startwerte")]
    [SerializeField] private int startGold = 150;
    [SerializeField] private int startSeelen = 0;
    [SerializeField] private float startAura = 0f;
    [SerializeField] private int startLeben = 3;

    public int Gold { get; private set; }
    public int Seelen { get; private set; }
    public float Aura { get; private set; }
    public int Leben { get; private set; }

    [Header("Aura-Maximum")]
    [SerializeField] private float maxAura = 100f;
    public float MaxAura => maxAura;

    // 
    //  EVENTS
    // 
    public static event System.Action<int> OnGoldGeaendert;
    public static event System.Action<int> OnSeelenGeaendert;
    public static event System.Action<float> OnAuraGeaendert;
    public static event System.Action<int> OnLebenGeaendert;

    //
    //  UNITY LIFECYCLE
    // 
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

    // 
    //  GOLD
    // 
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

    // 
    //  SEELEN
    // 
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

    // 
    //  AURA
    // 
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

    // 
    //  LEBEN
    // 
    public void LoseLeben()
    {
        SetzeLeben(Leben - 1);
        Debug.Log($"[ResourceManager] Leben verloren. Verbleibend: {Leben}");

        if (Leben <= 0)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    // 
    //  PRIVATE SETTER — feuern immer das Event
    // 
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
}