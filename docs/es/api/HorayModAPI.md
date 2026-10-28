<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# HorayModAPI

El punto de entrada estático que el juego expone a los mods. Suscríbete a los eventos de carga de bases de datos, sesión y aparición, y registra prefabs de red y estadísticas especiales. Suscríbete a los eventos solo una vez, en `OnModLoaded`. Para el ciclo de vida y las reglas de multijugador, consulta [Mods DLL](../dll-mods.md).

Generado el 2026-10-26 para la versión del juego **1.1.0**. [Índice de la referencia](README.md)

| Tipo | Descripción |
|---|---|
| [`HorayModAPI`](#horaymodapi) | El punto de entrada para los mods. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Un ayudante para registrar texto de localización de mods. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

El punto de entrada para los mods.

Ejemplo:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Constantes y campos

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Sin descripción todavía.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Sin descripción todavía.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Sin descripción todavía.*

### Eventos

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Se dispara justo después de inicializarse la base de datos de milagros. Registra o modifica milagros con `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Se dispara justo después de inicializarse la base de datos de armas. Registra o modifica armas con `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Se dispara justo después de inicializarse la base de datos de estados (efectos de estadísticas). Registra o modifica estados con `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Se dispara justo después de cargarse las categorías y los estados de estadísticas especiales. Registra entradas de visualización con `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Se dispara justo después de inicializarse la base de datos de objetos. Registra o modifica objetos con `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Se dispara justo después de inicializarse la base de datos de palabras clave. Registra o modifica palabras clave con `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Se dispara justo después de inicializarse la base de datos de pasivas. Registra o modifica pasivas con `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Se dispara justo después de inicializarse la base de datos de trajes. Registra o modifica trajes con `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Se dispara justo después de inicializarse la base de datos de props. Registra props con `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Se dispara justo después de inicializarse la base de datos de unidades (entidades de aparición de enemigos y NPC).


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Se dispara justo después de inicializarse la base de datos de roles de NPC.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Se dispara justo después de inicializarse la base de datos de partidas personalizadas.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Se dispara justo después de que todas las bases de datos terminen de inicializarse.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Se llama después de cargarse el menú de título.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Se llama cuando empieza la partida. (lado del servidor) Empezar significa hacer clic en una ranura de guardado en la pantalla de título para entrar en el juego. Los datos de sesión, como el jugador y la ubicación actual, todavía no se han cargado.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Se llama cuando empieza la partida. (lado del cliente) Empezar significa hacer clic en una ranura de guardado en la pantalla de título para entrar en el juego. Los datos de sesión, como el jugador y la ubicación actual, todavía no se han cargado.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Se llama cuando empieza una sesión de juego. (lado del servidor) Una sesión es una unidad de juego. Cuando el jugador muere, esa sesión termina y empieza una nueva.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Se llama cuando empieza una sesión de juego. (lado del cliente) Una sesión es una unidad de juego. Cuando el jugador muere, esa sesión termina y empieza una nueva.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Se llama después de cargarse los archivos de localización.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Se llama cuando se asigna una planta. (lado del servidor) Cada ubicación de Sephiria se define como una planta. Cuando un jugador se mueve entre ubicaciones, se ejecutan en orden los pasos siguientes. 1. Asignación de planta: crea y carga la nueva planta. 2. Movimiento: mueve al jugador al punto solicitado de la planta ya cargada por completo. 3. Eliminación de planta: elimina la ubicación en la que estaba el jugador antes, si está vacía (= no queda ningún jugador en ella).
param1: GUID de la planta  
param2: Nombre de la planta  
param3: Instancia de FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Se llama cuando se asigna una planta. (lado del cliente) Cada ubicación de Sephiria se define como una planta. Cuando un jugador se mueve entre ubicaciones, se ejecutan en orden los pasos siguientes. 1. Asignación de planta: crea y carga la nueva planta. 2. Movimiento: mueve al jugador al punto solicitado de la planta ya cargada por completo. 3. Eliminación de planta: elimina la ubicación en la que estaba el jugador antes, si está vacía (= no queda ningún jugador en ella).
param1: GUID de la planta  
param2: Nombre de la planta  
param3: Instancia de FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Se llama después de decidirse la composición aleatoria de enemigos de una etapa. (lado del servidor) Se dispara justo después de que el generador de plantas elija las fases de monstruos de una sala de batalla (o de una zona de batalla), antes de que el spawner las reciba. Sustituye o edita ctx.Phases para cambiar lo que aparece ahí. El juego no impone equilibrio en este punto; consulta RoomSpawnAssignContext para ver las reglas (RNG determinista mediante ctx.Seed, nunca tocar ctx.Sources, las entidades deben estar registradas en UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Se llama cuando aparece un jugador. (lado del servidor) Puedes registrar callbacks relacionados con el jugador dentro de este hook. Se lanza al final de PlayerAvatar.OnStartServer, después de aplicarse las estadísticas base. El traje, la planta y el contenido del inventario pueden rellenarse más tarde en el mismo fotograma o en los siguientes. Úsalo para suscribirte a los eventos de instancia del propio avatar (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Las suscripciones en el avatar mueren con el avatar, así que no hace falta cancelarlas.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Se llama cuando aparece el avatar de un jugador. (lado del cliente) Se lanza una vez por avatar al final de su OnStartClient; también se dispara para los jugadores remotos. Comprueba avatar.isLocalPlayer / avatar.isOwned para distinguir al jugador local. Las SyncVars ya están aplicadas, pero la planta y el contenido del inventario pueden llegar más tarde.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Se dispara cuando una unidad (jugador, enemigo, invocación o compañero) aparece en el servidor. Solo en el servidor (anfitrión).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Se dispara justo antes de que una unidad se elimine del servidor. Solo en el servidor (anfitrión).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Se dispara cuando un jugador esquiva un ataque. (lado del servidor)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Permite sustituir el prefab de perjuicio justo antes de que se aplique un perjuicio a una unidad. El manejador recibe (prefab actual, lanzador) y devuelve un prefab de sustitución; devolver null lo deja sin cambios.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Se dispara cuando el servidor toma el permiso de escritura del inventario y empieza a eliminar todos los efectos de objetos.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Se dispara justo después de que el servidor devuelva el permiso de escritura y vuelva a aplicar los efectos de objetos.

### Métodos

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Registra un objeto (prefab) que puede aparecer a través de la red. El objeto debe tener un componente NetworkIdentity.

El servidor (anfitrión) y los clientes deben mantener cada uno una lista de objetos que pueden usar la red, y esa lista debe ser idéntica para todos.

Ejemplo:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Devuelve: true si el registro tuvo éxito


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Registra un prefab creado en tiempo de ejecución (como un clon de una plantilla integrada) como objeto que puede aparecer en red, usando un assetId fijo calculado a partir de `stableKey`. Usa el nombre de la unidad del mod (`<modId>:<id>`) como clave.

Devuelve: true si el registro tuvo éxito


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Registra varios prefabs a la vez como objetos que pueden aparecer en red.

Devuelve: el número de prefabs registrados con éxito


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Elimina el registro de un prefab de red registrado.

Devuelve: true si se eliminó


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Registra una categoría de estadísticas especiales en la UI de estadísticas.

Devuelve: true si se registró


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Registra un StatusEntity como entrada de una categoría de estadísticas especiales en la UI de estadísticas. Si pasas `reader`, el valor se lee mediante esa función en lugar de con la configuración del asset.

Devuelve: true si se registró


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Obtiene las opciones por dispositivo vinculadas a modId. Los mods del Workshop pueden usar `Context.Options` en su lugar.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Abre un panel de interacción con objetos que se usa junto con el inventario. Devuelve false si la UI está ocupada.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Obtiene una fábrica de widgets de UI con el estilo del juego. Adjúntalos bajo un Canvas de UI existente.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Abre un cuadro de diálogo en el mundo. Devuelve false si la UI está ocupada; muestra la página siguiente con `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Abre un cuadro de diálogo con un desplazamiento en el espacio del mundo. Si es null, se usa el desplazamiento por defecto del prefab.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Abre un cuadro de diálogo con desplazamientos separados para el diálogo y la cámara. El enfoque de la cámara solo se aplica en local.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Un ayudante para registrar texto de localización de mods.

### Métodos

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Registra texto para un idioma concreto.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Registra texto para varios idiomas a la vez
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
