# DLL 模組

DLL module 讓你能在遊戲中執行以 C# 撰寫的程式碼。訂閱 `HorayModAPI` 就能掛接到玩家、物品、UI 等各種內容。
本文件涵蓋從建立專案到讓第一個 DLL 在遊戲中執行的所有步驟。完整的事件與註冊函式清單請參閱 [API 參考](api/README.md)。


## 需求

- [.NET SDK](https://dotnet.microsoft.com/download) 8 或更新版本
- C# 編輯器


## 建立專案

請參考 [`examples/dll-sample`](../../examples/dll-sample) 範例。本文件以它為基準。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>PhysicalDamagePlus</AssemblyName>
    <RootNamespace>PhysicalDamagePlus</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- 遊戲安裝資料夾。請依你的環境修改，或以 dotnet build -p:GameDir=... 傳入 -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\physical-damage-plus\bin\</OutDir>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="Assembly-CSharp">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <Reference Include="UnityEngine.CoreModule">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\UnityEngine.CoreModule.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <!-- 若要處理 PlayerAvatar 等網路物件請加入 Mirror，製作 UI 請加入 UnityEngine.UI，使用修補請加入 0Harmony，寫法相同 -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **以 `netstandard2.1` 為目標**，並將語言版本設為 C# 9。
2. **參考遊戲 DLL，但不要複製它們。** 將參考指向 `<遊戲安裝資料夾>/Sephiria_Data/Managed/` 中的檔案，並全部設為 `Private=false`。如果遊戲 DLL 被複製到模組資料夾中，同一個組件會被載入兩次，造成難以理解的錯誤，也會無謂地增加審核的檔案。
3. `Assembly-CSharp.dll` 包含了執行遊戲的大部分程式碼。


## 程式碼結構


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // 每個處理程序只呼叫一次。在這裡訂閱事件，而且只訂閱一次。
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] Loaded v{metadata.modVersion} | folder={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // 只在房主（伺服器）上呼叫。在這裡更改遊戲狀態。
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // 在每個參與者上呼叫。伺服器加上的值會透過同步傳來。
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/PhysicalDamagePlus.dll", "entryClass": "PhysicalDamagePlus.Entry" }
]
```

### WorkshopModBase

你的類別會從基底類別繼承以下成員。

| 成員 | 說明 |
|---|---|
| `metadata` | `modName`、`modVersion`、`modAuthor`。以 `metadata.json` 中的值填入。 |
| `Context` | `PackageRoot`（模組資料夾的絕對路徑）、`FileId`（Steam 工作坊物品 ID；本機套件則是一個絕不會與 Steam ID 衝突的獨立值）、`Definition`（已驗證的資訊清單）、`ResolvePath(relativePath)`。用它來尋找模組資料夾內的檔案。每位使用者的安裝路徑都不同，所以絕對不要寫死。 |
| `GetDataPackages()` | 將此模組指定為 `consumer` 的 `data` module 套件清單。請在啟用完成後呼叫（在 `OnLoad*Database` 處理常式內，或 `OnAllDatabasesReady` 之後）。 |

`Context` 從 `OnModLoaded` 被呼叫的那一刻起才有效。在建構函式中它仍然是 null。


## 生命週期

DLL 模組會在**開始遊戲並進入世界時**啟用。在標題畫面上還不會執行任何程式碼。啟動順序如下。

1. 世界場景載入，`GameDataLoader` 啟動。
2. 載入計畫中的 DLL module 依序載入，每個進入點類別被實例化，並呼叫 `OnModLoaded`。
3. 隨著資料庫初始化，`OnLoadItemDatabase`、`OnLoadUnitDatabase` 等事件會依序觸發。
4. 全部完成後，呼叫 `OnAllDatabasesReady`。

## 訂閱

`HorayModAPI` 類別提供預先定義的事件，讓你能掛接到許多情境。

- 事件**只訂閱一次**，在 `OnModLoaded` 中訂閱。資料庫事件每次載入都會再次觸發，所以「每次都必須做」的事情（註冊物品等）請放在處理常式裡面。
- 請謹慎管理訂閱與取消訂閱。在 `OnModUnloaded` 中取消訂閱（-=）你訂閱過的事件。

完整的事件清單在 [API 參考](api/README.md)。下表列出各種需求建議使用的事件。

| 想做的事 | 事件 |
|---|---|
| 新增物品或神器 | `OnLoadItemDatabase` |
| 新增武器、奇蹟、被動、服裝或道具 | `OnLoadWeaponDatabase`、`OnLoadMiracleDatabase`、`OnLoadPassiveDatabase`、`OnLoadCostumeDatabase`、`OnLoadPropDatabase` |
| 新增敵人或 NPC 單位 | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| 新增旅人（村莊 NPC） | `OnLoadSocialIDDatabase` |
| 新增能力值顯示 | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| 從程式碼註冊在地化文字 | `OnLocalizationReady`（`AddText`） |
| 在標題選單新增按鈕 | `OnTitleMenuReady` |
| 遊戲或工作階段開始 | `OnStartGameServerside/Clientside`、`OnStartSessionServerside/Clientside` |
| 樓層產生時 | `OnFloorAllocatedServerside/Clientside`、`OnAssignRoomSpawnPhases` |
| 玩家或單位生成 | `OnPlayerAvatarSpawned*`、`OnUnitSpawnedServerside`、`OnUnitDespawnedServerside` |
| 掛接戰鬥判定 | `OnPlayerEvade`、`OnGetDebuff` |
| 背包操作權限 | `GridInventoryStartPermission`、`GridInventoryEndPermission` |


## 避免衝突

模組新增的實體以 `<modId>:<id>` 格式命名，例如 `yourname.artifact-pack:flame-ring`。這條規則可以防止與其他模組衝突，也是在遊玩紀錄與統計中區分模組內容的方式。


## 伺服器端與用戶端

- Sephiria 是以多人遊戲為核心設計的，因此許多事件都分成伺服器端（Serverside）與用戶端（Clientside）兩個版本。
- 房主兩者都會觸發；其他參與者只會觸發 Clientside 版本。
- 原則上，更改遊戲狀態的事情（生成、掉落、傷害）只在 Serverside 做，只更改顯示內容的事情在 Clientside 做。


## Harmony 修補

當你需要更改事件碰不到的內容時，可以使用 Harmony。
自 1.1.0 版起，遊戲**內建 Harmony 2.4.3**，只要加入參考即可。

```xml
<Reference Include="0Harmony">
  <HintPath>$(GameDir)\Sephiria_Data\Managed\0Harmony.dll</HintPath>
  <Private>false</Private>
</Reference>
```

```csharp
protected override void OnModLoaded()
{
    var harmony = new HarmonyLib.Harmony("yourname.first-mod");
    harmony.PatchAll();
}
```

- **不要把 `0Harmony.dll` 隨模組一起打包。** 打包的副本會被忽略，即使你放的是不同版本，模組也不會以該版本執行。
- 直接用你的 `modId` 作為 Harmony ID，可以避免與其他人衝突。
- 修補在遊戲更新時遠比官方 API 更容易壞掉。能用事件做到的就用事件，修補留作最後手段。
- 不支援 BepInEx、MelonLoader 等外部載入器。行為類似外部載入器的模組（也就是尋找並執行工作坊以外的外掛 DLL 的模組）會被加上警告，必須由使用者同意後才會啟用。詳情請參閱[執行工作坊以外程式碼的模組](#執行工作坊以外程式碼的模組)。


## 輔助 DLL

如果你的模組使用其他程式庫（例如 Newtonsoft.Json 以外的第三方套件），請將它們的 DLL 放進模組資料夾，並在 `metadata.json` 的 `libraries` 中宣告。未宣告的 DLL 不會用於解析參考。

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- 不要包含遊戲已經附帶的 DLL（Unity、Mirror、Newtonsoft.Json、0Harmony 等）。遊戲的副本會先載入，所以模組附帶的副本不是被忽略就是產生衝突。
- 無法使用原生（C/C++）DLL。只會載入受控組件（managed assembly）。
- 如果同名的組件已經被其他模組載入，對於 `modules` 中的 DLL，遊戲不會從兩者中擇一，而是**暫緩執行**它。對於 `libraries` 中的 DLL，遊戲會改用已載入的那個並忽略你附帶的副本，因此版本不同時可能出現異常。比較安全的做法是把共用程式庫拆成獨立的模組，再透過 `dependencies` 連結。

### 使用其他模組的程式庫

如果多個模組使用相同的程式碼，請把這些程式碼放進一個上層模組，並讓下層模組透過 `dependencies` 連結到它。

1. 上層模組在 `modules` 或 `libraries` 中宣告共用程式碼，並在 `tags` 中加入 `Library`。
2. 下層模組在 `dependencies` 中填寫上層模組的 `modId`，並以 `Private=false` 參考上層模組的 DLL 進行建置。**不要把上層模組的 DLL 複製到下層模組中。**
3. 遊戲會先載入上層模組，因此下層模組的參考會解析為已載入的上層模組的組件。

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- 下層模組仍須擁有自己的 `dll` 模組和 `entryClass`。不支援由上層模組自行尋找並載入下層模組 DLL 的結構。請讓下層模組在自己的 `OnModLoaded` 中呼叫上層模組的註冊函式。
- 如果上層模組在更新中更改了公開的型別或成員，下層模組可能會失效。共用程式庫請注意不要刪除或更改既有的公開 API。

### 不要在執行時產生 DLL！

模組執行的所有程式碼都必須包含在已通過審核的工作坊物品中。如果模組執行期間模組內的 DLL 檔案發生變化，該模組的載入會受到限制。請將所有 DLL 原封不動地放入工作坊物品，不要進行任何壓縮或串流。也就是說，以下做法可能違反審核系統。請在上傳時就將所需的所有 DLL 放進模組。

- 模組在執行時從外部下載 DLL 並載入組件
- 模組在執行時自行產生並執行 DLL（包括用 Mono.Cecil 等工具改寫其他 DLL 後再執行）
- 模組將以自有壓縮方式封裝在內部的 DLL 解壓縮後使用

### 執行工作坊以外程式碼的模組

如果模組會尋找並執行使用者放在**模組資料夾之外**（例如遊戲安裝資料夾或 `AppData`）的 DLL（例如相容其他載入器外掛的模組），不會被駁回，但審核系統會為它加上**警告**。

- 使用者啟用該模組時會跳出警告視窗，必須同意後才會啟用。模組更新後會再次詢問。
- 以這種方式執行的外部 DLL 不會經過審核，由此產生的問題不在支援範圍內。
- 模組在執行時自行下載這類 DLL，屬於上述禁止的情形。

如果要使用其他模組的程式碼，請不要直接從該模組的資料夾載入 DLL，而是像[使用其他模組的程式庫](#使用其他模組的程式庫)所述那樣透過 `dependencies` 連結。遊戲沒有親自載入的 DLL 不會經過載入順序、名稱衝突和相依性檢查，因此不受支援。

如果需要改寫 DLL（例如針對其他 Harmony API 進行 IL 轉換），請在上傳之前先完成轉換，並把轉換後的 DLL 放進模組上傳。


## 建置與測試

```bash
dotnet build -c Release
```

- 建置成功且 DLL 出現在 `WorkshopLocal/<模組>/bin/` 後，啟動遊戲，確認你的模組出現在標題畫面 Steam 工作坊面板的「我的模組」底下，然後開始遊戲。
- 若將 `.pdb` 與建置輸出放在一起，記錄檔中的堆疊追蹤會包含行號。發布時請移除。


## 遊戲更新與相容性

遊戲更新時，`Assembly-CSharp.dll` 會改變。只使用官方事件的模組通常能繼續運作，但直接碰觸內部類別或以 Harmony 修補的模組可能會壞掉。

- 更新後，請針對新的 `Managed/` 資料夾重新建置並測試。
- 若在工作坊頁面的說明中註明**支援的遊戲版本**，使用者就能快速找到原因。發布時，遊戲版本也可能會自動記錄在工作坊物品的中繼資料中。
- 如果更改了程式碼，必須再次提交至審核系統。請參閱[發布至工作坊](publishing.md#程式碼審核)。


## 多人遊戲注意事項

- 大多數程式碼模組的 `multiplayer.installation` 會設為 `everyone`。
- 如果你的模組只在 `Serverside` 事件中更改遊戲狀態，而且更改的值是已經會同步的遊戲狀態（例如 `customStats`），參與者不需要任何程式碼或註冊，就可以宣告為 `host_only`。這樣參與者就不會下載該模組。會註冊新物品、使用 `RegisterNetworkPrefab`，或在 `Clientside` 事件中更改任何內容的模組都不能設為 `host_only`。
- 透過網路生成的物件（單位、投射物等）必須用 `RegisterNetworkPrefab` 註冊，才會在參與者端也被建立。若傳入 `stableKey`，即使模組版本改變也會保留相同的 ID。
- 房主與參與者連模組的**載入順序**都必須相同。請避免結果取決於該順序的程式碼。


## 網路同步（SyncVar, RPC, Command）

Sephiria 的多人連線功能是以開源網路函式庫「Mirror」實作的。在 Mirror 中，只要宣告下列屬性（Attribute），就能輕鬆使用通訊功能。

- `[SyncVar]`：加在類別成員變數上的屬性。在伺服器與用戶端之間同步變數的值。
- `[ClientRpc]`、`[TargetRpc]`：加在函式上的屬性。由伺服器要求用戶端執行函式。
- `[Command]`：加在函式上的屬性。由用戶端要求伺服器執行函式。

在模組環境中，上述屬性無法正常使用。程式碼編譯時，必須由 Mirror 的 Weaver 為每個屬性插入對應的傳輸程式碼，它們才會運作；但外部產生的 DLL 不會經過 Weaver，所以即使建置成功，上述屬性也不會發揮任何作用。這類 bug 在房主的畫面上看起來一切正常，只有參與者的畫面才會出問題，因此只靠自己一個人測試很難發現。（即使強制讓 DLL 經過了 Weaver，也可能出現函式雜湊衝突等 bug。）


### `SyncVar<T>`、`ModRpc`

為了解決這個問題，Sephiria 提供了可取代上述屬性的模組專用通訊程式碼，也就是 `SyncVar<T>` 和 `ModRpc`。

| 想做的事 | 使用的東西 |
|---|---|
| 讓數值持續保持一致（包含較晚加入的參與者） | `SyncVar<T>` |
| 伺服器 → 所有用戶端呼叫 | `ModRpc.Send` |
| 伺服器 → 特定用戶端呼叫 | `ModRpc.SendTo` |
| 用戶端 → 伺服器呼叫 | `ModRpc.SendCommand` |
| 修改遊戲已經在同步的值 | `Network<FieldName>` 屬性 |

- 此功能從遊戲版本 1.1.2 起可以使用。
- 使用此功能的模組，請將 `multiplayer.installation` 宣告為 `everyone`。


### 準備元件

`SyncVar<T>` 和 `ModRpc` 只能在繼承自 `NetworkBehaviour` 的元件中使用。該元件必須在房主與參與者雙方的同一個物件上，以**相同的順序**掛載。對已經生成的物件以 `AddComponent` 加上元件，不會被納入同步範圍，所以必須在註冊為網路預製物件之前就已經掛載好。

```csharp
private static GameObject prefab;

// 在 OnModLoaded 中訂閱 HorayModAPI.OnAllDatabasesReady。
private static void RegisterPrefab()
{
    if (prefab) return;

    // 放在未啟用的父物件底下。預製物件的 Awake 不會執行，Instantiate 出來的複本則以啟用狀態開始。
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// 在伺服器上生成。傳入第二個參數後，該玩家就成為擁有者。
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`：同步數值

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // 沒有 Weaver，所以在建構函式中手動註冊。欄位順序必須始終相同。
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- 數值要**在伺服器上**修改，例如 `stack.Value = 3`。參與者修改的值不會傳給伺服器。
- 較晚加入的參與者在進入時也會收到目前的值。
- `Callback` 在房主上於賦值時呼叫，在參與者上於收到值時呼叫。進入時收到的值與預設值不同，此時也會呼叫。回呼中發生的例外會記錄到日誌，同步繼續進行。
- 可以使用 `int`、`float`、`string`、`Vector3` 等基本資料型別。模組自己定義的 struct 不會自動產生序列化程式碼，請拆成多個欄位來使用。
- 要指向其他物件時，不要用 `SyncVar<GameObject>`，而是把 `netId` 以 `SyncVar<uint>` 傳送。接收方可能還沒有生成該物件。
- 一個元件上最多可註冊的 SyncVar 數量，連同繼承而來的在內共 64 個。

### `ModRpc`：呼叫函式

載入模組時註冊一次函式。呼叫時使用的名稱是 `modId:functionName`。

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// 伺服器 → 所有看得到此物件的用戶端
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// 伺服器 → 一個用戶端。連線傳入 null 時，會傳送給物件的擁有者。
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// 用戶端 → 伺服器
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- 請依寫入的順序讀取。寫入與讀取的順序、資料型別不一致，會造成值錯位或擲出例外。
- `RegisterCommand` 預設只有**物件的擁有者**才能呼叫。如果要在沒有擁有者的物件上接收，請傳入 `requiresAuthority: false`。這種情況下任何參與者都可以呼叫，請注意作弊等風險。
- 如果往與註冊時不同的方向傳送，或傳送未註冊的名稱，只會留下錯誤日誌，不會傳送。
- 接收方沒有該函式或元件型別不同時，只會留下警告日誌並忽略。處理常式中發生的例外也會記錄到日誌，遊戲繼續執行。
- 預設通道是可靠傳輸。如果是頻繁傳送的演出效果，可以傳入 `channelId: Channels.Unreliable` 參數。

### 必須遵守的事項

- 用戶端可以透過 Command 傳送任意的值。請在伺服器處理常式中驗證欄位編號、數量、目標等。
- 較晚加入的參與者收不到進入之前的 RPC。需要保持的狀態請用 `SyncVar<T>` 傳送，RPC 只用於聲音、特效這類轉瞬即逝的事情。
- **RPC 可能比 SyncVar 先抵達。** 在 RPC 處理常式中讀取 `stack.Value`，可能得到舊值。如果需要新值，請把它當作 RPC 參數一併傳送，或在 `Callback` 中處理。

### 修改 Sephiria 本體程式碼中的數值

直接對遊戲類別的 `[SyncVar]` 欄位賦值，不會傳給參與者。請使用前面加了 `Network` 的屬性。大小寫與欄位名稱完全一致。

```csharp
unit.maxHp = 500;         // 只有房主的值會改變。
unit.NetworkmaxHp = 500;  // 參與者也會收到。
```

物件生成之前（預製物件或剛 `Instantiate` 之後），直接賦值也會在生成時一併傳遞。

### 測試

只有房主一個人時，上述問題不會暴露。發布前請用兩台電腦，同時檢查房主與參與者雙方的畫面，並且要測試修改數值之後再晚進入的情況。啟用本機套件（`WorkshopLocal`）後其他人無法進入房間，所以多人測試要訂閱以 Unlisted 上傳到工作坊的項目來進行。第一次上傳的項目會自動設為 Unlisted。


## 疑難排解

| 症狀 | 檢查項目 |
|---|---|
| 狀態摘要顯示找不到進入點類別 | `entryClass` 中的命名空間或類別名稱與程式碼不符。巢狀類別寫成 `Outer+Inner`。 |
| 狀態摘要顯示進入點類別型別錯誤 | 進入點類別沒有繼承 `WorkshopModBase`（或 `HorayModBase`）、不是 public、是 abstract，或沒有 public 的無參數建構函式。 |
| 狀態摘要顯示模組因組件名稱衝突而被暫緩 | 其他模組（包括 AddOns）使用了相同的 `AssemblyName`。請更改你的名稱。 |
| 狀態摘要顯示程式庫載入失敗 | `libraries` 中列出的 DLL 不存在，或不是受控組件。 |
| 記錄檔中出現 `FileNotFoundException` | 參考的 DLL 不在模組資料夾中，或未在 `libraries` 中宣告。或者遊戲 DLL 以 `Private=true` 被複製進來，兩者產生衝突。 |
| 進入世界後立刻發生例外 | 資料庫事件處理常式中有例外。請在記錄檔的堆疊追蹤中尋找你的模組命名空間。 |
| 回到標題再重新進入後效果疊加 | 你在處理常式內又訂閱了一次。只在 `OnModLoaded` 中訂閱一次。 |


## 範例

[`examples/dll-sample`](../../examples/dll-sample) 是一個最小的模組，會在玩家生成時將物理傷害提高 5。它的 `package/` 資料夾包含預先建置好的 DLL，所以把該資料夾複製到 `WorkshopLocal/` 就能直接運作，不需要建置。若要改成你自己的模組，只需要更改 `AssemblyName`、`modId`、`entryClass` 與輸出路徑。開始遊戲並開啟能力值視窗，就能看到「物理傷害 +5」正在生效。
