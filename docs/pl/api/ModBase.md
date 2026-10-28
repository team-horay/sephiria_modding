<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj go bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# Punkt wejścia i kontekst

Klasy bazowe, po których dziedziczy klasa wejścia moda, oraz typy kontekstu i manifestu dołączane przez loader. Nowe mody powinny dziedziczyć po `WorkshopModBase`.

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. [Indeks dokumentacji](README.md)

| Typ | Opis |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | Baza wejścia dla modów z Warsztatu. |
| [`WorkshopModContext`](#workshopmodcontext) | Kontekst tylko do odczytu przekazywany modułowi DLL z Warsztatu. |
| [`HorayModBase`](#horaymodbase) | Baza dla klas wejścia modów DLL. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Jeden pakiet modułów danych do odczytu przez moda-konsumenta. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Zweryfikowana specyfikacja wykonania. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Jeden wpis w `modules[]` manifestu. |
| [`WorkshopDependency`](#workshopdependency) | Jeden wpis w `dependencies[]` manifestu. |
| [`WorkshopModuleType`](#workshopmoduletype) | Wartość `modules[].type` manifestu. |
| [`WorkshopInstallation`](#workshopinstallation) | Deklaracja autora, czy pozostali uczestnicy muszą zainstalować moda (`multiplayer.installation` w manifeście). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

Baza wejścia dla modów z Warsztatu.

### Właściwości

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Ważny od momentu wywołania OnModLoaded.

### Metody

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Pakiety modułów danych, które wskazują tego moda jako swojego konsumenta, w kolejności ładowania. Wywołuj po zakończeniu aktywacji (po zdarzeniach baz danych). Zwraca pustą listę, jeśli nie ma kontekstu (np. ścieżka AddOns).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Kontekst tylko do odczytu przekazywany modułowi DLL z Warsztatu. Ścieżka instalacji może się różnić, więc mody używają tych wartości do odnajdywania własnych plików.

### Właściwości

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Bezwzględna ścieżka katalogu głównego pakietu (folderu instalacji).


#### FileId

```csharp
public ulong FileId { get; }
```

ID pliku w Warsztacie Steam. Pakiety lokalne otrzymują unikalne ID wyprowadzone z ich ścieżki.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

Zweryfikowana specyfikacja wykonania.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

Wpis modułu dll, który utworzył tę instancję.


#### Options

```csharp
public ModOptions Options { get; }
```

Opcje zapisywane na urządzeniu dla tego modId. Współdzielone przez moduły DLL pakietu oraz przez jego kopie lokalne i subskrybowane.

### Konstruktory

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Brak opisu.*

### Metody

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Zamienia ścieżkę względną wobec katalogu głównego pakietu na ścieżkę bezwzględną. Ścieżki wychodzące poza katalog główny oraz ścieżki bezwzględne są odrzucane (rzuca wyjątek).


## HorayModBase

```csharp
public class HorayModBase
```

Baza dla klas wejścia modów DLL.

### Stałe i pola

#### metadata

```csharp
public Metadata metadata
```

Metadane otrzymane w `LoadMod`.

### Metody

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Wywoływane przez loader. Zapisuje metadane, a następnie wywołuje `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Punkt inicjalizacji moda. Jest wywoływany przed inicjalizacją baz danych, więc tutaj subskrybuj zdarzenia `HorayModAPI`.


#### UnloadMod

```csharp
public void UnloadMod()
```

Wywołuje `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Punkt wyładowania moda. Tutaj umieść porządki, takie jak anulowanie subskrypcji zdarzeń.

### Typy zagnieżdżone

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Informacje identyfikujące moda, które loader przekazuje do `LoadMod`.

##### Stałe i pola

###### modName

```csharp
public string modName
```

Wyświetlana nazwa.


###### modVersion

```csharp
public string modVersion
```

Wersja.


###### modAuthor

```csharp
public string modAuthor
```

Autor.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Jeden pakiet modułów danych do odczytu przez moda-konsumenta. Pobierz go przez `WorkshopModBase.GetDataPackages()`.

### Właściwości

#### ModId

```csharp
public string ModId { get; }
```

modId pakietu.


#### Version

```csharp
public string Version { get; }
```

Wersja pakietu.


#### FileId

```csharp
public ulong FileId { get; }
```

ID pliku w Warsztacie Steam.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Wyświetlana nazwa (info.title albo modId, jeśli brak).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Bezwzględna ścieżka katalogu głównego pakietu.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Moduły danych skierowane do tego konsumenta, w kolejności deklaracji.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Bezwzględne ścieżki folderów modułów, w kolejności deklaracji. To samo co Modules[i].FullPath.

### Metody

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Wybiera z rekordów aktywacji (w kolejności ładowania) pakiety, które mają moduły danych dla `consumerModId`. Uwzględnia tylko stany, w których zależności są spełnione (Active, AlreadyActive, NoDllModules). Dane z pakietów, które zawiodły lub zostały zastosowane częściowo, nie są przekazywane.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Zweryfikowana specyfikacja wykonania. Niezmienna.

### Właściwości

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Bezwzględna ścieżka katalogu głównego pakietu.


#### ModId

```csharp
public string ModId { get; }
```

modId z manifestu.


#### Version

```csharp
public string Version { get; }
```

Wersja z manifestu.


#### Author

```csharp
public string Author { get; }
```

Autor z manifestu. null, jeśli brak.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title albo modId, jeśli brak.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Wymaganie instalacji w trybie wieloosobowym.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Czy obiekt multiplayer został jawnie zadeklarowany. Jeśli false, Installation zawiera znormalizowaną wartość domyślną.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Lista modułów, w kolejności deklaracji.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Bezwzględne ścieżki pomocniczych zarządzanych plików DLL. Nie otrzymują one callbacków startowych.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Lista modów, od których zależy ten mod.


#### HasDll

```csharp
public bool HasDll { get; }
```

Wyprowadzane z tego, czy modules zawiera typ dll. Nie ma osobnej flagi.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Czy modules zawiera typ localization.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Czy modules zawiera typ costume.


#### HasData

```csharp
public bool HasData { get; }
```

Czy modules zawiera typ data.

### Konstruktory

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Brak opisu.*

### Metody

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Moduły danych, które otrzymuje `consumerModId`, w kolejności deklaracji. Pusta lista, jeśli brak. Moduł wskazujący konsumenta trafia tylko do tego moda; moduł, który go pomija, trafia do samego pakietu i do każdego moda, który bezpośrednio od niego zależy.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Jeden wpis w `modules[]` manifestu.

### Właściwości

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Typ modułu.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Ścieżka względna wobec katalogu głównego pakietu (separatory znormalizowane do '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Zweryfikowana ścieżka bezwzględna.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Pełna nazwa klasy wejścia modułu dll. null dla localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId moda, który odczytuje moduł danych. Jeśli pominięty (null), jest to sam pakiet plus każdy bezpośrednio zależny mod. Zawsze null dla modułów innych niż data.

### Konstruktory

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Tworzone przez walidator.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Tworzone przez walidator.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Jeden wpis w `dependencies[]` manifestu.

### Właściwości

#### ModId

```csharp
public string ModId { get; }
```

modId moda, od którego zależy ten mod.


#### Version

```csharp
public string Version { get; }
```

null oznacza dowolną wersję. Jeśli ustawione, akceptowane jest tylko dokładne dopasowanie (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Wskazówka, skąd pobrać moda. Po instalacji musi zostać sprawdzona względem faktycznego modId/wersji; sama w sobie nie dowodzi, że pobrany mod jest właściwy.

### Konstruktory

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Tworzone przez walidator.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Wartość `modules[].type` manifestu.

| Wartość | Opis |
|---|---|
| `Dll` | Zarządzany moduł DLL |
| `Localization` | Moduł folderu lokalizacji |
| `Costume` | Folder pakietu stroju bez kodu (costume.json/skin.json + PNG) |
| `Data` | Ogólny folder danych odczytywany przez innego moda (konsumenta); gra nie interpretuje jego zawartości |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

Deklaracja autora, czy pozostali uczestnicy muszą zainstalować moda (`multiplayer.installation` w manifeście).

| Wartość | Opis |
|---|---|
| `Unknown` | Niezadeklarowane (uwzględniane w porównaniu konfiguracji lobby tak jak Everyone) |
| `LocalOnly` | Działa tylko na urządzeniu użytkownika; wykluczone z porównania konfiguracji lobby |
| `Everyone` | Wszyscy w pokoju muszą zainstalować ten sam pakiet |
| `HostOnly` | Wystarczy, że zainstaluje go host |
