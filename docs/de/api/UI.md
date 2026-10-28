<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# UI

Dialog-, Item-Interaktions- und Widget-Typen, die von `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` und `TryGetUIWidgets` verwendet werden. Alle sind lokale UI; die Netzwerksynchronisierung liegt bei der Mod.

Generiert am 2026-10-26 für Spielversion **1.1.0**. [Referenzindex](README.md)

| Typ | Beschreibung |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Erstellt UI-Widgets im Stil des Spiels. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Textgrößenstufen. |
| [`DialoguePage`](#dialoguepage) | Eine Dialogseite (Fließtext und Auswahlmöglichkeiten). |
| [`DialogueChoice`](#dialoguechoice) | Eine einzelne Auswahlmöglichkeit. |
| [`DialogueSession`](#dialoguesession) | Handle auf einen offenen Dialog. |
| [`DialogueCloseReason`](#dialogueclosereason) | Gibt an, warum ein Dialog geschlossen wurde. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Einstellungen, die beim Öffnen eines Item-Interaktionspanels übergeben werden. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handle auf ein offenes Item-Interaktionspanel. |
| [`ItemInteractionItem`](#iteminteractionitem) | Momentaufnahme eines besessenen Items. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Gibt an, warum eine Item-Interaktion geschlossen wurde. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Gibt an, wo sich ein Item befindet. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Erstellt UI-Widgets im Stil des Spiels.

### Konstruktoren

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Übernimmt die Stile aus den Vorlagen.

### Methoden

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Erstellt ein leeres RectTransform-Kind.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Streckt die Anker, sodass sie das gesamte Elternelement ausfüllen.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Erstellt einen Container im Panel-Look des Spiels.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Erstellt ein Bild, das das Sprite in seiner nativen Pixelgröße anzeigt. Zeigereingaben werden durchgereicht.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Erstellt einen vertikalen Layout-Container.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Erstellt einen Raster-Layout-Container.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Erstellt einen horizontalen Layout-Container.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Erstellt eine vertikale Scroll-Ansicht.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Erstellt Text mit der Schriftart des Spiels.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Erstellt Text, der einen Lokalisierungsschlüssel anzeigt.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Wendet die Schriftregeln des Spiels auf bestehenden Text an.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Bindet einen Lokalisierungsschlüssel an bestehenden Text.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Erstellt eine Schaltfläche im Stil des Spiels.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Erstellt eine Schaltfläche, deren Beschriftung ein Lokalisierungsschlüssel ist.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Erstellt ein Item-Symbol des Spiels.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Setzt die bevorzugte Größe des LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Textgrößenstufen.

| Wert | Beschreibung |
|---|---|
| `Body` | Fließtext |
| `Small` | Kleiner Text |
| `Title` | Titel |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Eine Dialogseite (Fließtext und Auswahlmöglichkeiten).

### Eigenschaften

#### Text

```csharp
public string Text { get; }
```

*Noch keine Beschreibung.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Liste der Auswahlmöglichkeiten.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Auswahlmöglichkeit, die beim Öffnen der Seite fokussiert wird.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Ob ESC/Abbrechen sie schließen kann.

### Konstruktoren

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Erstellt eine Seite aus literalem Text.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Erstellt eine Seite, deren Fließtext ein Lokalisierungsschlüssel ist.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Eine einzelne Auswahlmöglichkeit. Beim Auswählen wird onSelected aufgerufen, das die nächste Seite anzeigt oder den Dialog schließt.

### Eigenschaften

#### Text

```csharp
public string Text { get; }
```

Anzeigetext. Lokalisierungsschlüssel werden beim Lesen übersetzt.


#### Interactable

```csharp
public bool Interactable { get; }
```

Ob sie ausgewählt werden kann.

### Konstruktoren

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Erstellt eine Auswahlmöglichkeit aus literalem Text.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Erstellt eine Auswahlmöglichkeit aus einem Lokalisierungsschlüssel.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handle auf einen offenen Dialog. `Show` zeigt die nächste Seite und `Close` beendet ihn.

### Events

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Wird nach dem Schließen einmal aufgerufen.

### Eigenschaften

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Aktuell angezeigte Seite.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Wird bei jedem Seitenwechsel erhöht.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Ob er offen ist.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Ob er auf eine Auswahl wartet.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Grund des Schließens. null, solange er offen ist.

### Konstruktoren

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Eine Sitzung, die die erste Seite anzeigt. Wird normalerweise von `HorayModAPI.TryShowDialogue` erstellt.

### Methoden

#### Show

```csharp
public bool Show(DialoguePage page)
```

Zeigt die nächste Seite. false, wenn die Sitzung geschlossen ist.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Wird von der Ansicht aufgerufen, um die Auswahleingabe weiterzugeben.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Schließt den Dialog.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Gibt an, warum ein Dialog geschlossen wurde.

| Wert | Beschreibung |
|---|---|
| `Completed` | Normal abgeschlossen |
| `Cancelled` | ESC/Abbrechen |
| `Interrupted` | Externe Unterbrechung, etwa ein Etagenwechsel oder das Deaktivieren des Panels |
| `TargetUnavailable` | Der Anker ist verschwunden |
| `PlayerUnavailable` | Kein Spieler, Spieler gestorben oder Kampf begonnen |
| `Error` | Exception im Callback |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Einstellungen, die beim Öffnen eines Item-Interaktionspanels übergeben werden. Der Aufrufer übernimmt Item-Validierung, Verbrauch und Netzwerk.

### Konstanten und Felder

#### Target

```csharp
public Transform Target
```

Interaktionsziel (erforderlich).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab für den Inhalt auf der linken Seite. Setze entweder dies oder `BuildContent`, nicht beides.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Baut den Inhalt auf der linken Seite im Code auf. Setze entweder dies oder `ContentPrefab`, nicht beides.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filter für auswählbare Items.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Callback bei Item-Klick. Fehlt er, schaltet Linksklick um und Rechtsklick hebt die Auswahl auf.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Wird bei `Confirm` aufgerufen.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Wird nach dem Schließen aufgerufen.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Maximale Anzahl ausgewählter Items.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Ob Standardrahmen und -innenabstand verwendet werden. Schalte es aus, wenn der Inhalt sein eigenes Bild zeichnet.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handle auf ein offenes Item-Interaktionspanel. Wird ungültig, wenn es geschlossen wird oder die Szene wechselt.

### Events

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Wird nach einer Änderung der Auswahl aufgerufen.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Wird beim Bestätigen aufgerufen.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Wird nach dem Schließen aufgerufen.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Wird vom Panel empfangen, wenn sich der Standardfokus ändert.

### Eigenschaften

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Lokaler Spieler.


#### Target

```csharp
public Transform Target { get; }
```

Interaktionsziel.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Widget-Factory.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Ob es offen ist.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Liste der ausgewählten Items.

### Methoden

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Ob das Item ausgewählt ist.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Ob das Item ausgewählt werden kann.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Wählt ein Item aus oder hebt die Auswahl auf.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Schaltet die Auswahl eines Items um.


#### ClearSelection

```csharp
public void ClearSelection()
```

Hebt die Auswahl aller Items auf.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Prüft Position und Zustand der ausgewählten Items nach einer Inventarsynchronisierung erneut.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Verarbeitet Item-Eingaben. Solange das Panel offen ist, werden Eingaben nicht an Inventaraktionen wie Benutzen, Verschieben oder Verkaufen weitergereicht.


#### Confirm

```csharp
public void Confirm()
```

Bestätigt die Auswahl. Ruft `OnConfirm` und `Confirmed` auf.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Setzt das Standardfokusziel für Tastatur/Gamepad.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Schließt das Panel.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Momentaufnahme eines besessenen Items. Beim Verbrauch auf dem Server erneut über die Instanz-ID nachschlagen.

### Eigenschaften

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Momentaufnahme der Item-Metadaten.


#### Position

```csharp
public ItemPosition Position { get; }
```

Rasterposition.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Wo sich das Item befindet.


#### Rotation

```csharp
public int Rotation { get; }
```

Drehung des Symbols.


#### InstanceID

```csharp
public int InstanceID { get; }
```

Instanz-ID.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Item-Definition. null, falls keine vorhanden.

### Konstruktoren

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Wird vom Panel erstellt.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Gibt an, warum eine Item-Interaktion geschlossen wurde.

| Wert | Beschreibung |
|---|---|
| `Cancelled` | ESC/Abbrechen |
| `TargetLost` | Das Ziel ist verschwunden |
| `PlayerLost` | Kein Spieler, Spieler gestorben oder Verbindung getrennt |
| `InventoryClosed` | Das Inventar wurde geschlossen |
| `Error` | Exception im Callback |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Gibt an, wo sich ein Item befindet.

| Wert | Beschreibung |
|---|---|
| `Inventory` | Rasterinventar |
| `SubBag` | Seitentasche |
