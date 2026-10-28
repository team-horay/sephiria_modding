<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# API 參考

DLL 模組可使用的公開型別與成員清單。專案設定、進入點、生命週期與多人遊戲規則請參閱 [DLL 模組](../dll-mods.md)。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。本文件會在遊戲更新時重新產生。

| 文件 | 型別 | 內容 |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`、`HorayModLocalizationContext` | 遊戲提供給模組的靜態進入點。 |
| [進入點與 Context](ModBase.md) | `WorkshopModBase`、`WorkshopModContext`、`HorayModBase`、`WorkshopDataPackage`、`WorkshopModDefinition`、`WorkshopModuleDefinition`、`WorkshopDependency`、`WorkshopModuleType`、`WorkshopInstallation` | 模組進入點類別所繼承的基底類別，以及載入器附加的 Context 與資訊清單型別。 |
| [模組選項](ModOptions.md) | `ModOptions`、`ModOptionsPage`、`ModOptionDefinition`、`ModOption<T>`、`ModToggleOption`、`ModSelectionOption`、`ModSliderOption`、`ModOptionText`、`ModOptionKind`、`ModOptionsRegistry` | 依裝置儲存的模組設定，以及在遊戲選項畫面新增開關、選擇方塊與滑桿的 API。 |
| [房間生成指派](RoomSpawn.md) | `RoomSpawnAssignContext`、`ERoomSpawnKind` | `HorayModAPI.OnAssignRoomSpawnPhases` 事件傳入的 Context。 |
| [專精屬性](SpecialStats.md) | `SpecialStatContext`、`SpecialStatValue`、`SpecialStatCategoryEntity`、`SpecialStatCategoryVisibility`、`SpecialStatDisplaySettings`、`SpecialStatValueSource`、`SpecialStatLabelColor`、`SpecialStatValueProvider` | 在 `HorayModAPI.OnLoadSpecialStatDatabase` 中以 `RegisterSpecialStatCategory` 與 `RegisterSpecialStat` 註冊的能力值顯示型別。 |
| [UI](UI.md) | `UIWidgetFactory`、`UIWidgetTextStyle`、`DialoguePage`、`DialogueChoice`、`DialogueSession`、`DialogueCloseReason`、`ItemInteractionOptions`、`ItemInteractionSession`、`ItemInteractionItem`、`ItemInteractionCloseReason`、`ItemInteractionItemSource` | `HorayModAPI.TryShowDialogue`、`TryShowItemInteraction` 與 `TryGetUIWidgets` 使用的對話、物品互動與小工具型別。 |

## HorayModAPI 事件一覽

依原始碼中宣告的順序列出。資料庫事件在每次載入世界時都會再次觸發。

| 事件 | 簽章 | 說明 |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | 在奇蹟資料庫初始化完成後立即觸發。 |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | 在武器資料庫初始化完成後立即觸發。 |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | 在狀態（能力值效果）資料庫初始化完成後立即觸發。 |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | 在專精屬性分類與狀態載入完成後立即觸發。 |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | 在物品資料庫初始化完成後立即觸發。 |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | 在關鍵字資料庫初始化完成後立即觸發。 |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | 在被動資料庫初始化完成後立即觸發。 |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | 在服裝資料庫初始化完成後立即觸發。 |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | 在道具資料庫初始化完成後立即觸發。 |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | 在單位資料庫（敵人與 NPC 生成實體）初始化完成後立即觸發。 |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | 在 NPC 角色資料庫初始化完成後立即觸發。 |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | 在自訂遊戲資料庫初始化完成後立即觸發。 |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | 在所有資料庫初始化完成後立即觸發。 |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | 在標題選單載入後呼叫。 |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | 在遊戲開始時呼叫。 |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | 在遊戲開始時呼叫。 |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | 在遊戲工作階段開始時呼叫。 |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | 在遊戲工作階段開始時呼叫。 |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | 在在地化檔案載入後呼叫。 |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | 在樓層被配置時呼叫。 |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | 在樓層被配置時呼叫。 |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | 在關卡的隨機敵人組合決定後呼叫。 |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | 在玩家生成時呼叫。 |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | 在玩家角色生成時呼叫。 |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | 在單位（玩家、敵人、召喚物或同伴）於伺服器生成時觸發。 |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | 在單位從伺服器移除之前立即觸發。 |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | 在玩家閃避攻擊時觸發。 |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | 讓你在負面狀態套用到單位之前立即替換其預製物件。 |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | 在伺服器取得背包寫入權限並開始移除所有物品效果時觸發。 |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | 在伺服器歸還寫入權限並重新套用物品效果後立即觸發。 |
