<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# Point d'entrée et contexte

Les classes de base dont hérite la classe de point d'entrée d'un mod, ainsi que les types de contexte et de manifeste attachés par le chargeur. Les nouveaux mods doivent hériter de `WorkshopModBase`.

Généré le 2026-10-26 pour la version **1.1.0** du jeu. [Index de la référence](README.md)

| Type | Description |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | La base de point d'entrée des mods du Workshop. |
| [`WorkshopModContext`](#workshopmodcontext) | Contexte en lecture seule fourni à un module DLL du Workshop. |
| [`HorayModBase`](#horaymodbase) | La base des classes de point d'entrée des mods DLL. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Un paquet de modules de données qu'un mod consommateur peut lire. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Une spécification d'exécution validée. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Une entrée du tableau `modules[]` du manifeste. |
| [`WorkshopDependency`](#workshopdependency) | Une entrée du tableau `dependencies[]` du manifeste. |
| [`WorkshopModuleType`](#workshopmoduletype) | Une valeur `modules[].type` du manifeste. |
| [`WorkshopInstallation`](#workshopinstallation) | La déclaration de l'auteur indiquant si les autres participants doivent installer le mod (`multiplayer.installation` du manifeste). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

La base de point d'entrée des mods du Workshop.

### Propriétés

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Valide dès l'appel de OnModLoaded.

### Méthodes

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Paquets de modules de données qui désignent ce mod comme consommateur, dans l'ordre de chargement. À appeler une fois l'activation terminée (après les événements de base de données). Renvoie une liste vide s'il n'y a pas de contexte (par ex. le chemin AddOns).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Contexte en lecture seule fourni à un module DLL du Workshop. Le chemin d'installation pouvant varier, les mods utilisent ces valeurs pour trouver leurs propres fichiers.

### Propriétés

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Chemin absolu de la racine du paquet (dossier d'installation).


#### FileId

```csharp
public ulong FileId { get; }
```

L'ID de fichier du Steam Workshop. Les paquets locaux reçoivent un ID unique dérivé de leur chemin.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

La spécification d'exécution validée.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

L'entrée de module dll qui a créé cette instance.


#### Options

```csharp
public ModOptions Options { get; }
```

Options propres à l'appareil pour ce modId. Partagées par les modules DLL du paquet et par ses copies locale et abonnée.

### Constructeurs

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Pas encore de description.*

### Méthodes

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Convertit un chemin relatif à la racine du paquet en chemin absolu. Les chemins qui sortent de la racine, ou les chemins absolus, sont refusés (lève une exception).


## HorayModBase

```csharp
public class HorayModBase
```

La base des classes de point d'entrée des mods DLL.

### Constantes et champs

#### metadata

```csharp
public Metadata metadata
```

Les métadonnées reçues dans `LoadMod`.

### Méthodes

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Appelé par le chargeur. Stocke les métadonnées, puis appelle `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

Le point d'initialisation du mod. Il est appelé avant l'initialisation des bases de données ; abonnez-vous donc ici aux événements de `HorayModAPI`.


#### UnloadMod

```csharp
public void UnloadMod()
```

Appelle `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

Le point de déchargement du mod. Placez-y le nettoyage, comme le désabonnement des événements.

### Types imbriqués

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Informations d'identification du mod transmises par le chargeur à `LoadMod`.

##### Constantes et champs

###### modName

```csharp
public string modName
```

Nom affiché.


###### modVersion

```csharp
public string modVersion
```

Version.


###### modAuthor

```csharp
public string modAuthor
```

Auteur.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Un paquet de modules de données qu'un mod consommateur peut lire. Obtenez-le avec `WorkshopModBase.GetDataPackages()`.

### Propriétés

#### ModId

```csharp
public string ModId { get; }
```

Le modId du paquet.


#### Version

```csharp
public string Version { get; }
```

La version du paquet.


#### FileId

```csharp
public ulong FileId { get; }
```

L'ID de fichier du Steam Workshop.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Nom affiché (info.title, ou modId s'il est absent).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Chemin absolu de la racine du paquet.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Modules de données qui ciblent ce consommateur, dans l'ordre de déclaration.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Chemins absolus des dossiers des modules, dans l'ordre de déclaration. Identique à Modules[i].FullPath.

### Méthodes

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Sélectionne, parmi les enregistrements d'activation (dans l'ordre de chargement), les paquets qui ont des modules de données pour `consumerModId`. N'inclut que les états où les dépendances sont satisfaites (Active, AlreadyActive, NoDllModules). Les données des paquets en échec ou partiellement appliqués ne sont pas transmises.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Une spécification d'exécution validée. Immuable.

### Propriétés

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Chemin absolu de la racine du paquet.


#### ModId

```csharp
public string ModId { get; }
```

Le modId du manifeste.


#### Version

```csharp
public string Version { get; }
```

La version du manifeste.


#### Author

```csharp
public string Author { get; }
```

L'auteur du manifeste. null s'il est absent.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, ou modId s'il est absent.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Exigence d'installation en multijoueur.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Si l'objet multiplayer a été déclaré explicitement. Si false, Installation contient la valeur par défaut normalisée.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Liste des modules, dans l'ordre de déclaration.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Chemins absolus des DLL managées auxiliaires. Elles ne reçoivent pas de rappels de démarrage.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Liste des mods dont dépend ce mod.


#### HasDll

```csharp
public bool HasDll { get; }
```

Dérivé de la présence d'un type dll dans modules. Il n'y a pas d'indicateur séparé.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Si modules contient un type localization.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Si modules contient un type costume.


#### HasData

```csharp
public bool HasData { get; }
```

Si modules contient un type data.

### Constructeurs

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Pas encore de description.*

### Méthodes

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Modules de données que `consumerModId` reçoit, dans l'ordre de déclaration. Liste vide s'il n'y en a aucun. Un module qui désigne un consommateur ne va qu'à ce mod ; un module qui l'omet va au paquet lui-même et à chaque mod qui en dépend directement.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Une entrée du tableau `modules[]` du manifeste.

### Propriétés

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Type du module.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Chemin relatif à la racine du paquet (séparateurs normalisés en '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Chemin absolu validé.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Nom complet de la classe de point d'entrée du module dll. null pour localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId du mod qui lit le module de données. S'il est omis (null), il s'agit du paquet lui-même plus chaque mod qui en dépend directement. Toujours null pour les modules autres que data.

### Constructeurs

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Créé par le validateur.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Créé par le validateur.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Une entrée du tableau `dependencies[]` du manifeste.

### Propriétés

#### ModId

```csharp
public string ModId { get; }
```

modId du mod dont on dépend.


#### Version

```csharp
public string Version { get; }
```

null signifie n'importe quelle version. Si défini, seule une correspondance exacte est acceptée (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Une indication sur l'endroit d'où télécharger. Doit être vérifié par rapport au modId/version réels après l'installation ; il ne prouve pas à lui seul que le mod téléchargé est le bon.

### Constructeurs

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Créé par le validateur.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Une valeur `modules[].type` du manifeste.

| Valeur | Description |
|---|---|
| `Dll` | Module DLL managée |
| `Localization` | Module de dossier de localisation |
| `Costume` | Dossier de paquet de tenue sans code (costume.json/skin.json + PNG) |
| `Data` | Dossier de données générique lu par un autre mod (le consommateur) ; le jeu n'interprète pas son contenu |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

La déclaration de l'auteur indiquant si les autres participants doivent installer le mod (`multiplayer.installation` du manifeste).

| Valeur | Description |
|---|---|
| `Unknown` | Non déclaré (inclus dans la comparaison de configuration du salon comme Everyone) |
| `LocalOnly` | Ne fonctionne que sur l'appareil de l'utilisateur ; exclu de la comparaison de configuration du salon |
| `Everyone` | Tout le monde dans le salon doit installer le même paquet |
| `HostOnly` | Seul l'hôte doit l'installer |
