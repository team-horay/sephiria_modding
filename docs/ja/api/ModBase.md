<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# エントリーポイントとコンテキスト

MODのエントリークラスが継承する基底クラスと、ローダーが付与するコンテキスト・マニフェスト型です。新しいMODは `WorkshopModBase` を継承してください。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。[リファレンス目次](README.md)

| 型 | 説明 |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | ワークショップMODのエントリー基底クラス。 |
| [`WorkshopModContext`](#workshopmodcontext) | ワークショップのDLLモジュールに渡される読み取り専用コンテキスト。 |
| [`HorayModBase`](#horaymodbase) | DLL MODのエントリークラスの基底。 |
| [`WorkshopDataPackage`](#workshopdatapackage) | コンシューマーMODが読み取るデータモジュールのパッケージ1つ。 |
| [`WorkshopModDefinition`](#workshopmoddefinition) | 検証済みの実行仕様。 |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | マニフェストの `modules[]` のエントリ1つ。 |
| [`WorkshopDependency`](#workshopdependency) | マニフェストの `dependencies[]` のエントリ1つ。 |
| [`WorkshopModuleType`](#workshopmoduletype) | マニフェストの `modules[].type` の値。 |
| [`WorkshopInstallation`](#workshopinstallation) | 他の参加者がMODをインストールする必要があるかについての作者の宣言(マニフェストの `multiplayer.installation`)。 |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

ワークショップMODのエントリー基底クラスです。

### プロパティ

#### Context

```csharp
public WorkshopModContext Context { get; }
```

OnModLoaded が呼ばれた時点から有効です。

### メソッド

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

このMODをコンシューマーに指定しているデータモジュールのパッケージを、ロード順で返します。有効化の完了後(データベースイベントの後)に呼び出してください。コンテキストがない場合(AddOns 経由など)は空のリストを返します。


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

ワークショップのDLLモジュールに渡される読み取り専用コンテキストです。インストールパスは環境によって異なるため、MODはこれらの値を使って自分のファイルを探します。

### プロパティ

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

パッケージルート(インストールフォルダ)の絶対パス。


#### FileId

```csharp
public ulong FileId { get; }
```

SteamワークショップのファイルID。ローカルパッケージには、パスから導出された一意のIDが割り当てられます。


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

検証済みの実行仕様。


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

このインスタンスを生成した dll モジュールのエントリ。


#### Options

```csharp
public ModOptions Options { get; }
```

この modId のデバイスごとのオプション。パッケージのDLLモジュール間、およびローカルコピーと購読コピーの間で共有されます。

### コンストラクター

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*説明はまだありません。*

### メソッド

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

パッケージルートからの相対パスを絶対パスに変換します。ルートの外に出るパスや絶対パスは拒否されます(例外をスローします)。


## HorayModBase

```csharp
public class HorayModBase
```

DLL MODのエントリークラスの基底です。

### 定数とフィールド

#### metadata

```csharp
public Metadata metadata
```

`LoadMod` で受け取ったメタデータ。

### メソッド

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

ローダーから呼ばれます。メタデータを保存してから `OnModLoaded` を呼び出します。


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

MODの初期化ポイントです。データベースの初期化前に呼ばれるので、ここで `HorayModAPI` のイベントを購読してください。


#### UnloadMod

```csharp
public void UnloadMod()
```

`OnModUnloaded` を呼び出します。


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

MODのアンロードポイントです。イベントの購読解除などの後片付けをここで行ってください。

### ネストした型

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

ローダーが `LoadMod` に渡すMODの識別情報です。

##### 定数とフィールド

###### modName

```csharp
public string modName
```

表示名。


###### modVersion

```csharp
public string modVersion
```

バージョン。


###### modAuthor

```csharp
public string modAuthor
```

作者。


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

コンシューマーMODが読み取るデータモジュールのパッケージ1つです。`WorkshopModBase.GetDataPackages()` で取得します。

### プロパティ

#### ModId

```csharp
public string ModId { get; }
```

パッケージの modId。


#### Version

```csharp
public string Version { get; }
```

パッケージのバージョン。


#### FileId

```csharp
public ulong FileId { get; }
```

SteamワークショップのファイルID。


#### DisplayName

```csharp
public string DisplayName { get; }
```

表示名(info.title、なければ modId)。


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

パッケージルートの絶対パス。


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

このコンシューマーを対象とするデータモジュール(宣言順)。


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

モジュールフォルダの絶対パス(宣言順)。Modules[i].FullPath と同じです。

### メソッド

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

有効化レコード(ロード順)から、`consumerModId` 向けのデータモジュールを持つパッケージを選び出します。依存関係が満たされている状態(Active、AlreadyActive、NoDllModules)のものだけを含めます。失敗した、または部分的にしか適用されていないパッケージのデータは渡されません。


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

検証済みの実行仕様です。不変です。

### プロパティ

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

パッケージルートの絶対パス。


#### ModId

```csharp
public string ModId { get; }
```

マニフェストの modId。


#### Version

```csharp
public string Version { get; }
```

マニフェストの version。


#### Author

```csharp
public string Author { get; }
```

マニフェストの author。ない場合は null。


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title、なければ modId。


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

マルチプレイでのインストール要件。


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

multiplayer オブジェクトが明示的に宣言されたかどうか。false の場合、Installation には正規化された既定値が入ります。


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

モジュールのリスト(宣言順)。


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

補助マネージドDLLの絶対パス。これらは起動コールバックを受け取りません。


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

このMODが依存するMODのリスト。


#### HasDll

```csharp
public bool HasDll { get; }
```

modules に dll 型が含まれるかどうかから導出されます。別個のフラグはありません。


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

modules に localization 型が含まれるかどうか。


#### HasCostume

```csharp
public bool HasCostume { get; }
```

modules に costume 型が含まれるかどうか。


#### HasData

```csharp
public bool HasData { get; }
```

modules に data 型が含まれるかどうか。

### コンストラクター

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*説明はまだありません。*

### メソッド

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

`consumerModId` が受け取るデータモジュール(宣言順)。ない場合は空のリスト。コンシューマーを指定したモジュールはそのMODにだけ渡され、省略したモジュールはこのパッケージ自身と、これに直接依存するすべてのMODに渡されます。


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

マニフェストの `modules[]` のエントリ1つです。

### プロパティ

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

モジュールの種類。


#### RelativePath

```csharp
public string RelativePath { get; }
```

パッケージルートからの相対パス(区切り文字は '/' に正規化)。


#### FullPath

```csharp
public string FullPath { get; }
```

検証済みの絶対パス。


#### EntryClass

```csharp
public string EntryClass { get; }
```

dll モジュールのエントリークラスの完全名。localization の場合は null。


#### Consumer

```csharp
public string Consumer { get; }
```

データモジュールを読み取るMODの modId。省略した場合(null)、パッケージ自身と直接依存するすべてのMODが対象です。data 以外のモジュールでは常に null。

### コンストラクター

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

バリデーターによって生成されます。


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

バリデーターによって生成されます。


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

マニフェストの `dependencies[]` のエントリ1つです。

### プロパティ

#### ModId

```csharp
public string ModId { get; }
```

依存先MODの modId。


#### Version

```csharp
public string Version { get; }
```

null は任意のバージョンを意味します。設定した場合、完全一致のみ受け付けます(v1)。


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

ダウンロード元のヒント。インストール後に実際の modId/version と照合する必要があり、これだけではダウンロードしたMODが正しいものである証明にはなりません。

### コンストラクター

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

バリデーターによって生成されます。


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

マニフェストの `modules[].type` の値です。

| 値 | 説明 |
|---|---|
| `Dll` | マネージドDLLモジュール |
| `Localization` | ローカライズフォルダモジュール |
| `Costume` | コード不要のコスチュームパッケージフォルダ(costume.json/skin.json + PNG) |
| `Data` | 別のMOD(コンシューマー)が読み取る汎用データフォルダ。ゲームはその内容を解釈しません |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

他の参加者がMODをインストールする必要があるかについての作者の宣言です(マニフェストの `multiplayer.installation`)。

| 値 | 説明 |
|---|---|
| `Unknown` | 未宣言(Everyone と同様にロビー構成の比較に含まれる) |
| `LocalOnly` | ユーザー自身のデバイスでのみ動作する。ロビー構成の比較から除外される |
| `Everyone` | ルームの全員が同じパッケージをインストールする必要がある |
| `HostOnly` | ホストだけがインストールすればよい |
