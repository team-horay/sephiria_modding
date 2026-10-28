<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# 房間生成指派

`HorayModAPI.OnAssignRoomSpawnPhases` 事件傳入的 Context。在樓層產生器為房間指派怪物階段的那一刻掛接進來。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。[參考索引](README.md)

| 型別 | 說明 |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | FloorGenerator 指派隨機敵人時呼叫的 Context。 |
| [`ERoomSpawnKind`](#eroomspawnkind) | 被指派的房間種類 |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

FloorGenerator 指派隨機敵人時呼叫的 Context。只在伺服器（房主）上觸發。

規則
- `Phases` 是關卡表的深層複製。你可以修改它或整個替換。保留為 null 會還原為原始複製。  
- `Sources` 是原始關卡資產。唯讀（修改它會弄髒編輯器資產）。  
在房間指派中，`Phases`（複製）的 containedFactions 是空的（GeneratePhaseData 不會複製它）。請從 Sources 讀取陣營。
- 需要隨機性時，請從 `Seed` 建立 `new System.Random(ctx.Seed)`。消耗 UnityEngine.Random 或樓層的亂數產生器會導致樓層在重新產生或重新加入時不一致。  
- 模組加入 spawnData 的實體必須在 UnitDatabase 中註冊，存檔還原與用戶端生成才能正常運作。

### 屬性

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

執行此次指派的樓層


#### Stage

```csharp
public StageEntity Stage { get; }
```

樓層的關卡。找不到時為 null。


#### Floor

```csharp
public FloorData Floor { get; }
```

樓層資料（種子、難度、關卡名稱）。沒有伺服器資料時為 null。


#### Difficulty

```csharp
public int Difficulty { get; }
```

用於挑選池的最終難度（包含玩家人數與 DIFFICULTY_UP）


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

被指派的房間種類


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

此次指派在樓層上的索引（從 0 開始）。每個產生器實例都從 0 開始。


#### Seed

```csharp
public int Seed { get; }
```

Hash(樓層種子, 指派索引)。模組亂數產生器用的種子。


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

原始關卡資產（唯讀）。房間為一個，戰鬥區域為 phaseCount 個。


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

第一個原始資產。Sources[0]。


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

將被指派的複製。可替換。

### 建構函式

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*尚無說明。*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

被指派的房間種類

| 值 | 說明 |
|---|---|
| `Normal` | 一般戰鬥房間（伏擊） |
| `Weak` | 弱化戰鬥房間（難度 ≤ 1 的池，無伏擊） |
| `BattleZone` | 戰鬥區域（合併多個階段後只執行一次掛鉤） |
