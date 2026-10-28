<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# HorayModAPI

遊戲提供給模組的靜態進入點。訂閱資料庫載入、工作階段與生成事件，並註冊網路預製物件與專精屬性。事件只在 `OnModLoaded` 中訂閱一次。生命週期與多人遊戲規則請參閱 [DLL 模組](../dll-mods.md)。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。[參考索引](README.md)

| 型別 | 說明 |
|---|---|
| [`HorayModAPI`](#horaymodapi) | 模組的進入點。 |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | 用於註冊模組在地化文字的輔助類別。 |


## HorayModAPI

```csharp
public static class HorayModAPI
```

模組的進入點。

範例：

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### 常數與欄位

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*尚無說明。*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*尚無說明。*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*尚無說明。*

### 事件

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

在奇蹟資料庫初始化完成後立即觸發。使用 `MiracleDatabase.Register`/`Modify` 註冊或修改奇蹟。


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

在武器資料庫初始化完成後立即觸發。使用 `WeaponDatabase.Register`/`Modify` 註冊或修改武器。


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

在狀態（能力值效果）資料庫初始化完成後立即觸發。使用 `StatusDatabase.Register`/`Modify` 註冊或修改狀態。


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

在專精屬性分類與狀態載入完成後立即觸發。使用 `RegisterSpecialStatCategory`/`RegisterSpecialStat` 註冊顯示項目。


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

在物品資料庫初始化完成後立即觸發。使用 `ItemDatabase.Register`/`Modify` 註冊或修改物品。


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

在關鍵字資料庫初始化完成後立即觸發。使用 `KeywordDatabase.Register`/`Modify` 註冊或修改關鍵字。


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

在被動資料庫初始化完成後立即觸發。使用 `PassiveDatabase.Register`/`Modify` 註冊或修改被動。


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

在服裝資料庫初始化完成後立即觸發。使用 `CostumeDatabase.Register`/`RegisterSkin`/`Modify` 註冊或修改服裝。


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

在道具資料庫初始化完成後立即觸發。使用 `PropDatabase.Register` 註冊道具。


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

在單位資料庫（敵人與 NPC 生成實體）初始化完成後立即觸發。


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

在 NPC 角色資料庫初始化完成後立即觸發。


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

在自訂遊戲資料庫初始化完成後立即觸發。


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

在所有資料庫初始化完成後立即觸發。


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

在標題選單載入後呼叫。


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

在遊戲開始時呼叫。（伺服器端）「開始」指的是在標題畫面點選存檔欄位進入遊戲。此時玩家與目前位置等工作階段資料尚未載入。


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

在遊戲開始時呼叫。（用戶端）「開始」指的是在標題畫面點選存檔欄位進入遊戲。此時玩家與目前位置等工作階段資料尚未載入。


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

在遊戲工作階段開始時呼叫。（伺服器端）工作階段是一次遊玩的單位。玩家死亡時，該工作階段結束並開始新的工作階段。


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

在遊戲工作階段開始時呼叫。（用戶端）工作階段是一次遊玩的單位。玩家死亡時，該工作階段結束並開始新的工作階段。


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

在在地化檔案載入後呼叫。


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

在樓層被配置時呼叫。（伺服器端）Sephiria 中的每個地點都定義為一個樓層。玩家在地點之間移動時，會依序執行以下步驟。1. 樓層配置：建立並載入新樓層。2. 移動：將玩家移動到已完全載入的樓層上的指定地點。3. 樓層移除：若玩家先前所在的地點已空（= 沒有玩家留在那裡），則移除它。
param1：樓層 GUID  
param2：樓層名稱  
param3：FloorGenerator 的實例


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

在樓層被配置時呼叫。（用戶端）Sephiria 中的每個地點都定義為一個樓層。玩家在地點之間移動時，會依序執行以下步驟。1. 樓層配置：建立並載入新樓層。2. 移動：將玩家移動到已完全載入的樓層上的指定地點。3. 樓層移除：若玩家先前所在的地點已空（= 沒有玩家留在那裡），則移除它。
param1：樓層 GUID  
param2：樓層名稱  
param3：FloorGenerator 的實例


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

在關卡的隨機敵人組合決定後呼叫。（伺服器端）在樓層產生器為戰鬥房間（或戰鬥區域）選定怪物階段之後、生成器收到它們之前立即觸發。替換或編輯 ctx.Phases 即可更改該處生成的內容。遊戲在此不會強制平衡，規則請參閱 RoomSpawnAssignContext（透過 ctx.Seed 使用確定性亂數、絕不碰 ctx.Sources、實體必須已在 UnitDatabase 中註冊）。


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

在玩家生成時呼叫。（伺服器端）你可以在這個掛鉤內註冊玩家相關的回呼。在 PlayerAvatar.OnStartServer 結尾、基礎能力值套用之後觸發。服裝、樓層與背包內容可能會在同一影格稍後或後續影格才填入。用它來訂閱該角色自身的實例事件（OnDamagedServerside、OnAttackUnit、OnKillUnit、OnDie、OnAddedBuff、OnEnteredFloorServerside 等）。角色上的訂閱會隨角色一起消失，所以不需要取消訂閱。


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

在玩家角色生成時呼叫。（用戶端）在每個角色的 OnStartClient 結尾觸發一次；遠端玩家也會觸發。透過 avatar.isLocalPlayer / avatar.isOwned 來分辨本機玩家。SyncVar 已套用，但樓層與背包內容可能稍後才會到達。


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

在單位（玩家、敵人、召喚物或同伴）於伺服器生成時觸發。僅限伺服器（房主）。


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

在單位從伺服器移除之前立即觸發。僅限伺服器（房主）。


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

在玩家閃避攻擊時觸發。（伺服器端）


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

讓你在負面狀態套用到單位之前立即替換其預製物件。處理常式會收到（目前的預製物件、施放者）並回傳替換用的預製物件；回傳 null 則維持不變。


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

在伺服器取得背包寫入權限並開始移除所有物品效果時觸發。


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

在伺服器歸還寫入權限並重新套用物品效果後立即觸發。

### 方法

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

註冊一個可透過網路生成的物件（預製物件）。該物件必須有 NetworkIdentity 元件。

伺服器（房主）與用戶端都必須各自持有可使用網路的物件清單，而且所有人的清單必須完全相同。

範例：

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

回傳：註冊成功時為 true


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

將執行期建立的預製物件（例如內建範本的複製品）註冊為可透過網路生成的物件，並使用由 `stableKey` 計算出的固定 assetId。請以模組單位名稱（`<modId>:<id>`）作為鍵。

回傳：註冊成功時為 true


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

一次將多個預製物件註冊為可透過網路生成的物件。

回傳：成功註冊的預製物件數量


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

取消註冊已註冊的網路預製物件。

回傳：已移除時為 true


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

在能力值 UI 中註冊一個專精屬性分類。

回傳：已註冊時為 true


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

將 StatusEntity 註冊為能力值 UI 中專精屬性分類的一個項目。若傳入 `reader`，值會透過該函式讀取，而不是使用資產設定。

回傳：已註冊時為 true


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

取得綁定到 modId 的依裝置選項。工作坊模組可以改用 `Context.Options`。


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

開啟與背包搭配使用的物品互動面板。若 UI 忙碌中則回傳 false。


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

取得遊戲風格 UI 小工具的工廠。請將它們附加在現有的 UI Canvas 之下。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

開啟世界對話框。若 UI 忙碌中則回傳 false；使用 `session.Show` 顯示下一頁。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

以世界空間偏移開啟對話框。若為 null，則使用預製物件的預設偏移。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

以分別設定的對話與攝影機偏移開啟對話框。攝影機聚焦只在本機生效。


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

用於註冊模組在地化文字的輔助類別。

### 方法

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

為特定語言註冊文字。


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

一次為多種語言註冊文字
texts：{ "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
