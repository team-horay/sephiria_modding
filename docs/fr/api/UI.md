<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# Interface

Les types de dialogue, d'interaction avec les objets et de widgets utilisés par `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` et `TryGetUIWidgets`. Il s'agit dans tous les cas d'une interface locale ; la synchronisation réseau est à la charge du mod.

Généré le 2026-10-26 pour la version **1.1.0** du jeu. [Index de la référence](README.md)

| Type | Description |
|---|---|
| [`UIWidgetFactory`](#uiwidgetfactory) | Crée des widgets d'interface au style du jeu. |
| [`UIWidgetTextStyle`](#uiwidgettextstyle) | Niveaux de taille du texte. |
| [`DialoguePage`](#dialoguepage) | Une page de dialogue (corps du texte et choix). |
| [`DialogueChoice`](#dialoguechoice) | Un choix unique. |
| [`DialogueSession`](#dialoguesession) | Handle vers un dialogue ouvert. |
| [`DialogueCloseReason`](#dialogueclosereason) | Indique pourquoi un dialogue s'est fermé. |
| [`ItemInteractionOptions`](#iteminteractionoptions) | Paramètres transmis à l'ouverture d'un panneau d'interaction avec les objets. |
| [`ItemInteractionSession`](#iteminteractionsession) | Handle vers un panneau d'interaction avec les objets ouvert. |
| [`ItemInteractionItem`](#iteminteractionitem) | Instantané d'un objet possédé. |
| [`ItemInteractionCloseReason`](#iteminteractionclosereason) | Indique pourquoi une interaction avec les objets s'est fermée. |
| [`ItemInteractionItemSource`](#iteminteractionitemsource) | Indique où se trouve un objet. |


## UIWidgetFactory

```csharp
public sealed class UIWidgetFactory
```

Crée des widgets d'interface au style du jeu.

### Constructeurs

#### UIWidgetFactory

```csharp
public UIWidgetFactory(TextMeshProUGUI textTemplate, Button buttonTemplate, UI_ItemIcon itemTemplate, Image panelTemplate = null)
```

Reprend les styles des modèles.

### Méthodes

#### CreateContainer

```csharp
public RectTransform CreateContainer(Transform parent, string name = "Container")
```

Crée un enfant RectTransform vide.


#### Stretch

```csharp
public static void Stretch(RectTransform rect)
```

Étire les ancres pour remplir tout le parent.


#### CreatePanel

```csharp
public RectTransform CreatePanel(Transform parent, string name = "Panel")
```

Crée un conteneur avec l'apparence des panneaux du jeu.


#### CreateImage

```csharp
public Image CreateImage(Transform parent, Sprite sprite, string name = "Image")
```

Crée une image qui affiche le sprite à sa taille native en pixels. Les entrées du pointeur la traversent.


#### CreateVerticalLayout

```csharp
public RectTransform CreateVerticalLayout(Transform parent, float spacing = 6, int padding = 0)
```

Crée un conteneur à disposition verticale.


#### CreateGrid

```csharp
public RectTransform CreateGrid(Transform parent, Vector2 cellSize, int columns = 4, float spacing = 4)
```

Crée un conteneur à disposition en grille.


#### CreateHorizontalLayout

```csharp
public RectTransform CreateHorizontalLayout(Transform parent, float spacing = 4)
```

Crée un conteneur à disposition horizontale.


#### CreateScrollView

```csharp
public ScrollRect CreateScrollView(Transform parent)
```

Crée une vue à défilement vertical.


#### CreateText

```csharp
public TextMeshProUGUI CreateText(Transform parent, string value, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Crée un texte utilisant la police du jeu.


#### CreateLocalizedText

```csharp
public TextMeshProUGUI CreateLocalizedText(Transform parent, string key, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Crée un texte qui affiche une clé de localisation.


#### ApplyTextStyle

```csharp
public void ApplyTextStyle(TextMeshProUGUI text, UIWidgetTextStyle style = UIWidgetTextStyle.Body)
```

Applique les règles de police du jeu à un texte existant.


#### BindLocalization

```csharp
public void BindLocalization(TextMeshProUGUI text, string key)
```

Lie une clé de localisation à un texte existant.


#### CreateButton

```csharp
public UI_HorayButton CreateButton(Transform parent, string label, UnityAction onClick)
```

Crée un bouton au style du jeu.


#### CreateLocalizedButton

```csharp
public UI_HorayButton CreateLocalizedButton(Transform parent, string key, UnityAction onClick)
```

Crée un bouton dont le libellé est une clé de localisation.


#### CreateItemIcon

```csharp
public UI_ItemIcon CreateItemIcon(Transform parent, ItemMetadata item, Action<PointerEventData.InputButton, UI_ItemIcon> onClick = null)
```

Crée une icône d'objet du jeu.


#### SetPreferredSize

```csharp
public static void SetPreferredSize(Component widget, float width, float height, float flexibleHeight = 0)
```

Définit la taille préférée du LayoutElement.


## UIWidgetTextStyle

```csharp
public enum UIWidgetTextStyle
```

Niveaux de taille du texte.

| Valeur | Description |
|---|---|
| `Body` | Corps du texte |
| `Small` | Petit texte |
| `Title` | Titre |


## DialoguePage

```csharp
public sealed class DialoguePage
```

Une page de dialogue (corps du texte et choix).

### Propriétés

#### Text

```csharp
public string Text { get; }
```

*Pas encore de description.*


#### Choices

```csharp
public IReadOnlyList<DialogueChoice> Choices { get; }
```

Liste des choix.


#### DefaultChoiceIndex

```csharp
public int DefaultChoiceIndex { get; set; }
```

Choix sur lequel placer le focus à l'ouverture de la page.


#### CanCancel

```csharp
public bool CanCancel { get; set; }
```

Si Échap/annuler peut la fermer.

### Constructeurs

#### DialoguePage

```csharp
public DialoguePage(string text, params DialogueChoice[] choices)
```

Crée une page à partir d'un texte littéral.


#### DialoguePage

```csharp
public DialoguePage(LocalizedString text, params DialogueChoice[] choices)
```

Crée une page dont le corps est une clé de localisation.


## DialogueChoice

```csharp
public sealed class DialogueChoice
```

Un choix unique. Le sélectionner appelle onSelected, qui affiche la page suivante ou ferme le dialogue.

### Propriétés

#### Text

```csharp
public string Text { get; }
```

Texte affiché. Les clés de localisation sont traduites à la lecture.


#### Interactable

```csharp
public bool Interactable { get; }
```

S'il peut être sélectionné.

### Constructeurs

#### DialogueChoice

```csharp
public DialogueChoice(string text, Action<DialogueSession> onSelected, bool interactable = true)
```

Crée un choix à partir d'un texte littéral.


#### DialogueChoice

```csharp
public DialogueChoice(LocalizedString text, Action<DialogueSession> onSelected, bool interactable = true)
```

Crée un choix à partir d'une clé de localisation.


## DialogueSession

```csharp
public sealed class DialogueSession
```

Handle vers un dialogue ouvert. `Show` affiche la page suivante et `Close` y met fin.

### Événements

#### Closed

```csharp
public event Action<DialogueCloseReason> Closed
```

Appelé une fois après la fermeture.

### Propriétés

#### CurrentPage

```csharp
public DialoguePage CurrentPage { get; }
```

Page actuellement affichée.


#### PageVersion

```csharp
public int PageVersion { get; }
```

S'incrémente à chaque changement de page.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

S'il est ouvert.


#### IsWaitingForChoice

```csharp
public bool IsWaitingForChoice { get; }
```

S'il attend un choix.


#### CloseReason

```csharp
public DialogueCloseReason? CloseReason { get; }
```

Raison de la fermeture. null tant qu'il est ouvert.

### Constructeurs

#### DialogueSession

```csharp
public DialogueSession(DialoguePage firstPage)
```

Une session affichant la première page. Généralement créée par `HorayModAPI.TryShowDialogue`.

### Méthodes

#### Show

```csharp
public bool Show(DialoguePage page)
```

Affiche la page suivante. false si la session est fermée.


#### TrySelect

```csharp
public bool TrySelect(int choiceIndex, int pageVersion)
```

Appelé par la vue pour transmettre l'entrée de choix.


#### Close

```csharp
public void Close(DialogueCloseReason reason = DialogueCloseReason.Completed)
```

Ferme le dialogue.


## DialogueCloseReason

```csharp
public enum DialogueCloseReason
```

Indique pourquoi un dialogue s'est fermé.

| Valeur | Description |
|---|---|
| `Completed` | Terminé normalement |
| `Cancelled` | Échap/annuler |
| `Interrupted` | Interruption externe, comme un changement d'étage ou la désactivation du panneau |
| `TargetUnavailable` | L'ancre a disparu |
| `PlayerUnavailable` | Pas de joueur, joueur mort, ou combat commencé |
| `Error` | Exception dans un rappel |


## ItemInteractionOptions

```csharp
public sealed class ItemInteractionOptions
```

Paramètres transmis à l'ouverture d'un panneau d'interaction avec les objets. L'appelant gère la validation, la consommation et la mise en réseau des objets.

### Constantes et champs

#### Target

```csharp
public Transform Target
```

Cible de l'interaction (obligatoire).


#### ContentPrefab

```csharp
public RectTransform ContentPrefab
```

Prefab du contenu de gauche. Définissez soit celui-ci, soit `BuildContent`, pas les deux.


#### BuildContent

```csharp
public Action<RectTransform, ItemInteractionSession> BuildContent
```

Construit le contenu de gauche en code. Définissez soit celui-ci, soit `ContentPrefab`, pas les deux.


#### CanSelectItem

```csharp
public Func<ItemInteractionItem, bool> CanSelectItem
```

Filtre des objets sélectionnables.


#### OnItemClicked

```csharp
public Action<ItemInteractionSession, ItemInteractionItem, PointerEventData.InputButton> OnItemClicked
```

Rappel au clic sur un objet. S'il est omis, le clic gauche bascule la sélection et le clic droit désélectionne.


#### OnConfirm

```csharp
public Action<ItemInteractionSession> OnConfirm
```

Appelé lors de `Confirm`.


#### OnClosed

```csharp
public Action<ItemInteractionCloseReason> OnClosed
```

Appelé après la fermeture.


#### MaxSelectedItems

```csharp
public int MaxSelectedItems
```

Nombre maximal d'objets sélectionnés.


#### UseDefaultFrame

```csharp
public bool UseDefaultFrame
```

S'il faut utiliser la bordure et la marge par défaut. Désactivez-le si le contenu dessine sa propre image.


## ItemInteractionSession

```csharp
public sealed class ItemInteractionSession
```

Handle vers un panneau d'interaction avec les objets ouvert. Devient invalide à la fermeture ou lors d'un changement de scène.

### Événements

#### SelectionChanged

```csharp
public event Action SelectionChanged
```

Appelé après un changement de sélection.


#### Confirmed

```csharp
public event Action<ItemInteractionSession> Confirmed
```

Appelé à la confirmation.


#### Closed

```csharp
public event Action<ItemInteractionCloseReason> Closed
```

Appelé après la fermeture.


#### DefaultSelectionChanged

```csharp
public event Action<GameObject> DefaultSelectionChanged
```

Reçu par le panneau lorsque le focus par défaut change.

### Propriétés

#### Player

```csharp
public PlayerAvatar Player { get; }
```

Joueur local.


#### Target

```csharp
public Transform Target { get; }
```

Cible de l'interaction.


#### UI

```csharp
public UIWidgetFactory UI { get; }
```

Fabrique de widgets.


#### IsOpen

```csharp
public bool IsOpen { get; }
```

S'il est ouvert.


#### SelectedItems

```csharp
public IReadOnlyList<ItemInteractionItem> SelectedItems { get; }
```

Liste des objets sélectionnés.

### Méthodes

#### IsSelected

```csharp
public bool IsSelected(int instanceID)
```

Si l'objet est sélectionné.


#### CanSelect

```csharp
public bool CanSelect(ItemInteractionItem item)
```

Si l'objet peut être sélectionné.


#### SetSelected

```csharp
public bool SetSelected(int instanceID, bool value)
```

Sélectionne ou désélectionne un objet.


#### ToggleSelection

```csharp
public bool ToggleSelection(int instanceID)
```

Bascule la sélection d'un objet.


#### ClearSelection

```csharp
public void ClearSelection()
```

Désélectionne tous les objets.


#### RefreshSelection

```csharp
public void RefreshSelection()
```

Revérifie la position et l'état des objets sélectionnés après une synchronisation de l'inventaire.


#### HandleItemInput

```csharp
public bool HandleItemInput(int instanceID, PointerEventData.InputButton button)
```

Gère l'entrée sur un objet. Tant que le panneau est ouvert, l'entrée n'est pas transmise aux actions d'utilisation, de déplacement ou de vente de l'inventaire.


#### Confirm

```csharp
public void Confirm()
```

Confirme la sélection. Appelle `OnConfirm` et `Confirmed`.


#### SetDefaultSelection

```csharp
public void SetDefaultSelection(GameObject selectable)
```

Définit la cible du focus par défaut pour le clavier/la manette.


#### Close

```csharp
public void Close(ItemInteractionCloseReason reason = ItemInteractionCloseReason.Cancelled)
```

Ferme le panneau.


## ItemInteractionItem

```csharp
public readonly struct ItemInteractionItem
```

Instantané d'un objet possédé. Lors de sa consommation sur le serveur, recherchez-le à nouveau par son ID d'instance.

### Propriétés

#### Metadata

```csharp
public ItemMetadata Metadata { get; }
```

Instantané des métadonnées de l'objet.


#### Position

```csharp
public ItemPosition Position { get; }
```

Position dans la grille.


#### Source

```csharp
public ItemInteractionItemSource Source { get; }
```

Où se trouve l'objet.


#### Rotation

```csharp
public int Rotation { get; }
```

Rotation de l'icône.


#### InstanceID

```csharp
public int InstanceID { get; }
```

ID d'instance.


#### Entity

```csharp
public ItemEntity Entity { get; }
```

Définition de l'objet. null s'il n'y en a pas.

### Constructeurs

#### ItemInteractionItem

```csharp
public ItemInteractionItem(ItemMetadata metadata, ItemPosition position, ItemInteractionItemSource source = ItemInteractionItemSource.Inventory, int rotation = 0)
```

Créé par le panneau.


## ItemInteractionCloseReason

```csharp
public enum ItemInteractionCloseReason
```

Indique pourquoi une interaction avec les objets s'est fermée.

| Valeur | Description |
|---|---|
| `Cancelled` | Échap/annuler |
| `TargetLost` | La cible a disparu |
| `PlayerLost` | Pas de joueur, joueur mort, ou déconnecté |
| `InventoryClosed` | L'inventaire a été fermé |
| `Error` | Exception dans un rappel |


## ItemInteractionItemSource

```csharp
public enum ItemInteractionItemSource
```

Indique où se trouve un objet.

| Valeur | Description |
|---|---|
| `Inventory` | Inventaire en grille |
| `SubBag` | Sacoche latérale |
