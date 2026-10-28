<!-- 자동 생성 파일입니다. 게임 저장소의 Tools/ModApiDocGen이 소스의 /// 주석에서 만듭니다. 직접 고치지 말고 소스 주석을 고친 뒤 다시 생성하세요. -->
# UI

`HorayModAPI.TryShowDialogue`, `TryShowItemInteraction`, `TryGetUIWidgets`가 쓰는 대화·아이템 상호작용·위젯 타입입니다. 전부 로컬 UI이며 네트워크 동기화는 모드의 몫입니다.

게임 버전 **1.1.0** 기준, 2026-10-26 생성. [레퍼런스 목차](README.md)

| 타입 | 설명 |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | 게임 스타일 UI 위젯 생성기입니다. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | 텍스트 크기 단계입니다. |
| [`DialoguePage`](#dialoguepage) | 대화 한 페이지(본문과 선택지)입니다. |
| [`DialogueChoice`](#dialoguechoice) | 선택지 하나입니다. |
| [`DialogueSession`](#dialoguesession) | 열려 있는 대화 하나의 핸들입니다. |
| [`DialogueCloseReason`](#dialogueclosereason) | 대화가 닫힌 이유를 식별하는 값입니다. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | 아이템 상호작용 패널을 열 때 넘기는 설정입니다. |
| [`ItemInteractionSession`](#iteminteractionsession) | 열려 있는 아이템 상호작용 패널 하나의 핸들입니다. |
| [`ItemInteractionItem`](#iteminteractionitem) | 소유 아이템 하나의 스냅샷입니다. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | 아이템 상호작용이 닫힌 이유를 식별하는 값입니다. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | 아이템이 있는 곳을 식별하는 값입니다. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

게임 스타일 UI 위젯 생성기입니다.

### 생성자

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

템플릿에서 스타일을 가져옵니다.

### 메서드

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

빈 RectTransform 자식을 만듭니다.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

부모 전체를 채우도록 앵커를 펼칩니다.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

게임 패널 외형의 컨테이너를 만듭니다.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

스프라이트를 원본 픽셀 크기로 표시하는 이미지를 만듭니다. 포인터 입력은 통과합니다.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

세로 레이아웃 컨테이너를 만듭니다.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

격자 레이아웃 컨테이너를 만듭니다.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

가로 레이아웃 컨테이너를 만듭니다.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

세로 스크롤 뷰를 만듭니다.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

게임 글꼴이 적용된 텍스트를 만듭니다.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

번역 키를 표시하는 텍스트를 만듭니다.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

기존 텍스트에 게임 글꼴 규칙을 적용합니다.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

기존 텍스트에 번역 키를 연결합니다.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

게임 스타일 버튼을 만듭니다.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

라벨이 번역 키인 버튼을 만듭니다.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

게임 아이템 아이콘을 만듭니다.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

LayoutElement의 선호 크기를 지정합니다.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

텍스트 크기 단계입니다.

| 값 | 설명 |
|---|---|
| `Body` | 본문 |
| `Small` | 작은 글씨 |
| `Title` | 제목 |


## DialoguePage

```csharp
public sealed class DialoguePage
```

대화 한 페이지(본문과 선택지)입니다.

### 프로퍼티

#### Text

```csharp
public string Text { get; }
```

*설명이 아직 없습니다.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

선택지 목록


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

열릴 때 포커스할 선택지


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

ESC/취소로 닫을 수 있는지

### 생성자

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

직접 쓴 문장으로 만듭니다.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

본문을 번역 키로 만듭니다.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

선택지 하나입니다. 고르면 onSelected가 호출되며, 그 안에서 다음 페이지를 보이거나 닫습니다.

### 프로퍼티

#### Text

```csharp
public string Text { get; }
```

표시 문장입니다. 번역 키는 읽는 시점에 번역됩니다.


#### Interactable

```csharp
public bool Interactable { get; }
```

선택할 수 있는지

### 생성자

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

직접 쓴 문장으로 만듭니다.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

번역 키로 만듭니다.


## DialogueSession

```csharp
public sealed class DialogueSession
```

열려 있는 대화 하나의 핸들입니다. `Show`로 다음 페이지를 보이고 `Close`로 끝냅니다.

### 이벤트

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

닫힌 뒤 한 번 호출됩니다.

### 프로퍼티

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

표시 중인 페이지


#### PageVersion

```csharp
public int PageVersion { get; }
```

페이지가 바뀔 때마다 증가합니다.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

열려 있는지


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

선택을 기다리는 중인지


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

닫힌 이유입니다. 열려 있으면 null.

### 생성자

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

첫 페이지를 표시한 세션입니다. 보통 `HorayModAPI.TryShowDialogue`가 만듭니다.

### 메서드

#### Show

```csharp
public bool Show(DialoguePage page)
```

다음 페이지를 표시합니다. 닫힌 세션이면 false.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

뷰가 선택 입력을 전달할 때 부릅니다.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

대화를 닫습니다.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

대화가 닫힌 이유를 식별하는 값입니다.

| 값 | 설명 |
|---|---|
| `Completed` | 정상 완료 |
| `Cancelled` | ESC/취소 |
| `Interrupted` | 층 이동·패널 비활성화 등 외부 중단 |
| `TargetUnavailable` | anchor가 사라짐 |
| `PlayerUnavailable` | 플레이어 없음·사망·전투 진입 |
| `Error` | 콜백 예외 |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

아이템 상호작용 패널을 열 때 넘기는 설정입니다. 아이템 검증·소모·네트워크 처리는 호출자가 맡습니다.

### 상수와 필드

#### Target

```csharp
public Transform Target
```

상호작용 대상(필수)


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

왼쪽 콘텐츠 프리팹입니다. `BuildContent`와 둘 중 하나만 지정합니다.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

왼쪽 콘텐츠를 코드로 만듭니다. `ContentPrefab`과 둘 중 하나만 지정합니다.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

선택 가능 필터


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

아이템 클릭 콜백입니다. 생략하면 좌클릭 토글·우클릭 해제.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

`Confirm` 때 호출됩니다.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

닫힌 뒤 호출됩니다.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

최대 선택 수


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

기본 테두리와 여백을 쓸지입니다. 콘텐츠가 자체 이미지를 그리면 끕니다.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

열려 있는 아이템 상호작용 패널 하나의 핸들입니다. 닫히거나 씬이 바뀌면 무효가 됩니다.

### 이벤트

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

선택이 바뀐 뒤 호출됩니다.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

확정 때 호출됩니다.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

닫힌 뒤 호출됩니다.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

기본 포커스가 바뀔 때 패널이 받습니다.

### 프로퍼티

#### Player

```csharp
public PlayerAvatar Player { get; }
```

로컬 플레이어


#### Target

```csharp
public Transform Target { get; }
```

상호작용 대상


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

위젯 팩토리


#### IsOpen

```csharp
public bool IsOpen { get; }
```

열려 있는지


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

선택한 아이템 목록

### 메서드

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

선택돼 있는지


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

선택할 수 있는지


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

선택하거나 해제합니다.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

선택을 토글합니다.


#### ClearSelection

```csharp
public void ClearSelection()
```

선택을 모두 해제합니다.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

인벤토리 동기화 뒤 선택한 아이템의 위치와 상태를 다시 확인합니다.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

아이템 입력을 처리합니다. 열려 있는 동안 입력은 인벤토리의 사용·이동·판매로 넘어가지 않습니다.


#### Confirm

```csharp
public void Confirm()
```

선택을 확정합니다. `OnConfirm`과 `Confirmed`를 부릅니다.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

키보드/패드의 기본 포커스 대상을 지정합니다.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

패널을 닫습니다.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

소유 아이템 하나의 스냅샷입니다. 서버에서 소모할 때는 인스턴스 ID로 다시 조회합니다.

### 프로퍼티

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

아이템 메타데이터 스냅샷


#### Position

```csharp
public ItemPosition Position { get; }
```

격자 위치


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

아이템이 있는 곳


#### Rotation

```csharp
public int Rotation { get; }
```

아이콘 회전


#### InstanceID

```csharp
public int InstanceID { get; }
```

인스턴스 ID


#### Entity

```csharp
public ItemEntity Entity { get; }
```

아이템 정의입니다. 없으면 null.

### 생성자

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

패널이 만듭니다.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

아이템 상호작용이 닫힌 이유를 식별하는 값입니다.

| 값 | 설명 |
|---|---|
| `Cancelled` | ESC/취소 |
| `TargetLost` | 대상이 사라짐 |
| `PlayerLost` | 플레이어 없음·사망·연결 해제 |
| `InventoryClosed` | 인벤토리가 닫힘 |
| `Error` | 콜백 예외 |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

아이템이 있는 곳을 식별하는 값입니다.

| 값 | 설명 |
|---|---|
| `Inventory` | 격자 인벤토리 |
| `SubBag` | 보조 가방 |
