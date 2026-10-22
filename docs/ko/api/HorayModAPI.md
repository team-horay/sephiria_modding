<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# HorayModAPI

게임이 모드에 공개하는 정적 진입점입니다. 데이터베이스 로드·세션·스폰 이벤트를 구독하고, 네트워크 프리팹과 전문 능력치를 등록합니다. 이벤트 구독은 `OnModLoaded`에서 한 번만 하세요. 생명주기와 멀티플레이 규칙은 [DLL 모드](../dll-mods.md)를 보세요.

게임 버전 **1.0.34** 기준, 2026-10-22 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`HorayModAPI`](#horaymodapi) |  |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | A helper class to register localization text for a mod. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

**WIP**

예시:

```csharp
ModAPI.OnMiracleDatabaseReady += () => {
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

MiracleDatabase


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

WeaponDatabase


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

StatusDatabase


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Categories and statuses are loaded. Register display-only stats, categories or custom value readers here.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

ItemDatabase


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

KeywordDatabase


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

PassiveDatabase


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

CostumeDatabase


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

PropDatabase


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

UnitDatabase (enemy / NPC spawn entities). Register custom units here with UnitDatabase.Register(entity), and register their prefabs with RegisterNetworkPrefab(prefab, stableKey) in the same handler. Entity names must be "<modId>:<id>". See Docs/UnitMod_Design.md.


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

SocialIDDatabase (NPC social IDs, role graphs, travelers). Register here with SocialIDDatabase.RegisterRole(roleGraph) → SocialIDDatabase.Register(socialID) → SocialIDDatabase.RegisterTravelerGroup(key, socialIDs), and register the avatar prefab with RegisterNetworkPrefab(prefab, stableKey) in the same handler. Names and keys must be "<modId>:<id>". Fires after UnitDatabase/FactionDatabase, so vanilla factions can be referenced. See Docs/TravelerMod_Guide.md.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

All databases initialized, ready to use (for mods that need cross-database references)


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

The title menu is ready, once per title scene instance, after built-in buttons are added. Subscribe in OnModLoaded and unsubscribe in OnModUnloaded. Use title.AddTitleSubMenu(localizationKey, onClick) or title.AddTitleMainMenu(...). Register label translations through OnLocalizationReady before entering the title. Buttons returned by these methods belong to the current scene and must not be cached across scenes.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

The networked game has started. Server(host) side. Raised before the first run is set up: there is no dungeon and no run state yet. Use OnStartSessionServerside when the run itself matters.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

The networked game has started. Client side (the host's local client included). Raised as soon as the client is up, before the world is populated. Use OnStartSessionClientside when the run itself matters.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

A game session (run) has started. Server(host) side. Raised on every new run: host start, continue, and restart after game over. The parameter is true when the run was restored from a save file (continue), false for a brand-new run.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

A game session (run) has started. Client side (the host's local client included). Clients that join a session already in progress receive it once, right after they connect. The parameter is the same value the server reported for this session.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Localization ready (for mods that need to register localization text)


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Floor allocated on the server (server-only). Fired right after the floor is connected to its FloorData and network-spawned, before generation starts. Only invoked on the host/server.
param1: Floor GUID  
param2: Floor Name  
param3: Instance of FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Floor allocated on the client (runs on every client, including the host's client). Fired after the floor finished generating locally. Use this for client-side add-ons such as UI overlays tied to a floor.
param1: Floor GUID  
param2: Floor Name  
param3: Instance of FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Serverside. Fired right after the floor generator picks the monster phases for a battle room (or a battle zone), before the spawner receives them. Replace or edit ctx.Phases to change what spawns there. The game does not enforce balance here — see RoomSpawnAssignContext for the rules (deterministic RNG via ctx.Seed, never touch ctx.Sources, entities must be registered in UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

A player avatar has been spawned on the server (host only). Raised at the end of PlayerAvatar.OnStartServer, after base stats are applied. Costume, floor and inventory contents may still be populated later in the same frame or on following frames. Use this to subscribe to the avatar's own instance events (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Subscriptions on the avatar die with the avatar, so no unsubscribe is needed for them.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

A player avatar has been spawned on this client (the host's local client included). Raised once per avatar at the end of its OnStartClient; also fires for remote players. Check avatar.isLocalPlayer / avatar.isOwned to tell the local player apart. SyncVars are already applied, but floor and inventory contents may arrive later.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Any UnitAvatar (players, enemies, summons, followers) has been spawned on the server (host only). Raised at the end of UnitAvatar.OnStartServer, before subclass setup. For players this fires before OnPlayerAvatarSpawnedServerside. Use it for OnKill / OnDie style effects on enemies.


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Any UnitAvatar is about to be removed from the server (host only). Raised at the start of UnitAvatar.OnStopServer, before the unit leaves its faction and leader. Do not keep references to the unit after this call.


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

*설명이 아직 없습니다.*


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

*설명이 아직 없습니다.*


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

인벤토리 변경 시작. 모든 효과를 제거함


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

권한을 해제하고 모든 효과를 반영함

### 메서드

#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Local options scoped by a stable manifest modId. Workshop mods can use Context.Options. Legacy AddOns may supply their own ID using the same format. Call Save after making changes.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Opens the shared inventory interaction on the Unity main thread. Returns false if UI is busy.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Game-styled widgets for any local UI. Parent them under an existing UI Canvas.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Opens a local world dialogue on the Unity main thread. Returns false if UI is busy. Use session.Show to advance; reward validation and networking remain the caller's job.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Follows anchor with a world-space offset. Null uses the view prefab's default offset.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Follows anchor with separate dialogue and camera offsets. Camera focus is optional and local only.


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

*설명이 아직 없습니다.*


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

*설명이 아직 없습니다.*


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Register a prefab as a network-spawnable object. The prefab must have a NetworkIdentity component. Call this during OnAllDatabasesReady or earlier events. Also registers with the current client if it is already connecting or connected.

예시:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myCustomPrefab);
};
```

반환: true if registration succeeded


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Register a runtime-created prefab (for example a clone of a built-in template) with a deterministic assetId derived from `stableKey`. Use the mod unit name ("<modId>:<id>") as the key.

Why: Instantiate copies the template's assetId, and Mirror would silently replace the template mapping on clients.  
Host and client compute the same id because they run the same mod set in the same order; if the id is taken, "stableKey#1", "#2", ... are tried in order and a warning is logged. Call this in OnLoadUnitDatabase or OnAllDatabasesReady, from code that runs on both host and client.

반환: true if registration succeeded


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Register multiple prefabs as network-spawnable objects at once.

반환: Number of successfully registered prefabs


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Unregister a previously registered network prefab.

반환: true if successfully removed


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

A helper class to register localization text for a mod.

### 메서드

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Register text for a specific language


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Register text for multiple languages at once
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
