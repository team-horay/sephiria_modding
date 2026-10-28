<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# Stats spéciales

Les types d'affichage de stats enregistrés avec `RegisterSpecialStatCategory` et `RegisterSpecialStat` dans `HorayModAPI.OnLoadSpecialStatDatabase`.

Généré le 2026-10-26 pour la version **1.1.0** du jeu. [Index de la référence](README.md)

| Type | Description |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Informations sur l'avatar cible transmises aux fonctions de lecture de valeur. |
| [`SpecialStatValue`](#specialstatvalue) | La valeur d'une ligne de stat spéciale. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Données d'une catégorie de stats spéciales dans l'interface des stats. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Indique quand une catégorie est affichée. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Données détaillées pour `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Indique d'où est lue la valeur affichée d'une stat spéciale. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Couleur du nom d'une ligne de stat spéciale. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Une ressource qui ne lit que la valeur affichée, sans StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Informations sur l'avatar cible transmises aux fonctions de lecture de valeur.

### Propriétés

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Unité cible


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

L'AvatarStatsHooker de la cible. Null s'il n'y en a pas.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Le contrôleur d'armes de la cible. Null s'il n'y en a pas.

### Constructeurs

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Si hooker ou weapons est omis, il est recherché à partir d'avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

La valeur d'une ligne de stat spéciale. Créez des valeurs numériques avec `Number`.

### Constantes et champs

#### text

```csharp
public string text
```

Chaîne affichée


#### hasStat

```csharp
public bool hasStat
```

S'il y a une valeur


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Couleur d'augmentation/diminution


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Si `numericValue` est valide


#### numericValue

```csharp
public double numericValue
```

Nombre à afficher


#### numberFormat

```csharp
public string numberFormat
```

Format d'affichage du nombre


#### symbolKey

```csharp
public string symbolKey
```

Clé de localisation du symbole d'unité


#### symbol

```csharp
public string symbol
```

Chaîne du symbole d'unité


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Valeurs de substitution des mots-clés pour l'infobulle

### Méthodes

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Crée une valeur numérique.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Données d'une catégorie de stats spéciales dans l'interface des stats.

### Constantes et champs

#### id

```csharp
public string id
```

Identifiant


#### enabled

```csharp
public bool enabled
```

Si la catégorie doit être affichée


#### displayName

```csharp
public LocalizedString displayName
```

Clé de localisation du titre


#### fallbackName

```csharp
public string fallbackName
```

Titre utilisé en l'absence de traduction


#### headerColor

```csharp
public Color headerColor
```

Couleur du titre


#### order

```csharp
public int order
```

Ordre de tri


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Condition d'affichage


#### requireWeapon

```csharp
public bool requireWeapon
```

S'il faut l'afficher uniquement lorsqu'une arme spécifique est équipée


#### weaponType

```csharp
public EWeaponType weaponType
```

Type d'arme requis lorsque `requireWeapon` est défini

### Propriétés

#### DisplayName

```csharp
public string DisplayName { get; }
```

Titre traduit


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Indique quand une catégorie est affichée.

| Valeur | Description |
|---|---|
| `Always` | Toujours |
| `WhenAnyStatHasValue` | Uniquement lorsqu'au moins une ligne a une valeur |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Données détaillées pour `StatusEntity.specialStat`. Détermine la façon dont cette stat est affichée dans l'interface des stats et dans l'interface de l'historique des parties.

### Constantes et champs

#### enabled

```csharp
public bool enabled
```

Si elle doit être affichée dans le panneau


#### category

```csharp
public SpecialStatCategoryEntity category
```

Catégorie à laquelle elle appartient (obligatoire)


#### order

```csharp
public int order
```

Ordre de tri au sein de la catégorie


#### showWhenZero

```csharp
public bool showWhenZero
```

Si la ligne doit être affichée même en l'absence de valeur


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Clé de localisation d'un nom utilisé uniquement dans le panneau des stats spéciales


#### fallbackLabel

```csharp
public string fallbackLabel
```

Nom utilisé en l'absence de traduction


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Choix de la couleur du nom


#### labelColor

```csharp
public Color labelColor
```

Couleur du nom lorsque `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Comment la valeur est lue


#### valueId

```csharp
public string valueId
```

ID de recherche StatHook ou clé de stat personnalisée. Si vide, StatusEntity.id est utilisé.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Ressource qui lit la valeur lorsque `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Diviseur pour l'affichage


#### displayOffset

```csharp
public float displayOffset
```

Décalage ajouté pour l'affichage


#### numberFormat

```csharp
public string numberFormat
```

Format d'affichage du nombre


#### includeSign

```csharp
public bool includeSign
```

S'il faut afficher le signe


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Indique d'où est lue la valeur affichée d'une stat spéciale.

| Valeur | Description |
|---|---|
| `StatHook` | Résultat de `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Valeur de `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | Résultat de `Read` de la ressource `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Couleur du nom d'une ligne de stat spéciale.

| Valeur | Description |
|---|---|
| `Default` | Blanc |
| `Keyword` | Couleur du mot-clé associé |
| `Override` | La valeur de `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Une ressource qui ne lit que la valeur affichée, sans StatusInstance.

### Méthodes

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Lit la valeur affichée actuelle de status.
