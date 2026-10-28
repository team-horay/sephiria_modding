<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# Raum-Spawn-Zuweisung

Der Kontext, den das Event `HorayModAPI.OnAssignRoomSpawnPhases` übergibt. Klinkt sich in dem Moment ein, in dem der Etagengenerator einem Raum Monsterphasen zuweist.

Generiert am 2026-10-26 für Spielversion **1.1.0**. [Referenzindex](README.md)

| Typ | Beschreibung |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Kontext, der aufgerufen wird, wenn FloorGenerator zufallsbasierte Gegner zuweist. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Art des Raums, der zugewiesen wird |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Kontext, der aufgerufen wird, wenn FloorGenerator zufallsbasierte Gegner zuweist. Wird nur auf dem Server (Host) ausgelöst.

Regeln
- `Phases` ist eine tiefe Kopie der Stage-Tabelle. Du kannst sie verändern oder vollständig ersetzen. Bleibt sie null, wird auf die ursprüngliche Kopie zurückgegriffen.  
- `Sources` ist das originale Stage-Asset. Nur lesen (Änderungen machen das Editor-Asset „dirty“).  
Bei der Raumzuweisung hat `Phases` (die Kopie) ein leeres containedFactions (GeneratePhaseData kopiert es nicht). Lies Fraktionen aus Sources.
- Für Zufall erstelle aus `Seed` ein `new System.Random(ctx.Seed)`. Wer UnityEngine.Random oder den RNG der Etage verbraucht, sorgt dafür, dass die Etage bei Neugenerierung oder erneutem Beitritt abweicht.  
- Entitäten in spawnData, die eine Mod hinzufügt, müssen in UnitDatabase registriert sein, damit Speicherstand-Wiederherstellung und clientseitiges Spawnen funktionieren.

### Eigenschaften

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

Die Etage, die diese Zuweisung durchführt


#### Stage

```csharp
public StageEntity Stage { get; }
```

Die Stage der Etage. Null, wenn nicht gefunden.


#### Floor

```csharp
public FloorData Floor { get; }
```

Etagendaten (Seed, Schwierigkeit, Stage-Name). Null, wenn keine Serverdaten vorhanden sind.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Endgültige Schwierigkeit, mit der der Pool gewählt wird (einschließlich Spieleranzahl und DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Art des Raums, der zugewiesen wird


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Index dieser Zuweisung auf der Etage (beginnend bei 0). Startet für jede Generator-Instanz bei 0.


#### Seed

```csharp
public int Seed { get; }
```

Hash(Etagen-Seed, Zuweisungsindex). Seed für den RNG der Mod.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Originale Stage-Assets (nur lesen). Eines für einen Raum, phaseCount für eine Kampfzone.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Erstes Original. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Kopie, die zugewiesen wird. Ersetzbar.

### Konstruktoren

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Noch keine Beschreibung.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Art des Raums, der zugewiesen wird

| Wert | Beschreibung |
|---|---|
| `Normal` | Normaler Kampfraum (Hinterhalt) |
| `Weak` | Schwacher Kampfraum (Pool mit Schwierigkeit ≤ 1, kein Hinterhalt) |
| `BattleZone` | Kampfzone (führt mehrere Phasen zusammen und ruft den Hook dann einmal auf) |
