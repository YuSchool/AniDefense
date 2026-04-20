// LevelDifficultyManager.cs
// Zweck: Liest beim Start die gewählte Schwierigkeit aus dem SaveManager und
//        stellt Multiplikatoren bereit die vom WaveManager beim Spawnen angewendet werden.
// Sinn: Eine einzige Stelle für alle Schwierigkeits-Anpassungen – erweiterbar
//       ohne andere Systeme anzufassen.
// Wird verwendet von: WaveManager (beim Spawnen jedes Gegners)

using UnityEngine;

public class LevelDifficultyManager : MonoBehaviour
{
    #region Singleton

    public static LevelDifficultyManager Instance { get; private set; }

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

    #region Multiplikatoren

    public float HP_Multiplikator { get; private set; } = 1f;
    public float Geschwindigkeit_Multiplikator { get; private set; } = 1f;
    public float SpawnAbstand_Multiplikator { get; private set; } = 1f;
    public float Belohnung_Multiplikator { get; private set; } = 1f;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        WendeSchwierigkeitAn(SaveManager.AktuelleSchwierigkeit);
    }

    #endregion

    #region Schwierigkeit

    private void WendeSchwierigkeitAn(Schwierigkeit schwierigkeit)
    {
        switch (schwierigkeit)
        {
            case Schwierigkeit.Leicht:
                HP_Multiplikator = 1f;
                Geschwindigkeit_Multiplikator = 1f;
                SpawnAbstand_Multiplikator = 1f;
                Belohnung_Multiplikator = 1f;
                break;

            case Schwierigkeit.Normal:
                HP_Multiplikator = 1.5f;
                Geschwindigkeit_Multiplikator = 1.2f;
                SpawnAbstand_Multiplikator = 0.8f; // Kürzere Pausen zwischen Spawns
                Belohnung_Multiplikator = 1.3f;
                break;

            case Schwierigkeit.Schwer:
                HP_Multiplikator = 2f;
                Geschwindigkeit_Multiplikator = 1.5f;
                SpawnAbstand_Multiplikator = 0.6f;
                Belohnung_Multiplikator = 1.6f;
                break;
        }

        Debug.Log($"[LevelDifficultyManager] Schwierigkeit: {schwierigkeit} " +
                  $"| HP x{HP_Multiplikator} " +
                  $"| Speed x{Geschwindigkeit_Multiplikator} " +
                  $"| SpawnAbstand x{SpawnAbstand_Multiplikator}");
    }

    #endregion
}