<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# Options de mod

API pour les paramètres propres à chaque mod enregistrés par appareil, et pour ajouter des interrupteurs, des listes de sélection et des curseurs à l'écran des options du jeu. Obtenez-la via `Context.Options` ou `HorayModAPI.GetModOptions(modId)`.

Généré le 2026-10-26 pour la version **1.1.0** du jeu. [Index de la référence](README.md)

| Type | Description |
|---|---|
| [`ModOptions`](#modoptions) | Valeurs d'options que chaque mod peut définir. |
| [`ModOptionsPage`](#modoptionspage) | La page d'un mod sur l'écran des options. |
| [`ModOptionDefinition`](#modoptiondefinition) | Définition commune d'un élément de l'écran des options. |
| [`ModOption<T>`](#modoptiont) | Un élément d'option qui contient une valeur. |
| [`ModToggleOption`](#modtoggleoption) | Une option marche/arrêt. |
| [`ModSelectionOption`](#modselectionoption) | Une option de liste de sélection. |
| [`ModSliderOption`](#modslideroption) | Une option de curseur. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Type de ligne sur l'écran des options. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Liste des pages de mods lues par l'écran des options. |


## ModOptions

```csharp
public sealed class ModOptions
```

Valeurs d'options que chaque mod peut définir.

### Propriétés

#### ModId

```csharp
public string ModId { get; }
```

Le modId de cet ensemble d'options


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Si le stockage est initialisé


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Nom du mod affiché sur l'écran des options. Pour les mods du Steam Workshop, vaut par défaut le nom affiché du manifeste.

### Méthodes

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Valeur bool enregistrée. Renvoie fallback si elle est absente ou d'un autre type.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Valeur int enregistrée. Renvoie fallback si elle est absente ou d'un autre type.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Valeur long enregistrée. Renvoie fallback si elle est absente ou d'un autre type.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Valeur float enregistrée. Renvoie fallback si elle est absente ou d'un autre type.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Valeur string enregistrée. Renvoie fallback si elle est absente ou d'un autre type.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Écrit une valeur bool en mémoire.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Écrit une valeur int en mémoire.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Écrit une valeur long en mémoire.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Écrit une valeur float en mémoire.


#### SetString

```csharp
public void SetString(string key, string value)
```

Écrit une valeur string en mémoire. N'accepte pas null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Si une valeur est enregistrée sous key


#### Remove

```csharp
public bool Remove(string key)
```

Supprime la valeur enregistrée, de sorte que les lectures suivantes renvoient fallback.


#### Reset

```csharp
public int Reset()
```

Efface toutes les options de ce mod. Renvoie le nombre d'options supprimées.


#### Save

```csharp
public bool Save()
```

Enregistre le fichier d'options (DeviceOptions). Set*/Remove/Reset ne modifient que la mémoire ; appelez donc cette méthode pour les écrire dans le fichier. Renvoie false s'il n'y a pas de stockage ou si l'écriture échoue.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Ajoute un interrupteur à la page de ce mod sur l'écran des options. Remplace tout élément ayant la même clé.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Ajoute une liste de sélection. La valeur est un index dans choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Ajoute une liste de sélection dont les choix sont donnés sous forme de clés de localisation.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Ajoute un curseur. Si step vaut 0, la valeur est continue.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Retire tous les éléments de ce mod de l'écran des options. Les valeurs enregistrées sont conservées.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

La page d'un mod sur l'écran des options. Ne contient que des définitions d'interface ; les valeurs enregistrées sont conservées séparément.

### Propriétés

#### Options

```csharp
public ModOptions Options { get; }
```

Les options de mod de cette page


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Nom du mod affiché sur l'écran des options


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Éléments dans l'ordre d'enregistrement


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Définition commune d'un élément de l'écran des options. Créée par `ModOptions.Add*`.

### Propriétés

#### Options

```csharp
public ModOptions Options { get; }
```

Les options de mod auxquelles cet élément appartient


#### Key

```csharp
public string Key { get; }
```

Clé de stockage


#### Label

```csharp
public ModOptionText Label { get; }
```

Nom affiché. Si vide, la clé est affichée.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Type de ligne

### Constructeurs

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Valide key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Un élément d'option qui contient une valeur. Conservez le handle pour lire et modifier la valeur depuis le code.

### Événements

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Appelé avec la nouvelle valeur après un changement de valeur.

### Propriétés

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Valeur par défaut donnée à l'enregistrement


#### Value

```csharp
public T Value { get; set; }
```

Valeur actuelle. La définir stocke la valeur en mémoire et lève `ValueChanged` si elle a changé.

### Constructeurs

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged devient le premier abonné de `ValueChanged`.

### Méthodes

#### Read

```csharp
protected abstract T Read()
```

Lit la valeur depuis le stockage.


#### Write

```csharp
protected abstract void Write(T value)
```

Écrit la valeur dans la mémoire du stockage.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Borne la valeur à la plage autorisée avant l'écriture.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Une option marche/arrêt.

### Propriétés

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Toujours Toggle

### Méthodes

#### Read

```csharp
protected override bool Read()
```

Lit la valeur bool.


#### Write

```csharp
protected override void Write(bool value)
```

Écrit la valeur bool.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Une option de liste de sélection. La valeur est un index dans `Choices`.

### Propriétés

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Toujours SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Liste des choix

### Méthodes

#### Read

```csharp
protected override int Read()
```

Index enregistré. La valeur par défaut s'il est hors plage.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Borne à la plage des choix.


#### Write

```csharp
protected override void Write(int value)
```

Écrit l'index.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Une option de curseur.

### Propriétés

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Toujours Slider


#### Min

```csharp
public float Min { get; }
```

Valeur minimale


#### Max

```csharp
public float Max { get; }
```

Valeur maximale


#### Step

```csharp
public float Step { get; }
```

Taille du pas. Continue si 0.

### Méthodes

#### Read

```csharp
protected override float Read()
```

Valeur enregistrée, normalisée


#### Normalize

```csharp
protected override float Normalize(float value)
```

Borne à la plage et aligne sur le pas.


#### Write

```csharp
protected override void Write(float value)
```

Écrit la valeur float.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Pas encore de description.*

### Méthodes

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Affiche la chaîne telle quelle.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Traduit la clé de la LocalizedString au moment de l'affichage.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Type de ligne sur l'écran des options.

| Valeur | Description |
|---|---|
| `Toggle` | Marche/arrêt |
| `SelectionBox` | Liste de sélection |
| `Slider` | Curseur |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Liste des pages de mods lues par l'écran des options.

### Méthodes

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Renvoie les pages à afficher, triées par modId.
