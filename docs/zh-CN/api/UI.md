<!-- 自动生成的文件。由游戏仓库中的 Tools/ModApiDocGen 根据源码中的 /// 注释生成。请勿直接编辑；请修改源码注释后重新生成。 -->
# UI

`HorayModAPI.TryShowDialogue`、`TryShowItemInteraction` 和 `TryGetUIWidgets` 所使用的对话、物品交互和控件类型。它们全都是本地 UI；网络同步由模组自行负责。

生成于 2026-10-26，对应游戏版本 **1.1.0**。[参考索引](README.md)

| 类型 | 说明 |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | 创建游戏风格的 UI 控件。 |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | 文本大小级别。 |
| [`DialoguePage`](#dialoguepage) | 对话的一页（正文和选项）。 |
| [`DialogueChoice`](#dialoguechoice) | 一个选项。 |
| [`DialogueSession`](#dialoguesession) | 一个已打开对话的句柄。 |
| [`DialogueCloseReason`](#dialogueclosereason) | 标识对话关闭的原因。 |
| [`ItemInteractionOptions`](#iteminteractionoptions) | 打开物品交互面板时传入的设置。 |
| [`ItemInteractionSession`](#iteminteractionsession) | 一个已打开物品交互面板的句柄。 |
| [`ItemInteractionItem`](#iteminteractionitem) | 一件已持有物品的快照。 |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | 标识物品交互关闭的原因。 |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | 标识物品所在的位置。 |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

创建游戏风格的 UI 控件。

### 构造函数

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

从模板中获取样式。

### 方法

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

创建一个空的 RectTransform 子对象。


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

拉伸锚点以填满整个父对象。


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

创建一个具有游戏面板外观的容器。


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

创建一个以原始像素尺寸显示精灵的图片。指针输入会穿透。


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

创建一个垂直布局容器。


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

创建一个网格布局容器。


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

创建一个水平布局容器。


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

创建一个垂直滚动视图。


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

使用游戏字体创建文本。


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

创建显示本地化键的文本。


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

把游戏的字体规则应用到现有文本上。


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

把本地化键绑定到现有文本上。


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

创建一个游戏风格的按钮。


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

创建一个标签为本地化键的按钮。


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

创建一个游戏物品图标。


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

设置 LayoutElement 的首选尺寸。


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

文本大小级别。

| 值 | 说明 |
|---|---|
| `Body` | 正文 |
| `Small` | 小字 |
| `Title` | 标题 |


## DialoguePage

```csharp
public sealed class DialoguePage
```

对话的一页（正文和选项）。

### 属性

#### Text

```csharp
public string Text { get; }
```

*暂无说明。*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

选项列表。


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

页面打开时聚焦的选项。


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

是否可以用 ESC/取消关闭。

### 构造函数

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

由字面文本创建一页。


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

创建一页，其正文为本地化键。


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

一个选项。选中它会调用 onSelected，由其显示下一页或关闭对话。

### 属性

#### Text

```csharp
public string Text { get; }
```

显示文本。本地化键会在读取时被翻译。


#### Interactable

```csharp
public bool Interactable { get; }
```

是否可以选择。

### 构造函数

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

由字面文本创建一个选项。


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

由本地化键创建一个选项。


## DialogueSession

```csharp
public sealed class DialogueSession
```

一个已打开对话的句柄。`Show` 显示下一页，`Close` 结束对话。

### 事件

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

关闭后调用一次。

### 属性

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

当前显示的页面。


#### PageVersion

```csharp
public int PageVersion { get; }
```

每次页面变化时递增。


#### IsOpen

```csharp
public bool IsOpen { get; }
```

是否处于打开状态。


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

是否正在等待选择。


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

关闭原因。打开期间为 null。

### 构造函数

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

显示第一页的会话。通常由 `HorayModAPI.TryShowDialogue` 创建。

### 方法

#### Show

```csharp
public bool Show(DialoguePage page)
```

显示下一页。会话已关闭时为 false。


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

由视图调用，用于传递选择输入。


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

关闭对话。


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

标识对话关闭的原因。

| 值 | 说明 |
|---|---|
| `Completed` | 正常完成 |
| `Cancelled` | ESC/取消 |
| `Interrupted` | 外部中断，例如楼层切换或面板被禁用 |
| `TargetUnavailable` | 锚点已消失 |
| `PlayerUnavailable` | 没有玩家、玩家死亡或战斗开始 |
| `Error` | 回调异常 |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

打开物品交互面板时传入的设置。物品的验证、消耗和网络处理由调用方负责。

### 常量与字段

#### Target

```csharp
public Transform Target
```

交互目标（必需）。


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

左侧内容的预制体。此项与 `BuildContent` 二选一，不能同时设置。


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

用代码构建左侧内容。此项与 `ContentPrefab` 二选一，不能同时设置。


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

可选物品的过滤器。


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

物品点击回调。省略时，左键切换选择，右键取消选择。


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

`Confirm` 时调用。


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

关闭后调用。


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

可选物品的最大数量。


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

是否使用默认的边框和内边距。如果内容自行绘制图片，请关闭此项。


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

一个已打开物品交互面板的句柄。关闭或场景切换后失效。

### 事件

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

选择变化后调用。


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

确认时调用。


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

关闭后调用。


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

默认焦点变化时由面板接收。

### 属性

#### Player

```csharp
public PlayerAvatar Player { get; }
```

本地玩家。


#### Target

```csharp
public Transform Target { get; }
```

交互目标。


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

控件工厂。


#### IsOpen

```csharp
public bool IsOpen { get; }
```

是否处于打开状态。


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

已选物品的列表。

### 方法

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

该物品是否已被选中。


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

该物品是否可以选择。


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

选中或取消选中一个物品。


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

切换一个物品的选中状态。


#### ClearSelection

```csharp
public void ClearSelection()
```

取消选中所有物品。


#### RefreshSelection

```csharp
public void RefreshSelection()
```

在背包同步之后重新检查已选物品的位置和状态。


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

处理物品输入。面板打开期间，输入不会传递给背包的使用、移动或出售操作。


#### Confirm

```csharp
public void Confirm()
```

确认选择。调用 `OnConfirm` 和 `Confirmed`。


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

设置键盘/手柄的默认焦点目标。


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

关闭面板。


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

一件已持有物品的快照。在服务器上消耗它时，请通过实例 ID 重新查找。

### 属性

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

物品元数据的快照。


#### Position

```csharp
public ItemPosition Position { get; }
```

网格位置。


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

物品所在的位置。


#### Rotation

```csharp
public int Rotation { get; }
```

图标旋转。


#### InstanceID

```csharp
public int InstanceID { get; }
```

实例 ID。


#### Entity

```csharp
public ItemEntity Entity { get; }
```

物品定义。没有时为 null。

### 构造函数

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

由面板创建。


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

标识物品交互关闭的原因。

| 值 | 说明 |
|---|---|
| `Cancelled` | ESC/取消 |
| `TargetLost` | 目标已消失 |
| `PlayerLost` | 没有玩家、玩家死亡或断开连接 |
| `InventoryClosed` | 背包已关闭 |
| `Error` | 回调异常 |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

标识物品所在的位置。

| 值 | 说明 |
|---|---|
| `Inventory` | 网格背包 |
| `SubBag` | 副背包 |
