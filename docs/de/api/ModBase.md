<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# Einstiegspunkt und Kontext

Die Basisklassen, von denen die Einstiegsklasse einer Mod erbt, sowie die Kontext- und Manifesttypen, die der Loader anhängt. Neue Mods sollten von `WorkshopModBase` erben.

Generiert am 2026-10-26 für Spielversion **1.1.0**. [Referenzindex](README.md)

| Typ | Beschreibung |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | Die Einstiegsbasis für Workshop-Mods. |
| [`WorkshopModContext`](#workshopmodcontext) | Schreibgeschützter Kontext, der einem Workshop-DLL-Modul übergeben wird. |
| [`HorayModBase`](#horaymodbase) | Die Basis für Einstiegsklassen von DLL-Mods. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Ein Paket von Datenmodulen, das eine Consumer-Mod lesen kann. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Eine validierte Ausführungsspezifikation. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Ein Eintrag in `modules[]` des Manifests. |
| [`WorkshopDependency`](#workshopdependency) | Ein Eintrag in `dependencies[]` des Manifests. |
| [`WorkshopModuleType`](#workshopmoduletype) | Ein Wert von `modules[].type` im Manifest. |
| [`WorkshopInstallation`](#workshopinstallation) | Die Angabe des Autors, ob andere Teilnehmer die Mod installieren müssen (Manifest `multiplayer.installation`). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

Die Einstiegsbasis für Workshop-Mods.

### Eigenschaften

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Gültig ab dem Aufruf von OnModLoaded.

### Methoden

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Datenmodulpakete, die diese Mod als ihren Consumer angeben, in Ladereihenfolge. Nach Abschluss der Aktivierung aufrufen (nach den Datenbank-Events). Gibt eine leere Liste zurück, wenn kein Kontext vorhanden ist (z. B. beim AddOns-Pfad).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Schreibgeschützter Kontext, der einem Workshop-DLL-Modul übergeben wird. Der Installationspfad kann variieren, daher verwenden Mods diese Werte, um ihre eigenen Dateien zu finden.

### Eigenschaften

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absoluter Pfad des Paketstamms (Installationsordner).


#### FileId

```csharp
public ulong FileId { get; }
```

Die Steam-Workshop-Datei-ID. Lokale Pakete erhalten eine aus ihrem Pfad abgeleitete eindeutige ID.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

Die validierte Ausführungsspezifikation.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

Der dll-Moduleintrag, der diese Instanz erstellt hat.


#### Options

```csharp
public ModOptions Options { get; }
```

Gerätebezogene Optionen für diese modId. Gemeinsam genutzt von den DLL-Modulen des Pakets sowie von seiner lokalen und abonnierten Kopie.

### Konstruktoren

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Noch keine Beschreibung.*

### Methoden

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Wandelt einen Pfad relativ zum Paketstamm in einen absoluten Pfad um. Pfade, die den Stamm verlassen, oder absolute Pfade werden abgelehnt (wirft eine Exception).


## HorayModBase

```csharp
public class HorayModBase
```

Die Basis für Einstiegsklassen von DLL-Mods.

### Konstanten und Felder

#### metadata

```csharp
public Metadata metadata
```

Die in `LoadMod` erhaltenen Metadaten.

### Methoden

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Wird vom Loader aufgerufen. Speichert die Metadaten und ruft dann `OnModLoaded` auf.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Der Initialisierungspunkt der Mod. Wird aufgerufen, bevor die Datenbanken initialisiert werden, abonniere `HorayModAPI`-Events also hier.


#### UnloadMod

```csharp
public void UnloadMod()
```

Ruft `OnModUnloaded` auf.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Der Entladepunkt der Mod. Aufräumarbeiten wie das Abbestellen von Events gehören hierher.

### Verschachtelte Typen

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Identifikationsinfos der Mod, die der Loader an `LoadMod` übergibt.

##### Konstanten und Felder

###### modName

```csharp
public string modName
```

Anzeigename.


###### modVersion

```csharp
public string modVersion
```

Version.


###### modAuthor

```csharp
public string modAuthor
```

Autor.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Ein Paket von Datenmodulen, das eine Consumer-Mod lesen kann. Abrufbar über `WorkshopModBase.GetDataPackages()`.

### Eigenschaften

#### ModId

```csharp
public string ModId { get; }
```

Die modId des Pakets.


#### Version

```csharp
public string Version { get; }
```

Die Version des Pakets.


#### FileId

```csharp
public ulong FileId { get; }
```

Die Steam-Workshop-Datei-ID.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Anzeigename (info.title, oder modId, falls nicht vorhanden).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absoluter Pfad des Paketstamms.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Datenmodule, die auf diesen Consumer abzielen, in Deklarationsreihenfolge.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Absolute Pfade der Modulordner, in Deklarationsreihenfolge. Entspricht Modules[i].FullPath.

### Methoden

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Wählt aus den Aktivierungsdatensätzen (in Ladereihenfolge) die Pakete aus, die Datenmodule für `consumerModId` haben. Berücksichtigt nur Zustände, in denen die Abhängigkeiten erfüllt sind (Active, AlreadyActive, NoDllModules). Daten aus fehlgeschlagenen oder teilweise angewendeten Paketen werden nicht weitergegeben.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Eine validierte Ausführungsspezifikation. Unveränderlich.

### Eigenschaften

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absoluter Pfad des Paketstamms.


#### ModId

```csharp
public string ModId { get; }
```

Die modId aus dem Manifest.


#### Version

```csharp
public string Version { get; }
```

Die Version aus dem Manifest.


#### Author

```csharp
public string Author { get; }
```

Der Autor aus dem Manifest. null, falls nicht vorhanden.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, oder modId, falls nicht vorhanden.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Installationsanforderung im Mehrspielermodus.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Ob das multiplayer-Objekt explizit deklariert wurde. Falls false, enthält Installation den normalisierten Standardwert.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Modulliste, in Deklarationsreihenfolge.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Absolute Pfade der zusätzlichen verwalteten DLLs. Diese erhalten keine Start-Callbacks.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Liste der Mods, von denen diese Mod abhängt.


#### HasDll

```csharp
public bool HasDll { get; }
```

Abgeleitet davon, ob modules einen dll-Typ enthält. Es gibt kein separates Flag.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Ob modules einen localization-Typ enthält.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Ob modules einen costume-Typ enthält.


#### HasData

```csharp
public bool HasData { get; }
```

Ob modules einen data-Typ enthält.

### Konstruktoren

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Noch keine Beschreibung.*

### Methoden

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Datenmodule, die `consumerModId` erhält, in Deklarationsreihenfolge. Leere Liste, falls keine. Ein Modul, das einen Consumer angibt, geht nur an diese Mod; ein Modul ohne Angabe geht an dieses Paket selbst und an jede Mod, die direkt davon abhängt.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Ein Eintrag in `modules[]` des Manifests.

### Eigenschaften

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Modultyp.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Pfad relativ zum Paketstamm (Trennzeichen auf '/' normalisiert).


#### FullPath

```csharp
public string FullPath { get; }
```

Validierter absoluter Pfad.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Vollständiger Name der Einstiegsklasse des dll-Moduls. null bei localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId der Mod, die das Datenmodul liest. Falls weggelassen (null), ist es das Paket selbst plus jede direkt abhängige Mod. Bei Nicht-Daten-Modulen immer null.

### Konstruktoren

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Wird vom Validator erstellt.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Wird vom Validator erstellt.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Ein Eintrag in `dependencies[]` des Manifests.

### Eigenschaften

#### ModId

```csharp
public string ModId { get; }
```

modId der Mod, von der abgehängt wird.


#### Version

```csharp
public string Version { get; }
```

null bedeutet beliebige Version. Falls gesetzt, wird nur eine exakte Übereinstimmung akzeptiert (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Ein Hinweis darauf, woher heruntergeladen werden kann. Muss nach der Installation gegen die tatsächliche modId/version geprüft werden; allein beweist er nicht, dass die heruntergeladene Mod die richtige ist.

### Konstruktoren

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Wird vom Validator erstellt.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Ein Wert von `modules[].type` im Manifest.

| Wert | Beschreibung |
|---|---|
| `Dll` | Verwaltetes DLL-Modul |
| `Localization` | Lokalisierungsordner-Modul |
| `Costume` | Codefreies Kostümpaket als Ordner (costume.json/skin.json + PNG) |
| `Data` | Allgemeiner Datenordner, der von einer anderen Mod (dem Consumer) gelesen wird; das Spiel interpretiert seinen Inhalt nicht |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

Die Angabe des Autors, ob andere Teilnehmer die Mod installieren müssen (Manifest `multiplayer.installation`).

| Wert | Beschreibung |
|---|---|
| `Unknown` | Nicht deklariert (beim Vergleich der Lobby-Konfiguration wie Everyone behandelt) |
| `LocalOnly` | Funktioniert nur auf dem eigenen Gerät des Nutzers; vom Vergleich der Lobby-Konfiguration ausgeschlossen |
| `Everyone` | Alle im Raum müssen dasselbe Paket installieren |
| `HostOnly` | Nur der Host muss es installieren |
