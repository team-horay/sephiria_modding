<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# API 参考

DLL 模组可用的公开类型与成员列表。项目设置、入口点、生命周期和多人游戏规则请参阅 [DLL 模组](../dll-mods.md)。

生成于 2026-10-26，对应游戏版本 **1.1.0**。本文档会在每次游戏更新时重新生成。

| 文档 | 类型 | 内容 |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`、`HorayModLocalizationContext` | 游戏向模组公开的静态入口点。 |
| [入口点与上下文](ModBase.md) | `WorkshopModBase`、`WorkshopModContext`、`HorayModBase`、`WorkshopDataPackage`、`WorkshopModDefinition`、`WorkshopModuleDefinition`、`WorkshopDependency`、`WorkshopModuleType`、`WorkshopInstallation` | 模组入口类所继承的基类，以及加载器附加的上下文和清单类型。 |
| [模组选项](ModOptions.md) | `ModOptions`、`ModOptionsPage`、`ModOptionDefinition`、`ModOption<T>`、`ModToggleOption`、`ModSelectionOption`、`ModSliderOption`、`ModOptionText`、`ModOptionKind`、`ModOptionsRegistry` | 按设备保存的模组设置，以及向游戏选项画面添加开关、选择框和滑块的 API。 |
| [房间刷怪分配](RoomSpawn.md) | `RoomSpawnAssignContext`、`ERoomSpawnKind` | `HorayModAPI.OnAssignRoomSpawnPhases` 事件传入的上下文。 |
| [专门属性](SpecialStats.md) | `SpecialStatContext`、`SpecialStatValue`、`SpecialStatCategoryEntity`、`SpecialStatCategoryVisibility`、`SpecialStatDisplaySettings`、`SpecialStatValueSource`、`SpecialStatLabelColor`、`SpecialStatValueProvider` | 在 `HorayModAPI.OnLoadSpecialStatDatabase` 中通过 `RegisterSpecialStatCategory` 和 `RegisterSpecialStat` 注册的属性显示类型。 |
| [UI](UI.md) | `UIWidgetFactory`、`UIWidgetTextStyle`、`DialoguePage`、`DialogueChoice`、`DialogueSession`、`DialogueCloseReason`、`ItemInteractionOptions`、`ItemInteractionSession`、`ItemInteractionItem`、`ItemInteractionCloseReason`、`ItemInteractionItemSource` | `HorayModAPI.TryShowDialogue`、`TryShowItemInteraction` 和 `TryGetUIWidgets` 所使用的对话、物品交互和控件类型。 |

## HorayModAPI 事件一览

按源码中的声明顺序列出。数据库事件在每次世界加载时都会再次触发。

| 事件 | 签名 | 说明 |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | 奇迹数据库初始化后立即触发。 |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | 武器数据库初始化后立即触发。 |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | 状态（属性效果）数据库初始化后立即触发。 |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | 专门属性分类和状态加载后立即触发。 |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | 物品数据库初始化后立即触发。 |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | 关键字数据库初始化后立即触发。 |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | 被动数据库初始化后立即触发。 |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | 服装数据库初始化后立即触发。 |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | 道具数据库初始化后立即触发。 |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | 单位数据库（敌人和 NPC 生成实体）初始化后立即触发。 |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | NPC 角色数据库初始化后立即触发。 |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | 自定义游戏数据库初始化后立即触发。 |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | 所有数据库初始化完成后立即触发。 |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | 标题菜单加载后调用。 |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | 游戏开始时调用。 |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | 游戏开始时调用。 |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | 游戏会话开始时调用。 |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | 游戏会话开始时调用。 |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | 本地化文件加载后调用。 |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | 楼层分配时调用。 |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | 楼层分配时调用。 |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | 关卡的随机敌人组成确定后调用。 |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | 玩家生成时调用。 |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | 玩家角色生成时调用。 |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | 单位（玩家、敌人、召唤物或同伴）在服务器上生成时触发。 |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | 单位从服务器移除之前触发。 |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | 玩家闪避攻击时触发。 |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | 在减益效果施加到单位之前，允许你替换减益预制体。 |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | 服务器获取背包写入权限并开始移除所有物品效果时触发。 |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | 服务器归还写入权限并重新应用物品效果后立即触发。 |
