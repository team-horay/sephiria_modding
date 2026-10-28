<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# 部屋のスポーン割り当て

`HorayModAPI.OnAssignRoomSpawnPhases` イベントが渡すコンテキストです。フロアジェネレーターが部屋にモンスターのフェーズを割り当てる瞬間にフックします。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。[リファレンス目次](README.md)

| 型 | 説明 |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | FloorGenerator がランダムベースの敵を割り当てるときに呼び出されるコンテキスト。 |
| [`ERoomSpawnKind`](#eroomspawnkind) | 割り当て対象の部屋の種類 |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

FloorGenerator がランダムベースの敵を割り当てるときに呼び出されるコンテキストです。サーバー(ホスト)でのみ発生します。

ルール
- `Phases` はステージテーブルのディープコピーです。変更しても、丸ごと差し替えても構いません。null のままにすると元のコピーに戻ります。  
- `Sources` は元のステージアセットです。読み取り専用です(変更するとエディターのアセットが汚れます)。  
部屋の割り当てでは、`Phases`(コピー)の containedFactions は空です(GeneratePhaseData はコピーしません)。ファクションは Sources から読み取ってください。
- 乱数には、`Seed` から `new System.Random(ctx.Seed)` を作成してください。UnityEngine.Random やフロアのRNGを消費すると、再生成や再参加の際にフロアが変わってしまいます。  
- MODが spawnData に追加したエンティティは、セーブの復元とクライアント側のスポーンが機能するように UnitDatabase に登録されている必要があります。

### プロパティ

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

この割り当てを行っているフロア


#### Stage

```csharp
public StageEntity Stage { get; }
```

フロアのステージ。見つからない場合は null。


#### Floor

```csharp
public FloorData Floor { get; }
```

フロアデータ(シード、難易度、ステージ名)。サーバーデータがない場合は null。


#### Difficulty

```csharp
public int Difficulty { get; }
```

プールの選択に使われる最終的な難易度(プレイヤー数と DIFFICULTY_UP を含む)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

割り当て対象の部屋の種類


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

フロア内でのこの割り当てのインデックス(0始まり)。ジェネレーターのインスタンスごとに 0 から始まります。


#### Seed

```csharp
public int Seed { get; }
```

Hash(フロアシード, 割り当てインデックス)。MODのRNG用のシードです。


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

元のステージアセット(読み取り専用)。部屋なら1つ、バトルゾーンなら phaseCount 個。


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

最初の元アセット。Sources[0]。


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

割り当てられるコピー。差し替え可能。

### コンストラクター

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*説明はまだありません。*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

割り当て対象の部屋の種類

| 値 | 説明 |
|---|---|
| `Normal` | 通常の戦闘部屋(待ち伏せあり) |
| `Weak` | 弱い戦闘部屋(難易度 ≤ 1 のプール、待ち伏せなし) |
| `BattleZone` | バトルゾーン(複数のフェーズを統合してから、フックを1回実行) |
