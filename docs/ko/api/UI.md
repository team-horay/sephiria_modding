<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# UI

`HorayModAPI.TryShowDialogue`, `TryShowItemInteraction`, `TryGetUIWidgets`가 쓰는 대화·아이템 상호작용·위젯 타입입니다. 전부 로컬 UI이며 네트워크 동기화는 모드의 몫입니다.

게임 버전 **1.0.34** 기준, 2026-10-22 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Scene-local game UI widgets. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) |  |
| [`DialoguePage`](#dialoguepage) |  |
| [`DialogueChoice`](#dialoguechoice) |  |
| [`DialogueSession`](#dialoguesession) | One local dialogue. |
| [`DialogueCloseReason`](#dialogueclosereason) |  |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Client UI configuration. |
| [`ItemInteractionSession`](#iteminteractionsession) | One opening of the shared panel. |
| [`ItemInteractionItem`](#iteminteractionitem) | A snapshot of one owned item. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) |  |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) |  |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Scene-local game UI widgets. Widgets belong to their parent; do not cache across scenes.

### 생성자

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

*설명이 아직 없습니다.*

### 메서드

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

*설명이 아직 없습니다.*


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

*설명이 아직 없습니다.*


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

*설명이 아직 없습니다.*


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Displays sprite art at its source pixel size, without intercepting pointer input.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

*설명이 아직 없습니다.*


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

*설명이 아직 없습니다.*


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

*설명이 아직 없습니다.*


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

*설명이 아직 없습니다.*


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

*설명이 아직 없습니다.*


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

*설명이 아직 없습니다.*


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

*설명이 아직 없습니다.*


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

*설명이 아직 없습니다.*


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

*설명이 아직 없습니다.*


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

*설명이 아직 없습니다.*


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

*설명이 아직 없습니다.*


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

*설명이 아직 없습니다.*


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Body` |  |
| `Small` |  |
| `Title` |  |


## DialoguePage

```csharp
public sealed class DialoguePage
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Text

```csharp
public string Text { get; }
```

Localized text is resolved when read, not when the page is constructed.


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

*설명이 아직 없습니다.*


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

*설명이 아직 없습니다.*


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

*설명이 아직 없습니다.*

### 생성자

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

*설명이 아직 없습니다.*


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

*설명이 아직 없습니다.*


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Text

```csharp
public string Text { get; }
```

Localized text is resolved when read, not when the choice is constructed.


#### Interactable

```csharp
public bool Interactable { get; }
```

*설명이 아직 없습니다.*

### 생성자

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

*설명이 아직 없습니다.*


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

*설명이 아직 없습니다.*


## DialogueSession

```csharp
public sealed class DialogueSession
```

One local dialogue. Keep this handle for delayed replies; Show returns false after it closes. Gameplay rewards and network requests belong to the caller, not the dialogue.

### 이벤트

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

*설명이 아직 없습니다.*

### 프로퍼티

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

*설명이 아직 없습니다.*


#### PageVersion

```csharp
public int PageVersion { get; }
```

*설명이 아직 없습니다.*


#### IsOpen

```csharp
public bool IsOpen { get; }
```

*설명이 아직 없습니다.*


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

*설명이 아직 없습니다.*


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

*설명이 아직 없습니다.*

### 생성자

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

*설명이 아직 없습니다.*

### 메서드

#### Show

```csharp
public bool Show(DialoguePage page)
```

*설명이 아직 없습니다.*


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

*설명이 아직 없습니다.*


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

*설명이 아직 없습니다.*


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Completed` |  |
| `Cancelled` |  |
| `Interrupted` |  |
| `TargetUnavailable` |  |
| `PlayerUnavailable` |  |
| `Error` |  |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Client UI configuration. Gameplay validation, item consumption and networking belong to the caller.

### 상수와 필드

#### Target

```csharp
public Transform Target
```

*설명이 아직 없습니다.*


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

*설명이 아직 없습니다.*


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

*설명이 아직 없습니다.*


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

*설명이 아직 없습니다.*


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

*설명이 아직 없습니다.*


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

*설명이 아직 없습니다.*


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

*설명이 아직 없습니다.*


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

*설명이 아직 없습니다.*


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Disable the standard frame and padding when the content draws its own artwork.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

One opening of the shared panel. Handles are invalid after closing or changing scenes.

### 이벤트

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

*설명이 아직 없습니다.*


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

*설명이 아직 없습니다.*


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

*설명이 아직 없습니다.*


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

*설명이 아직 없습니다.*

### 프로퍼티

#### Player

```csharp
public PlayerAvatar Player { get; }
```

*설명이 아직 없습니다.*


#### Target

```csharp
public Transform Target { get; }
```

*설명이 아직 없습니다.*


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

*설명이 아직 없습니다.*


#### IsOpen

```csharp
public bool IsOpen { get; }
```

*설명이 아직 없습니다.*


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

*설명이 아직 없습니다.*

### 메서드

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

*설명이 아직 없습니다.*


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

*설명이 아직 없습니다.*


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

*설명이 아직 없습니다.*


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

*설명이 아직 없습니다.*


#### ClearSelection

```csharp
public void ClearSelection()
```

*설명이 아직 없습니다.*


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Re-resolves identity and location after inventory synchronization, including subbag moves.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Consumes input while open. It never falls through to item use, movement or sale.


#### Confirm

```csharp
public void Confirm()
```

*설명이 아직 없습니다.*


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

*설명이 아직 없습니다.*


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

*설명이 아직 없습니다.*


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

A snapshot of one owned item. Resolve again before consuming items on the server.

### 프로퍼티

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

*설명이 아직 없습니다.*


#### Position

```csharp
public ItemPosition Position { get; }
```

*설명이 아직 없습니다.*


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

*설명이 아직 없습니다.*


#### Rotation

```csharp
public int Rotation { get; }
```

*설명이 아직 없습니다.*


#### InstanceID

```csharp
public int InstanceID { get; }
```

*설명이 아직 없습니다.*


#### Entity

```csharp
public ItemEntity Entity { get; }
```

*설명이 아직 없습니다.*

### 생성자

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

*설명이 아직 없습니다.*


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Cancelled` |  |
| `TargetLost` |  |
| `PlayerLost` |  |
| `InventoryClosed` |  |
| `Error` |  |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

*설명이 아직 없습니다.*

| 값 | 설명 |
|---|---|
| `Inventory` |  |
| `SubBag` |  |
