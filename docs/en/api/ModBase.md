<!-- Auto-generated file. Tools/ModApiDocGen in the game repo builds it from /// comments in the source. Don't edit it directly; fix the source comments and regenerate. -->
# Entry Point and Context

The base classes a mod's entry class inherits, plus the context and manifest types the loader attaches. New mods should inherit `WorkshopModBase`.

Generated 2026-10-26 for game version **1.1.0**. [Reference index](README.md)

| Type | Description |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | The entry base for Workshop mods. |
| [`WorkshopModContext`](#workshopmodcontext) | Read-only context given to a Workshop DLL module. |
| [`HorayModBase`](#horaymodbase) | The base for DLL mod entry classes. |
| [`WorkshopDataPackage`](#workshopdatapackage) | One package of data modules for a consumer mod to read. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | A validated execution spec. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | One entry in the manifest's `modules[]`. |
| [`WorkshopDependency`](#workshopdependency) | One entry in the manifest's `dependencies[]`. |
| [`WorkshopModuleType`](#workshopmoduletype) | A manifest `modules[].type` value. |
| [`WorkshopInstallation`](#workshopinstallation) | The author's declaration of whether other participants must install the mod (manifest `multiplayer.installation`). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

The entry base for Workshop mods.

### Properties

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Valid from the moment OnModLoaded is called.

### Methods

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Data module packages that name this mod as their consumer, in load order. Call after activation finishes (after the database events). Returns an empty list if there is no context (e.g. the AddOns path).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Read-only context given to a Workshop DLL module. The install path can vary, so mods use these values to find their own files.

### Properties

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absolute path of the package root (install folder).


#### FileId

```csharp
public ulong FileId { get; }
```

The Steam Workshop file ID. Local packages get a unique ID derived from their path.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

The validated execution spec.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

The dll module entry that created this instance.


#### Options

```csharp
public ModOptions Options { get; }
```

Per-device options for this modId. Shared by the package's DLL modules and by its local and subscribed copies.

### Constructors

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*No description yet.*

### Methods

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Converts a path relative to the package root into an absolute path. Paths that escape the root, or absolute paths, are rejected (throws).


## HorayModBase

```csharp
public class HorayModBase
```

The base for DLL mod entry classes.

### Constants and Fields

#### metadata

```csharp
public Metadata metadata
```

The metadata received in `LoadMod`.

### Methods

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Called by the loader. Stores the metadata, then calls `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

The mod's initialization point. It is called before the databases initialize, so subscribe to `HorayModAPI` events here.


#### UnloadMod

```csharp
public void UnloadMod()
```

Calls `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

The mod's unload point. Put cleanup such as unsubscribing from events here.

### Nested Types

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Mod identification info the loader passes to `LoadMod`.

##### Constants and Fields

###### modName

```csharp
public string modName
```

Display name.


###### modVersion

```csharp
public string modVersion
```

Version.


###### modAuthor

```csharp
public string modAuthor
```

Author.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

One package of data modules for a consumer mod to read. Get it with `WorkshopModBase.GetDataPackages()`.

### Properties

#### ModId

```csharp
public string ModId { get; }
```

The package's modId.


#### Version

```csharp
public string Version { get; }
```

The package's version.


#### FileId

```csharp
public ulong FileId { get; }
```

The Steam Workshop file ID.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Display name (info.title, or modId if absent).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absolute path of the package root.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Data modules that target this consumer, in declaration order.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Absolute paths of the module folders, in declaration order. Same as Modules[i].FullPath.

### Methods

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Picks, from the activation records (in load order), the packages that have data modules for `consumerModId`. Includes only states where dependencies are satisfied (Active, AlreadyActive, NoDllModules). Data from failed or partially applied packages is not passed on.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

A validated execution spec. Immutable.

### Properties

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absolute path of the package root.


#### ModId

```csharp
public string ModId { get; }
```

The manifest modId.


#### Version

```csharp
public string Version { get; }
```

The manifest version.


#### Author

```csharp
public string Author { get; }
```

The manifest author. null if absent.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, or modId if absent.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Multiplayer install requirement.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Whether the multiplayer object was explicitly declared. If false, Installation holds the normalized default.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Module list, in declaration order.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Absolute paths of auxiliary managed DLLs. These do not receive startup callbacks.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

List of mods this mod depends on.


#### HasDll

```csharp
public bool HasDll { get; }
```

Derived from whether modules contains a dll type. There is no separate flag.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Whether modules contains a localization type.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Whether modules contains a costume type.


#### HasData

```csharp
public bool HasData { get; }
```

Whether modules contains a data type.

### Constructors

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*No description yet.*

### Methods

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Data modules that `consumerModId` receives, in declaration order. Empty list if none. A module that names a consumer goes only to that mod; a module that omits it goes to this package itself and to every mod that directly depends on it.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

One entry in the manifest's `modules[]`.

### Properties

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Module type.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Path relative to the package root (separators normalized to '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Validated absolute path.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Full name of the dll module's entry class. null for localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId of the mod that reads the data module. If omitted (null), it is the package itself plus every directly dependent mod. Always null for non-data modules.

### Constructors

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Created by the validator.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Created by the validator.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

One entry in the manifest's `dependencies[]`.

### Properties

#### ModId

```csharp
public string ModId { get; }
```

modId of the mod depended on.


#### Version

```csharp
public string Version { get; }
```

null means any version. If set, only an exact match is accepted (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

A hint for where to download from. Must be checked against the actual modId/version after install; it does not by itself prove that the downloaded mod is the right one.

### Constructors

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Created by the validator.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

A manifest `modules[].type` value.

| Value | Description |
|---|---|
| `Dll` | Managed DLL module |
| `Localization` | Localization folder module |
| `Costume` | Code-free costume package folder (costume.json/skin.json + PNG) |
| `Data` | General-purpose data folder read by another mod (the consumer); the game does not interpret its contents |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

The author's declaration of whether other participants must install the mod (manifest `multiplayer.installation`).

| Value | Description |
|---|---|
| `Unknown` | Not declared (included in lobby setup comparison like Everyone) |
| `LocalOnly` | Works only on the user's own device; excluded from lobby setup comparison |
| `Everyone` | Everyone in the room must install the same package |
| `HostOnly` | Only the host needs to install it |
