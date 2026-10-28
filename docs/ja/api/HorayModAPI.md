<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# HorayModAPI

ゲームがMODに公開する静的なエントリーポイントです。データベースのロード、セッション、スポーンのイベントを購読し、ネットワークプレハブと専門ステータスを登録します。イベントの購読は `OnModLoaded` で1回だけ行ってください。ライフサイクルとマルチプレイのルールは [DLL MOD](../dll-mods.md) を参照してください。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。[リファレンス目次](README.md)

| 型 | 説明 |
|---|---|
| [`HorayModAPI`](#horaymodapi) | MODのエントリーポイント。 |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | MODのローカライズテキストを登録するためのヘルパー。 |


## HorayModAPI

```csharp
public static class HorayModAPI
```

MODのエントリーポイントです。

例:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### 定数とフィールド

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*説明はまだありません。*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*説明はまだありません。*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*説明はまだありません。*

### イベント

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

奇跡データベースの初期化直後に発生します。`MiracleDatabase.Register`/`Modify` で奇跡を登録または変更してください。


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

武器データベースの初期化直後に発生します。`WeaponDatabase.Register`/`Modify` で武器を登録または変更してください。


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

ステータス(ステータス効果)データベースの初期化直後に発生します。`StatusDatabase.Register`/`Modify` でステータスを登録または変更してください。


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

専門ステータスのカテゴリとステータスのロード直後に発生します。`RegisterSpecialStatCategory`/`RegisterSpecialStat` で表示エントリを登録してください。


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

アイテムデータベースの初期化直後に発生します。`ItemDatabase.Register`/`Modify` でアイテムを登録または変更してください。


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

キーワードデータベースの初期化直後に発生します。`KeywordDatabase.Register`/`Modify` でキーワードを登録または変更してください。


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

パッシブデータベースの初期化直後に発生します。`PassiveDatabase.Register`/`Modify` でパッシブを登録または変更してください。


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

コスチュームデータベースの初期化直後に発生します。`CostumeDatabase.Register`/`RegisterSkin`/`Modify` でコスチュームを登録または変更してください。


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

プロップデータベースの初期化直後に発生します。`PropDatabase.Register` でプロップを登録してください。


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

ユニットデータベース(敵とNPCのスポーンエンティティ)の初期化直後に発生します。


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

NPCロールデータベースの初期化直後に発生します。


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

カスタムゲームデータベースの初期化直後に発生します。


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

すべてのデータベースの初期化完了直後に発生します。


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

タイトルメニューのロード後に呼ばれます。


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

ゲーム開始時に呼ばれます。(サーバー側)開始とは、タイトル画面でセーブスロットをクリックしてゲームに入ることを指します。プレイヤーや現在地などのセッションデータはまだロードされていません。


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

ゲーム開始時に呼ばれます。(クライアント側)開始とは、タイトル画面でセーブスロットをクリックしてゲームに入ることを指します。プレイヤーや現在地などのセッションデータはまだロードされていません。


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

ゲームセッションの開始時に呼ばれます。(サーバー側)セッションとはプレイの単位です。プレイヤーが死亡するとそのセッションは終了し、新しいセッションが始まります。


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

ゲームセッションの開始時に呼ばれます。(クライアント側)セッションとはプレイの単位です。プレイヤーが死亡するとそのセッションは終了し、新しいセッションが始まります。


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

ローカライズファイルのロード後に呼ばれます。


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

フロアが割り当てられたときに呼ばれます。(サーバー側)Sephiriaのすべての場所はフロアとして定義されています。プレイヤーが場所の間を移動するとき、次の手順が順に実行されます。1. フロアの割り当て: 新しいフロアを生成してロードします。2. 移動: 完全にロードされたフロアの要求された地点にプレイヤーを移動します。3. フロアの削除: プレイヤーが以前いた場所が空(= プレイヤーが誰も残っていない)なら、その場所を削除します。
param1: フロアのGUID  
param2: フロア名  
param3: FloorGenerator のインスタンス


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

フロアが割り当てられたときに呼ばれます。(クライアント側)Sephiriaのすべての場所はフロアとして定義されています。プレイヤーが場所の間を移動するとき、次の手順が順に実行されます。1. フロアの割り当て: 新しいフロアを生成してロードします。2. 移動: 完全にロードされたフロアの要求された地点にプレイヤーを移動します。3. フロアの削除: プレイヤーが以前いた場所が空(= プレイヤーが誰も残っていない)なら、その場所を削除します。
param1: フロアのGUID  
param2: フロア名  
param3: FloorGenerator のインスタンス


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

ステージのランダムな敵構成が決まった後に呼ばれます。(サーバー側)フロアジェネレーターが戦闘部屋(またはバトルゾーン)のモンスターフェーズを選んだ直後、スポナーがそれを受け取る前に発生します。そこでスポーンする内容を変えるには ctx.Phases を差し替えるか編集してください。ゲームはここでバランスを強制しません。ルールは RoomSpawnAssignContext を参照してください(ctx.Seed による決定的なRNG、ctx.Sources には決して触れない、エンティティは UnitDatabase に登録されている必要がある)。


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

プレイヤーがスポーンしたときに呼ばれます。(サーバー側)このフックの中でプレイヤー関連のコールバックを登録できます。基本ステータスが適用された後、PlayerAvatar.OnStartServer の最後で発生します。コスチューム、フロア、インベントリの内容は、同じフレームの後半または後続のフレームで設定されることがあります。アバター自身のインスタンスイベント(OnDamagedServerside、OnAttackUnit、OnKillUnit、OnDie、OnAddedBuff、OnEnteredFloorServerside など)を購読するのに使ってください。アバターへの購読はアバターとともに消えるので、それらの購読解除は不要です。


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

プレイヤーアバターがスポーンしたときに呼ばれます。(クライアント側)アバターごとに1回、その OnStartClient の最後で発生します。リモートプレイヤーについても発生します。ローカルプレイヤーを区別するには avatar.isLocalPlayer / avatar.isOwned を確認してください。SyncVar はすでに適用されていますが、フロアとインベントリの内容は後から届くことがあります。


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

ユニット(プレイヤー、敵、召喚、仲間)がサーバーでスポーンしたときに発生します。サーバー(ホスト)のみ。


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

ユニットがサーバーから削除される直前に発生します。サーバー(ホスト)のみ。


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

プレイヤーが攻撃を回避したときに発生します。(サーバー側)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

ユニットにデバフが適用される直前に、デバフのプレハブを差し替えられます。ハンドラーは(現在のプレハブ, 付与者)を受け取り、差し替え用のプレハブを返します。null を返すと変更されません。


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

サーバーがインベントリの書き込み権限を取得し、すべてのアイテム効果の解除を始めたときに発生します。


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

サーバーが書き込み権限を返し、アイテム効果を再適用した直後に発生します。

### メソッド

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

ネットワーク経由でスポーンできるオブジェクト(プレハブ)を登録します。オブジェクトには NetworkIdentity コンポーネントが必要です。

サーバー(ホスト)とクライアントはそれぞれネットワークを使用できるオブジェクトのリストを保持しており、そのリストは全員で同一でなければなりません。

例:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

戻り値: 登録に成功した場合 true


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

実行時に生成したプレハブ(内蔵テンプレートのクローンなど)を、`stableKey` から計算した固定の assetId を使って、ネットワーク経由でスポーン可能なオブジェクトとして登録します。キーにはMODのユニット名(`<modId>:<id>`)を使ってください。

戻り値: 登録に成功した場合 true


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

複数のプレハブをネットワーク経由でスポーン可能なオブジェクトとして一度に登録します。

戻り値: 登録に成功したプレハブの数


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

登録済みのネットワークプレハブの登録を解除します。

戻り値: 削除された場合 true


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

ステータスUIに専門ステータスのカテゴリを登録します。

戻り値: 登録された場合 true


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

StatusEntity をステータスUIの専門ステータスカテゴリのエントリとして登録します。`reader` を渡すと、アセットの設定ではなくその関数を通じて値が読み取られます。

戻り値: 登録された場合 true


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

modId に紐づくデバイスごとのオプションを取得します。ワークショップMODは代わりに `Context.Options` を使えます。


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

インベントリと並べて使うアイテムインタラクションパネルを開きます。UIが使用中の場合は false を返します。


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

ゲーム風のUIウィジェットのファクトリを取得します。既存のUI Canvas の下に配置してください。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

ワールド内のダイアログボックスを開きます。UIが使用中の場合は false を返します。次のページは `session.Show` で表示します。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

ワールド空間のオフセットを指定してダイアログボックスを開きます。null の場合はプレハブの既定のオフセットが使われます。


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

ダイアログとカメラのオフセットを別々に指定してダイアログボックスを開きます。カメラのフォーカスはローカルでのみ適用されます。


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

MODのローカライズテキストを登録するためのヘルパーです。

### メソッド

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

特定の言語のテキストを登録します。


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

複数の言語のテキストを一度に登録します
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
