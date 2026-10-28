<!-- Auto-generated file. Tools/ModApiDocGen in the game repo builds it from /// comments in the source. Don't edit it directly; fix the source comments and regenerate. -->
# HorayModAPI

The static entry point the game exposes to mods. Subscribe to database load, session, and spawn events, and register network prefabs and Special Stats. Subscribe to events only once, in `OnModLoaded`. For the lifecycle and multiplayer rules, see [DLL Mods](../dll-mods.md).

Generated 2026-10-26 for game version **1.1.0**. [Reference index](README.md)

| Type | Description |
|---|---|
| [`HorayModAPI`](#horaymodapi) | The entry point for mods. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | A helper for registering mod localization text. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

The entry point for mods.

Example:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Constants and Fields

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*No description yet.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*No description yet.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*No description yet.*

### Events

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Fires right after the Miracle database is initialized. Register or modify Miracles with `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Fires right after the weapon database is initialized. Register or modify weapons with `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Fires right after the status (stat effect) database is initialized. Register or modify statuses with `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Fires right after special stat categories and statuses are loaded. Register display entries with `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Fires right after the item database is initialized. Register or modify items with `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Fires right after the keyword database is initialized. Register or modify keywords with `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Fires right after the passive database is initialized. Register or modify passives with `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Fires right after the costume database is initialized. Register or modify costumes with `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Fires right after the prop database is initialized. Register props with `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Fires right after the unit database (enemy and NPC spawn entities) is initialized.


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Fires right after the NPC role database is initialized.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Fires right after the custom game database is initialized.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Fires right after all databases finish initializing.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Called after the title menu loads.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Called when the game starts. (server-side) Starting means clicking a save slot on the title screen to enter the game. Session data such as the player and current location has not been loaded yet.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Called when the game starts. (client-side) Starting means clicking a save slot on the title screen to enter the game. Session data such as the player and current location has not been loaded yet.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Called when a game session starts. (server-side) A session is a unit of play. When the player dies, that session ends and a new one begins.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Called when a game session starts. (client-side) A session is a unit of play. When the player dies, that session ends and a new one begins.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Called after the localization files are loaded.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Called when a floor is allocated. (server-side) Every location in Sephiria is defined as a floor. When a player moves between locations, the following steps run in order. 1. Floor allocation: creates and loads the new floor. 2. Move: moves the player to the requested point on the fully loaded floor. 3. Floor removal: removes the location the player was in before, if it is empty (= no players remain there).
param1: Floor GUID  
param2: Floor Name  
param3: Instance of FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Called when a floor is allocated. (client-side) Every location in Sephiria is defined as a floor. When a player moves between locations, the following steps run in order. 1. Floor allocation: creates and loads the new floor. 2. Move: moves the player to the requested point on the fully loaded floor. 3. Floor removal: removes the location the player was in before, if it is empty (= no players remain there).
param1: Floor GUID  
param2: Floor Name  
param3: Instance of FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Called after the random enemy composition for a stage is decided. (server-side) Fired right after the floor generator picks the monster phases for a battle room (or a battle zone), before the spawner receives them. Replace or edit ctx.Phases to change what spawns there. The game does not enforce balance here — see RoomSpawnAssignContext for the rules (deterministic RNG via ctx.Seed, never touch ctx.Sources, entities must be registered in UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Called when a player spawns. (server-side) You can register player-related callbacks inside this hook. Raised at the end of PlayerAvatar.OnStartServer, after base stats are applied. Costume, floor and inventory contents may still be populated later in the same frame or on following frames. Use this to subscribe to the avatar's own instance events (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Subscriptions on the avatar die with the avatar, so no unsubscribe is needed for them.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Called when a player avatar spawns. (client-side) Raised once per avatar at the end of its OnStartClient; also fires for remote players. Check avatar.isLocalPlayer / avatar.isOwned to tell the local player apart. SyncVars are already applied, but floor and inventory contents may arrive later.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Fires when a unit (player, enemy, summon, or companion) spawns on the server. Server (host) only.


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Fires right before a unit is removed from the server. Server (host) only.


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Fires when a player dodges an attack. (server-side)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Lets you swap the debuff prefab right before a debuff is applied to a unit. The handler receives (current prefab, caster) and returns a replacement prefab; returning null leaves it unchanged.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Fires when the server takes inventory write permission and starts removing all item effects.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Fires right after the server returns write permission and reapplies item effects.

### Methods

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Registers an object (prefab) that can be spawned over the network. The object must have a NetworkIdentity component.

The server (host) and clients must each hold a list of objects that can use networking, and that list must be identical for everyone.

Example:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Returns: true if registration succeeded


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Registers a prefab created at runtime (such as a clone of a built-in template) as a network-spawnable object, using a fixed assetId computed from `stableKey`. Use the mod unit name (`<modId>:<id>`) as the key.

Returns: true if registration succeeded


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Registers multiple prefabs as network-spawnable objects at once.

Returns: the number of prefabs registered successfully


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Unregisters a registered network prefab.

Returns: true if it was removed


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Registers a Special Stats category in the stats UI.

Returns: true if registered


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Registers a StatusEntity as an entry in a Special Stats category in the stats UI. If you pass `reader`, the value is read through that function instead of the asset settings.

Returns: true if registered


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Gets the per-device options bound to modId. Workshop mods can use `Context.Options` instead.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Opens an item interaction panel used alongside the inventory. Returns false if the UI is busy.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Gets a factory for game-styled UI widgets. Attach them under an existing UI Canvas.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Opens a world dialogue box. Returns false if the UI is busy; show the next page with `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Opens a dialogue box with a world-space offset. If null, the prefab's default offset is used.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Opens a dialogue box with separate dialogue and camera offsets. Camera focus applies only locally.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

A helper for registering mod localization text.

### Methods

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Registers text for a specific language.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Register text for multiple languages at once
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
