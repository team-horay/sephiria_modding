<!-- 自動生成ファイルです。ゲームリポジトリの Tools/ModApiDocGen がソースの /// コメントから生成します。直接編集せず、ソースのコメントを修正して再生成してください。 -->
# UI

`HorayModAPI.TryShowDialogue`、`TryShowItemInteraction`、`TryGetUIWidgets` が使うダイアログ、アイテムインタラクション、ウィジェットの型です。すべてローカルUIであり、ネットワーク同期はMOD側で行います。

ゲームバージョン **1.1.0** 向けに 2026-10-26 に生成。[リファレンス目次](README.md)

| 型 | 説明 |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | ゲーム風のUIウィジェットを生成します。 |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | テキストサイズの段階。 |
| [`DialoguePage`](#dialoguepage) | ダイアログの1ページ(本文と選択肢)。 |
| [`DialogueChoice`](#dialoguechoice) | 選択肢1つ。 |
| [`DialogueSession`](#dialoguesession) | 開いているダイアログ1つのハンドル。 |
| [`DialogueCloseReason`](#dialogueclosereason) | ダイアログが閉じた理由を示します。 |
| [`ItemInteractionOptions`](#iteminteractionoptions) | アイテムインタラクションパネルを開くときに渡す設定。 |
| [`ItemInteractionSession`](#iteminteractionsession) | 開いているアイテムインタラクションパネル1つのハンドル。 |
| [`ItemInteractionItem`](#iteminteractionitem) | 所持アイテム1つのスナップショット。 |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | アイテムインタラクションが閉じた理由を示します。 |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | アイテムがどこにあるかを示します。 |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

ゲーム風のUIウィジェットを生成します。

### コンストラクター

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

テンプレートからスタイルを取得します。

### メソッド

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

空の RectTransform の子を生成します。


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

アンカーを親全体に広げます。


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

ゲームのパネルの見た目を持つコンテナを生成します。


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

スプライトをネイティブのピクセルサイズで表示する画像を生成します。ポインター入力は通過します。


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

縦方向のレイアウトコンテナを生成します。


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

グリッドレイアウトのコンテナを生成します。


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

横方向のレイアウトコンテナを生成します。


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

縦スクロールビューを生成します。


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

ゲームのフォントを使ったテキストを生成します。


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

ローカライズキーを表示するテキストを生成します。


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

既存のテキストにゲームのフォントルールを適用します。


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

既存のテキストにローカライズキーをバインドします。


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

ゲーム風のボタンを生成します。


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

ラベルがローカライズキーのボタンを生成します。


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

ゲームのアイテムアイコンを生成します。


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

LayoutElement の推奨サイズを設定します。


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

テキストサイズの段階です。

| 値 | 説明 |
|---|---|
| `Body` | 本文 |
| `Small` | 小さいテキスト |
| `Title` | タイトル |


## DialoguePage

```csharp
public sealed class DialoguePage
```

ダイアログの1ページ(本文と選択肢)です。

### プロパティ

#### Text

```csharp
public string Text { get; }
```

*説明はまだありません。*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

選択肢のリスト。


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

ページを開いたときにフォーカスする選択肢。


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

ESC/キャンセルで閉じられるかどうか。

### コンストラクター

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

リテラルのテキストからページを生成します。


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

本文がローカライズキーのページを生成します。


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

選択肢1つです。選択すると onSelected が呼ばれ、次のページを表示するかダイアログを閉じます。

### プロパティ

#### Text

```csharp
public string Text { get; }
```

表示テキスト。ローカライズキーは読み取り時に翻訳されます。


#### Interactable

```csharp
public bool Interactable { get; }
```

選択できるかどうか。

### コンストラクター

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

リテラルのテキストから選択肢を生成します。


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

ローカライズキーから選択肢を生成します。


## DialogueSession

```csharp
public sealed class DialogueSession
```

開いているダイアログ1つのハンドルです。`Show` で次のページを表示し、`Close` で終了します。

### イベント

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

閉じた後に1回呼ばれます。

### プロパティ

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

現在表示中のページ。


#### PageVersion

```csharp
public int PageVersion { get; }
```

ページが変わるたびに増加します。


#### IsOpen

```csharp
public bool IsOpen { get; }
```

開いているかどうか。


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

選択待ちかどうか。


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

閉じた理由。開いている間は null。

### コンストラクター

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

最初のページを表示するセッション。通常は `HorayModAPI.TryShowDialogue` によって生成されます。

### メソッド

#### Show

```csharp
public bool Show(DialoguePage page)
```

次のページを表示します。セッションが閉じている場合は false。


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

ビューから選択入力を渡すために呼ばれます。


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

ダイアログを閉じます。


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

ダイアログが閉じた理由を示します。

| 値 | 説明 |
|---|---|
| `Completed` | 正常に完了 |
| `Cancelled` | ESC/キャンセル |
| `Interrupted` | フロアの変更やパネルの無効化などの外部からの中断 |
| `TargetUnavailable` | アンカーが消えた |
| `PlayerUnavailable` | プレイヤーがいない、死亡した、または戦闘が始まった |
| `Error` | コールバックの例外 |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

アイテムインタラクションパネルを開くときに渡す設定です。アイテムの検証、消費、ネットワーク処理は呼び出し側が行います。

### 定数とフィールド

#### Target

```csharp
public Transform Target
```

インタラクションの対象(必須)。


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

左側のコンテンツのプレハブ。これか `BuildContent` のどちらか一方だけを設定してください。


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

左側のコンテンツをコードで構築します。これか `ContentPrefab` のどちらか一方だけを設定してください。


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

選択可能なアイテムのフィルター。


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

アイテムクリックのコールバック。省略すると、左クリックで切り替え、右クリックで選択解除になります。


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

`Confirm` 時に呼ばれます。


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

閉じた後に呼ばれます。


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

選択できるアイテムの最大数。


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

既定の枠と余白を使うかどうか。コンテンツが独自に画像を描画する場合はオフにしてください。


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

開いているアイテムインタラクションパネル1つのハンドルです。閉じたとき、またはシーンが変わったときに無効になります。

### イベント

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

選択が変わった後に呼ばれます。


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

確定時に呼ばれます。


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

閉じた後に呼ばれます。


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

既定のフォーカスが変わったときにパネルが受け取ります。

### プロパティ

#### Player

```csharp
public PlayerAvatar Player { get; }
```

ローカルプレイヤー。


#### Target

```csharp
public Transform Target { get; }
```

インタラクションの対象。


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

ウィジェットファクトリ。


#### IsOpen

```csharp
public bool IsOpen { get; }
```

開いているかどうか。


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

選択中のアイテムのリスト。

### メソッド

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

アイテムが選択されているかどうか。


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

アイテムを選択できるかどうか。


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

アイテムを選択または選択解除します。


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

アイテムの選択を切り替えます。


#### ClearSelection

```csharp
public void ClearSelection()
```

すべてのアイテムの選択を解除します。


#### RefreshSelection

```csharp
public void RefreshSelection()
```

インベントリの同期後に、選択中のアイテムの位置と状態を再確認します。


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

アイテムの入力を処理します。パネルが開いている間、入力はインベントリの使用、移動、売却の操作には渡されません。


#### Confirm

```csharp
public void Confirm()
```

選択を確定します。`OnConfirm` と `Confirmed` を呼び出します。


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

キーボード/ゲームパッド用の既定のフォーカス対象を設定します。


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

パネルを閉じます。


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

所持アイテム1つのスナップショットです。サーバーで消費する際は、インスタンスIDで再度検索してください。

### プロパティ

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

アイテムメタデータのスナップショット。


#### Position

```csharp
public ItemPosition Position { get; }
```

グリッド上の位置。


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

アイテムのある場所。


#### Rotation

```csharp
public int Rotation { get; }
```

アイコンの回転。


#### InstanceID

```csharp
public int InstanceID { get; }
```

インスタンスID。


#### Entity

```csharp
public ItemEntity Entity { get; }
```

アイテムの定義。ない場合は null。

### コンストラクター

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

パネルによって生成されます。


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

アイテムインタラクションが閉じた理由を示します。

| 値 | 説明 |
|---|---|
| `Cancelled` | ESC/キャンセル |
| `TargetLost` | 対象が消えた |
| `PlayerLost` | プレイヤーがいない、死亡した、または切断した |
| `InventoryClosed` | インベントリが閉じられた |
| `Error` | コールバックの例外 |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

アイテムがどこにあるかを示します。

| 値 | 説明 |
|---|---|
| `Inventory` | グリッドインベントリ |
| `SubBag` | サブバッグ |
