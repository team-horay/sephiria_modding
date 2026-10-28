<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# APIリファレンス

DLL MODから利用できる公開型とメンバーの一覧です。プロジェクトの設定、エントリーポイント、ライフサイクル、マルチプレイのルールは [DLL MOD](../dll-mods.md) で扱っています。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。このドキュメントはゲームが更新されるたびに再生成されます。

| ドキュメント | 型 | 内容 |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | ゲームがMODに公開する静的なエントリーポイント。 |
| [エントリーポイントとコンテキスト](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | MODのエントリークラスが継承する基底クラスと、ローダーが付与するコンテキスト・マニフェスト型。 |
| [MODオプション](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | デバイスごとのMOD設定と、ゲームの設定画面にトグル、選択ボックス、スライダーを追加するAPI。 |
| [部屋のスポーン割り当て](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | `HorayModAPI.OnAssignRoomSpawnPhases` イベントが渡すコンテキスト。 |
| [専門ステータス](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | `HorayModAPI.OnLoadSpecialStatDatabase` で `RegisterSpecialStatCategory` と `RegisterSpecialStat` を使って登録するステータス表示の型。 |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | `HorayModAPI.TryShowDialogue`、`TryShowItemInteraction`、`TryGetUIWidgets` が使うダイアログ、アイテムインタラクション、ウィジェットの型。 |

## HorayModAPI イベント一覧

ソースで宣言されている順に並んでいます。データベースイベントはワールドがロードされるたびに再度発生します。

| イベント | シグネチャ | 説明 |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | 奇跡データベースの初期化直後に発生します。 |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | 武器データベースの初期化直後に発生します。 |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | ステータス(ステータス効果)データベースの初期化直後に発生します。 |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | 専門ステータスのカテゴリとステータスのロード直後に発生します。 |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | アイテムデータベースの初期化直後に発生します。 |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | キーワードデータベースの初期化直後に発生します。 |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | パッシブデータベースの初期化直後に発生します。 |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | コスチュームデータベースの初期化直後に発生します。 |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | プロップデータベースの初期化直後に発生します。 |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | ユニットデータベース(敵とNPCのスポーンエンティティ)の初期化直後に発生します。 |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | NPCロールデータベースの初期化直後に発生します。 |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | カスタムゲームデータベースの初期化直後に発生します。 |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | すべてのデータベースの初期化完了直後に発生します。 |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | タイトルメニューのロード後に呼ばれます。 |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | ゲーム開始時に呼ばれます。 |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | ゲーム開始時に呼ばれます。 |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | ゲームセッションの開始時に呼ばれます。 |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | ゲームセッションの開始時に呼ばれます。 |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | ローカライズファイルのロード後に呼ばれます。 |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | フロアが割り当てられたときに呼ばれます。 |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | フロアが割り当てられたときに呼ばれます。 |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | ステージのランダムな敵構成が決まった後に呼ばれます。 |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | プレイヤーがスポーンしたときに呼ばれます。 |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | プレイヤーアバターがスポーンしたときに呼ばれます。 |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | ユニット(プレイヤー、敵、召喚、仲間)がサーバーでスポーンしたときに発生します。 |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | ユニットがサーバーから削除される直前に発生します。 |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | プレイヤーが攻撃を回避したときに発生します。 |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | ユニットにデバフが適用される直前に、デバフのプレハブを差し替えられます。 |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | サーバーがインベントリの書き込み権限を取得し、すべてのアイテム効果の解除を始めたときに発生します。 |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | サーバーが書き込み権限を返し、アイテム効果を再適用した直後に発生します。 |
