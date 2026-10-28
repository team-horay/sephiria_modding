<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# Statistiche speciali

Tipi per la visualizzazione delle statistiche registrati con `RegisterSpecialStatCategory` e `RegisterSpecialStat` in `HorayModAPI.OnLoadSpecialStatDatabase`.

Generato il 2026-10-26 per la versione del gioco **1.1.0**. [Indice del riferimento](README.md)

| Tipo | Descrizione |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Informazioni sull'avatar di destinazione passate alle funzioni di lettura dei valori. |
| [`SpecialStatValue`](#specialstatvalue) | Il valore di una riga di statistica speciale. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Dati di una categoria di Statistiche speciali nell'interfaccia delle statistiche. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Indica quando viene mostrata una categoria. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Dati dettagliati per `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Indica da dove viene letto il valore visualizzato di una statistica speciale. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Colore del nome di una riga di statistica speciale. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Un asset che legge solo il valore da visualizzare, senza una StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Informazioni sull'avatar di destinazione passate alle funzioni di lettura dei valori.

### Proprietà

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Unità di destinazione


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

L'AvatarStatsHooker della destinazione. Null se assente.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

Il controller delle armi della destinazione. Null se assente.

### Costruttori

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Se hooker o weapons è omesso, viene ricavato da avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

Il valore di una riga di statistica speciale. Crea i valori numerici con `Number`.

### Costanti e campi

#### text

```csharp
public string text
```

Stringa da visualizzare


#### hasStat

```csharp
public bool hasStat
```

Se c'è un valore


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Colore di aumento/diminuzione


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Se `numericValue` è valido


#### numericValue

```csharp
public double numericValue
```

Numero da visualizzare


#### numberFormat

```csharp
public string numberFormat
```

Formato di visualizzazione del numero


#### symbolKey

```csharp
public string symbolKey
```

Chiave di localizzazione del simbolo dell'unità


#### symbol

```csharp
public string symbol
```

Stringa del simbolo dell'unità


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Valori di sostituzione delle parole chiave per il tooltip

### Metodi

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Crea un valore numerico.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Dati di una categoria di Statistiche speciali nell'interfaccia delle statistiche.

### Costanti e campi

#### id

```csharp
public string id
```

Identificatore


#### enabled

```csharp
public bool enabled
```

Se mostrare la categoria


#### displayName

```csharp
public LocalizedString displayName
```

Chiave di localizzazione del titolo


#### fallbackName

```csharp
public string fallbackName
```

Titolo usato quando non c'è una traduzione


#### headerColor

```csharp
public Color headerColor
```

Colore del titolo


#### order

```csharp
public int order
```

Ordine di visualizzazione


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Condizione di visualizzazione


#### requireWeapon

```csharp
public bool requireWeapon
```

Se mostrare solo quando è equipaggiata un'arma specifica


#### weaponType

```csharp
public EWeaponType weaponType
```

Tipo di arma richiesto quando `requireWeapon` è impostato

### Proprietà

#### DisplayName

```csharp
public string DisplayName { get; }
```

Titolo tradotto


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Indica quando viene mostrata una categoria.

| Valore | Descrizione |
|---|---|
| `Always` | Sempre |
| `WhenAnyStatHasValue` | Solo quando almeno una riga ha un valore |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Dati dettagliati per `StatusEntity.specialStat`. Determina come questa statistica viene mostrata nell'interfaccia delle statistiche e in quella della cronologia delle partite.

### Costanti e campi

#### enabled

```csharp
public bool enabled
```

Se mostrarla nel pannello


#### category

```csharp
public SpecialStatCategoryEntity category
```

Categoria di appartenenza (obbligatoria)


#### order

```csharp
public int order
```

Ordine di visualizzazione all'interno della categoria


#### showWhenZero

```csharp
public bool showWhenZero
```

Se mostrare la riga anche quando non c'è un valore


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Chiave di localizzazione di un nome usato solo nel pannello delle Statistiche speciali


#### fallbackLabel

```csharp
public string fallbackLabel
```

Nome usato quando non c'è una traduzione


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Scelta del colore del nome


#### labelColor

```csharp
public Color labelColor
```

Colore del nome quando `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Come viene letto il valore


#### valueId

```csharp
public string valueId
```

ID di ricerca dello StatHook o chiave della statistica personalizzata. Se vuoto, viene usato StatusEntity.id.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Asset che legge il valore quando `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Divisore per la visualizzazione


#### displayOffset

```csharp
public float displayOffset
```

Offset aggiunto per la visualizzazione


#### numberFormat

```csharp
public string numberFormat
```

Formato di visualizzazione del numero


#### includeSign

```csharp
public bool includeSign
```

Se mostrare il segno


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Indica da dove viene letto il valore visualizzato di una statistica speciale.

| Valore | Descrizione |
|---|---|
| `StatHook` | Risultato di `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Valore di `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | Risultato di `Read` dell'asset `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Colore del nome di una riga di statistica speciale.

| Valore | Descrizione |
|---|---|
| `Default` | Bianco |
| `Keyword` | Colore della parola chiave associata |
| `Override` | Il valore di `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Un asset che legge solo il valore da visualizzare, senza una StatusInstance.

### Metodi

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Legge il valore attualmente visualizzato di status.
