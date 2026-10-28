<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# Назначение спавна в комнатах

Контекст, передаваемый событием `HorayModAPI.OnAssignRoomSpawnPhases`. Подключается в момент, когда генератор этажа назначает комнате фазы монстров.

Сгенерировано 2026-10-26 для версии игры **1.1.0**. [Оглавление справочника](README.md)

| Тип | Описание |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | Контекст, вызываемый, когда FloorGenerator назначает случайных врагов. |
| [`ERoomSpawnKind`](#eroomspawnkind) | Вид назначаемой комнаты |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

Контекст, вызываемый, когда FloorGenerator назначает случайных врагов. Вызывается только на сервере (хосте).

Правила
- `Phases` является глубокой копией таблицы этапа. Её можно изменять или заменять целиком. Если оставить null, используется исходная копия.  
- `Sources` является исходным ассетом этапа. Только для чтения (его изменение портит ассет редактора).  
При назначении комнаты у `Phases` (копии) пустой containedFactions (GeneratePhaseData его не копирует). Читайте фракции из Sources.
- Для случайности создавайте `new System.Random(ctx.Seed)` из `Seed`. Использование UnityEngine.Random или ГСЧ этажа приводит к тому, что этаж отличается при повторной генерации или переподключении.  
- Сущности в spawnData, добавленные модом, должны быть зарегистрированы в UnitDatabase, чтобы работали восстановление из сохранения и спавн на стороне клиента.

### Свойства

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

Этаж, выполняющий это назначение


#### Stage

```csharp
public StageEntity Stage { get; }
```

Этап этажа. Null, если не найден.


#### Floor

```csharp
public FloorData Floor { get; }
```

Данные этажа (сид, сложность, имя этапа). Null, если серверных данных нет.


#### Difficulty

```csharp
public int Difficulty { get; }
```

Итоговая сложность, используемая для выбора пула (учитывает количество игроков и DIFFICULTY_UP)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

Вид назначаемой комнаты


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

Индекс этого назначения на этаже (с нуля). Начинается с 0 для каждого экземпляра генератора.


#### Seed

```csharp
public int Seed { get; }
```

Hash(сид этажа, индекс назначения). Сид для ГСЧ мода.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

Исходные ассеты этапа (только для чтения). Один для комнаты, phaseCount для боевой зоны.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

Первый исходный ассет. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

Копия, которая будет назначена. Заменяемая.

### Конструкторы

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*Описания пока нет.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

Вид назначаемой комнаты

| Значение | Описание |
|---|---|
| `Normal` | Обычная боевая комната (засада) |
| `Weak` | Слабая боевая комната (пул сложности ≤ 1, без засады) |
| `BattleZone` | Боевая зона (объединяет несколько фаз, затем запускает хук один раз) |
