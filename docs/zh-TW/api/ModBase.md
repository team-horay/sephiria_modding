<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# 進入點與 Context

模組進入點類別所繼承的基底類別，以及載入器附加的 Context 與資訊清單型別。新模組應繼承 `WorkshopModBase`。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。[參考索引](README.md)

| 型別 | 說明 |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | 工作坊模組的進入點基底。 |
| [`WorkshopModContext`](#workshopmodcontext) | 提供給工作坊 DLL module 的唯讀 Context。 |
| [`HorayModBase`](#horaymodbase) | DLL 模組進入點類別的基底。 |
| [`WorkshopDataPackage`](#workshopdatapackage) | 供 consumer 模組讀取的一組資料 module 套件。 |
| [`WorkshopModDefinition`](#workshopmoddefinition) | 已驗證的執行規格。 |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | 資訊清單 `modules[]` 中的一個項目。 |
| [`WorkshopDependency`](#workshopdependency) | 資訊清單 `dependencies[]` 中的一個項目。 |
| [`WorkshopModuleType`](#workshopmoduletype) | 資訊清單 `modules[].type` 的值。 |
| [`WorkshopInstallation`](#workshopinstallation) | 作者宣告其他參與者是否必須安裝此模組（資訊清單 `multiplayer.installation`）。 |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

工作坊模組的進入點基底。

### 屬性

#### Context

```csharp
public WorkshopModContext Context { get; }
```

從 OnModLoaded 被呼叫的那一刻起有效。

### 方法

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

將此模組指定為 consumer 的資料 module 套件，依載入順序排列。請在啟用完成後（資料庫事件之後）呼叫。若沒有 Context（例如 AddOns 路徑），回傳空清單。


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

提供給工作坊 DLL module 的唯讀 Context。安裝路徑可能不同，因此模組透過這些值來尋找自己的檔案。

### 屬性

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

套件根目錄（安裝資料夾）的絕對路徑。


#### FileId

```csharp
public ulong FileId { get; }
```

Steam 工作坊檔案 ID。本機套件會取得由其路徑衍生的唯一 ID。


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

已驗證的執行規格。


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

建立此實例的 dll module 項目。


#### Options

```csharp
public ModOptions Options { get; }
```

此 modId 的依裝置選項。由套件的各 DLL module 以及其本機與訂閱副本共用。

### 建構函式

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*尚無說明。*

### 方法

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

將相對於套件根目錄的路徑轉換為絕對路徑。逃出根目錄的路徑或絕對路徑會被拒絕（擲回例外）。


## HorayModBase

```csharp
public class HorayModBase
```

DLL 模組進入點類別的基底。

### 常數與欄位

#### metadata

```csharp
public Metadata metadata
```

在 `LoadMod` 中收到的中繼資料。

### 方法

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

由載入器呼叫。儲存中繼資料，然後呼叫 `OnModLoaded`。


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

模組的初始化點。它在資料庫初始化之前被呼叫，所以請在這裡訂閱 `HorayModAPI` 事件。


#### UnloadMod

```csharp
public void UnloadMod()
```

呼叫 `OnModUnloaded`。


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

模組的卸載點。取消訂閱事件等清理工作請放在這裡。

### 巢狀型別

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

載入器傳給 `LoadMod` 的模組識別資訊。

##### 常數與欄位

###### modName

```csharp
public string modName
```

顯示名稱。


###### modVersion

```csharp
public string modVersion
```

版本。


###### modAuthor

```csharp
public string modAuthor
```

作者。


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

供 consumer 模組讀取的一組資料 module 套件。透過 `WorkshopModBase.GetDataPackages()` 取得。

### 屬性

#### ModId

```csharp
public string ModId { get; }
```

套件的 modId。


#### Version

```csharp
public string Version { get; }
```

套件的版本。


#### FileId

```csharp
public ulong FileId { get; }
```

Steam 工作坊檔案 ID。


#### DisplayName

```csharp
public string DisplayName { get; }
```

顯示名稱（info.title，若沒有則為 modId）。


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

套件根目錄的絕對路徑。


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

以此 consumer 為目標的資料 module，依宣告順序排列。


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

module 資料夾的絕對路徑，依宣告順序排列。與 Modules[i].FullPath 相同。

### 方法

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

從啟用紀錄（依載入順序）中挑出含有 `consumerModId` 資料 module 的套件。只包含相依性已滿足的狀態（Active、AlreadyActive、NoDllModules）。失敗或部分套用的套件資料不會被傳遞。


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

已驗證的執行規格。不可變。

### 屬性

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

套件根目錄的絕對路徑。


#### ModId

```csharp
public string ModId { get; }
```

資訊清單的 modId。


#### Version

```csharp
public string Version { get; }
```

資訊清單的 version。


#### Author

```csharp
public string Author { get; }
```

資訊清單的 author。若沒有則為 null。


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title，若沒有則為 modId。


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

多人遊戲安裝需求。


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

multiplayer 物件是否有明確宣告。若為 false，Installation 持有正規化後的預設值。


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

module 清單，依宣告順序排列。


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

輔助受控 DLL 的絕對路徑。這些不會收到啟動回呼。


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

此模組所依賴的模組清單。


#### HasDll

```csharp
public bool HasDll { get; }
```

由 modules 是否含有 dll 型別推導而來。沒有獨立的旗標。


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

modules 是否含有 localization 型別。


#### HasCostume

```csharp
public bool HasCostume { get; }
```

modules 是否含有 costume 型別。


#### HasData

```csharp
public bool HasData { get; }
```

modules 是否含有 data 型別。

### 建構函式

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*尚無說明。*

### 方法

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

`consumerModId` 會收到的資料 module，依宣告順序排列。若沒有則為空清單。指定了 consumer 的 module 只會給該模組；未指定的 module 則給此套件本身以及每個直接依賴它的模組。


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

資訊清單 `modules[]` 中的一個項目。

### 屬性

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

module 型別。


#### RelativePath

```csharp
public string RelativePath { get; }
```

相對於套件根目錄的路徑（分隔符號正規化為 '/'）。


#### FullPath

```csharp
public string FullPath { get; }
```

已驗證的絕對路徑。


#### EntryClass

```csharp
public string EntryClass { get; }
```

dll module 進入點類別的完整名稱。localization 則為 null。


#### Consumer

```csharp
public string Consumer { get; }
```

讀取此資料 module 的模組 modId。若省略（null），則為套件本身加上每個直接依賴的模組。非 data module 一律為 null。

### 建構函式

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

由驗證器建立。


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

由驗證器建立。


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

資訊清單 `dependencies[]` 中的一個項目。

### 屬性

#### ModId

```csharp
public string ModId { get; }
```

所依賴模組的 modId。


#### Version

```csharp
public string Version { get; }
```

null 表示任何版本。若有設定，只接受完全一致的版本（v1）。


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

下載來源的提示。安裝後必須與實際的 modId/version 核對；它本身並不能證明下載的模組是正確的。

### 建構函式

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

由驗證器建立。


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

資訊清單 `modules[].type` 的值。

| 值 | 說明 |
|---|---|
| `Dll` | 受控 DLL module |
| `Localization` | 在地化資料夾 module |
| `Costume` | 不含程式碼的服裝套件資料夾（costume.json/skin.json + PNG） |
| `Data` | 由其他模組（consumer）讀取的通用資料資料夾；遊戲不會解讀其內容 |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

作者宣告其他參與者是否必須安裝此模組（資訊清單 `multiplayer.installation`）。

| 值 | 說明 |
|---|---|
| `Unknown` | 未宣告（在大廳設定比對中視同 Everyone） |
| `LocalOnly` | 只在使用者自己的裝置上運作；排除在大廳設定比對之外 |
| `Everyone` | 房間內的所有人都必須安裝相同的套件 |
| `HostOnly` | 只有房主需要安裝 |
