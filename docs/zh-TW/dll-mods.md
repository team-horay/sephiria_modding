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
- 不支援 BepInEx、MelonLoader 等外部載入器。


## 輔助 DLL

如果你的模組使用其他程式庫（例如 Newtonsoft.Json 以外的第三方套件），請將它們的 DLL 放進模組資料夾，並在 `metadata.json` 的 `libraries` 中宣告。未宣告的 DLL 不會用於解析參考。

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- 不要包含遊戲已經附帶的 DLL（Unity、Mirror、Newtonsoft.Json、0Harmony 等）。遊戲的副本會先載入，所以模組附帶的副本不是被忽略就是產生衝突。
- 無法使用原生（C/C++）DLL。只會載入受控組件（managed assembly）。
- 如果同名的組件已經被其他模組載入，遊戲不會從兩者中擇一，而是**暫緩執行**它。比較安全的做法是把共用程式庫拆成獨立的模組，再透過 `dependencies` 連結。


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
- 如果更改了程式碼，必須再次通過審核才能發布。請參閱[發布至工作坊](publishing.md)。


## 多人遊戲注意事項

- 大多數程式碼模組的 `multiplayer.installation` 會設為 `everyone`。
- 如果你的模組只在 `Serverside` 事件中更改遊戲狀態，而且更改的值是已經會同步的遊戲狀態（例如 `customStats`），參與者不需要任何程式碼或註冊，就可以宣告為 `host_only`。這樣參與者就不會下載該模組。會註冊新物品、使用 `RegisterNetworkPrefab`，或在 `Clientside` 事件中更改任何內容的模組都不能設為 `host_only`。
- 透過網路生成的物件（單位、投射物等）必須用 `RegisterNetworkPrefab` 註冊，才會在參與者端也被建立。若傳入 `stableKey`，即使模組版本改變也會保留相同的 ID。
- 房主與參與者連模組的**載入順序**都必須相同。請避免結果取決於該順序的程式碼。


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
