<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте его напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# UI

Типы диалогов, взаимодействия с предметами и виджетов, используемые `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` и `TryGetUIWidgets`. Всё это локальный UI; сетевая синхронизация остаётся на усмотрение мода.

Сгенерировано 2026-10-26 для версии игры **1.1.0**. [Оглавление справочника](README.md)

| Тип | Описание |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Создаёт UI-виджеты в стиле игры. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Уровни размера текста. |
| [`DialoguePage`](#dialoguepage) | Одна страница диалога (основной текст и варианты выбора). |
| [`DialogueChoice`](#dialoguechoice) | Один вариант выбора. |
| [`DialogueSession`](#dialoguesession) | Дескриптор одного открытого диалога. |
| [`DialogueCloseReason`](#dialogueclosereason) | Определяет, почему диалог закрылся. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Параметры, передаваемые при открытии панели взаимодействия с предметами. |
| [`ItemInteractionSession`](#iteminteractionsession) | Дескриптор одной открытой панели взаимодействия с предметами. |
| [`ItemInteractionItem`](#iteminteractionitem) | Снимок одного предмета в собственности игрока. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Определяет, почему взаимодействие с предметами закрылось. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Определяет, где находится предмет. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Создаёт UI-виджеты в стиле игры.

### Конструкторы

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Берёт стили из шаблонов.

### Методы

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Создаёт пустой дочерний RectTransform.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Растягивает якоря на всю площадь родителя.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Создаёт контейнер с внешним видом игровой панели.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Создаёт изображение, показывающее спрайт в его исходном пиксельном размере. Ввод указателя проходит сквозь него.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Создаёт контейнер с вертикальной компоновкой.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Создаёт контейнер с сеточной компоновкой.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Создаёт контейнер с горизонтальной компоновкой.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Создаёт вертикальную прокручиваемую область.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Создаёт текст игровым шрифтом.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Создаёт текст, отображающий ключ локализации.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Применяет правила игрового шрифта к существующему тексту.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Привязывает ключ локализации к существующему тексту.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Создаёт кнопку в стиле игры.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Создаёт кнопку, надпись которой задана ключом локализации.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Создаёт иконку игрового предмета.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Задаёт предпочтительный размер LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Уровни размера текста.

| Значение | Описание |
|---|---|
| `Body` | Основной текст |
| `Small` | Мелкий текст |
| `Title` | Заголовок |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Одна страница диалога (основной текст и варианты выбора).

### Свойства

#### Text

```csharp
public string Text { get; }
```

*Описания пока нет.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Список вариантов выбора.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Вариант, на который ставится фокус при открытии страницы.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Можно ли закрыть через ESC/отмену.

### Конструкторы

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Создаёт страницу из литерального текста.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Создаёт страницу, основной текст которой задан ключом локализации.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Один вариант выбора. При его выборе вызывается onSelected, который показывает следующую страницу или закрывает диалог.

### Свойства

#### Text

```csharp
public string Text { get; }
```

Отображаемый текст. Ключи локализации переводятся при чтении.


#### Interactable

```csharp
public bool Interactable { get; }
```

Можно ли его выбрать.

### Конструкторы

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Создаёт вариант из литерального текста.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Создаёт вариант из ключа локализации.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Дескриптор одного открытого диалога. `Show` показывает следующую страницу, а `Close` завершает диалог.

### События

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Вызывается один раз после закрытия.

### Свойства

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Отображаемая сейчас страница.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Увеличивается при каждой смене страницы.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Открыт ли диалог.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Ожидает ли диалог выбора.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Причина закрытия. null, пока диалог открыт.

### Конструкторы

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Сессия, показывающая первую страницу. Обычно создаётся через `HorayModAPI.TryShowDialogue`.

### Методы

#### Show

```csharp
public bool Show(DialoguePage page)
```

Показывает следующую страницу. false, если сессия закрыта.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Вызывается представлением для передачи ввода выбора.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Закрывает диалог.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Определяет, почему диалог закрылся.

| Значение | Описание |
|---|---|
| `Completed` | Завершён нормально |
| `Cancelled` | ESC/отмена |
| `Interrupted` | Внешнее прерывание, например смена этажа или отключение панели |
| `TargetUnavailable` | Якорь исчез |
| `PlayerUnavailable` | Нет игрока, игрок погиб или начался бой |
| `Error` | Исключение в колбэке |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Параметры, передаваемые при открытии панели взаимодействия с предметами. Проверка, расходование предметов и сетевое взаимодействие остаются на вызывающей стороне.

### Константы и поля

#### Target

```csharp
public Transform Target
```

Цель взаимодействия (обязательно).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Префаб содержимого левой части. Задайте либо его, либо `BuildContent`, но не оба.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Строит содержимое левой части в коде. Задайте либо его, либо `ContentPrefab`, но не оба.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Фильтр выбираемых предметов.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Колбэк нажатия на предмет. Если не задан, левый клик переключает выбор, а правый снимает его.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Вызывается при `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Вызывается после закрытия.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Максимальное количество выбранных предметов.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Использовать ли рамку и отступы по умолчанию. Отключите, если содержимое рисует собственное изображение.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Дескриптор одной открытой панели взаимодействия с предметами. Становится недействительным при закрытии или смене сцены.

### События

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Вызывается после изменения выбора.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Вызывается при подтверждении.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Вызывается после закрытия.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Принимается панелью при смене фокуса по умолчанию.

### Свойства

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Локальный игрок.


#### Target

```csharp
public Transform Target { get; }
```

Цель взаимодействия.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Фабрика виджетов.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Открыта ли панель.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Список выбранных предметов.

### Методы

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Выбран ли предмет.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Можно ли выбрать предмет.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Выбирает предмет или снимает выбор.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Переключает выбор предмета.


#### ClearSelection

```csharp
public void ClearSelection()
```

Снимает выбор со всех предметов.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Повторно проверяет положение и состояние выбранных предметов после синхронизации инвентаря.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Обрабатывает ввод по предмету. Пока панель открыта, ввод не передаётся действиям использования, перемещения или продажи в инвентаре.


#### Confirm

```csharp
public void Confirm()
```

Подтверждает выбор. Вызывает `OnConfirm` и `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Задаёт цель фокуса по умолчанию для клавиатуры/геймпада.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Закрывает панель.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Снимок одного предмета в собственности игрока. При его расходовании на сервере найдите его заново по ID экземпляра.

### Свойства

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Снимок метаданных предмета.


#### Position

```csharp
public ItemPosition Position { get; }
```

Позиция в сетке.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Где находится предмет.


#### Rotation

```csharp
public int Rotation { get; }
```

Поворот иконки.


#### InstanceID

```csharp
public int InstanceID { get; }
```

ID экземпляра.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Определение предмета. null, если отсутствует.

### Конструкторы

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Создаётся панелью.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Определяет, почему взаимодействие с предметами закрылось.

| Значение | Описание |
|---|---|
| `Cancelled` | ESC/отмена |
| `TargetLost` | Цель исчезла |
| `PlayerLost` | Нет игрока, игрок погиб или отключился |
| `InventoryClosed` | Инвентарь был закрыт |
| `Error` | Исключение в колбэке |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Определяет, где находится предмет.

| Значение | Описание |
|---|---|
| `Inventory` | Сеточный инвентарь |
| `SubBag` | Подсумок |
