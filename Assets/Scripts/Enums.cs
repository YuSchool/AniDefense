// Enums.cs
// Zweck: Enthält alle Enums, die im Projekt verwendet werden.
// Sinn: Enums dienen als Liste von benannten Konstanten, die den Code lesbarer und wartbarer machen.
// Kein Verhalten oder Logik, sondern nur die Definition von Enums.

public enum Rarity
{
    R,
    SR,
    UR,
    LR
}

public enum RarityClass
{
    Rare,
    SuperRare,
    UltraRare,
    Legendary
}

public enum TowerType
{
    Melee, // Nahkampf-Turm
    Ranged, // Fernkampf-Turm
    Magic, // Magie-Turm
    Support // Unterstützungs-Turm
}

public enum EnemyType
{
    Standard, // Standard-Feind
    Tank, // Tank-Feind mit hoher Gesundheit
    Speed, // Schneller Feind mit geringer Gesundheit
    Armored, // Gepanzerter Feind mit mittlerer Gesundheit und hoher Verteidigung
    Magic, // Magischer Feind mit speziellen Fähigkeiten und Flugfähigkeit
    Boss // Boss-Feind mit sehr hoher Gesundheit und speziellen Fähigkeiten
}

public enum TargetPriority
{
    First, // Greift den ersten Feind an, der in Reichweite kommt
    Last, // Greift den letzten Feind an, der in Reichweite kommt
    Strongest, // Greift den stärksten Feind an
    Weakest // Greift den schwächsten Feind an
}
public enum GameState
{
    Idle,        // Spiel wartet auf Start
    Running,     // Wave läuft
    Paused,      // Pausiert
    WaveOver,    // Wave beendet, kurze Pause bis nächste
    GameOver,    // Alle Leben verloren
    Victory      // Alle Waves geschafft
}

public enum Schwierigkeit // Schwierigkeitsgrad des Levels
{
    Leicht,
    Normal,
    Schwer
}

