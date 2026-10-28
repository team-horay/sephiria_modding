<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# Startpunkt och kontext

Basklasserna som en mods startpunktsklass ärver, plus de kontext- och manifesttyper som laddaren kopplar på. Nya moddar bör ärva `WorkshopModBase`.

Genererad 2026-10-26 för spelversion **1.1.0**. [Referensindex](README.md)

| Typ | Beskrivning |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | Startpunktsbasen för Workshop-moddar. |
| [`WorkshopModContext`](#workshopmodcontext) | Skrivskyddad kontext som ges till en Workshop-DLL-modul. |
| [`HorayModBase`](#horaymodbase) | Basen för DLL-moddars startpunktsklasser. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Ett paket med datamoduler som en konsumentmod kan läsa. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | En validerad körningsspecifikation. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | En post i manifestets `modules[]`. |
| [`WorkshopDependency`](#workshopdependency) | En post i manifestets `dependencies[]`. |
| [`WorkshopModuleType`](#workshopmoduletype) | Ett värde för `modules[].type` i manifestet. |
| [`WorkshopInstallation`](#workshopinstallation) | Skaparens deklaration av om andra deltagare måste installera modden (manifestets `multiplayer.installation`). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

Startpunktsbasen för Workshop-moddar.

### Egenskaper

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Giltig från det ögonblick OnModLoaded anropas.

### Metoder

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Datamodulpaket som anger den här modden som sin konsument, i laddningsordning. Anropa efter att aktiveringen är klar (efter databashändelserna). Returnerar en tom lista om det inte finns någon kontext (t.ex. AddOns-vägen).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Skrivskyddad kontext som ges till en Workshop-DLL-modul. Installationssökvägen kan variera, så moddar använder de här värdena för att hitta sina egna filer.

### Egenskaper

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absolut sökväg till paketets rot (installationsmappen).


#### FileId

```csharp
public ulong FileId { get; }
```

Steam Workshop-fil-ID:t. Lokala paket får ett unikt ID härlett från sin sökväg.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

Den validerade körningsspecifikationen.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

Den dll-modulpost som skapade den här instansen.


#### Options

```csharp
public ModOptions Options { get; }
```

Enhetsspecifika alternativ för det här modId:t. Delas av paketets DLL-moduler och av dess lokala och prenumererade kopior.

### Konstruktorer

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Ingen beskrivning ännu.*

### Metoder

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Omvandlar en sökväg relativt paketets rot till en absolut sökväg. Sökvägar som lämnar roten, eller absoluta sökvägar, avvisas (kastar undantag).


## HorayModBase

```csharp
public class HorayModBase
```

Basen för DLL-moddars startpunktsklasser.

### Konstanter och fält

#### metadata

```csharp
public Metadata metadata
```

Metadatan som togs emot i `LoadMod`.

### Metoder

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Anropas av laddaren. Sparar metadatan och anropar sedan `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Moddens initieringspunkt. Den anropas innan databaserna initieras, så prenumerera på `HorayModAPI`-händelser här.


#### UnloadMod

```csharp
public void UnloadMod()
```

Anropar `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Moddens urladdningspunkt. Lägg städning som avprenumeration av händelser här.

### Nästlade typer

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Identifieringsinformation om modden som laddaren skickar till `LoadMod`.

##### Konstanter och fält

###### modName

```csharp
public string modName
```

Visningsnamn.


###### modVersion

```csharp
public string modVersion
```

Version.


###### modAuthor

```csharp
public string modAuthor
```

Skapare.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Ett paket med datamoduler som en konsumentmod kan läsa. Hämta det med `WorkshopModBase.GetDataPackages()`.

### Egenskaper

#### ModId

```csharp
public string ModId { get; }
```

Paketets modId.


#### Version

```csharp
public string Version { get; }
```

Paketets version.


#### FileId

```csharp
public ulong FileId { get; }
```

Steam Workshop-fil-ID:t.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Visningsnamn (info.title, eller modId om det saknas).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absolut sökväg till paketets rot.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Datamoduler som riktar sig till den här konsumenten, i deklarationsordning.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Absoluta sökvägar till modulmapparna, i deklarationsordning. Samma som Modules[i].FullPath.

### Metoder

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Plockar ut, från aktiveringsposterna (i laddningsordning), de paket som har datamoduler för `consumerModId`. Tar bara med tillstånd där beroendena är uppfyllda (Active, AlreadyActive, NoDllModules). Data från misslyckade eller delvis applicerade paket skickas inte vidare.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

En validerad körningsspecifikation. Oföränderlig.

### Egenskaper

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Absolut sökväg till paketets rot.


#### ModId

```csharp
public string ModId { get; }
```

Manifestets modId.


#### Version

```csharp
public string Version { get; }
```

Manifestets version.


#### Author

```csharp
public string Author { get; }
```

Manifestets author. null om det saknas.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, eller modId om det saknas.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Installationskrav för flerspelarläge.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Om multiplayer-objektet deklarerades uttryckligen. Om false innehåller Installation det normaliserade standardvärdet.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Modullista, i deklarationsordning.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Absoluta sökvägar till hjälp-DLL:er (hanterade). Dessa får inga startcallbacks.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Lista över moddar som den här modden är beroende av.


#### HasDll

```csharp
public bool HasDll { get; }
```

Härleds från om modules innehåller en dll-typ. Det finns ingen separat flagga.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Om modules innehåller en localization-typ.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Om modules innehåller en costume-typ.


#### HasData

```csharp
public bool HasData { get; }
```

Om modules innehåller en data-typ.

### Konstruktorer

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Ingen beskrivning ännu.*

### Metoder

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Datamoduler som `consumerModId` tar emot, i deklarationsordning. Tom lista om det inte finns några. En modul som anger en konsument går bara till den modden; en modul som utelämnar den går till paketet självt och till varje mod som är direkt beroende av det.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

En post i manifestets `modules[]`.

### Egenskaper

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Modultyp.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Sökväg relativt paketets rot (avgränsare normaliserade till '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Validerad absolut sökväg.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Fullständigt namn på dll-modulens startpunktsklass. null för localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId för modden som läser datamodulen. Om det utelämnas (null) är det paketet självt plus varje direkt beroende mod. Alltid null för moduler som inte är data-moduler.

### Konstruktorer

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Skapas av validatorn.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Skapas av validatorn.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

En post i manifestets `dependencies[]`.

### Egenskaper

#### ModId

```csharp
public string ModId { get; }
```

modId för modden som man är beroende av.


#### Version

```csharp
public string Version { get; }
```

null betyder valfri version. Om den är satt accepteras bara en exakt matchning (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

En ledtråd om var nedladdningen ska ske. Måste kontrolleras mot det faktiska modId:t/versionen efter installation; det bevisar inte i sig att den nedladdade modden är den rätta.

### Konstruktorer

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Skapas av validatorn.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Ett värde för `modules[].type` i manifestet.

| Värde | Beskrivning |
|---|---|
| `Dll` | Hanterad DLL-modul |
| `Localization` | Modul med lokaliseringsmapp |
| `Costume` | Kodfritt dräktpaket i mappform (costume.json/skin.json + PNG) |
| `Data` | Allmän datamapp som läses av en annan mod (konsumenten); spelet tolkar inte dess innehåll |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

Skaparens deklaration av om andra deltagare måste installera modden (manifestets `multiplayer.installation`).

| Värde | Beskrivning |
|---|---|
| `Unknown` | Inte deklarerat (tas med i jämförelsen av lobbyuppsättningen precis som Everyone) |
| `LocalOnly` | Fungerar bara på användarens egen enhet; undantas från jämförelsen av lobbyuppsättningen |
| `Everyone` | Alla i rummet måste installera samma paket |
| `HostOnly` | Bara värden behöver installera den |
