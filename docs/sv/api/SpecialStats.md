<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# Specialstats

Statvisningstyper som registreras med `RegisterSpecialStatCategory` och `RegisterSpecialStat` i `HorayModAPI.OnLoadSpecialStatDatabase`.

Genererad 2026-10-26 för spelversion **1.1.0**. [Referensindex](README.md)

| Typ | Beskrivning |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Information om målavataren som skickas till värdeläsande funktioner. |
| [`SpecialStatValue`](#specialstatvalue) | Värdet för en specialstat-rad. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Data för en specialstat-kategori i stat-UI:t. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Anger när en kategori visas. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Detaljerade data för `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Anger varifrån en specialstats visningsvärde läses. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Färg på en specialstat-rads namn. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | En asset som bara läser visningsvärdet, utan StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Information om målavataren som skickas till värdeläsande funktioner.

### Egenskaper

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Målenhet


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

Målets AvatarStatsHooker. Null om det inte finns någon.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Målets vapenkontroller. Null om det inte finns någon.

### Konstruktorer

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Om hooker eller weapons utelämnas slås de upp från avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

Värdet för en specialstat-rad. Skapa numeriska värden med `Number`.

### Konstanter och fält

#### text

```csharp
public string text
```

Visningssträng


#### hasStat

```csharp
public bool hasStat
```

Om det finns ett värde


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Färg för ökning/minskning


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Om `numericValue` är giltigt


#### numericValue

```csharp
public double numericValue
```

Tal för visning


#### numberFormat

```csharp
public string numberFormat
```

Visningsformat för tal


#### symbolKey

```csharp
public string symbolKey
```

Lokaliseringsnyckel för enhetssymbolen


#### symbol

```csharp
public string symbol
```

Enhetssymbolsträng


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Nyckelordsersättningsvärden för verktygstipset

### Metoder

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Skapar ett numeriskt värde.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Data för en specialstat-kategori i stat-UI:t.

### Konstanter och fält

#### id

```csharp
public string id
```

Identifierare


#### enabled

```csharp
public bool enabled
```

Om kategorin ska visas


#### displayName

```csharp
public LocalizedString displayName
```

Lokaliseringsnyckel för titeln


#### fallbackName

```csharp
public string fallbackName
```

Titel som används när det inte finns någon översättning


#### headerColor

```csharp
public Color headerColor
```

Titelfärg


#### order

```csharp
public int order
```

Sorteringsordning


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Visningsvillkor


#### requireWeapon

```csharp
public bool requireWeapon
```

Om den bara ska visas när ett visst vapen är utrustat


#### weaponType

```csharp
public EWeaponType weaponType
```

Vapentyp som krävs när `requireWeapon` är satt

### Egenskaper

#### DisplayName

```csharp
public string DisplayName { get; }
```

Översatt titel


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Anger när en kategori visas.

| Värde | Beskrivning |
|---|---|
| `Always` | Alltid |
| `WhenAnyStatHasValue` | Bara när minst en rad har ett värde |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Detaljerade data för `StatusEntity.specialStat`. Avgör hur den här staten visas i stat-UI:t och UI:t för spelhistorik.

### Konstanter och fält

#### enabled

```csharp
public bool enabled
```

Om den ska visas i panelen


#### category

```csharp
public SpecialStatCategoryEntity category
```

Kategori den tillhör (obligatorisk)


#### order

```csharp
public int order
```

Sorteringsordning inom kategorin


#### showWhenZero

```csharp
public bool showWhenZero
```

Om raden ska visas även när det inte finns något värde


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Lokaliseringsnyckel för ett namn som bara används i specialstat-panelen


#### fallbackLabel

```csharp
public string fallbackLabel
```

Namn som används när det inte finns någon översättning


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Val av namnfärg


#### labelColor

```csharp
public Color labelColor
```

Namnfärg när `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Hur värdet läses


#### valueId

```csharp
public string valueId
```

Uppslags-ID för StatHook eller nyckel för custom stat. Om det är tomt används StatusEntity.id.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Asset som läser värdet när `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Divisor för visning


#### displayOffset

```csharp
public float displayOffset
```

Förskjutning som läggs till för visning


#### numberFormat

```csharp
public string numberFormat
```

Visningsformat för tal


#### includeSign

```csharp
public bool includeSign
```

Om tecknet ska visas


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Anger varifrån en specialstats visningsvärde läses.

| Värde | Beskrivning |
|---|---|
| `StatHook` | Resultat av `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Värdet av `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | `Read`-resultatet från `valueProvider`-asseten |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Färg på en specialstat-rads namn.

| Värde | Beskrivning |
|---|---|
| `Default` | Vit |
| `Keyword` | Färgen på det relaterade nyckelordet |
| `Override` | Värdet i `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

En asset som bara läser visningsvärdet, utan StatusInstance.

### Metoder

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Läser det aktuella visningsvärdet för status.
