<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# Punto di ingresso e contesto

Le classi base da cui eredita la classe di ingresso di una mod, più i tipi di contesto e manifest che il loader vi associa. Le nuove mod dovrebbero ereditare da `WorkshopModBase`.

Generato il 2026-10-26 per la versione del gioco **1.1.0**. [Indice del riferimento](README.md)

| Tipo | Descrizione |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | La base di ingresso per le mod del Workshop. |
| [`WorkshopModContext`](#workshopmodcontext) | Contesto in sola lettura fornito a un modulo DLL del Workshop. |
| [`HorayModBase`](#horaymodbase) | La base per le classi di ingresso delle mod DLL. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Un pacchetto di moduli dati che una mod consumer può leggere. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Una specifica di esecuzione validata. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Una voce di `modules[]` del manifest. |
| [`WorkshopDependency`](#workshopdependency) | Una voce di `dependencies[]` del manifest. |
| [`WorkshopModuleType`](#workshopmoduletype) | Un valore di `modules[].type` del manifest. |
| [`WorkshopInstallation`](#workshopinstallation) | La dichiarazione dell'autore sulla necessità che gli altri partecipanti installino la mod (`multiplayer.installation` del manifest). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

La base di ingresso per le mod del Workshop.

### Proprietà

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Valido dal momento in cui viene chiamato OnModLoaded.

### Metodi

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Pacchetti di moduli dati che indicano questa mod come consumer, in ordine di caricamento. Chiamalo dopo che l'attivazione è terminata (dopo gli eventi dei database). Restituisce un elenco vuoto se non c'è un contesto (per esempio il percorso AddOns).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Contesto in sola lettura fornito a un modulo DLL del Workshop. Il percorso di installazione può variare, quindi le mod usano questi valori per trovare i propri file.

### Proprietà

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Percorso assoluto della radice del pacchetto (cartella di installazione).


#### FileId

```csharp
public ulong FileId { get; }
```

L'ID del file sullo Steam Workshop. I pacchetti locali ricevono un ID univoco derivato dal loro percorso.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

La specifica di esecuzione validata.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

La voce del modulo dll che ha creato questa istanza.


#### Options

```csharp
public ModOptions Options { get; }
```

Opzioni per dispositivo di questo modId. Condivise dai moduli DLL del pacchetto e dalle sue copie locale e sottoscritta.

### Costruttori

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Nessuna descrizione per ora.*

### Metodi

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Converte un percorso relativo alla radice del pacchetto in un percorso assoluto. I percorsi che escono dalla radice, o i percorsi assoluti, vengono rifiutati (solleva un'eccezione).


## HorayModBase

```csharp
public class HorayModBase
```

La base per le classi di ingresso delle mod DLL.

### Costanti e campi

#### metadata

```csharp
public Metadata metadata
```

I metadati ricevuti in `LoadMod`.

### Metodi

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Chiamato dal loader. Memorizza i metadati, poi chiama `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Il punto di inizializzazione della mod. Viene chiamato prima dell'inizializzazione dei database, quindi iscriviti qui agli eventi di `HorayModAPI`.


#### UnloadMod

```csharp
public void UnloadMod()
```

Chiama `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Il punto di scaricamento della mod. Metti qui le operazioni di pulizia, come la disiscrizione dagli eventi.

### Tipi annidati

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Informazioni identificative della mod che il loader passa a `LoadMod`.

##### Costanti e campi

###### modName

```csharp
public string modName
```

Nome visualizzato.


###### modVersion

```csharp
public string modVersion
```

Versione.


###### modAuthor

```csharp
public string modAuthor
```

Autore.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Un pacchetto di moduli dati che una mod consumer può leggere. Ottienilo con `WorkshopModBase.GetDataPackages()`.

### Proprietà

#### ModId

```csharp
public string ModId { get; }
```

Il modId del pacchetto.


#### Version

```csharp
public string Version { get; }
```

La versione del pacchetto.


#### FileId

```csharp
public ulong FileId { get; }
```

L'ID del file sullo Steam Workshop.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Nome visualizzato (info.title, oppure modId se assente).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Percorso assoluto della radice del pacchetto.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Moduli dati destinati a questo consumer, in ordine di dichiarazione.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Percorsi assoluti delle cartelle dei moduli, in ordine di dichiarazione. Equivale a Modules[i].FullPath.

### Metodi

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Sceglie, tra i record di attivazione (in ordine di caricamento), i pacchetti che hanno moduli dati per `consumerModId`. Include solo gli stati in cui le dipendenze sono soddisfatte (Active, AlreadyActive, NoDllModules). I dati dei pacchetti falliti o applicati parzialmente non vengono passati.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Una specifica di esecuzione validata. Immutabile.

### Proprietà

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Percorso assoluto della radice del pacchetto.


#### ModId

```csharp
public string ModId { get; }
```

Il modId del manifest.


#### Version

```csharp
public string Version { get; }
```

La versione del manifest.


#### Author

```csharp
public string Author { get; }
```

L'autore del manifest. null se assente.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, oppure modId se assente.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Requisito di installazione per il multigiocatore.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Se l'oggetto multiplayer è stato dichiarato esplicitamente. Se false, Installation contiene il valore predefinito normalizzato.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Elenco dei moduli, in ordine di dichiarazione.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Percorsi assoluti delle DLL gestite ausiliarie. Queste non ricevono callback di avvio.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Elenco delle mod da cui dipende questa mod.


#### HasDll

```csharp
public bool HasDll { get; }
```

Derivato dalla presenza di un tipo dll in modules. Non esiste un flag separato.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Se modules contiene un tipo localization.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Se modules contiene un tipo costume.


#### HasData

```csharp
public bool HasData { get; }
```

Se modules contiene un tipo data.

### Costruttori

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Nessuna descrizione per ora.*

### Metodi

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Moduli dati che `consumerModId` riceve, in ordine di dichiarazione. Elenco vuoto se non ce ne sono. Un modulo che indica un consumer va solo a quella mod; un modulo che lo omette va a questo stesso pacchetto e a ogni mod che dipende direttamente da esso.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Una voce di `modules[]` del manifest.

### Proprietà

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Tipo del modulo.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Percorso relativo alla radice del pacchetto (separatori normalizzati a '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Percorso assoluto validato.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Nome completo della classe di ingresso del modulo dll. null per localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId della mod che legge il modulo dati. Se omesso (null), è il pacchetto stesso più ogni mod che dipende direttamente da esso. Sempre null per i moduli non di tipo data.

### Costruttori

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Creato dal validatore.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Creato dal validatore.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Una voce di `dependencies[]` del manifest.

### Proprietà

#### ModId

```csharp
public string ModId { get; }
```

modId della mod da cui si dipende.


#### Version

```csharp
public string Version { get; }
```

null significa qualsiasi versione. Se impostato, viene accettata solo una corrispondenza esatta (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Un suggerimento su dove scaricare. Deve essere verificato rispetto a modId/version reali dopo l'installazione; da solo non dimostra che la mod scaricata sia quella giusta.

### Costruttori

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Creato dal validatore.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Un valore di `modules[].type` del manifest.

| Valore | Descrizione |
|---|---|
| `Dll` | Modulo DLL gestita |
| `Localization` | Modulo cartella di localizzazione |
| `Costume` | Cartella di un pacchetto costume senza codice (costume.json/skin.json + PNG) |
| `Data` | Cartella dati generica letta da un'altra mod (il consumer); il gioco non ne interpreta il contenuto |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

La dichiarazione dell'autore sulla necessità che gli altri partecipanti installino la mod (`multiplayer.installation` del manifest).

| Valore | Descrizione |
|---|---|
| `Unknown` | Non dichiarato (incluso nel confronto della configurazione della lobby come Everyone) |
| `LocalOnly` | Funziona solo sul dispositivo dell'utente; escluso dal confronto della configurazione della lobby |
| `Everyone` | Tutti nella stanza devono installare lo stesso pacchetto |
| `HostOnly` | Solo l'host deve installarlo |
