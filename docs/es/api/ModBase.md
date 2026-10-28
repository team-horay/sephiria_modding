<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# Punto de entrada y contexto

Las clases base de las que hereda la clase de entrada de un mod, más los tipos de contexto y manifiesto que adjunta el cargador. Los mods nuevos deben heredar de `WorkshopModBase`.

Generado el 2026-10-26 para la versión del juego **1.1.0**. [Índice de la referencia](README.md)

| Tipo | Descripción |
|---|---|
| [`WorkshopModBase`](#workshopmodbase) | La base de entrada para los mods del Workshop. |
| [`WorkshopModContext`](#workshopmodcontext) | Contexto de solo lectura que se entrega a un módulo DLL del Workshop. |
| [`HorayModBase`](#horaymodbase) | La base de las clases de entrada de mods DLL. |
| [`WorkshopDataPackage`](#workshopdatapackage) | Un paquete de módulos de datos para que lo lea un mod consumidor. |
| [`WorkshopModDefinition`](#workshopmoddefinition) | Una especificación de ejecución validada. |
| [`WorkshopModuleDefinition`](#workshopmoduledefinition) | Una entrada de `modules[]` del manifiesto. |
| [`WorkshopDependency`](#workshopdependency) | Una entrada de `dependencies[]` del manifiesto. |
| [`WorkshopModuleType`](#workshopmoduletype) | Un valor de `modules[].type` del manifiesto. |
| [`WorkshopInstallation`](#workshopinstallation) | La declaración del autor sobre si los demás participantes deben instalar el mod (`multiplayer.installation` del manifiesto). |


## WorkshopModBase

```csharp
public abstract class WorkshopModBase : HorayModBase
```

La base de entrada para los mods del Workshop.

### Propiedades

#### Context

```csharp
public WorkshopModContext Context { get; }
```

Válido desde el momento en que se llama a OnModLoaded.

### Métodos

#### GetDataPackages

```csharp
protected IReadOnlyList<WorkshopDataPackage> GetDataPackages()
```

Paquetes de módulos de datos que nombran a este mod como su consumidor, en orden de carga. Llámalo cuando la activación haya terminado (después de los eventos de base de datos). Devuelve una lista vacía si no hay contexto (p. ej. la ruta de AddOns).


## WorkshopModContext

```csharp
public sealed class WorkshopModContext
```

Contexto de solo lectura que se entrega a un módulo DLL del Workshop. La ruta de instalación puede variar, así que los mods usan estos valores para encontrar sus propios archivos.

### Propiedades

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Ruta absoluta de la raíz del paquete (carpeta de instalación).


#### FileId

```csharp
public ulong FileId { get; }
```

El ID de archivo del Workshop de Steam. Los paquetes locales reciben un ID único derivado de su ruta.


#### Definition

```csharp
public WorkshopModDefinition Definition { get; }
```

La especificación de ejecución validada.


#### Module

```csharp
public WorkshopModuleDefinition Module { get; }
```

La entrada de módulo dll que creó esta instancia.


#### Options

```csharp
public ModOptions Options { get; }
```

Opciones por dispositivo para este modId. Compartidas por los módulos DLL del paquete y por sus copias local y suscrita.

### Constructores

#### WorkshopModContext

```csharp
public WorkshopModContext(string packageRoot, ulong fileId, WorkshopModDefinition definition, WorkshopModuleDefinition module)
```

*Sin descripción todavía.*

### Métodos

#### ResolvePath

```csharp
public string ResolvePath(string relativePath)
```

Convierte una ruta relativa a la raíz del paquete en una ruta absoluta. Las rutas que escapan de la raíz, o las rutas absolutas, se rechazan (lanza una excepción).


## HorayModBase

```csharp
public class HorayModBase
```

La base de las clases de entrada de mods DLL.

### Constantes y campos

#### metadata

```csharp
public Metadata metadata
```

Los metadatos recibidos en `LoadMod`.

### Métodos

#### LoadMod

```csharp
public void LoadMod(Metadata metadata)
```

Lo llama el cargador. Guarda los metadatos y luego llama a `OnModLoaded`.


#### OnModLoaded

```csharp
protected virtual void OnModLoaded()
```

El punto de inicialización del mod. Se llama antes de que se inicialicen las bases de datos, así que suscríbete aquí a los eventos de `HorayModAPI`.


#### UnloadMod

```csharp
public void UnloadMod()
```

Llama a `OnModUnloaded`.


#### OnModUnloaded

```csharp
protected virtual void OnModUnloaded()
```

El punto de descarga del mod. Pon aquí la limpieza, como cancelar la suscripción a eventos.

### Tipos anidados

#### HorayModBase.Metadata

```csharp
public struct Metadata
```

Información de identificación del mod que el cargador pasa a `LoadMod`.

##### Constantes y campos

###### modName

```csharp
public string modName
```

Nombre para mostrar.


###### modVersion

```csharp
public string modVersion
```

Versión.


###### modAuthor

```csharp
public string modAuthor
```

Autor.


## WorkshopDataPackage

```csharp
public sealed class WorkshopDataPackage
```

Un paquete de módulos de datos para que lo lea un mod consumidor. Obtenlo con `WorkshopModBase.GetDataPackages()`.

### Propiedades

#### ModId

```csharp
public string ModId { get; }
```

El modId del paquete.


#### Version

```csharp
public string Version { get; }
```

La versión del paquete.


#### FileId

```csharp
public ulong FileId { get; }
```

El ID de archivo del Workshop de Steam.


#### DisplayName

```csharp
public string DisplayName { get; }
```

Nombre para mostrar (info.title, o modId si no existe).


#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Ruta absoluta de la raíz del paquete.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Módulos de datos dirigidos a este consumidor, en orden de declaración.


#### Folders

```csharp
public IReadOnlyList<string> Folders { get; }
```

Rutas absolutas de las carpetas de los módulos, en orden de declaración. Igual que Modules[i].FullPath.

### Métodos

#### Collect

```csharp
public static IReadOnlyList<WorkshopDataPackage> Collect(string consumerModId, IEnumerable<WorkshopModActivation> activations)
```

Elige, de entre los registros de activación (en orden de carga), los paquetes que tienen módulos de datos para `consumerModId`. Incluye solo los estados en que las dependencias están satisfechas (Active, AlreadyActive, NoDllModules). Los datos de paquetes fallidos o aplicados parcialmente no se transmiten.


## WorkshopModDefinition

```csharp
public sealed class WorkshopModDefinition
```

Una especificación de ejecución validada. Inmutable.

### Propiedades

#### PackageRoot

```csharp
public string PackageRoot { get; }
```

Ruta absoluta de la raíz del paquete.


#### ModId

```csharp
public string ModId { get; }
```

El modId del manifiesto.


#### Version

```csharp
public string Version { get; }
```

La versión del manifiesto.


#### Author

```csharp
public string Author { get; }
```

El autor del manifiesto. null si no existe.


#### DisplayName

```csharp
public string DisplayName { get; }
```

info.title, o modId si no existe.


#### Installation

```csharp
public WorkshopInstallation Installation { get; }
```

Requisito de instalación en multijugador.


#### MultiplayerDeclared

```csharp
public bool MultiplayerDeclared { get; }
```

Si el objeto multiplayer se declaró explícitamente. Si es false, Installation contiene el valor por defecto normalizado.


#### Modules

```csharp
public IReadOnlyList<WorkshopModuleDefinition> Modules { get; }
```

Lista de módulos, en orden de declaración.


#### LibraryPaths

```csharp
public IReadOnlyList<string> LibraryPaths { get; }
```

Rutas absolutas de las DLL administradas auxiliares. No reciben callbacks de arranque.


#### Dependencies

```csharp
public IReadOnlyList<WorkshopDependency> Dependencies { get; }
```

Lista de mods de los que depende este mod.


#### HasDll

```csharp
public bool HasDll { get; }
```

Se deriva de si modules contiene un tipo dll. No hay un indicador aparte.


#### HasLocalization

```csharp
public bool HasLocalization { get; }
```

Si modules contiene un tipo localization.


#### HasCostume

```csharp
public bool HasCostume { get; }
```

Si modules contiene un tipo costume.


#### HasData

```csharp
public bool HasData { get; }
```

Si modules contiene un tipo data.

### Constructores

#### WorkshopModDefinition

```csharp
public WorkshopModDefinition(string packageRoot, string modId, string version, string author, string displayName, WorkshopInstallation installation, bool multiplayerDeclared, IReadOnlyList<WorkshopModuleDefinition> modules, IReadOnlyList<string> libraryPaths, IReadOnlyList<WorkshopDependency> dependencies)
```

*Sin descripción todavía.*

### Métodos

#### DataModulesFor

```csharp
public IReadOnlyList<WorkshopModuleDefinition> DataModulesFor(string consumerModId)
```

Módulos de datos que recibe `consumerModId`, en orden de declaración. Lista vacía si no hay ninguno. Un módulo que nombra un consumidor va solo a ese mod; un módulo que lo omite va a este mismo paquete y a todos los mods que dependen directamente de él.


## WorkshopModuleDefinition

```csharp
public sealed class WorkshopModuleDefinition
```

Una entrada de `modules[]` del manifiesto.

### Propiedades

#### Type

```csharp
public WorkshopModuleType Type { get; }
```

Tipo de módulo.


#### RelativePath

```csharp
public string RelativePath { get; }
```

Ruta relativa a la raíz del paquete (separadores normalizados a '/').


#### FullPath

```csharp
public string FullPath { get; }
```

Ruta absoluta validada.


#### EntryClass

```csharp
public string EntryClass { get; }
```

Nombre completo de la clase de entrada del módulo dll. null para localization.


#### Consumer

```csharp
public string Consumer { get; }
```

modId del mod que lee el módulo de datos. Si se omite (null), es el propio paquete más todos los mods que dependen directamente de él. Siempre null para los módulos que no son de datos.

### Constructores

#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass)
```

Lo crea el validador.


#### WorkshopModuleDefinition

```csharp
public WorkshopModuleDefinition(WorkshopModuleType type, string relativePath, string fullPath, string entryClass, string consumer)
```

Lo crea el validador.


## WorkshopDependency

```csharp
public sealed class WorkshopDependency
```

Una entrada de `dependencies[]` del manifiesto.

### Propiedades

#### ModId

```csharp
public string ModId { get; }
```

modId del mod del que se depende.


#### Version

```csharp
public string Version { get; }
```

null significa cualquier versión. Si se define, solo se acepta una coincidencia exacta (v1).


#### WorkshopId

```csharp
public ulong? WorkshopId { get; }
```

Una pista de dónde descargarlo. Debe comprobarse contra el modId/version real tras la instalación; por sí solo no demuestra que el mod descargado sea el correcto.

### Constructores

#### WorkshopDependency

```csharp
public WorkshopDependency(string modId, string version, ulong? workshopId)
```

Lo crea el validador.


## WorkshopModuleType

```csharp
public enum WorkshopModuleType
```

Un valor de `modules[].type` del manifiesto.

| Valor | Descripción |
|---|---|
| `Dll` | Módulo de DLL administrada |
| `Localization` | Módulo de carpeta de localización |
| `Costume` | Carpeta de paquete de trajes sin código (costume.json/skin.json + PNG) |
| `Data` | Carpeta de datos de uso general que lee otro mod (el consumidor); el juego no interpreta su contenido |


## WorkshopInstallation

```csharp
public enum WorkshopInstallation
```

La declaración del autor sobre si los demás participantes deben instalar el mod (`multiplayer.installation` del manifiesto).

| Valor | Descripción |
|---|---|
| `Unknown` | Sin declarar (se incluye en la comparación de configuración de la sala igual que Everyone) |
| `LocalOnly` | Funciona solo en el dispositivo del propio usuario; se excluye de la comparación de configuración de la sala |
| `Everyone` | Todos los miembros de la sala deben instalar el mismo paquete |
| `HostOnly` | Solo el anfitrión necesita instalarlo |
