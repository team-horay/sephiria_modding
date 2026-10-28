<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# Asignación de apariciones en salas

El contexto que pasa el evento `HorayModAPI.OnAssignRoomSpawnPhases`. Se engancha en el momento en que el generador de plantas asigna fases de monstruos a una sala.

Generado el 2026-10-26 para la versión del juego **1.1.0**. [Índice de la referencia](README.md)

| Tipo | Descripción |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Contexto que se invoca cuando FloorGenerator asigna enemigos de forma aleatoria. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Tipo de sala que se está asignando |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Contexto que se invoca cuando FloorGenerator asigna enemigos de forma aleatoria. Solo se lanza en el servidor (anfitrión).

Reglas
- `Phases` es una copia profunda de la tabla de la etapa. Puedes modificarla o sustituirla por completo. Si la dejas en null, se vuelve a la copia original.  
- `Sources` es el asset original de la etapa. Solo lectura (modificarlo ensucia el asset del editor).  
En la asignación de salas, `Phases` (la copia) tiene un containedFactions vacío (GeneratePhaseData no lo copia). Lee las facciones desde Sources.
- Para la aleatoriedad, crea `new System.Random(ctx.Seed)` a partir de `Seed`. Consumir UnityEngine.Random o el RNG de la planta hace que la planta sea distinta al regenerarse o al volver a unirse.  
- Las entidades que un mod añade a spawnData deben estar registradas en UnitDatabase para que funcionen la restauración de guardado y la aparición en el lado del cliente.

### Propiedades

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

La planta que realiza esta asignación


#### Stage

```csharp
public StageEntity Stage { get; }
```

La etapa de la planta. Null si no se encuentra.


#### Floor

```csharp
public FloorData Floor { get; }
```

Datos de la planta (semilla, dificultad, nombre de la etapa). Null si no hay datos del servidor.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Dificultad final usada para elegir el grupo (incluye el número de jugadores y DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Tipo de sala que se está asignando


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Índice de esta asignación en la planta (empieza en 0). Empieza en 0 para cada instancia del generador.


#### Seed

```csharp
public int Seed { get; }
```

Hash(semilla de la planta, índice de asignación). Semilla para el RNG del mod.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Assets originales de la etapa (solo lectura). Uno para una sala, phaseCount para una zona de batalla.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Primer original. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Copia que se va a asignar. Sustituible.

### Constructores

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Sin descripción todavía.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Tipo de sala que se está asignando

| Valor | Descripción |
|---|---|
| `Normal` | Sala de combate normal (emboscada) |
| `Weak` | Sala de combate débil (grupo de dificultad ≤ 1, sin emboscada) |
| `BattleZone` | Zona de batalla (fusiona varias fases y luego ejecuta el hook una vez) |
