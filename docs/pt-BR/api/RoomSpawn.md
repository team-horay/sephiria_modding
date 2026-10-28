<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# Atribuição de surgimento por sala

O contexto passado pelo evento `HorayModAPI.OnAssignRoomSpawnPhases`. Intercepta o momento em que o gerador de andares atribui fases de monstros a uma sala.

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. [Índice da referência](README.md)

| Tipo | Descrição |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Contexto invocado quando o FloorGenerator atribui inimigos de forma aleatória. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Tipo de sala que está sendo atribuída |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Contexto invocado quando o FloorGenerator atribui inimigos de forma aleatória. Disparado apenas no servidor (anfitrião).

Regras
- `Phases` é uma cópia profunda da tabela do estágio. Você pode modificá-la ou substituí-la por completo. Deixá-la null reverte para a cópia original.  
- `Sources` é o asset original do estágio. Somente leitura (modificá-lo suja o asset do editor).  
Na atribuição de sala, `Phases` (a cópia) tem um containedFactions vazio (GeneratePhaseData não o copia). Leia as facções a partir de Sources.
- Para aleatoriedade, crie `new System.Random(ctx.Seed)` a partir de `Seed`. Consumir UnityEngine.Random ou o RNG do andar faz com que o andar fique diferente ao ser gerado novamente ou ao reentrar.  
- Entidades em spawnData adicionadas por um mod precisam estar registradas no UnitDatabase para que a restauração de saves e o surgimento do lado do cliente funcionem.

### Propriedades

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

O andar que está realizando esta atribuição


#### Stage

```csharp
public StageEntity Stage { get; }
```

O estágio do andar. Null se não for encontrado.


#### Floor

```csharp
public FloorData Floor { get; }
```

Dados do andar (seed, dificuldade, nome do estágio). Null se não houver dados do servidor.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Dificuldade final usada para escolher o pool (inclui o número de jogadores e DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Tipo de sala que está sendo atribuída


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Índice desta atribuição no andar (começando em 0). Começa em 0 para cada instância de gerador.


#### Seed

```csharp
public int Seed { get; }
```

Hash(seed do andar, índice da atribuição). Seed para o RNG do mod.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Assets originais do estágio (somente leitura). Um para uma sala, phaseCount para uma zona de batalha.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Primeiro original. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Cópia a ser atribuída. Substituível.

### Construtores

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Ainda sem descrição.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Tipo de sala que está sendo atribuída

| Valor | Descrição |
|---|---|
| `Normal` | Sala de combate normal (emboscada) |
| `Weak` | Sala de combate fraca (pool de dificuldade ≤ 1, sem emboscada) |
| `BattleZone` | Zona de batalha (mescla várias fases e depois executa o hook uma vez) |
