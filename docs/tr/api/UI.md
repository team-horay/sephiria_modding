<!-- Otomatik üretilen dosya. Oyun deposundaki Tools/ModApiDocGen, kaynaktaki /// yorumlarından oluşturur. Doğrudan düzenlemeyin; kaynak yorumlarını düzeltip yeniden üretin. -->
# Arayüz

`HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` ve `TryGetUIWidgets` tarafından kullanılan diyalog, eşya etkileşimi ve widget türleri. Hepsi yerel arayüzdür; ağ eşitlemesi moda bırakılmıştır.

Oyun sürümü **1.1.0** için 2026-10-26 tarihinde üretildi. [Referans dizini](README.md)

| Tür | Açıklama |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Oyun stilinde arayüz widget'ları oluşturur. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Metin boyutu düzeyleri. |
| [`DialoguePage`](#dialoguepage) | Bir diyalog sayfası (gövde metni ve seçenekler). |
| [`DialogueChoice`](#dialoguechoice) | Tek bir seçenek. |
| [`DialogueSession`](#dialoguesession) | Açık bir diyaloğun tanıtıcısı. |
| [`DialogueCloseReason`](#dialogueclosereason) | Bir diyaloğun neden kapandığını belirtir. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Bir eşya etkileşim paneli açılırken geçirilen ayarlar. |
| [`ItemInteractionSession`](#iteminteractionsession) | Açık bir eşya etkileşim panelinin tanıtıcısı. |
| [`ItemInteractionItem`](#iteminteractionitem) | Sahip olunan bir eşyanın anlık görüntüsü. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Bir eşya etkileşiminin neden kapandığını belirtir. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Bir eşyanın nerede bulunduğunu belirtir. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Oyun stilinde arayüz widget'ları oluşturur.

### Yapıcılar

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Stilleri şablonlardan alır.

### Yöntemler

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Boş bir RectTransform alt öğesi oluşturur.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Çapaları üst öğenin tamamını dolduracak şekilde uzatır.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Oyunun panel görünümüne sahip bir kapsayıcı oluşturur.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Sprite'ı özgün piksel boyutunda gösteren bir görsel oluşturur. İşaretçi girişi içinden geçer.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Dikey bir yerleşim kapsayıcısı oluşturur.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Izgara yerleşimli bir kapsayıcı oluşturur.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Yatay bir yerleşim kapsayıcısı oluşturur.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Dikey bir kaydırma görünümü oluşturur.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Oyun yazı tipini kullanarak metin oluşturur.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Bir yerelleştirme anahtarını gösteren metin oluşturur.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Oyunun yazı tipi kurallarını mevcut bir metne uygular.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Mevcut bir metne bir yerelleştirme anahtarı bağlar.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Oyun stilinde bir düğme oluşturur.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Etiketi bir yerelleştirme anahtarı olan bir düğme oluşturur.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Bir oyun eşya simgesi oluşturur.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

LayoutElement'in tercih edilen boyutunu ayarlar.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Metin boyutu düzeyleri.

| Değer | Açıklama |
|---|---|
| `Body` | Gövde metni |
| `Small` | Küçük metin |
| `Title` | Başlık |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Bir diyalog sayfası (gövde metni ve seçenekler).

### Özellikler

#### Text

```csharp
public string Text { get; }
```

*Henüz açıklama yok.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Seçeneklerin listesi.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Sayfa açıldığında odaklanılacak seçenek.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

ESC/iptal ile kapatılıp kapatılamayacağı.

### Yapıcılar

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Düz metinden bir sayfa oluşturur.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Gövdesi bir yerelleştirme anahtarı olan bir sayfa oluşturur.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Tek bir seçenek. Seçilmesi onSelected'ı çağırır; bu da sonraki sayfayı gösterir veya diyaloğu kapatır.

### Özellikler

#### Text

```csharp
public string Text { get; }
```

Gösterim metni. Yerelleştirme anahtarları okunurken çevrilir.


#### Interactable

```csharp
public bool Interactable { get; }
```

Seçilip seçilemeyeceği.

### Yapıcılar

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Düz metinden bir seçenek oluşturur.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Bir yerelleştirme anahtarından seçenek oluşturur.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Açık bir diyaloğun tanıtıcısı. `Show` sonraki sayfayı gösterir, `Close` sonlandırır.

### Olaylar

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Kapandıktan sonra bir kez çağrılır.

### Özellikler

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Şu anda gösterilen sayfa.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Sayfa her değiştiğinde artar.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Açık olup olmadığı.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Bir seçim beklenip beklenmediği.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Kapanma nedeni. Açıkken null.

### Yapıcılar

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

İlk sayfayı gösteren bir oturum. Genellikle `HorayModAPI.TryShowDialogue` tarafından oluşturulur.

### Yöntemler

#### Show

```csharp
public bool Show(DialoguePage page)
```

Sonraki sayfayı gösterir. Oturum kapalıysa false.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Seçim girişini iletmek için görünüm tarafından çağrılır.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Diyaloğu kapatır.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Bir diyaloğun neden kapandığını belirtir.

| Değer | Açıklama |
|---|---|
| `Completed` | Normal şekilde tamamlandı |
| `Cancelled` | ESC/iptal |
| `Interrupted` | Kat değişimi veya panelin devre dışı bırakılması gibi dış kesinti |
| `TargetUnavailable` | Çapa artık yok |
| `PlayerUnavailable` | Oyuncu yok, oyuncu öldü veya savaş başladı |
| `Error` | Geri çağrı istisnası |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Bir eşya etkileşim paneli açılırken geçirilen ayarlar. Eşya doğrulama, tüketim ve ağ işlemleri çağıranın sorumluluğundadır.

### Sabitler ve Alanlar

#### Target

```csharp
public Transform Target
```

Etkileşim hedefi (zorunlu).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Sol taraftaki içeriğin prefab'ı. Bunu ya da `BuildContent` alanını ayarlayın, ikisini birden değil.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Sol taraftaki içeriği kodla oluşturur. Bunu ya da `ContentPrefab` alanını ayarlayın, ikisini birden değil.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Seçilebilir eşyalar için filtre.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Eşya tıklama geri çağrısı. Atlanırsa sol tık seçimi değiştirir, sağ tık seçimi kaldırır.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

`Confirm` sırasında çağrılır.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Kapandıktan sonra çağrılır.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

En fazla seçili eşya sayısı.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Varsayılan kenarlık ve dolgunun kullanılıp kullanılmayacağı. İçerik kendi görselini çiziyorsa kapatın.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Açık bir eşya etkileşim panelinin tanıtıcısı. Kapandığında veya sahne değiştiğinde geçersiz olur.

### Olaylar

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Seçim değiştikten sonra çağrılır.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Onaylandığında çağrılır.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Kapandıktan sonra çağrılır.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Varsayılan odak değiştiğinde panel tarafından alınır.

### Özellikler

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Yerel oyuncu.


#### Target

```csharp
public Transform Target { get; }
```

Etkileşim hedefi.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Widget fabrikası.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Açık olup olmadığı.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Seçili eşyaların listesi.

### Yöntemler

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Eşyanın seçili olup olmadığı.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Eşyanın seçilip seçilemeyeceği.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Bir eşyayı seçer veya seçimini kaldırır.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Bir eşyanın seçimini değiştirir.


#### ClearSelection

```csharp
public void ClearSelection()
```

Tüm eşyaların seçimini kaldırır.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Envanter eşitlemesinden sonra seçili eşyaların konumunu ve durumunu yeniden denetler.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Eşya girişini işler. Panel açıkken giriş, envanterdeki kullanma, taşıma veya satma eylemlerine iletilmez.


#### Confirm

```csharp
public void Confirm()
```

Seçimi onaylar. `OnConfirm` ve `Confirmed` çağrılır.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Klavye/oyun kumandası için varsayılan odak hedefini ayarlar.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Paneli kapatır.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Sahip olunan bir eşyanın anlık görüntüsü. Sunucuda tüketirken örnek kimliğiyle yeniden arayın.

### Özellikler

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Eşya meta verisinin anlık görüntüsü.


#### Position

```csharp
public ItemPosition Position { get; }
```

Izgara konumu.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Eşyanın bulunduğu yer.


#### Rotation

```csharp
public int Rotation { get; }
```

Simge dönüşü.


#### InstanceID

```csharp
public int InstanceID { get; }
```

Örnek kimliği.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Eşya tanımı. Yoksa null.

### Yapıcılar

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Panel tarafından oluşturulur.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Bir eşya etkileşiminin neden kapandığını belirtir.

| Değer | Açıklama |
|---|---|
| `Cancelled` | ESC/iptal |
| `TargetLost` | Hedef artık yok |
| `PlayerLost` | Oyuncu yok, oyuncu öldü veya bağlantı kesildi |
| `InventoryClosed` | Envanter kapatıldı |
| `Error` | Geri çağrı istisnası |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Bir eşyanın nerede bulunduğunu belirtir.

| Değer | Açıklama |
|---|---|
| `Inventory` | Izgara envanteri |
| `SubBag` | Yan Çanta |
