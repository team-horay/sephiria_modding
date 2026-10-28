<!-- Auto-generated file. Tools/ModApiDocGen in the game repo builds it from /// comments in the source. Don't edit directly; fix the source comments and regenerate. -->
# Special Stats

Stat display types registered with `RegisterSpecialStatCategory` and `RegisterSpecialStat` in `HorayModAPI.OnLoadSpecialStatDatabase`.

Generated 2026-10-26 for game version **1.1.0**. [Reference index](README.md)

| Type | Description |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Target avatar info passed to value-reading functions. |
| [`SpecialStatValue`](#specialstatvalue) | The value of one special stat row. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Data for a Special Stats category in the stats UI. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Identifies when a category is shown. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Detailed data for `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Identifies where a special stat's display value is read from. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Color of a special stat row's name. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | An asset that reads only the display value, without a StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Target avatar info passed to value-reading functions.

### Properties

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Target unit


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

The target's AvatarStatsHooker. Null if none.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

The target's weapon controller. Null if none.

### Constructors

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

If hooker or weapons is omitted, it is looked up from avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

The value of one special stat row. Create numeric values with `Number`.

### Constants and Fields

#### text

```csharp
public string text
```

Display string


#### hasStat

```csharp
public bool hasStat
```

Whether there is a value


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Increase/decrease color


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Whether `numericValue` is valid


#### numericValue

```csharp
public double numericValue
```

Number for display


#### numberFormat

```csharp
public string numberFormat
```

Number display format


#### symbolKey

```csharp
public string symbolKey
```

Localization key for the unit symbol


#### symbol

```csharp
public string symbol
```

Unit symbol string


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Keyword substitution values for the tooltip

### Methods

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Creates a numeric value.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Data for a Special Stats category in the stats UI.

### Constants and Fields

#### id

```csharp
public string id
```

Identifier


#### enabled

```csharp
public bool enabled
```

Whether to show the category


#### displayName

```csharp
public LocalizedString displayName
```

Localization key for the title


#### fallbackName

```csharp
public string fallbackName
```

Title used when there is no translation


#### headerColor

```csharp
public Color headerColor
```

Title color


#### order

```csharp
public int order
```

Sort order


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Display condition


#### requireWeapon

```csharp
public bool requireWeapon
```

Whether to show only when a specific weapon is equipped


#### weaponType

```csharp
public EWeaponType weaponType
```

Weapon type required when `requireWeapon` is set

### Properties

#### DisplayName

```csharp
public string DisplayName { get; }
```

Translated title


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Identifies when a category is shown.

| Value | Description |
|---|---|
| `Always` | Always |
| `WhenAnyStatHasValue` | Only when at least one row has a value |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Detailed data for `StatusEntity.specialStat`. Determines how this stat is shown in the stats UI and the run history UI.

### Constants and Fields

#### enabled

```csharp
public bool enabled
```

Whether to show it in the panel


#### category

```csharp
public SpecialStatCategoryEntity category
```

Category it belongs to (required)


#### order

```csharp
public int order
```

Sort order within the category


#### showWhenZero

```csharp
public bool showWhenZero
```

Whether to show the row even when there is no value


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Localization key for a name used only in the Special Stats panel


#### fallbackLabel

```csharp
public string fallbackLabel
```

Name used when there is no translation


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Name color choice


#### labelColor

```csharp
public Color labelColor
```

Name color when `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

How the value is read


#### valueId

```csharp
public string valueId
```

StatHook lookup ID or custom stat key. If empty, StatusEntity.id is used.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Asset that reads the value when `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Divisor for display


#### displayOffset

```csharp
public float displayOffset
```

Offset added for display


#### numberFormat

```csharp
public string numberFormat
```

Number display format


#### includeSign

```csharp
public bool includeSign
```

Whether to show the sign


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Identifies where a special stat's display value is read from.

| Value | Description |
|---|---|
| `StatHook` | Result of `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Value of `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | `Read` result of the `valueProvider` asset |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Color of a special stat row's name.

| Value | Description |
|---|---|
| `Default` | White |
| `Keyword` | Color of the related keyword |
| `Override` | The `labelColor` value |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

An asset that reads only the display value, without a StatusInstance.

### Methods

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Reads the current display value of status.
