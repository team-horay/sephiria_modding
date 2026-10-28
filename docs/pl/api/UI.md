<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj go bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# UI

Typy dialogów, interakcji z przedmiotami i widżetów używane przez `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` i `TryGetUIWidgets`. Wszystkie są lokalnym interfejsem; synchronizacja sieciowa należy do moda.

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. [Indeks dokumentacji](README.md)

| Typ | Opis |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Tworzy widżety interfejsu w stylu gry. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Poziomy rozmiaru tekstu. |
| [`DialoguePage`](#dialoguepage) | Jedna strona dialogu (treść i opcje wyboru). |
| [`DialogueChoice`](#dialoguechoice) | Pojedyncza opcja wyboru. |
| [`DialogueSession`](#dialoguesession) | Uchwyt do jednego otwartego dialogu. |
| [`DialogueCloseReason`](#dialogueclosereason) | Określa, dlaczego dialog został zamknięty. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Ustawienia przekazywane przy otwieraniu panelu interakcji z przedmiotami. |
| [`ItemInteractionSession`](#iteminteractionsession) | Uchwyt do jednego otwartego panelu interakcji z przedmiotami. |
| [`ItemInteractionItem`](#iteminteractionitem) | Migawka jednego posiadanego przedmiotu. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Określa, dlaczego interakcja z przedmiotami została zamknięta. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Określa, gdzie znajduje się przedmiot. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Tworzy widżety interfejsu w stylu gry.

### Konstruktory

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Pobiera style z szablonów.

### Metody

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Tworzy pusty element potomny RectTransform.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Rozciąga kotwice tak, aby wypełnić całego rodzica.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Tworzy kontener o wyglądzie panelu z gry.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Tworzy obraz wyświetlający sprite w jego natywnym rozmiarze w pikselach. Zdarzenia wskaźnika przechodzą przez niego.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Tworzy kontener z układem pionowym.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Tworzy kontener z układem siatki.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Tworzy kontener z układem poziomym.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Tworzy pionowy widok przewijany.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Tworzy tekst czcionką gry.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Tworzy tekst wyświetlający klucz lokalizacji.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Stosuje reguły czcionki gry do istniejącego tekstu.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Wiąże klucz lokalizacji z istniejącym tekstem.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Tworzy przycisk w stylu gry.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Tworzy przycisk, którego etykieta jest kluczem lokalizacji.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Tworzy ikonę przedmiotu z gry.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Ustawia preferowany rozmiar LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Poziomy rozmiaru tekstu.

| Wartość | Opis |
|---|---|
| `Body` | Tekst podstawowy |
| `Small` | Mały tekst |
| `Title` | Tytuł |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Jedna strona dialogu (treść i opcje wyboru).

### Właściwości

#### Text

```csharp
public string Text { get; }
```

*Brak opisu.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Lista opcji wyboru.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Opcja, na której ustawiany jest fokus przy otwarciu strony.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Czy ESC/anulowanie może go zamknąć.

### Konstruktory

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Tworzy stronę z dosłownego tekstu.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Tworzy stronę, której treść jest kluczem lokalizacji.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Pojedyncza opcja wyboru. Jej wybranie wywołuje onSelected, które pokazuje następną stronę lub zamyka dialog.

### Właściwości

#### Text

```csharp
public string Text { get; }
```

Wyświetlany tekst. Klucze lokalizacji są tłumaczone przy odczycie.


#### Interactable

```csharp
public bool Interactable { get; }
```

Czy można ją wybrać.

### Konstruktory

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Tworzy opcję wyboru z dosłownego tekstu.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Tworzy opcję wyboru z klucza lokalizacji.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Uchwyt do jednego otwartego dialogu. `Show` pokazuje następną stronę, a `Close` go kończy.

### Zdarzenia

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Wywoływane raz po zamknięciu.

### Właściwości

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Aktualnie wyświetlana strona.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Zwiększa się przy każdej zmianie strony.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Czy jest otwarty.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Czy czeka na wybór.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Powód zamknięcia. null, dopóki jest otwarty.

### Konstruktory

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Sesja wyświetlająca pierwszą stronę. Zwykle tworzona przez `HorayModAPI.TryShowDialogue`.

### Metody

#### Show

```csharp
public bool Show(DialoguePage page)
```

Pokazuje następną stronę. false, jeśli sesja jest zamknięta.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Wywoływane przez widok w celu przekazania wyboru.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Zamyka dialog.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Określa, dlaczego dialog został zamknięty.

| Wartość | Opis |
|---|---|
| `Completed` | Zakończony normalnie |
| `Cancelled` | ESC/anulowanie |
| `Interrupted` | Przerwanie z zewnątrz, np. zmiana piętra lub wyłączenie panelu |
| `TargetUnavailable` | Kotwica zniknęła |
| `PlayerUnavailable` | Brak gracza, gracz zginął lub rozpoczęła się walka |
| `Error` | Wyjątek w callbacku |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Ustawienia przekazywane przy otwieraniu panelu interakcji z przedmiotami. Wywołujący odpowiada za walidację i zużywanie przedmiotów oraz obsługę sieci.

### Stałe i pola

#### Target

```csharp
public Transform Target
```

Cel interakcji (wymagany).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab zawartości po lewej stronie. Ustaw to albo `BuildContent`, nie oba naraz.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Buduje zawartość po lewej stronie w kodzie. Ustaw to albo `ContentPrefab`, nie oba naraz.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filtr przedmiotów możliwych do wybrania.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Callback kliknięcia przedmiotu. Jeśli pominięty, lewy przycisk przełącza zaznaczenie, a prawy je usuwa.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Wywoływane przy `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Wywoływane po zamknięciu.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Maksymalna liczba zaznaczonych przedmiotów.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Czy używać domyślnej ramki i marginesów. Wyłącz, jeśli zawartość rysuje własny obraz.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Uchwyt do jednego otwartego panelu interakcji z przedmiotami. Traci ważność po zamknięciu lub przy zmianie sceny.

### Zdarzenia

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Wywoływane po zmianie zaznaczenia.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Wywoływane przy potwierdzeniu.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Wywoływane po zamknięciu.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Odbierane przez panel, gdy zmienia się domyślny fokus.

### Właściwości

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Lokalny gracz.


#### Target

```csharp
public Transform Target { get; }
```

Cel interakcji.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Fabryka widżetów.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Czy jest otwarty.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Lista zaznaczonych przedmiotów.

### Metody

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Czy przedmiot jest zaznaczony.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Czy przedmiot można zaznaczyć.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Zaznacza lub odznacza przedmiot.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Przełącza zaznaczenie przedmiotu.


#### ClearSelection

```csharp
public void ClearSelection()
```

Odznacza wszystkie przedmioty.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Ponownie sprawdza pozycję i stan zaznaczonych przedmiotów po synchronizacji ekwipunku.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Obsługuje wejście dotyczące przedmiotu. Gdy panel jest otwarty, wejście nie jest przekazywane do akcji użycia, przeniesienia ani sprzedaży w ekwipunku.


#### Confirm

```csharp
public void Confirm()
```

Potwierdza zaznaczenie. Wywołuje `OnConfirm` i `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Ustawia domyślny cel fokusu dla klawiatury/pada.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Zamyka panel.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Migawka jednego posiadanego przedmiotu. Przy zużywaniu go na serwerze wyszukaj go ponownie po ID instancji.

### Właściwości

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Migawka metadanych przedmiotu.


#### Position

```csharp
public ItemPosition Position { get; }
```

Pozycja w siatce.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Gdzie znajduje się przedmiot.


#### Rotation

```csharp
public int Rotation { get; }
```

Obrót ikony.


#### InstanceID

```csharp
public int InstanceID { get; }
```

ID instancji.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Definicja przedmiotu. null, jeśli brak.

### Konstruktory

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Tworzone przez panel.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Określa, dlaczego interakcja z przedmiotami została zamknięta.

| Wartość | Opis |
|---|---|
| `Cancelled` | ESC/anulowanie |
| `TargetLost` | Cel zniknął |
| `PlayerLost` | Brak gracza, gracz zginął lub się rozłączył |
| `InventoryClosed` | Ekwipunek został zamknięty |
| `Error` | Wyjątek w callbacku |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Określa, gdzie znajduje się przedmiot.

| Wartość | Opis |
|---|---|
| `Inventory` | Ekwipunek siatkowy |
| `SubBag` | Boczna torba |
