<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# Tilldelning av rumsspawn

Kontexten som skickas av händelsen `HorayModAPI.OnAssignRoomSpawnPhases`. Kopplar in sig i det ögonblick våningsgeneratorn tilldelar monsterfaser till ett rum.

Genererad 2026-10-26 för spelversion **1.1.0**. [Referensindex](README.md)

| Typ | Beskrivning |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Kontext som anropas när FloorGenerator tilldelar slumpbaserade fiender. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Typ av rum som tilldelas |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Kontext som anropas när FloorGenerator tilldelar slumpbaserade fiender. Utlöses bara på servern (värden).

Regler
- `Phases` är en djup kopia av banans tabell. Du får ändra den eller ersätta den helt. Lämnar du den som null återgår den till originalkopian.  
- `Sources` är banans originalasset. Skrivskyddad (att ändra den smutsar ner editor-asseten).  
Vid rumstilldelning har `Phases` (kopian) ett tomt containedFactions (GeneratePhaseData kopierar det inte). Läs fraktioner från Sources.
- För slumpmässighet: skapa `new System.Random(ctx.Seed)` från `Seed`. Om du förbrukar UnityEngine.Random eller våningens slumpgenerator blir våningen annorlunda vid omgenerering eller återanslutning.  
- Entiteter i spawnData som en mod lägger till måste vara registrerade i UnitDatabase för att återställning från sparfil och spawn på klientsidan ska fungera.

### Egenskaper

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

Våningen som utför den här tilldelningen


#### Stage

```csharp
public StageEntity Stage { get; }
```

Våningens bana. Null om den inte hittas.


#### Floor

```csharp
public FloorData Floor { get; }
```

Våningsdata (seed, svårighetsgrad, bannamn). Null om det inte finns några serverdata.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Slutlig svårighetsgrad som används för att välja poolen (inkluderar antal spelare och DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Typ av rum som tilldelas


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Index för den här tilldelningen på våningen (0-baserat). Börjar på 0 för varje generatorinstans.


#### Seed

```csharp
public int Seed { get; }
```

Hash(våningens seed, tilldelningsindex). Seed för moddens slumpgenerator.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Banans originalassets (skrivskyddade). En för ett rum, phaseCount för en stridszon.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Första originalet. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Kopian som ska tilldelas. Kan ersättas.

### Konstruktorer

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Ingen beskrivning ännu.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Typ av rum som tilldelas

| Värde | Beskrivning |
|---|---|
| `Normal` | Normalt stridsrum (bakhåll) |
| `Weak` | Svagt stridsrum (pool för svårighetsgrad ≤ 1, inget bakhåll) |
| `BattleZone` | Stridszon (slår ihop flera faser och kör sedan hooken en gång) |
