<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# Attribution des apparitions en salle

Le contexte transmis par l'événement `HorayModAPI.OnAssignRoomSpawnPhases`. S'intercale au moment où le générateur d'étage attribue les phases de monstres à une salle.

Généré le 2026-10-26 pour la version **1.1.0** du jeu. [Index de la référence](README.md)

| Type | Description |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Contexte invoqué lorsque FloorGenerator attribue des ennemis de façon aléatoire. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Type de salle en cours d'attribution |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Contexte invoqué lorsque FloorGenerator attribue des ennemis de façon aléatoire. Levé uniquement sur le serveur (hôte).

Règles
- `Phases` est une copie profonde de la table de l'étape. Vous pouvez la modifier ou la remplacer entièrement. La laisser à null rétablit la copie d'origine.  
- `Sources` est la ressource d'origine de l'étape. Lecture seule (la modifier altère la ressource de l'éditeur).  
Lors de l'attribution d'une salle, `Phases` (la copie) a un containedFactions vide (GeneratePhaseData ne le copie pas). Lisez les factions depuis Sources.
- Pour l'aléatoire, créez `new System.Random(ctx.Seed)` à partir de `Seed`. Consommer UnityEngine.Random ou le RNG de l'étage fait différer l'étage lors d'une régénération ou d'une reconnexion.  
- Les entités ajoutées par un mod dans spawnData doivent être enregistrées dans UnitDatabase pour que la restauration de sauvegarde et l'apparition côté client fonctionnent.

### Propriétés

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

L'étage qui effectue cette attribution


#### Stage

```csharp
public StageEntity Stage { get; }
```

L'étape de l'étage. Null si introuvable.


#### Floor

```csharp
public FloorData Floor { get; }
```

Données de l'étage (graine, difficulté, nom de l'étape). Null s'il n'y a pas de données serveur.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Difficulté finale utilisée pour choisir le pool (inclut le nombre de joueurs et DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Type de salle en cours d'attribution


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Index de cette attribution sur l'étage (à partir de 0). Repart de 0 pour chaque instance de générateur.


#### Seed

```csharp
public int Seed { get; }
```

Hash(graine de l'étage, index d'attribution). Graine pour le RNG du mod.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Ressources d'origine de l'étape (lecture seule). Une pour une salle, phaseCount pour une zone de combat.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Premier original. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Copie à attribuer. Remplaçable.

### Constructeurs

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Pas encore de description.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Type de salle en cours d'attribution

| Valeur | Description |
|---|---|
| `Normal` | Salle de combat normale (embuscade) |
| `Weak` | Salle de combat faible (pool de difficulté ≤ 1, sans embuscade) |
| `BattleZone` | Zone de combat (fusionne plusieurs phases, puis exécute le hook une seule fois) |
