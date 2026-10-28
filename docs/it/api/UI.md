<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# Interfaccia

Tipi per dialoghi, interazione con gli oggetti e widget usati da `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` e `TryGetUIWidgets`. Sono tutti elementi di interfaccia locali; la sincronizzazione di rete è a carico della mod.

Generato il 2026-10-26 per la versione del gioco **1.1.0**. [Indice del riferimento](README.md)

| Tipo | Descrizione |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Crea widget di interfaccia nello stile del gioco. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Livelli di dimensione del testo. |
| [`DialoguePage`](#dialoguepage) | Una pagina di dialogo (testo e scelte). |
| [`DialogueChoice`](#dialoguechoice) | Una singola scelta. |
| [`DialogueSession`](#dialoguesession) | Handle di un dialogo aperto. |
| [`DialogueCloseReason`](#dialogueclosereason) | Indica perché un dialogo si è chiuso. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Impostazioni passate all'apertura di un pannello di interazione con gli oggetti. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handle di un pannello di interazione con gli oggetti aperto. |
| [`ItemInteractionItem`](#iteminteractionitem) | Istantanea di un oggetto posseduto. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Indica perché un'interazione con gli oggetti si è chiusa. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Indica dove si trova un oggetto. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Crea widget di interfaccia nello stile del gioco.

### Costruttori

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Prende gli stili dai template.

### Metodi

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Crea un RectTransform figlio vuoto.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Estende gli anchor fino a riempire l'intero genitore.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Crea un contenitore con l'aspetto dei pannelli del gioco.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Crea un'immagine che mostra lo sprite alla sua dimensione nativa in pixel. L'input del puntatore passa attraverso.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Crea un contenitore con layout verticale.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Crea un contenitore con layout a griglia.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Crea un contenitore con layout orizzontale.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Crea una vista a scorrimento verticale.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Crea un testo con il font del gioco.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Crea un testo che mostra una chiave di localizzazione.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Applica le regole del font del gioco a un testo esistente.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Associa una chiave di localizzazione a un testo esistente.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Crea un pulsante nello stile del gioco.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Crea un pulsante la cui etichetta è una chiave di localizzazione.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Crea l'icona di un oggetto del gioco.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Imposta la dimensione preferita del LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Livelli di dimensione del testo.

| Valore | Descrizione |
|---|---|
| `Body` | Testo del corpo |
| `Small` | Testo piccolo |
| `Title` | Titolo |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Una pagina di dialogo (testo e scelte).

### Proprietà

#### Text

```csharp
public string Text { get; }
```

*Nessuna descrizione per ora.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Elenco delle scelte.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Scelta su cui mettere il focus all'apertura della pagina.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Se ESC/annulla può chiuderla.

### Costruttori

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Crea una pagina da testo letterale.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Crea una pagina il cui corpo è una chiave di localizzazione.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Una singola scelta. Selezionarla chiama onSelected, che mostra la pagina successiva o chiude il dialogo.

### Proprietà

#### Text

```csharp
public string Text { get; }
```

Testo visualizzato. Le chiavi di localizzazione vengono tradotte alla lettura.


#### Interactable

```csharp
public bool Interactable { get; }
```

Se può essere selezionata.

### Costruttori

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Crea una scelta da testo letterale.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Crea una scelta da una chiave di localizzazione.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handle di un dialogo aperto. `Show` mostra la pagina successiva e `Close` lo termina.

### Eventi

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Chiamato una volta dopo la chiusura.

### Proprietà

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Pagina attualmente mostrata.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Aumenta ogni volta che la pagina cambia.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Se è aperto.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Se è in attesa di una scelta.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Motivo della chiusura. null finché è aperto.

### Costruttori

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Una sessione che mostra la prima pagina. Di solito creata da `HorayModAPI.TryShowDialogue`.

### Metodi

#### Show

```csharp
public bool Show(DialoguePage page)
```

Mostra la pagina successiva. false se la sessione è chiusa.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Chiamato dalla vista per inoltrare l'input di scelta.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Chiude il dialogo.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Indica perché un dialogo si è chiuso.

| Valore | Descrizione |
|---|---|
| `Completed` | Completato normalmente |
| `Cancelled` | ESC/annulla |
| `Interrupted` | Interruzione esterna, come un cambio di piano o la disattivazione del pannello |
| `TargetUnavailable` | L'anchor non esiste più |
| `PlayerUnavailable` | Nessun giocatore, giocatore morto o combattimento iniziato |
| `Error` | Eccezione in una callback |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Impostazioni passate all'apertura di un pannello di interazione con gli oggetti. Il chiamante gestisce validazione, consumo e rete degli oggetti.

### Costanti e campi

#### Target

```csharp
public Transform Target
```

Destinazione dell'interazione (obbligatoria).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab per il contenuto sul lato sinistro. Imposta questo oppure `BuildContent`, non entrambi.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Costruisce il contenuto sul lato sinistro da codice. Imposta questo oppure `ContentPrefab`, non entrambi.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filtro per gli oggetti selezionabili.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Callback al clic su un oggetto. Se omessa, il clic sinistro alterna la selezione e il clic destro deseleziona.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Chiamata su `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Chiamata dopo la chiusura.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Numero massimo di oggetti selezionati.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Se usare il bordo e il padding predefiniti. Disattivalo se il contenuto disegna una propria immagine.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handle di un pannello di interazione con gli oggetti aperto. Diventa non valido alla chiusura o al cambio di scena.

### Eventi

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Chiamato dopo che la selezione cambia.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Chiamato alla conferma.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Chiamato dopo la chiusura.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Ricevuto dal pannello quando il focus predefinito cambia.

### Proprietà

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Giocatore locale.


#### Target

```csharp
public Transform Target { get; }
```

Destinazione dell'interazione.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Factory dei widget.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Se è aperto.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Elenco degli oggetti selezionati.

### Metodi

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Se l'oggetto è selezionato.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Se l'oggetto può essere selezionato.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Seleziona o deseleziona un oggetto.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Alterna la selezione di un oggetto.


#### ClearSelection

```csharp
public void ClearSelection()
```

Deseleziona tutti gli oggetti.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Ricontrolla posizione e stato degli oggetti selezionati dopo una sincronizzazione dell'inventario.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Gestisce l'input sugli oggetti. Finché il pannello è aperto, l'input non passa alle azioni di uso, spostamento o vendita dell'inventario.


#### Confirm

```csharp
public void Confirm()
```

Conferma la selezione. Chiama `OnConfirm` e `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Imposta la destinazione del focus predefinito per tastiera/gamepad.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Chiude il pannello.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Istantanea di un oggetto posseduto. Quando lo consumi sul server, cercalo di nuovo tramite l'ID di istanza.

### Proprietà

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Istantanea dei metadati dell'oggetto.


#### Position

```csharp
public ItemPosition Position { get; }
```

Posizione nella griglia.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Dove si trova l'oggetto.


#### Rotation

```csharp
public int Rotation { get; }
```

Rotazione dell'icona.


#### InstanceID

```csharp
public int InstanceID { get; }
```

ID di istanza.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Definizione dell'oggetto. null se assente.

### Costruttori

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Creato dal pannello.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Indica perché un'interazione con gli oggetti si è chiusa.

| Valore | Descrizione |
|---|---|
| `Cancelled` | ESC/annulla |
| `TargetLost` | La destinazione non esiste più |
| `PlayerLost` | Nessun giocatore, giocatore morto o disconnesso |
| `InventoryClosed` | L'inventario è stato chiuso |
| `Error` | Eccezione in una callback |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Indica dove si trova un oggetto.

| Valore | Descrizione |
|---|---|
| `Inventory` | Inventario a griglia |
| `SubBag` | Borsa laterale |
