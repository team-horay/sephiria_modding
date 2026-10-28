<!-- Auto-generated file. Tools/ModApiDocGen in the game repo builds it from /// comments in the source. Don't edit it directly; fix the source comments and regenerate. -->
# UI

Dialogue, item interaction, and widget types used by `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction`, and `TryGetUIWidgets`. All of them are local UI; network sync is up to the mod.

Generated 2026-10-26 for game version **1.1.0**. [Reference index](README.md)

| Type | Description |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Creates game-styled UI widgets. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Text size levels. |
| [`DialoguePage`](#dialoguepage) | One page of dialogue (body text and choices). |
| [`DialogueChoice`](#dialoguechoice) | A single choice. |
| [`DialogueSession`](#dialoguesession) | Handle to one open dialogue. |
| [`DialogueCloseReason`](#dialogueclosereason) | Identifies why a dialogue closed. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Settings passed when opening an item interaction panel. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handle to one open item interaction panel. |
| [`ItemInteractionItem`](#iteminteractionitem) | Snapshot of one owned item. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Identifies why an item interaction closed. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Identifies where an item is located. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Creates game-styled UI widgets.

### Constructors

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Takes styles from the templates.

### Methods

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Creates an empty RectTransform child.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Stretches the anchors to fill the entire parent.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Creates a container with the game's panel look.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Creates an image that shows the sprite at its native pixel size. Pointer input passes through.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Creates a vertical layout container.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Creates a grid layout container.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Creates a horizontal layout container.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Creates a vertical scroll view.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Creates text using the game font.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Creates text that displays a localization key.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Applies the game's font rules to existing text.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Binds a localization key to existing text.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Creates a game-styled button.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Creates a button whose label is a localization key.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Creates a game item icon.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Sets the LayoutElement's preferred size.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Text size levels.

| Value | Description |
|---|---|
| `Body` | Body text |
| `Small` | Small text |
| `Title` | Title |


## DialoguePage

```csharp
public sealed class DialoguePage
```

One page of dialogue (body text and choices).

### Properties

#### Text

```csharp
public string Text { get; }
```

*No description yet.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

List of choices.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Choice to focus when the page opens.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Whether ESC/cancel can close it.

### Constructors

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Creates a page from literal text.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Creates a page whose body is a localization key.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

A single choice. Selecting it calls onSelected, which shows the next page or closes the dialogue.

### Properties

#### Text

```csharp
public string Text { get; }
```

Display text. Localization keys are translated when read.


#### Interactable

```csharp
public bool Interactable { get; }
```

Whether it can be selected.

### Constructors

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Creates a choice from literal text.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Creates a choice from a localization key.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handle to one open dialogue. `Show` shows the next page and `Close` ends it.

### Events

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Called once after closing.

### Properties

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Page currently shown.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Increments each time the page changes.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Whether it is open.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Whether it is waiting for a choice.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Close reason. null while open.

### Constructors

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

A session showing the first page. Usually created by `HorayModAPI.TryShowDialogue`.

### Methods

#### Show

```csharp
public bool Show(DialoguePage page)
```

Shows the next page. false if the session is closed.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Called by the view to pass on choice input.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Closes the dialogue.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Identifies why a dialogue closed.

| Value | Description |
|---|---|
| `Completed` | Completed normally |
| `Cancelled` | ESC/cancel |
| `Interrupted` | External interruption, such as a floor change or the panel being disabled |
| `TargetUnavailable` | The anchor is gone |
| `PlayerUnavailable` | No player, player died, or combat started |
| `Error` | Callback exception |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Settings passed when opening an item interaction panel. The caller handles item validation, consumption, and networking.

### Constants and Fields

#### Target

```csharp
public Transform Target
```

Interaction target (required).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab for the left-side content. Set either this or `BuildContent`, not both.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Builds the left-side content in code. Set either this or `ContentPrefab`, not both.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filter for selectable items.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Item click callback. If omitted, left-click toggles and right-click deselects.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Called on `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Called after closing.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Maximum number of selected items.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Whether to use the default border and padding. Turn it off if the content draws its own image.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handle to one open item interaction panel. Becomes invalid when closed or when the scene changes.

### Events

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Called after the selection changes.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Called on confirm.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Called after closing.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Received by the panel when the default focus changes.

### Properties

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Local player.


#### Target

```csharp
public Transform Target { get; }
```

Interaction target.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Widget factory.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Whether it is open.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

List of selected items.

### Methods

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Whether the item is selected.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Whether the item can be selected.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Selects or deselects an item.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Toggles an item's selection.


#### ClearSelection

```csharp
public void ClearSelection()
```

Deselects all items.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Rechecks the position and state of selected items after an inventory sync.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Handles item input. While the panel is open, input does not pass through to inventory use, move, or sell actions.


#### Confirm

```csharp
public void Confirm()
```

Confirms the selection. Calls `OnConfirm` and `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Sets the default focus target for keyboard/gamepad.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Closes the panel.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Snapshot of one owned item. When consuming it on the server, look it up again by instance ID.

### Properties

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Snapshot of the item metadata.


#### Position

```csharp
public ItemPosition Position { get; }
```

Grid position.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Where the item is located.


#### Rotation

```csharp
public int Rotation { get; }
```

Icon rotation.


#### InstanceID

```csharp
public int InstanceID { get; }
```

Instance ID.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Item definition. null if none.

### Constructors

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Created by the panel.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Identifies why an item interaction closed.

| Value | Description |
|---|---|
| `Cancelled` | ESC/cancel |
| `TargetLost` | The target is gone |
| `PlayerLost` | No player, player died, or disconnected |
| `InventoryClosed` | The inventory was closed |
| `Error` | Callback exception |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Identifies where an item is located.

| Value | Description |
|---|---|
| `Inventory` | Grid inventory |
| `SubBag` | Side Bag |
