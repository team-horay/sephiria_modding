<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# Przydział spawnów w pokojach

Kontekst przekazywany przez zdarzenie `HorayModAPI.OnAssignRoomSpawnPhases`. Podpina się w momencie, gdy generator piętra przydziela pokojowi fazy potworów.

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. [Indeks dokumentacji](README.md)

| Typ | Opis |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Kontekst wywoływany, gdy FloorGenerator przydziela losowych wrogów. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Rodzaj przydzielanego pokoju |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Kontekst wywoływany, gdy FloorGenerator przydziela losowych wrogów. Wywoływany tylko na serwerze (u hosta).

Zasady
- `Phases` to głęboka kopia tabeli etapu. Możesz ją modyfikować albo całkowicie zastąpić. Pozostawienie wartości null przywraca oryginalną kopię.  
- `Sources` to oryginalny zasób etapu. Tylko do odczytu (modyfikacja zanieczyszcza zasób edytora).  
Przy przydziale pokoju `Phases` (kopia) ma puste containedFactions (GeneratePhaseData go nie kopiuje). Frakcje odczytuj z Sources.
- Do losowości utwórz `new System.Random(ctx.Seed)` z `Seed`. Pobieranie liczb z UnityEngine.Random lub RNG piętra sprawia, że piętro różni się przy ponownym generowaniu lub ponownym dołączeniu.  
- Encje dodane przez moda do spawnData muszą być zarejestrowane w UnitDatabase, aby działało przywracanie zapisu i spawnowanie po stronie klienta.

### Właściwości

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

Piętro wykonujące ten przydział


#### Stage

```csharp
public StageEntity Stage { get; }
```

Etap piętra. Null, jeśli nie znaleziono.


#### Floor

```csharp
public FloorData Floor { get; }
```

Dane piętra (seed, poziom trudności, nazwa etapu). Null, jeśli nie ma danych serwera.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Ostateczny poziom trudności użyty do wyboru puli (uwzględnia liczbę graczy i DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Rodzaj przydzielanego pokoju


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Indeks tego przydziału na piętrze (liczony od 0). Zaczyna się od 0 dla każdej instancji generatora.


#### Seed

```csharp
public int Seed { get; }
```

Hash(seed piętra, indeks przydziału). Seed dla RNG moda.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Oryginalne zasoby etapu (tylko do odczytu). Jeden dla pokoju, phaseCount dla strefy walki.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Pierwszy oryginał. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Kopia, która zostanie przydzielona. Można ją zastąpić.

### Konstruktory

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Brak opisu.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Rodzaj przydzielanego pokoju

| Wartość | Opis |
|---|---|
| `Normal` | Zwykły pokój walki (zasadzka) |
| `Weak` | Słaby pokój walki (pula o trudności ≤ 1, bez zasadzki) |
| `BattleZone` | Strefa walki (scala wiele faz, a następnie uruchamia hook raz) |
