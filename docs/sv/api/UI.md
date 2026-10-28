<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# UI

Dialog-, föremålsinteraktions- och widgettyper som används av `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` och `TryGetUIWidgets`. Alla är lokalt UI; nätverkssynkronisering är upp till modden.

Genererad 2026-10-26 för spelversion **1.1.0**. [Referensindex](README.md)

| Typ | Beskrivning |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Skapar UI-widgetar i spelets stil. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Textstorleksnivåer. |
| [`DialoguePage`](#dialoguepage) | En dialogsida (brödtext och val). |
| [`DialogueChoice`](#dialoguechoice) | Ett enskilt val. |
| [`DialogueSession`](#dialoguesession) | Handtag till en öppen dialog. |
| [`DialogueCloseReason`](#dialogueclosereason) | Anger varför en dialog stängdes. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Inställningar som skickas när en föremålsinteraktionspanel öppnas. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handtag till en öppen föremålsinteraktionspanel. |
| [`ItemInteractionItem`](#iteminteractionitem) | Ögonblicksbild av ett ägt föremål. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Anger varför en föremålsinteraktion stängdes. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Anger var ett föremål finns. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Skapar UI-widgetar i spelets stil.

### Konstruktorer

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Hämtar stilar från mallarna.

### Metoder

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Skapar ett tomt RectTransform-barn.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Sträcker ut ankarna så att de fyller hela föräldern.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Skapar en container med spelets panelutseende.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Skapar en bild som visar spriten i dess ursprungliga pixelstorlek. Pekarinmatning passerar igenom.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Skapar en vertikal layoutcontainer.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Skapar en rutnätslayoutcontainer.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Skapar en horisontell layoutcontainer.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Skapar en vertikal rullningsvy.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Skapar text med spelets teckensnitt.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Skapar text som visar en lokaliseringsnyckel.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Tillämpar spelets teckensnittsregler på befintlig text.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Binder en lokaliseringsnyckel till befintlig text.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Skapar en knapp i spelets stil.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Skapar en knapp vars etikett är en lokaliseringsnyckel.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Skapar en föremålsikon från spelet.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Sätter LayoutElement-objektets föredragna storlek.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Textstorleksnivåer.

| Värde | Beskrivning |
|---|---|
| `Body` | Brödtext |
| `Small` | Liten text |
| `Title` | Titel |


## DialoguePage

```csharp
public sealed class DialoguePage
```

En dialogsida (brödtext och val).

### Egenskaper

#### Text

```csharp
public string Text { get; }
```

*Ingen beskrivning ännu.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Lista över val.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Valet som ska fokuseras när sidan öppnas.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Om ESC/avbryt kan stänga den.

### Konstruktorer

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Skapar en sida från bokstavlig text.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Skapar en sida vars brödtext är en lokaliseringsnyckel.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Ett enskilt val. När det väljs anropas onSelected, som visar nästa sida eller stänger dialogen.

### Egenskaper

#### Text

```csharp
public string Text { get; }
```

Visningstext. Lokaliseringsnycklar översätts när de läses.


#### Interactable

```csharp
public bool Interactable { get; }
```

Om det kan väljas.

### Konstruktorer

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Skapar ett val från bokstavlig text.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Skapar ett val från en lokaliseringsnyckel.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handtag till en öppen dialog. `Show` visar nästa sida och `Close` avslutar den.

### Händelser

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Anropas en gång efter stängning.

### Egenskaper

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Sidan som visas just nu.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Ökas varje gång sidan ändras.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Om den är öppen.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Om den väntar på ett val.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Orsak till stängning. null medan den är öppen.

### Konstruktorer

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

En session som visar den första sidan. Skapas vanligtvis av `HorayModAPI.TryShowDialogue`.

### Metoder

#### Show

```csharp
public bool Show(DialoguePage page)
```

Visar nästa sida. false om sessionen är stängd.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Anropas av vyn för att skicka vidare valinmatning.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Stänger dialogen.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Anger varför en dialog stängdes.

| Värde | Beskrivning |
|---|---|
| `Completed` | Slutförd normalt |
| `Cancelled` | ESC/avbryt |
| `Interrupted` | Externt avbrott, till exempel ett våningsbyte eller att panelen inaktiverades |
| `TargetUnavailable` | Ankaret är borta |
| `PlayerUnavailable` | Ingen spelare, spelaren dog eller strid började |
| `Error` | Undantag i callback |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Inställningar som skickas när en föremålsinteraktionspanel öppnas. Anroparen sköter validering, förbrukning och nätverkshantering av föremål.

### Konstanter och fält

#### Target

```csharp
public Transform Target
```

Interaktionsmål (obligatoriskt).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab för innehållet på vänster sida. Sätt antingen den här eller `BuildContent`, inte båda.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Bygger innehållet på vänster sida i kod. Sätt antingen den här eller `ContentPrefab`, inte båda.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filter för valbara föremål.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Callback vid klick på föremål. Om den utelämnas växlar vänsterklick valet och högerklick avmarkerar.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Anropas vid `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Anropas efter stängning.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Maximalt antal valda föremål.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Om standardramen och standardutfyllnaden ska användas. Stäng av den om innehållet ritar sin egen bild.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handtag till en öppen föremålsinteraktionspanel. Blir ogiltigt när panelen stängs eller när scenen byts.

### Händelser

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Anropas efter att valet har ändrats.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Anropas vid bekräftelse.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Anropas efter stängning.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Tas emot av panelen när standardfokus ändras.

### Egenskaper

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Lokal spelare.


#### Target

```csharp
public Transform Target { get; }
```

Interaktionsmål.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Widgetfabrik.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Om den är öppen.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Lista över valda föremål.

### Metoder

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Om föremålet är valt.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Om föremålet kan väljas.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Väljer eller avmarkerar ett föremål.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Växlar ett föremåls val.


#### ClearSelection

```csharp
public void ClearSelection()
```

Avmarkerar alla föremål.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Kontrollerar på nytt positionen och tillståndet för valda föremål efter en inventariesynkronisering.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Hanterar föremålsinmatning. Medan panelen är öppen går inmatningen inte vidare till inventariets använd-, flytta- eller säljåtgärder.


#### Confirm

```csharp
public void Confirm()
```

Bekräftar valet. Anropar `OnConfirm` och `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Sätter standardfokusmålet för tangentbord/handkontroll.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Stänger panelen.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Ögonblicksbild av ett ägt föremål. När det förbrukas på servern: slå upp det igen via instans-ID.

### Egenskaper

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Ögonblicksbild av föremålets metadata.


#### Position

```csharp
public ItemPosition Position { get; }
```

Rutnätsposition.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Var föremålet finns.


#### Rotation

```csharp
public int Rotation { get; }
```

Ikonens rotation.


#### InstanceID

```csharp
public int InstanceID { get; }
```

Instans-ID.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Föremålsdefinition. null om det inte finns någon.

### Konstruktorer

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Skapas av panelen.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Anger varför en föremålsinteraktion stängdes.

| Värde | Beskrivning |
|---|---|
| `Cancelled` | ESC/avbryt |
| `TargetLost` | Målet är borta |
| `PlayerLost` | Ingen spelare, spelaren dog eller kopplades från |
| `InventoryClosed` | Inventariet stängdes |
| `Error` | Undantag i callback |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Anger var ett föremål finns.

| Värde | Beskrivning |
|---|---|
| `Inventory` | Rutnätsinventarie |
| `SubBag` | Sidoväska |
