<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# UI

Tipos de diálogo, interacción con objetos y widgets que usan `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` y `TryGetUIWidgets`. Todos son UI local; la sincronización de red corre a cargo del mod.

Generado el 2026-10-26 para la versión del juego **1.1.0**. [Índice de la referencia](README.md)

| Tipo | Descripción |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Crea widgets de UI con el estilo del juego. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Niveles de tamaño de texto. |
| [`DialoguePage`](#dialoguepage) | Una página de diálogo (texto del cuerpo y opciones). |
| [`DialogueChoice`](#dialoguechoice) | Una única opción. |
| [`DialogueSession`](#dialoguesession) | Handle de un diálogo abierto. |
| [`DialogueCloseReason`](#dialogueclosereason) | Identifica por qué se cerró un diálogo. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Configuración que se pasa al abrir un panel de interacción con objetos. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handle de un panel de interacción con objetos abierto. |
| [`ItemInteractionItem`](#iteminteractionitem) | Instantánea de un objeto que se posee. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Identifica por qué se cerró una interacción con objetos. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Identifica dónde se encuentra un objeto. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Crea widgets de UI con el estilo del juego.

### Constructores

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Toma los estilos de las plantillas.

### Métodos

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Crea un RectTransform hijo vacío.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Estira los anclajes para llenar todo el padre.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Crea un contenedor con el aspecto de panel del juego.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Crea una imagen que muestra el sprite a su tamaño de píxel nativo. La entrada del puntero pasa a través de ella.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Crea un contenedor de disposición vertical.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Crea un contenedor de disposición en cuadrícula.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Crea un contenedor de disposición horizontal.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Crea una vista de desplazamiento vertical.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Crea texto usando la fuente del juego.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Crea texto que muestra una clave de localización.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Aplica las reglas de fuente del juego a un texto existente.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Vincula una clave de localización a un texto existente.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Crea un botón con el estilo del juego.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Crea un botón cuya etiqueta es una clave de localización.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Crea un icono de objeto del juego.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Define el tamaño preferido del LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Niveles de tamaño de texto.

| Valor | Descripción |
|---|---|
| `Body` | Texto del cuerpo |
| `Small` | Texto pequeño |
| `Title` | Título |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Una página de diálogo (texto del cuerpo y opciones).

### Propiedades

#### Text

```csharp
public string Text { get; }
```

*Sin descripción todavía.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Lista de opciones.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Opción que recibe el foco cuando se abre la página.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Si ESC/cancelar puede cerrarla.

### Constructores

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Crea una página a partir de texto literal.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Crea una página cuyo cuerpo es una clave de localización.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Una única opción. Al seleccionarla se llama a onSelected, que muestra la página siguiente o cierra el diálogo.

### Propiedades

#### Text

```csharp
public string Text { get; }
```

Texto para mostrar. Las claves de localización se traducen al leerlas.


#### Interactable

```csharp
public bool Interactable { get; }
```

Si se puede seleccionar.

### Constructores

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Crea una opción a partir de texto literal.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Crea una opción a partir de una clave de localización.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handle de un diálogo abierto. `Show` muestra la página siguiente y `Close` lo termina.

### Eventos

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Se llama una vez después de cerrarse.

### Propiedades

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Página que se muestra actualmente.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Se incrementa cada vez que cambia la página.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Si está abierto.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Si está esperando una elección.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Motivo del cierre. null mientras está abierto.

### Constructores

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Una sesión que muestra la primera página. Normalmente la crea `HorayModAPI.TryShowDialogue`.

### Métodos

#### Show

```csharp
public bool Show(DialoguePage page)
```

Muestra la página siguiente. false si la sesión está cerrada.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Lo llama la vista para transmitir la entrada de elección.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Cierra el diálogo.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Identifica por qué se cerró un diálogo.

| Valor | Descripción |
|---|---|
| `Completed` | Completado con normalidad |
| `Cancelled` | ESC/cancelar |
| `Interrupted` | Interrupción externa, como un cambio de planta o la desactivación del panel |
| `TargetUnavailable` | El anclaje ya no existe |
| `PlayerUnavailable` | No hay jugador, el jugador murió o empezó el combate |
| `Error` | Excepción en un callback |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Configuración que se pasa al abrir un panel de interacción con objetos. El que lo llama se encarga de la validación, el consumo y la red de los objetos.

### Constantes y campos

#### Target

```csharp
public Transform Target
```

Objetivo de la interacción (obligatorio).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab para el contenido del lado izquierdo. Define este o `BuildContent`, no ambos.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Construye el contenido del lado izquierdo desde código. Define este o `ContentPrefab`, no ambos.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filtro de objetos seleccionables.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Callback de clic en un objeto. Si se omite, el clic izquierdo alterna la selección y el clic derecho la quita.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Se llama en `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Se llama después de cerrarse.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Número máximo de objetos seleccionados.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Si se usan el borde y el relleno por defecto. Desactívalo si el contenido dibuja su propia imagen.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handle de un panel de interacción con objetos abierto. Deja de ser válido al cerrarse o al cambiar de escena.

### Eventos

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Se llama después de que cambie la selección.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Se llama al confirmar.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Se llama después de cerrarse.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Lo recibe el panel cuando cambia el foco por defecto.

### Propiedades

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Jugador local.


#### Target

```csharp
public Transform Target { get; }
```

Objetivo de la interacción.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Fábrica de widgets.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Si está abierto.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Lista de objetos seleccionados.

### Métodos

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Si el objeto está seleccionado.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Si el objeto se puede seleccionar.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Selecciona o deselecciona un objeto.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Alterna la selección de un objeto.


#### ClearSelection

```csharp
public void ClearSelection()
```

Deselecciona todos los objetos.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Vuelve a comprobar la posición y el estado de los objetos seleccionados después de una sincronización del inventario.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Gestiona la entrada sobre objetos. Mientras el panel está abierto, la entrada no pasa a las acciones de usar, mover o vender del inventario.


#### Confirm

```csharp
public void Confirm()
```

Confirma la selección. Llama a `OnConfirm` y `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Define el objetivo del foco por defecto para teclado/mando.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Cierra el panel.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Instantánea de un objeto que se posee. Al consumirlo en el servidor, búscalo de nuevo por ID de instancia.

### Propiedades

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Instantánea de los metadatos del objeto.


#### Position

```csharp
public ItemPosition Position { get; }
```

Posición en la cuadrícula.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Dónde se encuentra el objeto.


#### Rotation

```csharp
public int Rotation { get; }
```

Rotación del icono.


#### InstanceID

```csharp
public int InstanceID { get; }
```

ID de instancia.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Definición del objeto. null si no tiene.

### Constructores

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Lo crea el panel.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Identifica por qué se cerró una interacción con objetos.

| Valor | Descripción |
|---|---|
| `Cancelled` | ESC/cancelar |
| `TargetLost` | El objetivo ya no existe |
| `PlayerLost` | No hay jugador, el jugador murió o se desconectó |
| `InventoryClosed` | Se cerró el inventario |
| `Error` | Excepción en un callback |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Identifica dónde se encuentra un objeto.

| Valor | Descripción |
|---|---|
| `Inventory` | Inventario de cuadrícula |
| `SubBag` | Bolsa secundaria |
