<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# Mod-Optionen

API für Mod-Einstellungen, die pro Gerät gespeichert werden, und zum Hinzufügen von Schaltern, Auswahlfeldern und Schiebereglern zum Optionsbildschirm des Spiels. Abrufbar über `Context.Options` oder `HorayModAPI.GetModOptions(modId)`.

Generiert am 2026-10-26 für Spielversion **1.1.0**. [Referenzindex](README.md)

| Typ | Beschreibung |
|---|---|
| [`ModOptions`](#modoptions) | Optionswerte, die jede Mod setzen kann. |
| [`ModOptionsPage`](#modoptionspage) | Die Seite einer Mod auf dem Optionsbildschirm. |
| [`ModOptionDefinition`](#modoptiondefinition) | Gemeinsame Definition eines Eintrags auf dem Optionsbildschirm. |
| [`ModOption<T>`](#modoptiont) | Ein Optionseintrag, der einen Wert hält. |
| [`ModToggleOption`](#modtoggleoption) | Eine An/Aus-Option. |
| [`ModSelectionOption`](#modselectionoption) | Eine Auswahlfeld-Option. |
| [`ModSliderOption`](#modslideroption) | Eine Schieberegler-Option. |
| [`ModOptionText`](#modoptiontext) |  |
| [`ModOptionKind`](#modoptionkind) | Art der Zeile auf dem Optionsbildschirm. |
| [`ModOptionsRegistry`](#modoptionsregistry) | Liste der Mod-Seiten, die der Optionsbildschirm liest. |


## ModOptions

```csharp
public sealed class ModOptions
```

Optionswerte, die jede Mod setzen kann.

### Eigenschaften

#### ModId

```csharp
public string ModId { get; }
```

Die modId dieses Optionssatzes


#### IsAvailable

```csharp
public bool IsAvailable { get; }
```

Ob der Speicher initialisiert ist


#### DisplayName

```csharp
public ModOptionText DisplayName { get; set; }
```

Mod-Name, der auf dem Optionsbildschirm angezeigt wird. Bei Steam-Workshop-Mods standardmäßig der Anzeigename aus dem Manifest.

### Methoden

#### GetBool

```csharp
public bool GetBool(string key, bool fallback = false)
```

Gespeicherter bool-Wert. Gibt fallback zurück, wenn er fehlt oder einen anderen Typ hat.


#### GetInt

```csharp
public int GetInt(string key, int fallback = 0)
```

Gespeicherter int-Wert. Gibt fallback zurück, wenn er fehlt oder einen anderen Typ hat.


#### GetLong

```csharp
public long GetLong(string key, long fallback = 0)
```

Gespeicherter long-Wert. Gibt fallback zurück, wenn er fehlt oder einen anderen Typ hat.


#### GetFloat

```csharp
public float GetFloat(string key, float fallback = 0)
```

Gespeicherter float-Wert. Gibt fallback zurück, wenn er fehlt oder einen anderen Typ hat.


#### GetString

```csharp
public string GetString(string key, string fallback = "")
```

Gespeicherter string-Wert. Gibt fallback zurück, wenn er fehlt oder einen anderen Typ hat.


#### SetBool

```csharp
public void SetBool(string key, bool value)
```

Schreibt einen bool-Wert in den Speicher.


#### SetInt

```csharp
public void SetInt(string key, int value)
```

Schreibt einen int-Wert in den Speicher.


#### SetLong

```csharp
public void SetLong(string key, long value)
```

Schreibt einen long-Wert in den Speicher.


#### SetFloat

```csharp
public void SetFloat(string key, float value)
```

Schreibt einen float-Wert in den Speicher.


#### SetString

```csharp
public void SetString(string key, string value)
```

Schreibt einen string-Wert in den Speicher. Akzeptiert kein null.


#### HasKey

```csharp
public bool HasKey(string key)
```

Ob unter key ein Wert gespeichert ist


#### Remove

```csharp
public bool Remove(string key)
```

Entfernt den gespeicherten Wert, sodass spätere Lesezugriffe fallback zurückgeben.


#### Reset

```csharp
public int Reset()
```

Löscht alle Optionen dieser Mod. Gibt die Anzahl der entfernten Einträge zurück.


#### Save

```csharp
public bool Save()
```

Speichert die Optionsdatei (DeviceOptions). Set*/Remove/Reset ändern nur den Speicher, rufe also dies auf, um sie in die Datei zu schreiben. Gibt false zurück, wenn kein Speicher vorhanden ist oder das Schreiben fehlschlägt.


#### AddToggle

```csharp
public ModToggleOption AddToggle(string key, bool defaultValue = false, ModOptionText label = default, Action<bool> onChanged = null)
```

Fügt der Seite dieser Mod auf dem Optionsbildschirm einen Schalter hinzu. Ersetzt jeden Eintrag mit demselben key.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, string[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Fügt ein Auswahlfeld hinzu. Der Wert ist ein Index in choices.


#### AddSelectionBox

```csharp
public ModSelectionOption AddSelectionBox(string key, LocalizedString[] choices, int defaultValue = 0, ModOptionText label = default, Action<int> onChanged = null)
```

Fügt ein Auswahlfeld hinzu, dessen Auswahlmöglichkeiten als Lokalisierungsschlüssel angegeben werden.


#### AddSlider

```csharp
public ModSliderOption AddSlider(string key, float min = 0, float max = 1, float defaultValue = 0, float step = 0, ModOptionText label = default, Action<float> onChanged = null)
```

Fügt einen Schieberegler hinzu. Ist step 0, ist der Wert kontinuierlich.


#### UnregisterUI

```csharp
public void UnregisterUI()
```

Entfernt alle Optionsbildschirm-Einträge dieser Mod. Gespeicherte Werte bleiben erhalten.


## ModOptionsPage

```csharp
public sealed class ModOptionsPage
```

Die Seite einer Mod auf dem Optionsbildschirm. Enthält nur UI-Definitionen; gespeicherte Werte werden getrennt gehalten.

### Eigenschaften

#### Options

```csharp
public ModOptions Options { get; }
```

Die Mod-Optionen dieser Seite


#### ModId

```csharp
public string ModId { get; }
```

`Options.ModId`


#### DisplayName

```csharp
public ModOptionText DisplayName { get; }
```

Mod-Name, der auf dem Optionsbildschirm angezeigt wird


#### Items

```csharp
public IReadOnlyList<ModOptionDefinition> Items { get; }
```

Einträge in Registrierungsreihenfolge


## ModOptionDefinition

```csharp
public abstract class ModOptionDefinition
```

Gemeinsame Definition eines Eintrags auf dem Optionsbildschirm. Wird von `ModOptions.Add*` erstellt.

### Eigenschaften

#### Options

```csharp
public ModOptions Options { get; }
```

Die Mod-Optionen, zu denen dieser Eintrag gehört


#### Key

```csharp
public string Key { get; }
```

Speicherschlüssel


#### Label

```csharp
public ModOptionText Label { get; }
```

Anzeigename. Ist er leer, wird der key angezeigt.


#### Kind

```csharp
public abstract ModOptionKind Kind { get; }
```

Art der Zeile

### Konstruktoren

#### ModOptionDefinition

```csharp
protected ModOptionDefinition(ModOptions options, string key, ModOptionText label)
```

Validiert key.


## ModOption<T>

```csharp
public abstract class ModOption<T> : ModOptionDefinition
```

Ein Optionseintrag, der einen Wert hält. Behalte das Handle, um den Wert aus dem Code zu lesen und zu ändern.

### Events

#### ValueChanged

```csharp
public event Action<T> ValueChanged
```

Wird nach einer Wertänderung mit dem neuen Wert aufgerufen.

### Eigenschaften

#### DefaultValue

```csharp
public T DefaultValue { get; }
```

Bei der Registrierung angegebener Standardwert


#### Value

```csharp
public T Value { get; set; }
```

Aktueller Wert. Beim Setzen wird der Wert im Speicher abgelegt und `ValueChanged` ausgelöst, falls er sich geändert hat.

### Konstruktoren

#### ModOption

```csharp
protected ModOption(ModOptions options, string key, T defaultValue, ModOptionText label, Action<T> onChanged)
```

onChanged wird der erste Abonnent von `ValueChanged`.

### Methoden

#### Read

```csharp
protected abstract T Read()
```

Liest den Wert aus dem Speicher.


#### Write

```csharp
protected abstract void Write(T value)
```

Schreibt den Wert in den Speicher.


#### Normalize

```csharp
protected virtual T Normalize(T value)
```

Begrenzt den Wert vor dem Schreiben auf den erlaubten Bereich.


## ModToggleOption

```csharp
public sealed class ModToggleOption : ModOption<bool>
```

Eine An/Aus-Option.

### Eigenschaften

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Immer Toggle

### Methoden

#### Read

```csharp
protected override bool Read()
```

Liest den bool-Wert.


#### Write

```csharp
protected override void Write(bool value)
```

Schreibt den bool-Wert.


## ModSelectionOption

```csharp
public sealed class ModSelectionOption : ModOption<int>
```

Eine Auswahlfeld-Option. Der Wert ist ein Index in `Choices`.

### Eigenschaften

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Immer SelectionBox


#### Choices

```csharp
public IReadOnlyList<ModOptionText> Choices { get; }
```

Liste der Auswahlmöglichkeiten

### Methoden

#### Read

```csharp
protected override int Read()
```

Gespeicherter Index. Der Standardwert, falls außerhalb des Bereichs.


#### Normalize

```csharp
protected override int Normalize(int value)
```

Begrenzt auf den Bereich der Auswahlmöglichkeiten.


#### Write

```csharp
protected override void Write(int value)
```

Schreibt den Index.


## ModSliderOption

```csharp
public sealed class ModSliderOption : ModOption<float>
```

Eine Schieberegler-Option.

### Eigenschaften

#### Kind

```csharp
public override ModOptionKind Kind { get; }
```

Immer Slider


#### Min

```csharp
public float Min { get; }
```

Minimalwert


#### Max

```csharp
public float Max { get; }
```

Maximalwert


#### Step

```csharp
public float Step { get; }
```

Schrittweite. Kontinuierlich bei 0.

### Methoden

#### Read

```csharp
protected override float Read()
```

Gespeicherter Wert, normalisiert


#### Normalize

```csharp
protected override float Normalize(float value)
```

Begrenzt auf den Bereich und rastet auf die Schrittweite ein.


#### Write

```csharp
protected override void Write(float value)
```

Schreibt den float-Wert.


## ModOptionText

```csharp
public readonly struct ModOptionText
```

*Noch keine Beschreibung.*

### Methoden

#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(string text)
```

Zeigt den String unverändert an.


#### implicit operator ModOptionText

```csharp
public static implicit operator ModOptionText(LocalizedString text)
```

Übersetzt den Schlüssel des LocalizedString zum Anzeigezeitpunkt.


## ModOptionKind

```csharp
public enum ModOptionKind
```

Art der Zeile auf dem Optionsbildschirm.

| Wert | Beschreibung |
|---|---|
| `Toggle` | An/Aus |
| `SelectionBox` | Auswahlfeld |
| `Slider` | Schieberegler |


## ModOptionsRegistry

```csharp
public static class ModOptionsRegistry
```

Liste der Mod-Seiten, die der Optionsbildschirm liest.

### Methoden

#### GetPages

```csharp
public static IReadOnlyList<ModOptionsPage> GetPages()
```

Gibt die anzuzeigenden Seiten zurück, sortiert nach modId.
