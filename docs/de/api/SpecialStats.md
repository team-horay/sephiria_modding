<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# Spezialwerte

Typen für die Werteanzeige, die in `HorayModAPI.OnLoadSpecialStatDatabase` mit `RegisterSpecialStatCategory` und `RegisterSpecialStat` registriert werden.

Generiert am 2026-10-26 für Spielversion **1.1.0**. [Referenzindex](README.md)

| Typ | Beschreibung |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Infos zum Ziel-Avatar, die an Wertlesefunktionen übergeben werden. |
| [`SpecialStatValue`](#specialstatvalue) | Der Wert einer Spezialwert-Zeile. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Daten einer Spezialwert-Kategorie in der Werte-UI. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Legt fest, wann eine Kategorie angezeigt wird. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Detaildaten für `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Legt fest, woher der Anzeigewert eines Spezialwerts gelesen wird. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Farbe des Namens einer Spezialwert-Zeile. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Ein Asset, das nur den Anzeigewert liest, ohne StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Infos zum Ziel-Avatar, die an Wertlesefunktionen übergeben werden.

### Eigenschaften

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Zieleinheit


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

Der AvatarStatsHooker des Ziels. Null, falls keiner vorhanden.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Der Waffen-Controller des Ziels. Null, falls keiner vorhanden.

### Konstruktoren

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Wird hooker oder weapons weggelassen, wird der Wert aus avatar ermittelt.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

Der Wert einer Spezialwert-Zeile. Erstelle numerische Werte mit `Number`.

### Konstanten und Felder

#### text

```csharp
public string text
```

Anzeigestring


#### hasStat

```csharp
public bool hasStat
```

Ob ein Wert vorhanden ist


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Farbe für Erhöhung/Verringerung


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Ob `numericValue` gültig ist


#### numericValue

```csharp
public double numericValue
```

Zahl für die Anzeige


#### numberFormat

```csharp
public string numberFormat
```

Anzeigeformat der Zahl


#### symbolKey

```csharp
public string symbolKey
```

Lokalisierungsschlüssel für das Einheitensymbol


#### symbol

```csharp
public string symbol
```

String des Einheitensymbols


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Schlüsselwort-Ersetzungswerte für den Tooltip

### Methoden

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Erstellt einen numerischen Wert.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Daten einer Spezialwert-Kategorie in der Werte-UI.

### Konstanten und Felder

#### id

```csharp
public string id
```

Kennung


#### enabled

```csharp
public bool enabled
```

Ob die Kategorie angezeigt wird


#### displayName

```csharp
public LocalizedString displayName
```

Lokalisierungsschlüssel für den Titel


#### fallbackName

```csharp
public string fallbackName
```

Titel, der verwendet wird, wenn keine Übersetzung vorhanden ist


#### headerColor

```csharp
public Color headerColor
```

Titelfarbe


#### order

```csharp
public int order
```

Sortierreihenfolge


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Anzeigebedingung


#### requireWeapon

```csharp
public bool requireWeapon
```

Ob nur angezeigt wird, wenn eine bestimmte Waffe ausgerüstet ist


#### weaponType

```csharp
public EWeaponType weaponType
```

Erforderlicher Waffentyp, wenn `requireWeapon` gesetzt ist

### Eigenschaften

#### DisplayName

```csharp
public string DisplayName { get; }
```

Übersetzter Titel


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Legt fest, wann eine Kategorie angezeigt wird.

| Wert | Beschreibung |
|---|---|
| `Always` | Immer |
| `WhenAnyStatHasValue` | Nur wenn mindestens eine Zeile einen Wert hat |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Detaildaten für `StatusEntity.specialStat`. Bestimmt, wie dieser Wert in der Werte-UI und der Durchlauf-Historie angezeigt wird.

### Konstanten und Felder

#### enabled

```csharp
public bool enabled
```

Ob er im Panel angezeigt wird


#### category

```csharp
public SpecialStatCategoryEntity category
```

Kategorie, zu der er gehört (erforderlich)


#### order

```csharp
public int order
```

Sortierreihenfolge innerhalb der Kategorie


#### showWhenZero

```csharp
public bool showWhenZero
```

Ob die Zeile auch ohne Wert angezeigt wird


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Lokalisierungsschlüssel für einen Namen, der nur im Spezialwerte-Panel verwendet wird


#### fallbackLabel

```csharp
public string fallbackLabel
```

Name, der verwendet wird, wenn keine Übersetzung vorhanden ist


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Wahl der Namensfarbe


#### labelColor

```csharp
public Color labelColor
```

Namensfarbe bei `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Wie der Wert gelesen wird


#### valueId

```csharp
public string valueId
```

StatHook-Such-ID oder Custom-Stat-Schlüssel. Ist sie leer, wird StatusEntity.id verwendet.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Asset, das den Wert bei `SpecialStatValueSource.Provider` liest


#### divideForDisplay

```csharp
public float divideForDisplay
```

Divisor für die Anzeige


#### displayOffset

```csharp
public float displayOffset
```

Für die Anzeige addierter Versatz


#### numberFormat

```csharp
public string numberFormat
```

Anzeigeformat der Zahl


#### includeSign

```csharp
public bool includeSign
```

Ob das Vorzeichen angezeigt wird


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Legt fest, woher der Anzeigewert eines Spezialwerts gelesen wird.

| Wert | Beschreibung |
|---|---|
| `StatHook` | Ergebnis von `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Wert von `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | `Read`-Ergebnis des `valueProvider`-Assets |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Farbe des Namens einer Spezialwert-Zeile.

| Wert | Beschreibung |
|---|---|
| `Default` | Weiß |
| `Keyword` | Farbe des zugehörigen Schlüsselworts |
| `Override` | Der Wert von `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Ein Asset, das nur den Anzeigewert liest, ohne StatusInstance.

### Methoden

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Liest den aktuellen Anzeigewert von status.
