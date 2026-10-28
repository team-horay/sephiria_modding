<!-- Arquivo gerado automaticamente. Tools/ModApiDocGen no repositório do jogo o constrói a partir dos comentários /// no código-fonte. Não o edite diretamente; corrija os comentários no código-fonte e gere novamente. -->
# UI

Tipos de diálogo, interação com itens e widgets usados por `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` e `TryGetUIWidgets`. Todos são UI local; a sincronização de rede fica por conta do mod.

Gerado em 2026-10-26 para a versão **1.1.0** do jogo. [Índice da referência](README.md)

| Tipo | Descrição |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Cria widgets de UI no estilo do jogo. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Níveis de tamanho de texto. |
| [`DialoguePage`](#dialoguepage) | Uma página de diálogo (texto principal e opções). |
| [`DialogueChoice`](#dialoguechoice) | Uma única opção. |
| [`DialogueSession`](#dialoguesession) | Handle para um diálogo aberto. |
| [`DialogueCloseReason`](#dialogueclosereason) | Identifica por que um diálogo foi fechado. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Configurações passadas ao abrir um painel de interação com itens. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handle para um painel de interação com itens aberto. |
| [`ItemInteractionItem`](#iteminteractionitem) | Instantâneo de um item possuído. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Identifica por que uma interação com itens foi fechada. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Identifica onde um item está localizado. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Cria widgets de UI no estilo do jogo.

### Construtores

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Obtém os estilos a partir dos modelos.

### Métodos

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Cria um RectTransform filho vazio.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Estica as âncoras para preencher todo o pai.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Cria um contêiner com o visual de painel do jogo.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Cria uma imagem que exibe o sprite no seu tamanho nativo em pixels. A entrada do ponteiro passa através dela.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Cria um contêiner de layout vertical.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Cria um contêiner de layout em grade.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Cria um contêiner de layout horizontal.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Cria uma área de rolagem vertical.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Cria um texto usando a fonte do jogo.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Cria um texto que exibe uma chave de localização.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Aplica as regras de fonte do jogo a um texto existente.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Vincula uma chave de localização a um texto existente.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Cria um botão no estilo do jogo.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Cria um botão cujo rótulo é uma chave de localização.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Cria um ícone de item do jogo.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Define o tamanho preferido do LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Níveis de tamanho de texto.

| Valor | Descrição |
|---|---|
| `Body` | Texto principal |
| `Small` | Texto pequeno |
| `Title` | Título |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Uma página de diálogo (texto principal e opções).

### Propriedades

#### Text

```csharp
public string Text { get; }
```

*Ainda sem descrição.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Lista de opções.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Opção a receber o foco quando a página abre.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Se ESC/cancelar pode fechá-la.

### Construtores

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Cria uma página a partir de um texto literal.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Cria uma página cujo texto principal é uma chave de localização.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Uma única opção. Selecioná-la chama onSelected, que exibe a próxima página ou fecha o diálogo.

### Propriedades

#### Text

```csharp
public string Text { get; }
```

Texto de exibição. Chaves de localização são traduzidas na leitura.


#### Interactable

```csharp
public bool Interactable { get; }
```

Se pode ser selecionada.

### Construtores

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Cria uma opção a partir de um texto literal.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Cria uma opção a partir de uma chave de localização.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handle para um diálogo aberto. `Show` exibe a próxima página e `Close` o encerra.

### Eventos

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Chamado uma vez depois do fechamento.

### Propriedades

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Página exibida no momento.


#### PageVersion

```csharp
public int PageVersion { get; }
```

Incrementa cada vez que a página muda.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Se está aberto.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

Se está aguardando uma escolha.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Motivo do fechamento. null enquanto estiver aberto.

### Construtores

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Uma sessão exibindo a primeira página. Normalmente criada por `HorayModAPI.TryShowDialogue`.

### Métodos

#### Show

```csharp
public bool Show(DialoguePage page)
```

Exibe a próxima página. false se a sessão estiver fechada.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Chamado pela view para repassar a entrada de escolha.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Fecha o diálogo.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Identifica por que um diálogo foi fechado.

| Valor | Descrição |
|---|---|
| `Completed` | Concluído normalmente |
| `Cancelled` | ESC/cancelar |
| `Interrupted` | Interrupção externa, como uma mudança de andar ou a desativação do painel |
| `TargetUnavailable` | A âncora deixou de existir |
| `PlayerUnavailable` | Sem jogador, jogador morreu ou o combate começou |
| `Error` | Exceção em callback |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Configurações passadas ao abrir um painel de interação com itens. Quem chama cuida da validação, do consumo e da rede dos itens.

### Constantes e campos

#### Target

```csharp
public Transform Target
```

Alvo da interação (obrigatório).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab do conteúdo do lado esquerdo. Defina este ou `BuildContent`, não ambos.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Constrói o conteúdo do lado esquerdo pelo código. Defina este ou `ContentPrefab`, não ambos.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filtro de itens selecionáveis.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Callback de clique em item. Se omitido, o clique esquerdo alterna a seleção e o clique direito desmarca.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Chamado em `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Chamado depois do fechamento.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Número máximo de itens selecionados.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

Se deve usar a borda e o preenchimento padrão. Desative se o conteúdo desenha a própria imagem.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handle para um painel de interação com itens aberto. Torna-se inválido ao ser fechado ou quando a cena muda.

### Eventos

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Chamado depois que a seleção muda.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Chamado na confirmação.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Chamado depois do fechamento.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Recebido pelo painel quando o foco padrão muda.

### Propriedades

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Jogador local.


#### Target

```csharp
public Transform Target { get; }
```

Alvo da interação.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Fábrica de widgets.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

Se está aberto.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Lista de itens selecionados.

### Métodos

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Se o item está selecionado.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Se o item pode ser selecionado.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Seleciona ou desmarca um item.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Alterna a seleção de um item.


#### ClearSelection

```csharp
public void ClearSelection()
```

Desmarca todos os itens.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Verifica novamente a posição e o estado dos itens selecionados depois de uma sincronização do inventário.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Trata a entrada de item. Enquanto o painel está aberto, a entrada não é repassada às ações de usar, mover ou vender do inventário.


#### Confirm

```csharp
public void Confirm()
```

Confirma a seleção. Chama `OnConfirm` e `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Define o alvo de foco padrão para teclado/controle.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Fecha o painel.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Instantâneo de um item possuído. Ao consumi-lo no servidor, procure-o novamente pelo ID de instância.

### Propriedades

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Instantâneo dos metadados do item.


#### Position

```csharp
public ItemPosition Position { get; }
```

Posição na grade.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Onde o item está localizado.


#### Rotation

```csharp
public int Rotation { get; }
```

Rotação do ícone.


#### InstanceID

```csharp
public int InstanceID { get; }
```

ID de instância.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Definição do item. null se não houver.

### Construtores

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Criado pelo painel.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Identifica por que uma interação com itens foi fechada.

| Valor | Descrição |
|---|---|
| `Cancelled` | ESC/cancelar |
| `TargetLost` | O alvo deixou de existir |
| `PlayerLost` | Sem jogador, jogador morreu ou desconectou |
| `InventoryClosed` | O inventário foi fechado |
| `Error` | Exceção em callback |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Identifica onde um item está localizado.

| Valor | Descrição |
|---|---|
| `Inventory` | Inventário em grade |
| `SubBag` | Bolsa secundária |
