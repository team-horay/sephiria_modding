<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# HorayModAPI

게임이 모드에 공개하는 정적 진입점입니다. 데이터베이스 로드·세션·스폰 이벤트를 구독하고, 네트워크 프리팹과 전문 능력치를 등록합니다. 이벤트 구독은 `OnModLoaded`에서 한 번만 하세요. 생명주기와 멀티플레이 규칙은 [DLL 모드](../dll-mods.md)를 보세요.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`HorayModAPI`](#horaymodapi) | 모드용 진입점입니다. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | 모드 번역 텍스트를 등록하는 도우미입니다. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

모드용 진입점입니다.

예시:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### 상수와 필드

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*설명이 아직 없습니다.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*설명이 아직 없습니다.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*설명이 아직 없습니다.*

### 이벤트

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

기적 데이터베이스가 초기화된 직후 발생합니다. `MiracleDatabase.Register`·`Modify`로 기적을 등록·수정합니다.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

무기 데이터베이스가 초기화된 직후 발생합니다. `WeaponDatabase.Register`·`Modify`로 무기를 등록·수정합니다.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

\ 상태(스탯 효과) 데이터베이스가 초기화된 직후 발생합니다. `StatusDatabase.Register`·`Modify`로 상태를 등록·수정합니다.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

전문 능력치 카테고리와 상태가 적재된 직후 발생합니다. `RegisterSpecialStatCategory`·`RegisterSpecialStat`로 표시 항목을 등록합니다.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

아이템 데이터베이스가 초기화된 직후 발생합니다. `ItemDatabase.Register`·`Modify`로 아이템을 등록·수정합니다.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

키워드 데이터베이스가 초기화된 직후 발생합니다. `KeywordDatabase.Register`·`Modify`로 키워드를 등록·수정합니다.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

패시브 데이터베이스가 초기화된 직후 발생합니다. `PassiveDatabase.Register`·`Modify`로 패시브를 등록·수정합니다.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

코스튬 데이터베이스가 초기화된 직후 발생합니다. `CostumeDatabase.Register`·`RegisterSkin`·`Modify`로 코스튬을 등록·수정합니다.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

프롭 데이터베이스가 초기화된 직후 발생합니다. `PropDatabase.Register`로 프롭을 등록합니다.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

유닛 데이터베이스(적·NPC 스폰 엔티티)가 초기화된 직후 발생합니다.


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

NPC 역할 데이터베이스가 초기화된 직후 발생합니다.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

사용자 지정 게임 데이터베이스가 초기화된 직후 발생합니다.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

모든 데이터베이스 초기화가 끝난 직후 발생합니다.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

타이틀 메뉴가 로드된 이후 호출됩니다.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

게임이 시작될 때 호출됩니다. (server-side) 시작이란, 타이틀 화면에서 세이브 슬롯을 클릭하여 게임에 진입하는 행위를 의미합니다. 아직 플레이어나 현재 장소 등 세션 데이터의 로드가 되기 이전입니다.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

게임이 시작될 때 호툴됩니다. (client-side) 시작이란, 타이틀 화면에서 세이브 슬롯을 클릭하여 게임에 진입하는 행위를 의미합니다. 아직 플레이어나 현재 장소 등 세션 데이터의 로드가 되기 이전입니다.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

게임 세션이 시작될 때 호출됩니다. (server-side) 세션은 게임의 단위입니다. 플레이어가 사망하면 해당 세션이 종료되고, 새로운 세션이 시작됩니다.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

게임 세션이 시작될 때 호출됩니다. (client-side) 세션은 게임의 단위입니다. 플레이어가 사망하면 해당 세션이 종료되고, 새로운 세션이 시작됩니다.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

번역 파일이 로드된 후 호출됩니다.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

층 할당시 호출됩니다. (server-side) 세피리아의 모든 장소는 층(floor)으로 정의됩니다. 플레이어가 장소를 이동할 시에는 다음과 같은 절차를 순차적으로 거칩니다. 1. 층 할당 (Allocation) : 새로운 층을 생성 & 로드합니다. 2. 이동 : 로드가 다 된 층의 요청 지점으로 플레이어의 위치를 이동시킵니다. 3. 층 제거 : 플레이어가 이전에 있던 장소는 그 장소가 비어 있는 경우 (=플레이어들이 더는 그 장소에 없는 경우) 제거합니다.
param1: Floor GUID  
param2: Floor Name  
param3: Instance of FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

층 할당시 호출됩니다. (client-side) 세피리아의 모든 장소는 층(floor)으로 정의됩니다. 플레이어가 장소를 이동할 시에는 다음과 같은 절차를 순차적으로 거칩니다. 1. 층 할당 (Allocation) : 새로운 층을 생성 & 로드합니다. 2. 이동 : 로드가 다 된 층의 요청 지점으로 플레이어의 위치를 이동시킵니다. 3. 층 제거 : 플레이어가 이전에 있던 장소는 그 장소가 비어 있는 경우 (=플레이어들이 더는 그 장소에 없는 경우) 제거합니다.
param1: Floor GUID  
param2: Floor Name  
param3: Instance of FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

스테이지의 랜덤 적이 어떤 구성으로 결정된 이후 호출됩니다. (server-side) Fired right after the floor generator picks the monster phases for a battle room (or a battle zone), before the spawner receives them. Replace or edit ctx.Phases to change what spawns there. The game does not enforce balance here — see RoomSpawnAssignContext for the rules (deterministic RNG via ctx.Seed, never touch ctx.Sources, entities must be registered in UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

플레이어가 스폰될 때 호출됩니다. (server-side) 이 훅 안에서 플레이어와 관련한 콜백을 등록할 수 있습니다. Raised at the end of PlayerAvatar.OnStartServer, after base stats are applied. Costume, floor and inventory contents may still be populated later in the same frame or on following frames. Use this to subscribe to the avatar's own instance events (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Subscriptions on the avatar die with the avatar, so no unsubscribe is needed for them.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

플레이어 아바타가 스폰될 때 호출됩니다. (client-side) Raised once per avatar at the end of its OnStartClient; also fires for remote players. Check avatar.isLocalPlayer / avatar.isOwned to tell the local player apart. SyncVars are already applied, but floor and inventory contents may arrive later.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

유닛(플레이어·적·소환수·동료)이 서버에 스폰됐을 때 발생합니다. 서버(호스트) 전용입니다.


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

유닛이 서버에서 제거되기 직전에 발생합니다. 서버(호스트) 전용입니다.


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

플레이어가 공격을 회피했을 때 발생합니다. (server-side)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

유닛에 디버프가 적용되기 직전, 적용할 디버프 프리팹을 바꿀 수 있습니다. 핸들러는 (현재 프리팹, 시전자)를 받아 대체 프리팹을 돌려주고, null을 돌려주면 그대로 둡니다.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

서버가 인벤토리 쓰기 권한을 얻어 아이템 효과를 전부 제거하기 시작할 때 발생합니다.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

서버가 쓰기 권한을 반납하며 아이템 효과를 다시 반영한 직후 발생합니다.

### 메서드

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

네트워크 스폰 가능한 오브젝트(프리팹)을 등록합니다. 개체에는 NetworkIdentity 컴포넌트가 있어야 합니다.

서버(호스트)와 클라이언트는 네트워크 통신 기능을 사용할 수 있는 개체들의 목록을 소유해야 하며 그 목록은 모두가 똑같이 공유되어야 합니다.

예시:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

반환: true if registration succeeded


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

런타임에 만든 프리팹(내장 템플릿의 복제본 등)을 `stableKey`에서 계산한 고정 assetId로 네트워크 스폰 가능 오브젝트로 등록합니다. 키는 모드 유닛 이름(`<modId>:<id>`)을 씁니다.

반환: 등록에 성공하면 true


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

여러 프리팹을 한 번에 네트워크 스폰 가능 오브젝트로 등록합니다.

반환: 등록에 성공한 프리팹 수


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

등록한 네트워크 프리팹을 해제합니다.

반환: 제거했으면 true


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

스탯 UI의 전문 능력 항목 카테고리를 등록합니다.

반환: 등록했으면 true


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

StatusEntity를 스탯 UI의 전문 능력 카테고리 내의 항목으로 등록합니다. `reader`를 전달하면 에셋 설정 대신 그 함수로 값을 읽습니다.

반환: 등록했으면 true


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

modId에 묶인 기기별 옵션을 얻습니다. 창작마당 모드는 `Context.Options`를 써도 됩니다.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

인벤토리와 함께 쓰는 아이템 상호작용 패널을 엽니다. UI가 사용 중이면 false를 돌려줍니다.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

게임 스타일 UI 위젯 생성기를 얻습니다. 기존 UI Canvas 아래에 붙여 씁니다.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

월드 대화창을 엽니다. UI가 사용 중이면 false를 돌려주고, 다음 페이지는 `session.Show`로 넘깁니다.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

월드 좌표 오프셋을 지정해 대화창을 엽니다. null이면 프리팹 기본 오프셋을 씁니다.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

대화창 오프셋과 카메라 오프셋을 따로 지정해 대화창을 엽니다. 카메라 포커스는 로컬에서만 적용됩니다.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

모드 번역 텍스트를 등록하는 도우미입니다.

### 메서드

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

특정 언어의 텍스트를 등록합니다.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Register text for multiple languages at once
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
