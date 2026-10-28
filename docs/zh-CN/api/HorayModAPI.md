<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# HorayModAPI

游戏向模组公开的静态入口点。可以订阅数据库加载、会话和生成事件，并注册网络预制体和专门属性。请只在 `OnModLoaded` 中订阅一次事件。生命周期和多人游戏规则请参阅 [DLL 模组](../dll-mods.md)。

生成于 2026-10-26，对应游戏版本 **1.1.0**。[参考索引](README.md)

| 类型 | 说明 |
|---|---|
| [`HorayModAPI`](#horaymodapi) | 模组的入口点。 |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | 用于注册模组本地化文本的辅助类。 |


## HorayModAPI

```csharp
public static class HorayModAPI
```

模组的入口点。

示例：

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### 常量与字段

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*暂无说明。*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*暂无说明。*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*暂无说明。*

### 事件

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

奇迹数据库初始化后立即触发。使用 `MiracleDatabase.Register`/`Modify` 注册或修改奇迹。


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

武器数据库初始化后立即触发。使用 `WeaponDatabase.Register`/`Modify` 注册或修改武器。


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

状态（属性效果）数据库初始化后立即触发。使用 `StatusDatabase.Register`/`Modify` 注册或修改状态。


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

专门属性分类和状态加载后立即触发。使用 `RegisterSpecialStatCategory`/`RegisterSpecialStat` 注册显示条目。


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

物品数据库初始化后立即触发。使用 `ItemDatabase.Register`/`Modify` 注册或修改物品。


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

关键字数据库初始化后立即触发。使用 `KeywordDatabase.Register`/`Modify` 注册或修改关键字。


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

被动数据库初始化后立即触发。使用 `PassiveDatabase.Register`/`Modify` 注册或修改被动。


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

服装数据库初始化后立即触发。使用 `CostumeDatabase.Register`/`RegisterSkin`/`Modify` 注册或修改服装。


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

道具数据库初始化后立即触发。使用 `PropDatabase.Register` 注册道具。


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

单位数据库（敌人和 NPC 生成实体）初始化后立即触发。


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

NPC 角色数据库初始化后立即触发。


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

自定义游戏数据库初始化后立即触发。


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

所有数据库初始化完成后立即触发。


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

标题菜单加载后调用。


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

游戏开始时调用。（服务器端）“开始”指在标题画面点击存档槽进入游戏。此时玩家和当前位置等会话数据尚未加载。


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

游戏开始时调用。（客户端）“开始”指在标题画面点击存档槽进入游戏。此时玩家和当前位置等会话数据尚未加载。


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

游戏会话开始时调用。（服务器端）会话是游玩的一个单位。玩家死亡时，该会话结束并开始新的会话。


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

游戏会话开始时调用。（客户端）会话是游玩的一个单位。玩家死亡时，该会话结束并开始新的会话。


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

本地化文件加载后调用。


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

楼层分配时调用。（服务器端）Sephiria 中的每个地点都被定义为一个楼层。玩家在地点之间移动时，会依次执行以下步骤。1. 楼层分配：创建并加载新楼层。2. 移动：把玩家移动到已完全加载的楼层上的指定位置。3. 楼层移除：如果玩家之前所在的地点已经为空（= 没有玩家留在那里），则将其移除。
param1：楼层 GUID  
param2：楼层名称  
param3：FloorGenerator 的实例


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

楼层分配时调用。（客户端）Sephiria 中的每个地点都被定义为一个楼层。玩家在地点之间移动时，会依次执行以下步骤。1. 楼层分配：创建并加载新楼层。2. 移动：把玩家移动到已完全加载的楼层上的指定位置。3. 楼层移除：如果玩家之前所在的地点已经为空（= 没有玩家留在那里），则将其移除。
param1：楼层 GUID  
param2：楼层名称  
param3：FloorGenerator 的实例


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

关卡的随机敌人组成确定后调用。（服务器端）在楼层生成器为战斗房间（或战斗区域）选定怪物阶段之后、怪物生成器接收它们之前立即触发。替换或编辑 ctx.Phases 即可改变该处生成的内容。游戏在这里不会强制平衡性，规则请参阅 RoomSpawnAssignContext（通过 ctx.Seed 使用确定性随机数、绝不触碰 ctx.Sources、实体必须在 UnitDatabase 中注册）。


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

玩家生成时调用。（服务器端）可以在这个钩子内注册与玩家相关的回调。在 PlayerAvatar.OnStartServer 的末尾、基础属性应用之后触发。服装、楼层和背包内容可能在同一帧的稍后或后续帧中才填充。可用它来订阅角色自身的实例事件（OnDamagedServerside、OnAttackUnit、OnKillUnit、OnDie、OnAddedBuff、OnEnteredFloorServerside 等）。对角色的订阅会随角色一起销毁，所以不需要为它们取消订阅。


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

玩家角色生成时调用。（客户端）每个角色在其 OnStartClient 末尾触发一次；远程玩家也会触发。通过 avatar.isLocalPlayer / avatar.isOwned 可以区分本地玩家。SyncVar 已经应用，但楼层和背包内容可能稍后才到达。


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

单位（玩家、敌人、召唤物或同伴）在服务器上生成时触发。仅限服务器（主机）。


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

单位从服务器移除之前触发。仅限服务器（主机）。


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

玩家闪避攻击时触发。（服务器端）


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

在减益效果施加到单位之前，允许你替换减益预制体。处理程序接收（当前预制体, 施放者）并返回替换用的预制体；返回 null 则保持不变。


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

服务器获取背包写入权限并开始移除所有物品效果时触发。


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

服务器归还写入权限并重新应用物品效果后立即触发。

### 方法

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

注册一个可以通过网络生成的对象（预制体）。该对象必须带有 NetworkIdentity 组件。

服务器（主机）和客户端各自持有一份可使用网络功能的对象列表，并且这份列表对所有人必须完全一致。

示例：

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

返回值：注册成功时为 true


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

把运行时创建的预制体（例如内置模板的克隆）注册为可网络生成的对象，并使用由 `stableKey` 计算出的固定 assetId。请使用模组单位名（`<modId>:<id>`）作为键。

返回值：注册成功时为 true


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

一次性把多个预制体注册为可网络生成的对象。

返回值：成功注册的预制体数量


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

取消注册一个已注册的网络预制体。

返回值：已移除时为 true


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

在属性 UI 中注册一个专门属性分类。

返回值：注册成功时为 true


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

把一个 StatusEntity 注册为属性 UI 中某个专门属性分类下的条目。如果传入 `reader`，则通过该函数读取值，而不是使用资产设置。

返回值：注册成功时为 true


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

获取绑定到 modId 的按设备保存的选项。创意工坊模组可以改用 `Context.Options`。


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

打开一个与背包并用的物品交互面板。UI 忙碌时返回 false。


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

获取游戏风格 UI 控件的工厂。请把它们挂到现有的 UI Canvas 下。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

打开一个世界对话框。UI 忙碌时返回 false；用 `session.Show` 显示下一页。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

打开一个带有世界空间偏移的对话框。为 null 时使用预制体的默认偏移。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

打开一个对话框，并分别指定对话偏移和摄像机偏移。摄像机聚焦仅在本地生效。


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

用于注册模组本地化文本的辅助类。

### 方法

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

为特定语言注册文本。


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

一次性为多种语言注册文本
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
