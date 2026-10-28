<!-- Auto-generated file. Tools/ModApiDocGen in the game repo builds it from /// comments in the source. Don't edit directly; fix the source comments and regenerate. -->
# Room Spawn Assignment

The context passed by the `HorayModAPI.OnAssignRoomSpawnPhases` event. Hooks in at the moment the floor generator assigns monster phases to a room.

Generated 2026-10-26 for game version **1.1.0**. [Reference index](README.md)

| Type | Description |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Context invoked when FloorGenerator assigns random-based enemies. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Kind of room being assigned |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Context invoked when FloorGenerator assigns random-based enemies. Raised only on the server (host).

Rules
- `Phases` is a deep copy of the stage table. You may modify it or replace it entirely. Leaving it null reverts to the original copy.  
- `Sources` is the original stage asset. Read only (modifying it dirties the editor asset).  
In room assignment, `Phases` (the copy) has an empty containedFactions (GeneratePhaseData does not copy it). Read factions from Sources.
- For randomness, create `new System.Random(ctx.Seed)` from `Seed`. Consuming UnityEngine.Random or the floor's RNG makes the floor differ on regeneration or rejoin.  
- Entities in spawnData added by a mod must be registered in UnitDatabase for save restore and client-side spawning to work.

### Properties

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

The floor performing this assignment


#### Stage

```csharp
public StageEntity Stage { get; }
```

The floor's stage. Null if not found.


#### Floor

```csharp
public FloorData Floor { get; }
```

Floor data (seed, difficulty, stage name). Null if there is no server data.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Final difficulty used to pick the pool (includes player count and DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Kind of room being assigned


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Index of this assignment on the floor (0-based). Starts at 0 for each generator instance.


#### Seed

```csharp
public int Seed { get; }
```

Hash(floor seed, assignment index). Seed for the mod's RNG.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Original stage assets (read only). One for a room, phaseCount for a battle zone.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

First original. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Copy to be assigned. Replaceable.

### Constructors

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*No description yet.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Kind of room being assigned

| Value | Description |
|---|---|
| `Normal` | Normal combat room (ambush) |
| `Weak` | Weak combat room (difficulty ≤ 1 pool, no ambush) |
| `BattleZone` | Battle zone (merges multiple phases, then runs the hook once) |
