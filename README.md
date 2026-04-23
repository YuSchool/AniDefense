# AniDefense
> Ein 2D Top-Down Tower Defense Spiel mit Anime-Thematik — Abschlussarbeit 2026

**Entwickler:** Yusuf Şükün  
**Kurs:** GME-24.01 | SRH Fachschulen GmbH  
**Repository:** https://github.com/YuSchool/AniDefense  
**Engine:** Unity 6 (6000.3.10f1) | URP 2D | C#  
**Stand:** 23.04.2026

---

## Beschreibung

AniDefense verbindet klassische Tower-Defense-Mechaniken mit einer Anime-Thematik. Statt generischer Türme platziert der Spieler Anime-Charaktere als Defender, die gegnerische Antagonisten auf vordefinierten Routen aufhalten müssen.

**Design-Philosophie:** Entspanntes Solo-Erlebnis ohne Werbung, ohne Zeitdruck, ohne Multiplayer. Der Spieler soll Strategien ausprobieren und Charaktere upgraden — in seinem eigenen Tempo.

---

## Voraussetzungen

- Unity 6 (6000.3.10f1) oder höher
- Universal Render Pipeline (URP) Package installiert
- TextMeshPro Package installiert

---

## Projekt öffnen & starten

1. Repository klonen:
   ```
   git clone https://github.com/YuSchool/AniDefense.git
   ```
2. Projekt in Unity Hub öffnen
3. Startszene öffnen: `Assets/Scenes/00_TitleScreen`
4. **Diese Szene muss als erste Szene in den Build Settings eingetragen sein**
5. Play drücken oder Build starten

**Build Settings Reihenfolge:**
```
0 — 00_TitleScreen
1 — Level_00
2 — Level_01
3 — Level_02
```

---

## Spielprinzip

- Wähle im **Dungeon-Screen** ein Level und eine Schwierigkeit aus
- Platziere **Türme (Anime-Charaktere)** auf markierten Baufeldern
- Starte die Wave und verhindere, dass Gegner die Basis erreichen
- Überlebe alle **5 Waves** um das Level zu gewinnen
- Sammle **Gold**, **Seelen** und **Aura** durch besiegte Gegner
- Upgrade deine Türme zwischen den Waves

---

## Schwierigkeitssystem

| Schwierigkeit | HP | Speed | SpawnAbstand | Belohnung |
|---|---|---|---|---|
| Leicht | ×1.0 | ×1.0 | ×1.0 | ×1.0 |
| Normal | ×1.5 | ×1.2 | ×0.8 | ×1.3 |
| Schwer | ×2.0 | ×1.5 | ×0.6 | ×1.6 |

Schwierigkeiten müssen der Reihe nach freigeschaltet werden.

---

## Freischaltlogik

```
Level_00 Leicht → schaltet frei: Level_00 Normal + Level_01 Leicht
Level_00 Normal → schaltet frei: Level_00 Schwer
Level_00 Schwer → schaltet frei: Level_02 Leicht
Level_01 Leicht → schaltet frei: Level_01 Normal
Level_01 Normal → schaltet frei: Level_01 Schwer
Level_02 Leicht → schaltet frei: Level_02 Normal
Level_02 Normal → schaltet frei: Level_02 Schwer
```

Fortschritt wird dauerhaft als JSON gespeichert unter:  
`Application.persistentDataPath/spielstand.json`

---

## Vorgaben & Umsetzung

| Vorgabe | Umsetzung | Status |
|---|---|---|
| Unity 6 (6000.0.68f1 / 6000.3.10f1) | Unity 6 (6000.3.10f1) mit URP 2D | ✅ |
| GitHub Repository | https://github.com/YuSchool/AniDefense | ✅ |
| README.md | Dieses Dokument | ✅ |
| Medienkatalog | Medienkatalog_AniDefense.odt im Repository | ✅ |
| Maximal USK 12 | Stilisierte Anime-Kämpfe, keine Gewalt | ✅ |
| Mindestens 3 Spielkarten | Level_00, Level_01, Level_02 (je 3 Schwierigkeiten = 9 Varianten) | ✅ |
| Mindestens 3 Türme | SonGoku, Zorro, Saitama, BasicTower (je 4 Upgrade-Stufen) | ✅ |
| Mindestens 3 Gegnertypen | 5 Typen: Standard, Tank, Speed, Armored, Magic | ✅ |
| Tower-Platzierung per Ressource | Gold-System auf Tilemap-Baufelder | ✅ |
| Skalierbare Implementierung | ScriptableObjects, Prefabs, Polymorphie | ✅ |
| HUD (Wellen, Leben, Geld) | Gold, Seelen, Aura, Wave X/5, 3 Herzen | ✅ |
| Bedienungsanleitung | Tutorial-Reiter ingame im Options-Screen | ✅ |

---

## Projektstruktur

```
Assets/
├── Assets/Extern/     — Externe Assets (Fonts, Icons, Portraits, Sounds, UI)
├── Data/              — ScriptableObjects (EnemyData, TowerData, WaveData)
├── Prefabs/           — Fertige Prefabs (Enemies, Towers, UI)
├── Scenes/            — 00_TitleScreen, Level_00, Level_01, Level_02
└── Scripts/
    ├── Core/          — GameManager, ResourceManager, GameTimeManager, AudioManager
    ├── Data/          — EnemyData, TowerData, WaveData, WaveEntry
    ├── Enemies/       — EnemyBase, PathFollower, DropHandler + 5 Typen
    ├── LevelSystem/   — LevelDifficultyManager
    ├── Placement/     — TowerPlacer
    ├── SaveSystem/    — SaveManager
    ├── Towers/        — TowerBase, TowerShooter, TowerUpgrader + Charaktere
    ├── UI/            — HUDController, UIManager, TowerSelectionUI, UpgradeUI
    │                    DungeonScreenManager, LevelKarteUI, TitleScreenManager
    │                    OptionScreenManager, AudioTrigger
    ├── Waves/         — WaveManager, SpawnPoint
    └── Enums.cs
```

---

## Verwendete Third-Party Assets

### Fonts
| Asset | Quelle | Lizenz |
|---|---|---|
| Manga Font | https://www.fontget.com/font/manga/ | Free |
| Anime Inept Font | https://www.fontget.com/font/anime-inept/ | Free CC |

### Portraits / Charaktere
| Asset | Quelle | Lizenz |
|---|---|---|
| Free Anime Character Art Pack 6 | https://assetstore.unity.com/packages/2d/characters/free-anime-character-art-pack-6-303256 | Unity Asset Store Free |

### Icons
| Asset | Quelle | Lizenz |
|---|---|---|
| Skymon Icon Pack Free | https://assetstore.unity.com/packages/2d/gui/icons/skymon-icon-pack-free-282424 | Unity Asset Store Free |

### UI Assets
| Asset | Quelle | Lizenz |
|---|---|---|
| Tiny Dungeon (Kenney) | https://kenney.nl/assets/tiny-dungeon | CC0 |
| Tiny Town (Kenney) | https://kenney.nl/assets/tiny-town | CC0 |
| Minimal Fantasy GUI | https://etahoshi.itch.io/minimal-fantasy-gui-by-eta | Free |
| Kit Nesia 2 | https://wenrexa.itch.io/kit-nesia2 | Free |
| SteampunkUI | https://assetstore.unity.com/packages/2d/gui/icons/steampunkui-238976 | Unity Asset Store Free |

### Sounds
| Asset | Quelle | Lizenz |
|---|---|---|
| Winning Sound Effect | https://pixabay.com/de/sound-effects/film-spezialeffekte-winning-218995/ | Pixabay Free |
| Pop Bubble | https://pixabay.com/de/sound-effects/technologie-pop-bubble-508712/ | Pixabay Free |
| Music Sting Night | https://pixabay.com/de/sound-effects/musical-mischief-at-night-music-sting-443258/ | Pixabay Free |
| Drop The Bass | https://pixabay.com/de/sound-effects/drop-the-bass-463664/ | Pixabay Free |
| Kids Funk Intro | https://pixabay.com/de/sound-effects/musical-kids-funk-intro-music-499479/ | Pixabay Free |
| Button Sound | https://pixabay.com/de/sound-effects/film-spezialeffekte-button-394464/ | Pixabay Free |
| Happy Music Loop | https://pixabay.com/de/sound-effects/musical-happy-music-loop-500008/ | Pixabay Free |
| Victory Fanfare | https://pixabay.com/de/sound-effects/musical-phatphrogstudiocom-victory-fanfare-2-474663/ | Pixabay Free |
| Drop Coin | https://pixabay.com/de/sound-effects/film-spezialeffekte-drop-coin-384921/ | Pixabay Free |
| Beep | https://pixabay.com/de/sound-effects/film-spezialeffekte-beep-313342/ | Pixabay Free |
| Whistle Slide Down | https://pixabay.com/de/sound-effects/film-spezialeffekte-whistle-slide-down-03-352451/ | Pixabay Free |
| You Lose Voice | https://pixabay.com/de/sound-effects/film-spezialeffekte-you-loseheavy-echoed-voice-230555/ | Pixabay Free |
| Ghostly Howl | https://pixabay.com/de/sound-effects/grusel-ghostly-howl-laa-99322/ | Pixabay Free |
| Made in Abyss Music | https://pixabay.com/de/sound-effects/musical-madeinabyss-194408/ | Pixabay Free |
| Closed Door | https://pixabay.com/de/sound-effects/haushalt-closed-door-411628/ | Pixabay Free |
| Game Start | https://pixabay.com/de/sound-effects/film-spezialeffekte-game-start-317318/ | Pixabay Free |
| Level Up | https://pixabay.com/de/sound-effects/film-spezialeffekte-level-up-289723/ | Pixabay Free |
| Shoot Sound | https://pixabay.com/de/sound-effects/film-spezialeffekte-shoot-5-102360/ | Pixabay Free |

---

## KI-Unterstützung

Grundgerüste wurden mit KI-Tools generieren lassen. Alle Inhalte wurden vom Entwickler geprüft und angepasst.

| Tool | Einsatz |
|---|---|
| Claude (Anthropic) — https://claude.ai | Grundgerüste für Scripts und Dokumentation |
| ChatGPT (OpenAI) — https://chatgpt.com | Grundgerüste für Scripts und Erklärungen |

---

## Bekannte Bugs & Offene Punkte

- Aura-Variable (float → int Anpassung ausstehend)
- Upgrade-Button zeigt keine Sperrung bei Endstufe
- Victory → "Weiter" Button lädt falsche Szene
- Auflösungsreiter und Steuerungsreiter: optisch implementiert, Funktionen ausstehend
- Panel_User, Panel_Items, Panel_Summon in Planung
- Map-Designs für Level_01 und Level_02 in Planung
- Charakter-Sprites und individuelle Fähigkeiten in Planung

---

## Lizenz

Dieses Projekt ist eine Abschlussarbeit und nicht für kommerzielle Nutzung bestimmt.  
Alle verwendeten Third-Party Assets unterliegen ihren jeweiligen Lizenzen (siehe Medienkatalog).

---

*AniDefense — Abschlussarbeit 2026 — Yusuf Şükün — GME-24.01 — SRH Fachschulen GmbH*
