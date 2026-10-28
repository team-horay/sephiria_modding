<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# Estadísticas especiales

Tipos de visualización de estadísticas que se registran con `RegisterSpecialStatCategory` y `RegisterSpecialStat` en `HorayModAPI.OnLoadSpecialStatDatabase`.

Generado el 2026-10-26 para la versión del juego **1.1.0**. [Índice de la referencia](README.md)

| Tipo | Descripción |
|---|---|
| [`SpecialStatContext`](#specialstatcontext) | Información del avatar objetivo que se pasa a las funciones de lectura de valores. |
| [`SpecialStatValue`](#specialstatvalue) | El valor de una fila de estadística especial. |
| [`SpecialStatCategoryEntity`](#specialstatcategoryentity) | Datos de una categoría de estadísticas especiales en la UI de estadísticas. |
| [`SpecialStatCategoryVisibility`](#specialstatcategoryvisibility) | Identifica cuándo se muestra una categoría. |
| [`SpecialStatDisplaySettings`](#specialstatdisplaysettings) | Datos detallados de `StatusEntity.specialStat`. |
| [`SpecialStatValueSource`](#specialstatvaluesource) | Identifica de dónde se lee el valor mostrado de una estadística especial. |
| [`SpecialStatLabelColor`](#specialstatlabelcolor) | Color del nombre de una fila de estadística especial. |
| [`SpecialStatValueProvider`](#specialstatvalueprovider) | Un asset que lee solo el valor mostrado, sin un StatusInstance. |


## SpecialStatContext

```csharp
public sealed class SpecialStatContext
```

Información del avatar objetivo que se pasa a las funciones de lectura de valores.

### Propiedades

#### Avatar

```csharp
public UnitAvatar Avatar { get; }
```

Unidad objetivo


#### Hooker

```csharp
public AvatarStatsHooker Hooker { get; }
```

El AvatarStatsHooker del objetivo. Null si no tiene.


#### Weapons

```csharp
public WeaponControllerSimple Weapons { get; }
```

El controlador de armas del objetivo. Null si no tiene.

### Constructores

#### SpecialStatContext

```csharp
public SpecialStatContext(UnitAvatar avatar, AvatarStatsHooker hooker = null, WeaponControllerSimple weapons = null)
```

Si se omite hooker o weapons, se busca a partir de avatar.


## SpecialStatValue

```csharp
public struct SpecialStatValue
```

El valor de una fila de estadística especial. Crea valores numéricos con `Number`.

### Constantes y campos

#### text

```csharp
public string text
```

Cadena para mostrar


#### hasStat

```csharp
public bool hasStat
```

Si hay un valor


#### sign

```csharp
public AvatarStatsHooker.EStatValueSign sign
```

Color de aumento/disminución


#### hasNumericValue

```csharp
public bool hasNumericValue
```

Si `numericValue` es válido


#### numericValue

```csharp
public double numericValue
```

Número para mostrar


#### numberFormat

```csharp
public string numberFormat
```

Formato de visualización del número


#### symbolKey

```csharp
public string symbolKey
```

Clave de localización del símbolo de la unidad


#### symbol

```csharp
public string symbol
```

Cadena del símbolo de la unidad


#### tooltipValues

```csharp
public Loc.KeywordValue[] tooltipValues
```

Valores de sustitución de palabras clave para la descripción emergente

### Métodos

#### Number

```csharp
public static SpecialStatValue Number(double number, bool hasStat, AvatarStatsHooker.EStatValueSign sign, string symbol = "", string format = "0.#", string symbolKey = "")
```

Crea un valor numérico.


## SpecialStatCategoryEntity

```csharp
public class SpecialStatCategoryEntity : ScriptableObject
```

Datos de una categoría de estadísticas especiales en la UI de estadísticas.

### Constantes y campos

#### id

```csharp
public string id
```

Identificador


#### enabled

```csharp
public bool enabled
```

Si se muestra la categoría


#### displayName

```csharp
public LocalizedString displayName
```

Clave de localización del título


#### fallbackName

```csharp
public string fallbackName
```

Título que se usa cuando no hay traducción


#### headerColor

```csharp
public Color headerColor
```

Color del título


#### order

```csharp
public int order
```

Orden de clasificación


#### visibility

```csharp
public SpecialStatCategoryVisibility visibility
```

Condición de visualización


#### requireWeapon

```csharp
public bool requireWeapon
```

Si se muestra solo cuando hay equipada un arma concreta


#### weaponType

```csharp
public EWeaponType weaponType
```

Tipo de arma necesario cuando `requireWeapon` está activado

### Propiedades

#### DisplayName

```csharp
public string DisplayName { get; }
```

Título traducido


## SpecialStatCategoryVisibility

```csharp
public enum SpecialStatCategoryVisibility
```

Identifica cuándo se muestra una categoría.

| Valor | Descripción |
|---|---|
| `Always` | Siempre |
| `WhenAnyStatHasValue` | Solo cuando al menos una fila tiene un valor |


## SpecialStatDisplaySettings

```csharp
public class SpecialStatDisplaySettings
```

Datos detallados de `StatusEntity.specialStat`. Determina cómo se muestra esta estadística en la UI de estadísticas y en la UI del historial de partidas.

### Constantes y campos

#### enabled

```csharp
public bool enabled
```

Si se muestra en el panel


#### category

```csharp
public SpecialStatCategoryEntity category
```

Categoría a la que pertenece (obligatorio)


#### order

```csharp
public int order
```

Orden de clasificación dentro de la categoría


#### showWhenZero

```csharp
public bool showWhenZero
```

Si se muestra la fila incluso cuando no hay valor


#### labelOverride

```csharp
public LocalizedString labelOverride
```

Clave de localización de un nombre que se usa solo en el panel de estadísticas especiales


#### fallbackLabel

```csharp
public string fallbackLabel
```

Nombre que se usa cuando no hay traducción


#### labelColorMode

```csharp
public SpecialStatLabelColor labelColorMode
```

Elección del color del nombre


#### labelColor

```csharp
public Color labelColor
```

Color del nombre cuando es `SpecialStatLabelColor.Override`


#### valueSource

```csharp
public SpecialStatValueSource valueSource
```

Cómo se lee el valor


#### valueId

```csharp
public string valueId
```

ID de búsqueda de StatHook o clave de estadística personalizada. Si está vacío, se usa StatusEntity.id.


#### valueProvider

```csharp
public SpecialStatValueProvider valueProvider
```

Asset que lee el valor cuando es `SpecialStatValueSource.Provider`


#### divideForDisplay

```csharp
public float divideForDisplay
```

Divisor para la visualización


#### displayOffset

```csharp
public float displayOffset
```

Desplazamiento que se suma para la visualización


#### numberFormat

```csharp
public string numberFormat
```

Formato de visualización del número


#### includeSign

```csharp
public bool includeSign
```

Si se muestra el signo


## SpecialStatValueSource

```csharp
public enum SpecialStatValueSource
```

Identifica de dónde se lee el valor mostrado de una estadística especial.

| Valor | Descripción |
|---|---|
| `StatHook` | Resultado de `AvatarStatsHooker.HookStat(valueId)` |
| `CustomStat` | Valor de `UnitAvatar.GetCustomStat(valueId)` |
| `Provider` | Resultado de `Read` del asset `valueProvider` |


## SpecialStatLabelColor

```csharp
public enum SpecialStatLabelColor
```

Color del nombre de una fila de estadística especial.

| Valor | Descripción |
|---|---|
| `Default` | Blanco |
| `Keyword` | Color de la palabra clave relacionada |
| `Override` | El valor de `labelColor` |


## SpecialStatValueProvider

```csharp
public abstract class SpecialStatValueProvider : ScriptableObject
```

Un asset que lee solo el valor mostrado, sin un StatusInstance.

### Métodos

#### Read

```csharp
public abstract SpecialStatValue Read(SpecialStatContext context, StatusEntity status)
```

Lee el valor mostrado actual de status.
