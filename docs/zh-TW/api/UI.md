<!-- 自動產生的檔案。由遊戲儲存庫中的 Tools/ModApiDocGen 從原始碼的 /// 註解產生。請勿直接編輯；請修正原始碼註解後重新產生。 -->
# UI

`HorayModAPI.TryShowDialogue`、`TryShowItemInteraction` 與 `TryGetUIWidgets` 使用的對話、物品互動與小工具型別。它們全都是本機 UI；網路同步由模組自行負責。

於 2026-10-26 針對遊戲版本 **1.1.0** 產生。[參考索引](README.md)

| 型別 | 說明 |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | 建立遊戲風格的 UI 小工具。 |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | 文字大小等級。 |
| [`DialoguePage`](#dialoguepage) | 一頁對話（內文與選項）。 |
| [`DialogueChoice`](#dialoguechoice) | 單一選項。 |
| [`DialogueSession`](#dialoguesession) | 一個開啟中對話的控制代碼。 |
| [`DialogueCloseReason`](#dialogueclosereason) | 指出對話關閉的原因。 |
| [`ItemInteractionOptions`](#iteminteractionoptions) | 開啟物品互動面板時傳入的設定。 |
| [`ItemInteractionSession`](#iteminteractionsession) | 一個開啟中物品互動面板的控制代碼。 |
| [`ItemInteractionItem`](#iteminteractionitem) | 一個持有物品的快照。 |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | 指出物品互動關閉的原因。 |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | 指出物品所在的位置。 |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

建立遊戲風格的 UI 小工具。

### 建構函式

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

從範本取得樣式。

### 方法

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

建立一個空的 RectTransform 子物件。


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

將錨點拉伸至填滿整個父物件。


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

建立一個具有遊戲面板外觀的容器。


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

建立一個以原生像素大小顯示精靈圖的圖片。指標輸入會穿透。


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

建立一個垂直排版容器。


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

建立一個格線排版容器。


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

建立一個水平排版容器。


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

建立一個垂直捲動檢視。


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

使用遊戲字型建立文字。


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

建立顯示在地化鍵的文字。


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

將遊戲的字型規則套用到現有文字。


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

將在地化鍵綁定到現有文字。


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

建立遊戲風格的按鈕。


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

建立標籤為在地化鍵的按鈕。


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

建立遊戲物品圖示。


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

設定 LayoutElement 的偏好大小。


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

文字大小等級。

| 值 | 說明 |
|---|---|
| `Body` | 內文 |
| `Small` | 小字 |
| `Title` | 標題 |


## DialoguePage

```csharp
public sealed class DialoguePage
```

一頁對話（內文與選項）。

### 屬性

#### Text

```csharp
public string Text { get; }
```

*尚無說明。*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

選項清單。


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

頁面開啟時要聚焦的選項。


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

是否可用 ESC／取消關閉。

### 建構函式

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

從字面文字建立頁面。


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

建立內文為在地化鍵的頁面。


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

單一選項。選取時會呼叫 onSelected，由它顯示下一頁或關閉對話。

### 屬性

#### Text

```csharp
public string Text { get; }
```

顯示文字。在地化鍵會在讀取時翻譯。


#### Interactable

```csharp
public bool Interactable { get; }
```

是否可選取。

### 建構函式

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

從字面文字建立選項。


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

從在地化鍵建立選項。


## DialogueSession

```csharp
public sealed class DialogueSession
```

一個開啟中對話的控制代碼。`Show` 顯示下一頁，`Close` 結束對話。

### 事件

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

關閉後呼叫一次。

### 屬性

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

目前顯示的頁面。


#### PageVersion

```csharp
public int PageVersion { get; }
```

每次頁面變更時遞增。


#### IsOpen

```csharp
public bool IsOpen { get; }
```

是否開啟中。


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

是否正在等待選擇。


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

關閉原因。開啟中為 null。

### 建構函式

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

顯示第一頁的工作階段。通常由 `HorayModAPI.TryShowDialogue` 建立。

### 方法

#### Show

```csharp
public bool Show(DialoguePage page)
```

顯示下一頁。工作階段已關閉時為 false。


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

由檢視呼叫，用來傳遞選擇輸入。


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

關閉對話。


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

指出對話關閉的原因。

| 值 | 說明 |
|---|---|
| `Completed` | 正常完成 |
| `Cancelled` | ESC／取消 |
| `Interrupted` | 外部中斷，例如樓層變更或面板被停用 |
| `TargetUnavailable` | 錨點已消失 |
| `PlayerUnavailable` | 沒有玩家、玩家死亡或戰鬥開始 |
| `Error` | 回呼例外 |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

開啟物品互動面板時傳入的設定。物品驗證、消耗與網路處理由呼叫端負責。

### 常數與欄位

#### Target

```csharp
public Transform Target
```

互動目標（必要）。


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

左側內容的預製物件。請設定此項或 `BuildContent` 其中之一，不可兩者都設。


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

以程式碼建構左側內容。請設定此項或 `ContentPrefab` 其中之一，不可兩者都設。


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

可選取物品的篩選器。


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

物品點擊回呼。若省略，左鍵切換、右鍵取消選取。


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

在 `Confirm` 時呼叫。


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

關閉後呼叫。


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

可選取物品的最大數量。


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

是否使用預設邊框與內距。若內容會自行繪製圖片，請關閉。


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

一個開啟中物品互動面板的控制代碼。關閉或場景變更時失效。

### 事件

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

選取變更後呼叫。


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

確認時呼叫。


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

關閉後呼叫。


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

預設焦點變更時由面板接收。

### 屬性

#### Player

```csharp
public PlayerAvatar Player { get; }
```

本機玩家。


#### Target

```csharp
public Transform Target { get; }
```

互動目標。


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

小工具工廠。


#### IsOpen

```csharp
public bool IsOpen { get; }
```

是否開啟中。


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

已選取物品的清單。

### 方法

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

物品是否已選取。


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

物品是否可選取。


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

選取或取消選取物品。


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

切換物品的選取狀態。


#### ClearSelection

```csharp
public void ClearSelection()
```

取消選取所有物品。


#### RefreshSelection

```csharp
public void RefreshSelection()
```

在背包同步後重新檢查已選取物品的位置與狀態。


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

處理物品輸入。面板開啟期間，輸入不會穿透到背包的使用、移動或販賣動作。


#### Confirm

```csharp
public void Confirm()
```

確認選取。呼叫 `OnConfirm` 與 `Confirmed`。


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

設定鍵盤／手把的預設焦點目標。


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

關閉面板。


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

一個持有物品的快照。在伺服器上消耗它時，請以實例 ID 重新查詢。

### 屬性

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

物品中繼資料的快照。


#### Position

```csharp
public ItemPosition Position { get; }
```

格線位置。


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

物品所在的位置。


#### Rotation

```csharp
public int Rotation { get; }
```

圖示旋轉。


#### InstanceID

```csharp
public int InstanceID { get; }
```

實例 ID。


#### Entity

```csharp
public ItemEntity Entity { get; }
```

物品定義。沒有時為 null。

### 建構函式

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

由面板建立。


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

指出物品互動關閉的原因。

| 值 | 說明 |
|---|---|
| `Cancelled` | ESC／取消 |
| `TargetLost` | 目標已消失 |
| `PlayerLost` | 沒有玩家、玩家死亡或已斷線 |
| `InventoryClosed` | 背包已關閉 |
| `Error` | 回呼例外 |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

指出物品所在的位置。

| 值 | 說明 |
|---|---|
| `Inventory` | 格線背包 |
| `SubBag` | 副背包 |
