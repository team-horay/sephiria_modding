<!-- ไฟล์ที่สร้างอัตโนมัติ Tools/ModApiDocGen ในที่เก็บของเกมสร้างจากคอมเมนต์ /// ในซอร์ส อย่าแก้ไขโดยตรง ให้แก้คอมเมนต์ในซอร์สแล้วสร้างใหม่ -->
# UI

ชนิดสำหรับบทสนทนา การโต้ตอบไอเทม และวิดเจ็ตที่ใช้โดย `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` และ `TryGetUIWidgets` ทั้งหมดเป็น UI ในเครื่อง การซิงก์เครือข่ายเป็นหน้าที่ของม็อด

สร้างเมื่อ 2026-10-26 สำหรับเกมเวอร์ชัน **1.1.0** [ดัชนีอ้างอิง](README.md)

| ชนิด | คำอธิบาย |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | สร้างวิดเจ็ต UI ในสไตล์ของเกม |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | ระดับขนาดข้อความ |
| [`DialoguePage`](#dialoguepage) | บทสนทนาหนึ่งหน้า (เนื้อหาและตัวเลือก) |
| [`DialogueChoice`](#dialoguechoice) | ตัวเลือกเดี่ยว |
| [`DialogueSession`](#dialoguesession) | handle ของบทสนทนาที่เปิดอยู่หนึ่งรายการ |
| [`DialogueCloseReason`](#dialogueclosereason) | ระบุสาเหตุที่บทสนทนาปิด |
| [`ItemInteractionOptions`](#iteminteractionoptions) | การตั้งค่าที่ส่งเมื่อเปิดแผงการโต้ตอบไอเทม |
| [`ItemInteractionSession`](#iteminteractionsession) | handle ของแผงการโต้ตอบไอเทมที่เปิดอยู่หนึ่งรายการ |
| [`ItemInteractionItem`](#iteminteractionitem) | สแนปช็อตของไอเทมที่ครอบครองหนึ่งชิ้น |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | ระบุสาเหตุที่การโต้ตอบไอเทมปิด |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | ระบุว่าไอเทมอยู่ที่ใด |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

สร้างวิดเจ็ต UI ในสไตล์ของเกม

### คอนสตรักเตอร์

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

นำสไตล์มาจากเทมเพลต

### เมธอด

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

สร้าง RectTransform ลูกที่ว่างเปล่า


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

ยืด anchor ให้เต็มพื้นที่ของ parent ทั้งหมด


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

สร้างคอนเทนเนอร์ที่มีรูปลักษณ์แผงของเกม


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

สร้างภาพที่แสดงสไปรต์ในขนาดพิกเซลดั้งเดิม อินพุตจากพอยน์เตอร์จะทะลุผ่าน


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

สร้างคอนเทนเนอร์เลย์เอาต์แนวตั้ง


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

สร้างคอนเทนเนอร์เลย์เอาต์แบบกริด


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

สร้างคอนเทนเนอร์เลย์เอาต์แนวนอน


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

สร้างมุมมองเลื่อนแนวตั้ง


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

สร้างข้อความโดยใช้ฟอนต์ของเกม


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

สร้างข้อความที่แสดงคีย์แปลภาษา


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

ใช้กฎฟอนต์ของเกมกับข้อความที่มีอยู่


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

ผูกคีย์แปลภาษากับข้อความที่มีอยู่


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

สร้างปุ่มในสไตล์ของเกม


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

สร้างปุ่มที่ป้ายชื่อเป็นคีย์แปลภาษา


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

สร้างไอคอนไอเทมของเกม


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

ตั้งค่าขนาดที่ต้องการของ LayoutElement


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

ระดับขนาดข้อความ

| ค่า | คำอธิบาย |
|---|---|
| `Body` | ข้อความเนื้อหา |
| `Small` | ข้อความขนาดเล็ก |
| `Title` | ชื่อเรื่อง |


## DialoguePage

```csharp
public sealed class DialoguePage
```

บทสนทนาหนึ่งหน้า (เนื้อหาและตัวเลือก)

### คุณสมบัติ

#### Text

```csharp
public string Text { get; }
```

*ยังไม่มีคำอธิบาย*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

รายการตัวเลือก


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

ตัวเลือกที่จะโฟกัสเมื่อหน้าเปิด


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

ESC/ยกเลิก สามารถปิดได้หรือไม่

### คอนสตรักเตอร์

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

สร้างหน้าจากข้อความตามตัวอักษร


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

สร้างหน้าที่เนื้อหาเป็นคีย์แปลภาษา


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

ตัวเลือกเดี่ยว การเลือกจะเรียก onSelected ซึ่งจะแสดงหน้าถัดไปหรือปิดบทสนทนา

### คุณสมบัติ

#### Text

```csharp
public string Text { get; }
```

ข้อความที่แสดง คีย์แปลภาษาจะถูกแปลเมื่ออ่าน


#### Interactable

```csharp
public bool Interactable { get; }
```

เลือกได้หรือไม่

### คอนสตรักเตอร์

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

สร้างตัวเลือกจากข้อความตามตัวอักษร


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

สร้างตัวเลือกจากคีย์แปลภาษา


## DialogueSession

```csharp
public sealed class DialogueSession
```

handle ของบทสนทนาที่เปิดอยู่หนึ่งรายการ `Show` แสดงหน้าถัดไป และ `Close` จบบทสนทนา

### อีเวนต์

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

ถูกเรียกครั้งเดียวหลังปิด

### คุณสมบัติ

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

หน้าที่แสดงอยู่ในปัจจุบัน


#### PageVersion

```csharp
public int PageVersion { get; }
```

เพิ่มขึ้นทุกครั้งที่หน้าเปลี่ยน


#### IsOpen

```csharp
public bool IsOpen { get; }
```

เปิดอยู่หรือไม่


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

กำลังรอการเลือกหรือไม่


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

สาเหตุการปิด เป็น null ขณะเปิดอยู่

### คอนสตรักเตอร์

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

เซสชันที่แสดงหน้าแรก โดยปกติสร้างโดย `HorayModAPI.TryShowDialogue`

### เมธอด

#### Show

```csharp
public bool Show(DialoguePage page)
```

แสดงหน้าถัดไป คืนค่า false หากเซสชันปิดแล้ว


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

ถูกเรียกโดยมุมมองเพื่อส่งต่ออินพุตการเลือก


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

ปิดบทสนทนา


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

ระบุสาเหตุที่บทสนทนาปิด

| ค่า | คำอธิบาย |
|---|---|
| `Completed` | เสร็จสิ้นตามปกติ |
| `Cancelled` | ESC/ยกเลิก |
| `Interrupted` | การขัดจังหวะจากภายนอก เช่น การเปลี่ยนชั้นหรือแผงถูกปิดใช้งาน |
| `TargetUnavailable` | anchor หายไป |
| `PlayerUnavailable` | ไม่มีผู้เล่น ผู้เล่นตาย หรือการต่อสู้เริ่มขึ้น |
| `Error` | ข้อยกเว้นใน callback |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

การตั้งค่าที่ส่งเมื่อเปิดแผงการโต้ตอบไอเทม ผู้เรียกเป็นผู้จัดการการตรวจสอบไอเทม การใช้ไอเทม และเครือข่าย

### ค่าคงที่และฟิลด์

#### Target

```csharp
public Transform Target
```

เป้าหมายของการโต้ตอบ (จำเป็น)


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

prefab สำหรับเนื้อหาด้านซ้าย ตั้งค่านี้หรือ `BuildContent` อย่างใดอย่างหนึ่ง ไม่ใช่ทั้งสอง


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

สร้างเนื้อหาด้านซ้ายด้วยโค้ด ตั้งค่านี้หรือ `ContentPrefab` อย่างใดอย่างหนึ่ง ไม่ใช่ทั้งสอง


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

ตัวกรองสำหรับไอเทมที่เลือกได้


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

callback เมื่อคลิกไอเทม หากละไว้ คลิกซ้ายจะสลับการเลือก และคลิกขวาจะยกเลิกการเลือก


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

ถูกเรียกเมื่อ `Confirm`


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

ถูกเรียกหลังปิด


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

จำนวนไอเทมที่เลือกได้สูงสุด


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

ใช้ขอบและระยะขอบเริ่มต้นหรือไม่ ปิดหากเนื้อหาวาดภาพของตัวเอง


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

handle ของแผงการโต้ตอบไอเทมที่เปิดอยู่หนึ่งรายการ จะใช้ไม่ได้เมื่อปิดหรือเมื่อซีนเปลี่ยน

### อีเวนต์

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

ถูกเรียกหลังการเลือกเปลี่ยนแปลง


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

ถูกเรียกเมื่อยืนยัน


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

ถูกเรียกหลังปิด


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

แผงจะได้รับเมื่อโฟกัสเริ่มต้นเปลี่ยนแปลง

### คุณสมบัติ

#### Player

```csharp
public PlayerAvatar Player { get; }
```

ผู้เล่นในเครื่อง


#### Target

```csharp
public Transform Target { get; }
```

เป้าหมายของการโต้ตอบ


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

แฟกทอรีวิดเจ็ต


#### IsOpen

```csharp
public bool IsOpen { get; }
```

เปิดอยู่หรือไม่


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

รายการไอเทมที่เลือก

### เมธอด

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

ไอเทมถูกเลือกหรือไม่


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

ไอเทมเลือกได้หรือไม่


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

เลือกหรือยกเลิกการเลือกไอเทม


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

สลับการเลือกของไอเทม


#### ClearSelection

```csharp
public void ClearSelection()
```

ยกเลิกการเลือกไอเทมทั้งหมด


#### RefreshSelection

```csharp
public void RefreshSelection()
```

ตรวจสอบตำแหน่งและสถานะของไอเทมที่เลือกอีกครั้งหลังการซิงก์ช่องเก็บของ


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

จัดการอินพุตของไอเทม ขณะที่แผงเปิดอยู่ อินพุตจะไม่ทะลุไปยังการใช้ ย้าย หรือขายในช่องเก็บของ


#### Confirm

```csharp
public void Confirm()
```

ยืนยันการเลือก เรียก `OnConfirm` และ `Confirmed`


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

ตั้งค่าเป้าหมายโฟกัสเริ่มต้นสำหรับคีย์บอร์ด/เกมแพด


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

ปิดแผง


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

สแนปช็อตของไอเทมที่ครอบครองหนึ่งชิ้น เมื่อจะใช้ไอเทมบนเซิร์ฟเวอร์ ให้ค้นหาอีกครั้งด้วย instance ID

### คุณสมบัติ

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

สแนปช็อตของ metadata ไอเทม


#### Position

```csharp
public ItemPosition Position { get; }
```

ตำแหน่งในกริด


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

ตำแหน่งที่ไอเทมอยู่


#### Rotation

```csharp
public int Rotation { get; }
```

การหมุนของไอคอน


#### InstanceID

```csharp
public int InstanceID { get; }
```

Instance ID


#### Entity

```csharp
public ItemEntity Entity { get; }
```

นิยามของไอเทม เป็น null หากไม่มี

### คอนสตรักเตอร์

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

สร้างโดยแผง


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

ระบุสาเหตุที่การโต้ตอบไอเทมปิด

| ค่า | คำอธิบาย |
|---|---|
| `Cancelled` | ESC/ยกเลิก |
| `TargetLost` | เป้าหมายหายไป |
| `PlayerLost` | ไม่มีผู้เล่น ผู้เล่นตาย หรือตัดการเชื่อมต่อ |
| `InventoryClosed` | ช่องเก็บของถูกปิด |
| `Error` | ข้อยกเว้นใน callback |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

ระบุว่าไอเทมอยู่ที่ใด

| ค่า | คำอธิบาย |
|---|---|
| `Inventory` | ช่องเก็บของแบบกริด |
| `SubBag` | กระเป๋าข้าง |
