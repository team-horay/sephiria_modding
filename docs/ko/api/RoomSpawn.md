<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# 방 스폰 배정

`HorayModAPI.OnAssignRoomSpawnPhases` 이벤트가 넘기는 컨텍스트입니다. 층 생성기가 방에 몬스터 phases를 배정하는 순간 개입합니다.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`RoomSpawnAssignContext`](#roomspawnassigncontext) | FloorGenerator 안의 랜덤 기반 적을 배정하는 순간 호출되는 컨텍스트. |
| [`ERoomSpawnKind`](#eroomspawnkind) | 배정 대상 방의 종류 |


## RoomSpawnAssignContext

```csharp
public sealed class RoomSpawnAssignContext
```

FloorGenerator 안의 랜덤 기반 적을 배정하는 순간 호출되는 컨텍스트. 서버(호스트)에서만 발생합니다.

규칙
- `Phases`는 스테이지 테이블의 깊은 복제본입니다. 수정하거나 통째로 교체해도 됩니다. null로 두면 원래 복제본으로 되돌립니다.  
- `Sources`는 스테이지 에셋의 원본입니다. 읽기만 합니다(수정하면 에디터 에셋이 더럽혀집니다).  
방 배정의 `Phases`(복제본)는 containedFactions가 비어 있습니다(GeneratePhaseData가 복사하지 않음). 진영은 Sources에서 읽습니다.
- 난수는 `Seed`로 `new System.Random(ctx.Seed)`를 만들어 씁니다. UnityEngine.Random이나 층 난수를 소비하면 재생성·리조인 때 층이 달라집니다.  
- 모드가 넣는 spawnData의 엔티티는 UnitDatabase에 등록돼 있어야 세이브 복원과 클라이언트 스폰이 성립합니다.

### 프로퍼티

#### Generator

```csharp
public FloorGenerator Generator { get; }
```

이 배정을 수행 중인 층


#### Stage

```csharp
public StageEntity Stage { get; }
```

층의 스테이지. 못 찾으면 null.


#### Floor

```csharp
public FloorData Floor { get; }
```

층 데이터(시드·난이도·스테이지명). 서버 데이터가 없으면 null.


#### Difficulty

```csharp
public int Difficulty { get; }
```

풀을 뽑을 때 쓴 최종 난이도(인원·DIFFICULTY_UP 반영)


#### Kind

```csharp
public ERoomSpawnKind Kind { get; }
```

배정 대상 방의 종류


#### AssignIndex

```csharp
public int AssignIndex { get; }
```

이 층에서 몇 번째 배정인지(0부터). 생성기 인스턴스마다 0에서 시작합니다.


#### Seed

```csharp
public int Seed { get; }
```

Hash(층 시드, 배정 순번). 모드 난수의 씨앗.


#### Sources

```csharp
public IReadOnlyList<MonsterSpawnPhases> Sources { get; }
```

스테이지 에셋 원본(읽기 전용). 방은 1개, 배틀존은 phaseCount개.


#### Source

```csharp
public MonsterSpawnPhases Source { get; }
```

첫 원본. Sources[0].


#### Phases

```csharp
public MonsterSpawnPhases Phases { get; set; }
```

배정될 복제본. 교체 가능.

### 생성자

#### RoomSpawnAssignContext

```csharp
public RoomSpawnAssignContext(FloorGenerator generator, StageEntity stage, FloorData floor, int difficulty, ERoomSpawnKind kind, int assignIndex, int seed, IReadOnlyList<MonsterSpawnPhases> sources, MonsterSpawnPhases phases)
```

*설명이 아직 없습니다.*


## ERoomSpawnKind

```csharp
public enum ERoomSpawnKind
```

배정 대상 방의 종류

| 값 | 설명 |
|---|---|
| `Normal` | 일반 전투 방(매복) |
| `Weak` | 약한 전투 방(difficulty ≤ 1 풀, 비매복) |
| `BattleZone` | 배틀존(여러 phases를 병합한 뒤 한 번만 훅을 탑니다) |
