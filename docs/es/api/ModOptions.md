<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# Opciones de mod

API para ajustes por mod que se guardan por dispositivo, y para añadir interruptores, cuadros de selección y deslizadores a la pantalla de opciones del juego. Obtenla desde `Context.Options` o `HorayModAPI.GetModOptions(modId)`.

Generado el 2026-10-26 para la versión del juego **1.1.0**. [Índice de la referencia](README.md)

| Tipo | Descripción |
|---|---|
| [`ModOptions`](#modoptions) | Valores de opciones que puede definir cada mod. |
| [`ModOptionsPage`](#modoptionspage) | La página de un mod en la pantalla de opciones. |
| [`ModOptionDefinition`](#modoptiondefinition) | Definición común de un elemento de la pantalla de opciones. |
| [`ModOption<T>`](#modoptiont) | Un elemento de opción que contiene un valor. |
| [`ModToggleOption`](#modtoggleoption) | Una opción de activar/desactivar. |
| [`ModSelectionOption`](#modselectionoption) | Una opción de cuadro de selección. |
| [`ModSliderOption`](#modslideroption) | Una opción de deslizador. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Tipo de fila en la pantalla de opciones. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Lista de páginas de mods que lee la pantalla de opciones. |


## ModOptions

```csharp
public sealed class ModOptions
```

Valores de opciones que puede definir cada mod.

### Propiedades

#### ModId

```csharp
public string ModId { get; }
```

El modId de este conjunto de opciones


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Si el almacenamiento está inicializado


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Nombre del mod que se muestra en la pantalla de opciones. Para los mods del Workshop de Steam, por defecto es el nombre para mostrar del manifiesto.

### Métodos

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Valor bool guardado. Devuelve fallback si falta o es de otro tipo.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Valor int guardado. Devuelve fallback si falta o es de otro tipo.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Valor long guardado. Devuelve fallback si falta o es de otro tipo.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Valor float guardado. Devuelve fallback si falta o es de otro tipo.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Valor string guardado. Devuelve fallback si falta o es de otro tipo.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Escribe un valor bool en memoria.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Escribe un valor int en memoria.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Escribe un valor long en memoria.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Escribe un valor float en memoria.


#### SetString

```csharp
public void SetString(string key, string value)
```

Escribe un valor string en memoria. No acepta null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Si hay un valor guardado bajo key


#### Remove

```csharp
public bool Remove(string key)
```

Elimina el valor guardado, de modo que las lecturas posteriores devuelven fallback.


#### Reset

```csharp
public int Reset()
```

Borra todas las opciones de este mod. Devuelve el número de opciones eliminadas.


#### Save

```csharp
public bool Save()
```

Guarda el archivo de opciones (DeviceOptions). Set*/Remove/Reset solo cambian la memoria, así que llama a esto para escribirlas en el archivo. Devuelve false si no hay almacenamiento o si falla la escritura.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Añade un interruptor a la página de este mod en la pantalla de opciones. Sustituye cualquier elemento con la misma clave.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Añade un cuadro de selección. El valor es un índice dentro de choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Añade un cuadro de selección cuyas opciones se dan como claves de localización.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Añade un deslizador. Si step es 0, el valor es continuo.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Elimina todos los elementos de este mod de la pantalla de opciones. Los valores guardados se conservan.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

La página de un mod en la pantalla de opciones. Solo contiene definiciones de UI; los valores guardados se conservan por separado.

### Propiedades

#### Options

```csharp
public ModOptions Options { get; }
```

Opciones del mod de esta página


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Nombre del mod que se muestra en la pantalla de opciones


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Elementos en orden de registro


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Definición común de un elemento de la pantalla de opciones. Lo crea `ModOptions.Add*`.

### Propiedades

#### Options

```csharp
public ModOptions Options { get; }
```

Las opciones de mod a las que pertenece este elemento


#### Key

```csharp
public string Key { get; }
```

Clave de almacenamiento


#### Label

```csharp
public ModOptionText Label { get; }
```

Nombre para mostrar. Si está vacío, se muestra la clave.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Tipo de fila

### Constructores

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Valida key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Un elemento de opción que contiene un valor. Conserva el handle para leer y cambiar el valor desde código.

### Eventos

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Se llama con el nuevo valor después de que cambie el valor.

### Propiedades

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Valor por defecto dado en el registro


#### Value

```csharp
public T Value { get; set; }
```

Valor actual. Al asignarlo, se guarda el valor en memoria y se lanza `ValueChanged` si cambió.

### Constructores

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged se convierte en el primer suscriptor de `ValueChanged`.

### Métodos

#### Read

```csharp
protected abstract T Read()
```

Lee el valor del almacenamiento.


#### Write

```csharp
protected abstract void Write(T value)
```

Escribe el valor en la memoria de almacenamiento.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Limita el valor al rango permitido antes de escribirlo.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Una opción de activar/desactivar.

### Propiedades

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Siempre Toggle

### Métodos

#### Read

```csharp
protected override bool Read()
```

Lee el valor bool.


#### Write

```csharp
protected override void Write(bool value)
```

Escribe el valor bool.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Una opción de cuadro de selección. El valor es un índice dentro de `Choices`.

### Propiedades

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Siempre SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Lista de opciones

### Métodos

#### Read

```csharp
protected override int Read()
```

Índice guardado. El valor por defecto si está fuera de rango.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Limita al rango de opciones.


#### Write

```csharp
protected override void Write(int value)
```

Escribe el índice.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Una opción de deslizador.

### Propiedades

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Siempre Slider


#### Min

```csharp
public float Min { get; }
```

Valor mínimo


#### Max

```csharp
public float Max { get; }
```

Valor máximo


#### Step

```csharp
public float Step { get; }
```

Tamaño del paso. Continuo si es 0.

### Métodos

#### Read

```csharp
protected override float Read()
```

Valor guardado, normalizado


#### Normalize

```csharp
protected override float Normalize(float value)
```

Limita al rango y ajusta al paso.


#### Write

```csharp
protected override void Write(float value)
```

Escribe el valor float.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Sin descripción todavía.*

### Métodos

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Muestra la cadena tal cual.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Traduce la clave del LocalizedString en el momento de mostrarla.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Tipo de fila en la pantalla de opciones.

| Valor | Descripción |
|---|---|
| `Toggle` | Activar/desactivar |
| `SelectionBox` | Cuadro de selección |
| `Slider` | Deslizador |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Lista de páginas de mods que lee la pantalla de opciones.

### Métodos

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Devuelve las páginas que se van a mostrar, ordenadas por modId.
