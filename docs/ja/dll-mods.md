# DLL MOD

DLLモジュールを使うと、C#で書いたコードをゲーム内で実行できます。`HorayModAPI` を購読して、プレイヤー、アイテム、UIなどにフックしましょう。
このドキュメントでは、プロジェクトの作成から最初のDLLをゲームで動かすところまでを扱います。イベントと登録関数の全リストは [APIリファレンス](api/README.md) を参照してください。


## 必要なもの

- [.NET SDK](https://dotnet.microsoft.com/download) 8 以降
- C#エディター


## プロジェクトの作成

[`examples/dll-sample`](../../examples/dll-sample) のサンプルを参考にしてください。このドキュメントでもこれを基準に説明します。

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
    <!-- ゲームのインストールフォルダ。環境に合わせて変更するか、dotnet build -p:GameDir=... で渡してください -->
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
    <!-- PlayerAvatar などのネットワークオブジェクトを扱うなら Mirror、UIを作るなら UnityEngine.UI、パッチを当てるなら 0Harmony を同じ方法で追加してください -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **`netstandard2.1` をターゲット**にし、言語バージョンを C# 9 に設定します。
2. **ゲームのDLLは参照するだけで、コピーしないでください。** 参照先を `<ゲームのインストールフォルダ>/Sephiria_Data/Managed/` 内のファイルに向け、すべて `Private=false` にします。ゲームのDLLがMODフォルダにコピーされると、同じアセンブリが2回ロードされて原因の分かりにくいエラーが起きるうえ、審査対象のファイルが無駄に増えます。
3. `Assembly-CSharp.dll` には、ゲームを動かすコードの大部分が含まれています。


## コードの構造


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // プロセスごとに1回呼ばれます。イベントの購読はここで、1回だけ行ってください。
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

        // ホスト(サーバー)でのみ呼ばれます。ゲームの状態の変更はここで行います。
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // すべての参加者で呼ばれます。サーバーが加算した値は同期を通じて届きます。
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

クラスは基底クラスから次のものを継承します。

| メンバー | 説明 |
|---|---|
| `metadata` | `modName`、`modVersion`、`modAuthor`。`metadata.json` の値が入ります。 |
| `Context` | `PackageRoot`(MODフォルダの絶対パス)、`FileId`(SteamワークショップアイテムID。ローカルパッケージの場合はSteamのIDと決して衝突しない別の値)、`Definition`(検証済みのマニフェスト)、`ResolvePath(relativePath)`。MODフォルダ内のファイルを探すのに使います。インストールパスはユーザーごとに異なるので、決してハードコードしないでください。 |
| `GetDataPackages()` | このMODを `consumer` に指定している `data` モジュールパッケージのリストです。有効化が完了した後(`OnLoad*Database` ハンドラーの中、または `OnAllDatabasesReady` の後)に呼び出してください。 |

`Context` は `OnModLoaded` が呼ばれた時点から有効です。コンストラクターの中ではまだnullです。


## ライフサイクル

DLL MODは**ゲームを開始してワールドに入ったとき**に有効化されます。タイトル画面ではまだコードは実行されません。起動時の順序は次のとおりです。

1. ワールドシーンがロードされ、`GameDataLoader` が開始します。
2. ロード計画内のDLLモジュールが順番にロードされ、各エントリークラスがインスタンス化されて `OnModLoaded` が呼ばれます。
3. データベースの初期化に伴い、`OnLoadItemDatabase`、`OnLoadUnitDatabase` などのイベントが次々と発生します。
4. すべて完了すると、`OnAllDatabasesReady` が呼ばれます。

## 購読

`HorayModAPI` クラスは、さまざまな状況にフックできる定義済みのイベントを提供します。

- イベントの購読は `OnModLoaded` で**1回だけ**行ってください。データベースイベントはロードのたびに再度発生するので、「毎回必ず行うべきこと」(アイテムの登録など)はハンドラーの中で行います。
- 購読と購読解除は慎重に管理してください。購読したイベントは `OnModUnloaded` で購読解除(-=)します。

イベントの全リストは [APIリファレンス](api/README.md) にあります。以下の表は、やりたいことごとに推奨されるイベントをまとめたものです。

| やりたいこと | イベント |
|---|---|
| アイテムやアーティファクトを追加する | `OnLoadItemDatabase` |
| 武器、奇跡、パッシブ、コスチューム、プロップを追加する | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| 敵やNPCユニットを追加する | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| 旅人(村のNPC)を追加する | `OnLoadSocialIDDatabase` |
| ステータス表示を追加する | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| コードからローカライズテキストを登録する | `OnLocalizationReady` (`AddText`) |
| タイトルメニューにボタンを追加する | `OnTitleMenuReady` |
| ゲームやセッションの開始 | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| フロアが生成されたとき | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| プレイヤーやユニットのスポーン | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| 戦闘判定にフックする | `OnPlayerEvade`, `OnGetDebuff` |
| インベントリ操作の権限 | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## 衝突を避ける

MODが追加するエンティティは `<modId>:<id>` の形式で命名されます(例: `yourname.artifact-pack:flame-ring`)。このルールにより他のMODとの衝突が防がれ、探検記録や統計でMODのコンテンツを区別できるようになります。


## サーバー側、クライアント側

- Sephiriaはマルチプレイを前提に作られているため、多くのイベントにサーバー側とクライアント側の別々のバージョンがあります。
- ホストでは両方が発生し、他の参加者ではClientsideだけが発生します。
- 原則として、ゲームの状態を変えること(スポーン、ドロップ、ダメージ)はServersideでのみ行い、表示だけを変えることはClientsideで行ってください。


## Harmonyパッチ

イベントでは届かない部分を変更する必要がある場合は、Harmonyを使えます。
パッチ1.1.0以降、ゲームに **Harmony 2.4.3 が内蔵**されているので、参照を追加するだけで済みます。

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

- **`0Harmony.dll` をMODに同梱しないでください。** 同梱したコピーは無視され、別のバージョンを入れたとしても、MODはそのバージョンでは動作しません。
- HarmonyのIDに `modId` をそのまま使えば、他と衝突しません。
- パッチは公式APIよりもゲームの更新でずっと壊れやすいです。できることはイベントで済ませ、パッチは最後の手段にしてください。
- BepInExやMelonLoaderなどの外部ローダーには対応していません。


## 補助DLL

MODが他のライブラリ(Newtonsoft.Json以外のサードパーティパッケージなど)を使う場合は、そのDLLをMODフォルダに置き、`metadata.json` の `libraries` で宣言します。宣言されていないDLLは参照解決に使われません。

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- ゲームにすでに同梱されているDLL(Unity、Mirror、Newtonsoft.Json、0Harmonyなど)は含めないでください。ゲーム側のコピーが先にロードされるため、MODに含めたコピーは無視されるか衝突します。
- ネイティブ(C/C++)DLLは使えません。マネージドアセンブリだけがロードされます。
- 同じ名前のアセンブリがすでに別のMODによってロードされている場合、ゲームはどちらか一方を選ぶのではなく、**実行を保留**します。共有ライブラリは別のMODに分けて、`dependencies` で結び付けるほうが安全です。

### 実行中にDLLを作らないでください!

MODの実行中にMOD内のDLLファイルが変わると、そのMODのロードが制限されます。すべてのDLLは、圧縮やストリーミングなどを一切せず、そのままワークショップアイテムに含めてください。つまり、次のようなものは審査システムに違反する可能性があります。必要なDLLはすべて、アップロードする時点でMODに入れておいてください。

- 実行中に外部からDLLをダウンロードしてアセンブリをロードするMOD
- 実行中に独自のDLLを作成して実行するMOD
- 内部に独自の圧縮方式で格納したDLLを展開して使うMOD


## ビルドとテスト

```bash
dotnet build -c Release
```

- ビルドが成功して `WorkshopLocal/<mod>/bin/` にDLLが現れたら、ゲームを起動し、タイトル画面のSteamワークショップパネルの「自分のMOD」にMODが表示されることを確認してから、ゲームを開始します。
- ビルド出力と一緒に `.pdb` を置いておくと、ログのスタックトレースに行番号が含まれます。公開時には外してください。


## ゲームの更新と互換性

ゲームが更新されると、`Assembly-CSharp.dll` が変わります。公式イベントだけを使うMODは通常そのまま動作しますが、内部クラスに直接触れるMODやHarmonyでパッチを当てるMODは壊れることがあります。

- 更新後は、新しい `Managed/` フォルダに対して再ビルドしてテストしてください。
- ワークショップページの説明に**対応しているゲームバージョン**を書いておくと、ユーザーが原因をすぐに特定できます。公開時には、ゲームバージョンがワークショップアイテムのメタデータに自動で記録されることもあります。
- コードを変更した場合は、再度審査システムに申請する必要があります。[ワークショップへの公開](publishing.md#コード審査) を参照してください。


## マルチプレイでの注意点

- ほとんどのコードMODは `multiplayer.installation` を `everyone` にすることになるでしょう。
- MODが `Serverside` イベントでのみゲームの状態を変更し、変更する値がすでに同期されているゲームの状態(`customStats` など)で、参加者側にコードや登録が不要な場合は、`host_only` と宣言できます。その場合、参加者はMODをダウンロードしません。新しいアイテムを登録する、`RegisterNetworkPrefab` を使う、`Clientside` イベントで何かを変更するMODは `host_only` にできません。
- ネットワーク経由でスポーンされるオブジェクト(ユニット、弾など)は、参加者側でも生成されるように `RegisterNetworkPrefab` で登録する必要があります。`stableKey` を渡すと、MODのバージョンが変わっても同じIDが維持されます。
- ホストと参加者は、MODの**ロード順**まで同じである必要があります。結果がその順序に依存するコードは避けてください。


## トラブルシューティング

| 症状 | 確認すること |
|---|---|
| 状態の要約に、エントリークラスが見つからないと表示される | `entryClass` の名前空間またはクラス名がコードと一致していません。ネストしたクラスは `Outer+Inner` と書きます。 |
| 状態の要約に、エントリークラスの型エラーが表示される | エントリークラスが `WorkshopModBase`(または `HorayModBase`)を継承していない、publicでない、abstractである、またはpublicな引数なしコンストラクターがありません。 |
| 状態の要約に、アセンブリ名の衝突でMODが保留されたと表示される | 別のMOD(AddOnsを含む)が同じ `AssemblyName` を使っています。自分のものを変更してください。 |
| 状態の要約に、ライブラリのロード失敗が表示される | `libraries` に挙げたDLLが存在しないか、マネージドアセンブリではありません。 |
| ログに `FileNotFoundException` が出る | 参照しているDLLがMODフォルダにないか、`libraries` で宣言されていません。または、ゲームのDLLが `Private=true` でコピーされて両者が衝突しています。 |
| ワールドに入った直後に例外が出る | データベースイベントのハンドラー内の例外です。ログのスタックトレースでMODの名前空間を探してください。 |
| タイトルに戻って再度入ると効果が重複する | ハンドラーの中で再度購読しています。購読は `OnModLoaded` で1回だけ行ってください。 |


## サンプル

[`examples/dll-sample`](../../examples/dll-sample) は、プレイヤーがスポーンしたときに物理ダメージを5上げる最小限のMODです。`package/` フォルダにはビルド済みのDLLが含まれているので、このフォルダを `WorkshopLocal/` にコピーすれば、ビルドなしですぐに動作します。自分のMODにするには、`AssemblyName`、`modId`、`entryClass`、出力パスを変更するだけです。ゲームを開始してステータスウィンドウを開くと、物理ダメージ +5 として動作していることが確認できます。
