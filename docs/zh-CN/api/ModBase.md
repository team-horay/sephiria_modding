<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# 入口点与上下文

模组入口类所继承的基类，以及加载器附加的上下文和清单类型。新模组应继承 `WorkshopModBase`。

生成于 2026-10-26，对应游戏版本 **1.1.0**。[参考索引](README.md)

| 类型 | 说明 |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | 创意工坊模组的入口基类。 |
| [`WorkshopModContext`](#workshopmodcontext) | 提供给创意工坊 DLL 模块的只读上下文。 |
| [`HorayModBase`](#horaymodbase) | DLL 模组入口类的基类。 |
| [`WorkshopDataPackage`](#workshopdatapackage) | 供消费方模组读取的一个数据模块包。 |
| [`WorkshopModDefinition`](#workshopmoddefinition) | 经过验证的执行规格。 |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | 清单 `modules[]` 中的一个条目。 |
| [`WorkshopDependency`](#workshopdependency) | 清单 `dependencies[]` 中的一个条目。 |
| [`WorkshopModuleType`](#workshopmoduletype) | 清单 `modules[].type` 的值。 |
| [`WorkshopInstallation`](#workshopinstallation) | 作者对其他参与者是否必须安装该模组的声明（清单 `multiplayer.installation`）。 |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

创意工坊模组的入口基类。

### 属性

#### Context

```csharp
public WorkshopModContext Context { get; }
```

从 OnModLoaded 被调用的那一刻起有效。

### 方法

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

把本模组指定为消费方的数据模块包，按加载顺序排列。请在激活完成后（数据库事件之后）调用。没有上下文时（例如 AddOns 路径）返回空列表。


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

提供给创意工坊 DLL 模块的只读上下文。安装路径可能各不相同，所以模组通过这些值来查找自己的文件。

### 属性

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

包根目录（安装文件夹）的绝对路径。


#### FileId

```csharp
public ulong FileId { get; }
```

Steam 创意工坊文件 ID。本地包会获得一个由其路径派生的唯一 ID。


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

经过验证的执行规格。


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

创建此实例的 dll 模块条目。


#### Options

```csharp
public ModOptions Options { get; }
```

此 modId 的按设备保存的选项。由包内的各个 DLL 模块以及其本地副本和订阅副本共享。

### 构造函数

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*暂无说明。*

### 方法

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

把相对于包根目录的路径转换为绝对路径。跳出根目录的路径或绝对路径会被拒绝（抛出异常）。


## HorayModBase

```csharp
public class HorayModBase
```

DLL 模组入口类的基类。

### 常量与字段

#### metadata

```csharp
public Metadata metadata
```

在 `LoadMod` 中接收到的元数据。

### 方法

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

由加载器调用。保存元数据，然后调用 `OnModLoaded`。


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

模组的初始化点。它在数据库初始化之前被调用，所以请在这里订阅 `HorayModAPI` 事件。


#### UnloadMod

```csharp
public void UnloadMod()
```

调用 `OnModUnloaded`。


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

模组的卸载点。取消订阅事件等清理工作请放在这里。

### 嵌套类型

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

加载器传给 `LoadMod` 的模组识别信息。

##### 常量与字段

###### modName

```csharp
public string modName
```

显示名称。


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

供消费方模组读取的一个数据模块包。通过 `WorkshopModBase.GetDataPackages()` 获取。

### 属性

#### ModId

```csharp
public string ModId { get; }
```

该包的 modId。


#### Version

```csharp
public string Version { get; }
```

该包的版本。


#### FileId

```csharp
public ulong FileId { get; }
```

Steam 创意工坊文件 ID。


#### DisplayName

```csharp
public string DisplayName { get; }
```

显示名称（info.title，若缺失则为 modId）。


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

包根目录的绝对路径。


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

面向此消费方的数据模块，按声明顺序排列。


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

模块文件夹的绝对路径，按声明顺序排列。等同于 Modules[i].FullPath。

### 方法

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

从激活记录（按加载顺序）中挑出含有面向 `consumerModId` 的数据模块的包。只包含依赖已满足的状态（Active、AlreadyActive、NoDllModules）。失败或仅部分应用的包的数据不会被传递。


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

经过验证的执行规格。不可变。

### 属性

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

包根目录的绝对路径。


#### ModId

```csharp
public string ModId { get; }
```

清单中的 modId。


#### Version

```csharp
public string Version { get; }
```

清单中的 version。


#### Author

```csharp
public string Author { get; }
```

清单中的 author。缺失时为 null。


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title，若缺失则为 modId。


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

多人游戏安装要求。


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

是否显式声明了 multiplayer 对象。为 false 时，Installation 保存的是规范化后的默认值。


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

模块列表，按声明顺序排列。


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

辅助托管 DLL 的绝对路径。这些 DLL 不会收到启动回调。


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

此模组所依赖的模组列表。


#### HasDll

```csharp
public bool HasDll { get; }
```

由 modules 中是否包含 dll 类型推导得出。没有单独的标志。


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

modules 中是否包含 localization 类型。


#### HasCostume

```csharp
public bool HasCostume { get; }
```

modules 中是否包含 costume 类型。


#### HasData

```csharp
public bool HasData { get; }
```

modules 中是否包含 data 类型。

### 构造函数

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*暂无说明。*

### 方法

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

`consumerModId` 会接收到的数据模块，按声明顺序排列。没有时为空列表。指定了消费方的模块只提供给该模组；省略消费方的模块提供给此包本身以及每一个直接依赖它的模组。


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

清单 `modules[]` 中的一个条目。

### 属性

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

模块类型。


#### RelativePath

```csharp
public string RelativePath { get; }
```

相对于包根目录的路径（分隔符已规范化为 '/'）。


#### FullPath

```csharp
public string FullPath { get; }
```

经过验证的绝对路径。


#### EntryClass

```csharp
public string EntryClass { get; }
```

dll 模块入口类的完整名称。localization 模块为 null。


#### Consumer

```csharp
public string Consumer { get; }
```

读取该数据模块的模组的 modId。省略（null）时，为此包本身加上每一个直接依赖它的模组。非 data 模块始终为 null。

### 构造函数

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

由验证器创建。


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

由验证器创建。


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

清单 `dependencies[]` 中的一个条目。

### 属性

#### ModId

```csharp
public string ModId { get; }
```

所依赖模组的 modId。


#### Version

```csharp
public string Version { get; }
```

null 表示任意版本。若设置了值，则只接受精确匹配（v1）。


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

下载来源的提示。安装后必须与实际的 modId/version 核对；它本身并不能证明下载的模组就是正确的那个。

### 构造函数

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

由验证器创建。


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

清单 `modules[].type` 的值。

| 值 | 说明 |
|---|---|
| `Dll` | 托管 DLL 模块 |
| `Localization` | 本地化文件夹模块 |
| `Costume` | 无需代码的服装包文件夹（costume.json/skin.json + PNG） |
| `Data` | 由另一个模组（消费方）读取的通用数据文件夹；游戏不解释其内容 |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

作者对其他参与者是否必须安装该模组的声明（清单 `multiplayer.installation`）。

| 值 | 说明 |
|---|---|
| `Unknown` | 未声明（在大厅配置比较中按 Everyone 处理） |
| `LocalOnly` | 仅在用户自己的设备上生效；不参与大厅配置比较 |
| `Everyone` | 房间内所有人都必须安装相同的包 |
| `HostOnly` | 只需主机安装 |
