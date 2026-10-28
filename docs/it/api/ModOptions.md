<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# Opzioni delle mod

API per le impostazioni di ogni mod salvate per dispositivo, e per aggiungere interruttori, caselle di selezione e cursori alla schermata delle opzioni del gioco. Ottienila da `Context.Options` o da `HorayModAPI.GetModOptions(modId)`.

Generato il 2026-10-26 per la versione del gioco **1.1.0**. [Indice del riferimento](README.md)

| Tipo | Descrizione |
|---|---|
| [`ModOptions`](#modoptions) | Valori delle opzioni che ogni mod può impostare. |
| [`ModOptionsPage`](#modoptionspage) | La pagina di una mod nella schermata delle opzioni. |
| [`ModOptionDefinition`](#modoptiondefinition) | Definizione comune di una voce della schermata delle opzioni. |
| [`ModOption<T>`](#modoptiont) | Una voce di opzione che contiene un valore. |
| [`ModToggleOption`](#modtoggleoption) | Un'opzione on/off. |
| [`ModSelectionOption`](#modselectionoption) | Un'opzione a casella di selezione. |
| [`ModSliderOption`](#modslideroption) | Un'opzione a cursore. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Tipo di riga nella schermata delle opzioni. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Elenco delle pagine delle mod lette dalla schermata delle opzioni. |


## ModOptions

```csharp
public sealed class ModOptions
```

Valori delle opzioni che ogni mod può impostare.

### Proprietà

#### ModId

```csharp
public string ModId { get; }
```

Il modId di questo insieme di opzioni


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Se lo storage è inizializzato


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Nome della mod mostrato nella schermata delle opzioni. Per le mod dello Steam Workshop, il valore predefinito è il nome visualizzato del manifest.

### Metodi

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Valore bool salvato. Restituisce fallback se manca o è di un tipo diverso.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Valore int salvato. Restituisce fallback se manca o è di un tipo diverso.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Valore long salvato. Restituisce fallback se manca o è di un tipo diverso.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Valore float salvato. Restituisce fallback se manca o è di un tipo diverso.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Valore string salvato. Restituisce fallback se manca o è di un tipo diverso.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Scrive un valore bool in memoria.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Scrive un valore int in memoria.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Scrive un valore long in memoria.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Scrive un valore float in memoria.


#### SetString

```csharp
public void SetString(string key, string value)
```

Scrive un valore string in memoria. Non accetta null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Se c'è un valore salvato sotto key


#### Remove

```csharp
public bool Remove(string key)
```

Rimuove il valore salvato, così le letture successive restituiscono fallback.


#### Reset

```csharp
public int Reset()
```

Cancella tutte le opzioni di questa mod. Restituisce il numero di valori rimossi.


#### Save

```csharp
public bool Save()
```

Salva il file delle opzioni (DeviceOptions). Set*/Remove/Reset modificano solo la memoria, quindi chiama questo metodo per scriverle su file. Restituisce false se non c'è storage o se la scrittura fallisce.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Aggiunge un interruttore alla pagina di questa mod nella schermata delle opzioni. Sostituisce qualsiasi voce con la stessa chiave.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Aggiunge una casella di selezione. Il valore è un indice in choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Aggiunge una casella di selezione le cui scelte sono fornite come chiavi di localizzazione.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Aggiunge un cursore. Se step è 0, il valore è continuo.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Rimuove tutte le voci di questa mod dalla schermata delle opzioni. I valori salvati restano.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

La pagina di una mod nella schermata delle opzioni. Contiene solo le definizioni dell'interfaccia; i valori salvati sono conservati a parte.

### Proprietà

#### Options

```csharp
public ModOptions Options { get; }
```

Le opzioni della mod di questa pagina


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Nome della mod mostrato nella schermata delle opzioni


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Voci in ordine di registrazione


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Definizione comune di una voce della schermata delle opzioni. Creata da `ModOptions.Add*`.

### Proprietà

#### Options

```csharp
public ModOptions Options { get; }
```

Le opzioni della mod a cui appartiene questa voce


#### Key

```csharp
public string Key { get; }
```

Chiave di storage


#### Label

```csharp
public ModOptionText Label { get; }
```

Nome visualizzato. Se vuoto, viene mostrata la chiave.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Tipo di riga

### Costruttori

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Valida key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Una voce di opzione che contiene un valore. Conserva l'handle per leggere e modificare il valore dal codice.

### Eventi

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Chiamato con il nuovo valore dopo che il valore è cambiato.

### Proprietà

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Valore predefinito fornito alla registrazione


#### Value

```csharp
public T Value { get; set; }
```

Valore attuale. Impostarlo memorizza il valore in memoria ed emette `ValueChanged` se è cambiato.

### Costruttori

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged diventa il primo iscritto di `ValueChanged`.

### Metodi

#### Read

```csharp
protected abstract T Read()
```

Legge il valore dallo storage.


#### Write

```csharp
protected abstract void Write(T value)
```

Scrive il valore nella memoria dello storage.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Limita il valore all'intervallo consentito prima della scrittura.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Un'opzione on/off.

### Proprietà

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Sempre Toggle

### Metodi

#### Read

```csharp
protected override bool Read()
```

Legge il valore bool.


#### Write

```csharp
protected override void Write(bool value)
```

Scrive il valore bool.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Un'opzione a casella di selezione. Il valore è un indice in `Choices`.

### Proprietà

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Sempre SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Elenco delle scelte

### Metodi

#### Read

```csharp
protected override int Read()
```

Indice salvato. Il valore predefinito se fuori intervallo.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Limita all'intervallo delle scelte.


#### Write

```csharp
protected override void Write(int value)
```

Scrive l'indice.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Un'opzione a cursore.

### Proprietà

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Sempre Slider


#### Min

```csharp
public float Min { get; }
```

Valore minimo


#### Max

```csharp
public float Max { get; }
```

Valore massimo


#### Step

```csharp
public float Step { get; }
```

Dimensione del passo. Continuo se 0.

### Metodi

#### Read

```csharp
protected override float Read()
```

Valore salvato, normalizzato


#### Normalize

```csharp
protected override float Normalize(float value)
```

Limita all'intervallo e allinea al passo.


#### Write

```csharp
protected override void Write(float value)
```

Scrive il valore float.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Nessuna descrizione per ora.*

### Metodi

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Mostra la stringa così com'è.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Traduce la chiave della LocalizedString al momento della visualizzazione.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Tipo di riga nella schermata delle opzioni.

| Valore | Descrizione |
|---|---|
| `Toggle` | On/off |
| `SelectionBox` | Casella di selezione |
| `Slider` | Cursore |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Elenco delle pagine delle mod lette dalla schermata delle opzioni.

### Metodi

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Restituisce le pagine da mostrare, ordinate per modId.
