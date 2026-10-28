<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# Assegnazione degli spawn nelle stanze

Il contesto passato dall'evento `HorayModAPI.OnAssignRoomSpawnPhases`. Si aggancia al momento in cui il generatore di piani assegna le fasi dei mostri a una stanza.

Generato il 2026-10-26 per la versione del gioco **1.1.0**. [Indice del riferimento](README.md)

| Tipo | Descrizione |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Contesto invocato quando FloorGenerator assegna nemici in modo casuale. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Tipo di stanza in fase di assegnazione |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Contesto invocato quando FloorGenerator assegna nemici in modo casuale. Emesso solo sul server (host).

Regole
- `Phases` è una copia profonda della tabella dello stage. Puoi modificarla o sostituirla del tutto. Se la lasci null, si torna alla copia originale.  
- `Sources` è l'asset originale dello stage. Sola lettura (modificarlo sporca l'asset dell'editor).  
Nell'assegnazione delle stanze, `Phases` (la copia) ha un containedFactions vuoto (GeneratePhaseData non lo copia). Leggi le fazioni da Sources.
- Per la casualità, crea `new System.Random(ctx.Seed)` a partire da `Seed`. Consumare UnityEngine.Random o l'RNG del piano fa sì che il piano cambi alla rigenerazione o al rientro in partita.  
- Le entità aggiunte da una mod in spawnData devono essere registrate in UnitDatabase perché il ripristino dei salvataggi e lo spawn lato client funzionino.

### Proprietà

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

Il piano che esegue questa assegnazione


#### Stage

```csharp
public StageEntity Stage { get; }
```

Lo stage del piano. Null se non trovato.


#### Floor

```csharp
public FloorData Floor { get; }
```

Dati del piano (seed, difficoltà, nome dello stage). Null se non ci sono dati del server.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Difficoltà finale usata per scegliere il pool (include il numero di giocatori e DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Tipo di stanza in fase di assegnazione


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Indice di questa assegnazione nel piano (a partire da 0). Riparte da 0 per ogni istanza del generatore.


#### Seed

```csharp
public int Seed { get; }
```

Hash(seed del piano, indice di assegnazione). Seed per l'RNG della mod.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Asset originali dello stage (sola lettura). Uno per una stanza, phaseCount per una zona di battaglia.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Primo originale. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Copia da assegnare. Sostituibile.

### Costruttori

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Nessuna descrizione per ora.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Tipo di stanza in fase di assegnazione

| Valore | Descrizione |
|---|---|
| `Normal` | Stanza di combattimento normale (imboscata) |
| `Weak` | Stanza di combattimento debole (pool con difficoltà ≤ 1, senza imboscata) |
| `BattleZone` | Zona di battaglia (unisce più fasi, poi esegue l'hook una sola volta) |
