<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# 房间刷怪分配

`HorayModAPI.OnAssignRoomSpawnPhases` 事件传入的上下文。在楼层生成器为房间分配怪物阶段的时刻进行挂接。

生成于 2026-10-26，对应游戏版本 **1.1.0**。[参考索引](README.md)

| 类型 | 说明 |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | FloorGenerator 分配随机敌人时调用的上下文。 |
| [`ERoomSpawnKind`](#eroomspawnkind) | 正在分配的房间类型 |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

FloorGenerator 分配随机敌人时调用的上下文。仅在服务器（主机）上触发。

规则
- `Phases` 是关卡表的深拷贝。你可以修改它或整个替换。若将其置为 null，则恢复为原始副本。  
- `Sources` 是原始关卡资产。只读（修改它会污染编辑器资产）。  
在房间分配中，`Phases`（副本）的 containedFactions 为空（GeneratePhaseData 不会复制它）。请从 Sources 读取阵营。
- 需要随机数时，请用 `Seed` 创建 `new System.Random(ctx.Seed)`。消耗 UnityEngine.Random 或楼层的随机数生成器会导致楼层在重新生成或重新加入时不一致。  
- 模组添加到 spawnData 中的实体必须在 UnitDatabase 中注册，存档恢复和客户端生成才能正常工作。

### 属性

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

执行本次分配的楼层


#### Stage

```csharp
public StageEntity Stage { get; }
```

楼层的关卡。找不到时为 null。


#### Floor

```csharp
public FloorData Floor { get; }
```

楼层数据（种子、难度、关卡名）。没有服务器数据时为 null。


#### Difficulty

```csharp
public int Difficulty { get; }
```

用于选择池的最终难度（包含玩家数量和 DIFFICULTY_UP）


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

正在分配的房间类型


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

本次分配在楼层中的索引（从 0 开始）。每个生成器实例都从 0 开始。


#### Seed

```csharp
public int Seed { get; }
```

Hash(楼层种子, 分配索引)。供模组随机数生成器使用的种子。


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

原始关卡资产（只读）。房间为一个，战斗区域为 phaseCount 个。


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

第一个原始资产。即 Sources[0]。


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

将要分配的副本。可替换。

### 构造函数

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*暂无说明。*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

正在分配的房间类型

| 值 | 说明 |
|---|---|
| `Normal` | 普通战斗房间（伏击） |
| `Weak` | 弱化战斗房间（难度 ≤ 1 的池，无伏击） |
| `BattleZone` | 战斗区域（合并多个阶段后只运行一次钩子） |
