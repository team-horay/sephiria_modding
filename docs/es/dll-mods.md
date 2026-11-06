# Mods DLL

Los módulos DLL te permiten ejecutar código escrito en C# dentro del juego. Suscríbete a `HorayModAPI` para engancharte a jugadores, objetos, UI y más.
Este documento cubre todo, desde crear un proyecto hasta conseguir que tu primera DLL se ejecute en el juego. Para la lista completa de eventos y funciones de registro, consulta la [Referencia de la API](api/README.md).


## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) 8 o posterior
- Un editor de C#


## Crear un proyecto

Toma como referencia el ejemplo [`examples/dll-sample`](../../examples/dll-sample). Este documento lo usa como base.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>PhysicalDamagePlus</AssemblyName>
    <RootNamespace>PhysicalDamagePlus</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- Carpeta de instalación del juego. Cámbiala según tu configuración o pásala con dotnet build -p:GameDir=... -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\physical-damage-plus\bin\</OutDir>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="Assembly-CSharp">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <Reference Include="UnityEngine.CoreModule">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\UnityEngine.CoreModule.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <!-- Añade de la misma forma Mirror si trabajas con objetos de red como PlayerAvatar, UnityEngine.UI si construyes UI y 0Harmony si aplicas parches -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Apunta a `netstandard2.1`** y fija la versión del lenguaje en C# 9.
2. **Haz referencia a las DLL del juego, pero no las copies.** Apunta las referencias a los archivos de `<carpeta de instalación del juego>/Sephiria_Data/Managed/` y ponlas todas en `Private=false`. Si las DLL del juego se copian en la carpeta de tu mod, el mismo ensamblado se carga dos veces, lo que provoca errores difíciles de entender, y además añade archivos innecesarios a la revisión.
3. `Assembly-CSharp.dll` contiene la mayor parte del código que ejecuta el juego.


## Estructura del código


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Se llama una vez por proceso. Suscríbete a los eventos aquí, y solo una vez.
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] Loaded v{metadata.modVersion} | folder={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // Se llama solo en el anfitrión (servidor). Cambia el estado del juego aquí.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Se llama en todos los participantes. El valor que añadió el servidor llega mediante la sincronización.
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/PhysicalDamagePlus.dll", "entryClass": "PhysicalDamagePlus.Entry" }
]
```

### WorkshopModBase

Tu clase hereda lo siguiente de la clase base.

| Miembro | Descripción |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Se rellena con los valores de `metadata.json`. |
| `Context` | `PackageRoot` (ruta absoluta de la carpeta del mod), `FileId` (ID del elemento del Workshop de Steam; para paquetes locales, un valor aparte que nunca colisiona con un ID de Steam), `Definition` (el manifiesto validado), `ResolvePath(relativePath)`. Úsalo para encontrar archivos dentro de la carpeta de tu mod. La ruta de instalación es distinta para cada usuario, así que nunca la escribas a mano en el código. |
| `GetDataPackages()` | La lista de paquetes de módulos `data` que nombran a este mod como su `consumer`. Llámalo cuando la activación haya terminado (dentro de un manejador `OnLoad*Database` o después de `OnAllDatabasesReady`). |

`Context` es válido desde el momento en que se llama a `OnModLoaded`. En el constructor todavía es null.


## Ciclo de vida

Los mods DLL se activan **cuando empiezas una partida y entras en el mundo**. En la pantalla de título todavía no se ejecuta código. El orden de arranque es el siguiente.

1. Se carga la escena del mundo y arranca `GameDataLoader`.
2. Los módulos DLL del plan de carga se cargan en orden, se instancia cada clase de entrada y se llama a `OnModLoaded`.
3. A medida que se inicializan las bases de datos, se disparan uno tras otro eventos como `OnLoadItemDatabase` y `OnLoadUnitDatabase`.
4. Cuando todo ha terminado, se llama a `OnAllDatabasesReady`.

## Suscribirse

La clase `HorayModAPI` proporciona eventos predefinidos que te permiten engancharte a muchas situaciones.

- Suscríbete a los eventos **solo una vez**, en `OnModLoaded`. Los eventos de base de datos se disparan de nuevo en cada carga, así que haz las cosas que "deben ocurrir cada vez" (registrar objetos, etc.) dentro de los manejadores.
- Gestiona con cuidado la suscripción y la cancelación de suscripción. Cancela la suscripción (-=) a los eventos a los que te suscribiste en `OnModUnloaded`.

La lista completa de eventos está en la [Referencia de la API](api/README.md). La tabla de abajo indica el evento recomendado para cada cosa que puedas querer hacer.

| Qué quieres hacer | Evento |
|---|---|
| Añadir objetos o artefactos | `OnLoadItemDatabase` |
| Añadir armas, milagros, pasivas, trajes o props | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Añadir unidades enemigas o NPC | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Añadir viajeros (NPC del pueblo) | `OnLoadSocialIDDatabase` |
| Añadir una estadística a la pantalla de estadísticas | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Registrar texto localizado desde código | `OnLocalizationReady` (`AddText`) |
| Añadir un botón al menú de título | `OnTitleMenuReady` |
| Inicio de partida o de sesión | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Cuando se genera una planta | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Aparición de jugadores o unidades | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Engancharse a comprobaciones de combate | `OnPlayerEvade`, `OnGetDebuff` |
| Permisos de manipulación del inventario | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Evitar conflictos

Las entidades que añade tu mod se nombran con el formato `<modId>:<id>`, p. ej. `yourname.artifact-pack:flame-ring`. Esta regla evita conflictos con otros mods y es la forma en que se distingue el contenido de los mods en el historial de partidas y en las estadísticas.


## Lado del servidor, lado del cliente

- Sephiria está construido en torno al multijugador, así que muchos eventos vienen en versiones separadas para el lado del servidor y el lado del cliente.
- El anfitrión dispara ambas; los demás participantes solo disparan la Clientside.
- Como regla general, haz las cosas que cambian el estado del juego (apariciones, botines, daño) solo en Serverside, y las cosas que solo cambian lo que se muestra en Clientside.


## Parches de Harmony

Cuando necesites cambiar algo a lo que los eventos no llegan, puedes usar Harmony.
Desde el parche 1.1.0, **Harmony 2.4.3 viene integrado** en el juego, así que solo tienes que añadir una referencia.

```xml
<Reference Include="0Harmony">
  <HintPath>$(GameDir)\Sephiria_Data\Managed\0Harmony.dll</HintPath>
  <Private>false</Private>
</Reference>
```

```csharp
protected override void OnModLoaded()
{
    var harmony = new HarmonyLib.Harmony("yourname.first-mod");
    harmony.PatchAll();
}
```

- **No incluyas `0Harmony.dll` en tu mod.** Una copia incluida se ignora, y aunque incluyas una versión distinta, tu mod no se ejecutará con esa versión.
- Usar tu `modId` tal cual como ID de Harmony evita que colisione con otros.
- Los parches se rompen con las actualizaciones del juego mucho más fácilmente que la API oficial. Haz lo que puedas con eventos y deja los parches como último recurso.
- No se admiten cargadores externos como BepInEx y MelonLoader. Un mod que funciona como un cargador externo (es decir, que busca y ejecuta DLL de plugins de fuera del Workshop) recibe una advertencia y solo se activa después de que el usuario la acepte. Consulta los detalles en [Mods que ejecutan código de fuera del Workshop](#mods-que-ejecutan-código-de-fuera-del-workshop).


## DLL auxiliares

Si tu mod usa otras bibliotecas (p. ej. paquetes de terceros distintos de Newtonsoft.Json), pon sus DLL en la carpeta de tu mod y decláralas en `libraries` dentro de `metadata.json`. Las DLL que no se declaran no se usan para resolver referencias.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- No incluyas DLL que ya vienen con el juego (Unity, Mirror, Newtonsoft.Json, 0Harmony, etc.). La copia del juego se carga primero, así que la copia que incluye tu mod se ignora o entra en conflicto.
- No se pueden usar DLL nativas (C/C++). Solo se cargan ensamblados administrados.
- Si otro mod ya ha cargado un ensamblado con el mismo nombre, con una DLL de `modules` el juego no elige uno de los dos; **deja el mod sin ejecutar**. Con una DLL de `libraries`, el juego usa la que ya está cargada e ignora tu copia, lo que puede causar fallos si las versiones difieren. Es más seguro separar las bibliotecas compartidas en un mod aparte y enlazarlo mediante `dependencies`.

### Usar la biblioteca de otro mod

Si varios mods usan el mismo código, colócalo en un mod principal y haz que los mods dependientes lo enlacen mediante `dependencies`.

1. El mod principal declara el código compartido en `modules` o `libraries` y añade `Library` a sus `tags`.
2. El mod dependiente indica el `modId` del mod principal en `dependencies` y se compila contra la DLL del mod principal con `Private=false`. **No copies la DLL del mod principal dentro del mod dependiente.**
3. El juego carga primero el mod principal, así que las referencias del mod dependiente se resuelven con el ensamblado del mod principal que ya está cargado.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- El mod dependiente también necesita su propio módulo `dll` y su `entryClass`. No se admite que el mod principal busque y cargue por su cuenta las DLL del mod dependiente. En su lugar, haz que el mod dependiente llame a la función de registro del mod principal desde su propio `OnModLoaded`.
- Si el mod principal cambia sus tipos o miembros públicos en una actualización, los mods dependientes pueden romperse. Evita eliminar o cambiar las API públicas existentes de una biblioteca compartida.

### ¡No crees DLL en tiempo de ejecución!

Todo el código que ejecuta un mod debe estar dentro de un elemento del Workshop ya revisado. Si los archivos DLL de un mod cambian mientras el mod se está ejecutando, se restringe la carga de ese mod. Incluye todas las DLL tal cual en el elemento del Workshop, sin ningún tipo de compresión ni streaming. Es decir, lo siguiente puede infringir el sistema de revisión. Incluye en el mod todas las DLL que necesites al subirlo.

- El mod descarga una DLL del exterior mientras se ejecuta y carga el ensamblado
- El mod crea y ejecuta su propia DLL mientras se ejecuta (incluido reescribir otra DLL con Mono.Cecil o similar y luego ejecutarla)
- El mod extrae una DLL empaquetada en su interior con su propio formato de compresión y la usa

### Mods que ejecutan código de fuera del Workshop

Un mod que busca y ejecuta DLL que el usuario coloca **fuera de la carpeta del mod**, por ejemplo en la carpeta de instalación del juego o en `AppData` (como un mod de compatibilidad con plugins de otro cargador), no se rechaza, pero el sistema de revisión le añade una **advertencia**.

- Al activar el mod aparece una advertencia, y el mod solo se activa si el usuario la acepta. Cuando el mod se actualiza, se vuelve a preguntar.
- Las DLL externas que se ejecutan así no se revisan, y no se ofrece soporte para los problemas que causen.
- Un mod que descarga él mismo esas DLL mientras se ejecuta entra en los casos prohibidos indicados arriba.

Para usar el código de otro mod, no cargues DLL directamente desde la carpeta de ese mod; enlázalo mediante `dependencies` como se explica en [Usar la biblioteca de otro mod](#usar-la-biblioteca-de-otro-mod). Las DLL que el juego no ha cargado por sí mismo se saltan el orden de carga, la comprobación de conflictos de nombres y la de dependencias, por lo que no se admiten.

Si necesitas reescribir una DLL (por ejemplo, convertir IL para otra API de Harmony), hazlo antes de subir el mod e incluye en él la DLL ya convertida.


## Compilar y probar

```bash
dotnet build -c Release
```

- Cuando la compilación tenga éxito y la DLL aparezca en `WorkshopLocal/<mod>/bin/`, inicia el juego, comprueba que tu mod aparece en "Mis mods" en el panel del Taller de Steam de la pantalla de título y luego empieza una partida.
- Si dejas el `.pdb` junto con el resultado de la compilación, las trazas de pila del registro incluyen números de línea. Déjalo fuera cuando publiques.


## Actualizaciones del juego y compatibilidad

Cuando el juego se actualiza, `Assembly-CSharp.dll` cambia. Los mods que solo usan eventos oficiales normalmente siguen funcionando, pero los mods que tocan clases internas directamente o aplican parches con Harmony pueden romperse.

- Después de una actualización, recompila contra la nueva carpeta `Managed/` y prueba.
- Si indicas la **versión del juego compatible** en la descripción de tu página del Workshop, los usuarios pueden encontrar la causa rápidamente. La versión del juego también puede registrarse automáticamente en los metadatos del elemento del Workshop cuando publicas.
- Cuando cambies tu código, debe enviarse de nuevo al sistema de revisión. Consulta [Publicar en el Workshop](publishing.md#revisión-de-código).


## Consideraciones sobre multijugador

- La mayoría de los mods con código tendrán `multiplayer.installation` en `everyone`.
- Si tu mod cambia el estado del juego solo en eventos `Serverside`, y los valores que cambia son estado del juego que ya se sincroniza (p. ej. `customStats`), de modo que los participantes no necesitan ni código ni registros, puedes declararlo como `host_only`. Los participantes entonces no descargan el mod. Un mod que registra objetos nuevos, usa `RegisterNetworkPrefab` o cambia algo en eventos `Clientside` no puede ser `host_only`.
- Los objetos que aparecen a través de la red (unidades, proyectiles, etc.) deben registrarse con `RegisterNetworkPrefab` para que también se creen en el lado de los participantes. Si pasas un `stableKey`, se mantiene el mismo ID aunque cambie la versión de tu mod.
- El anfitrión y los participantes deben tener incluso el mismo **orden de carga** de mods. Evita código cuyos resultados dependan de ese orden.


## Sincronización de red (SyncVar, RPC, Command)

El multijugador de Sephiria está implementado con la biblioteca de red de código abierto "Mirror". En Mirror puedes usar la comunicación fácilmente declarando los siguientes atributos.

- `[SyncVar]`: atributo que se coloca en una variable miembro de una clase. Sincroniza el valor de la variable entre el servidor y los clientes.
- `[ClientRpc]`, `[TargetRpc]`: atributos que se colocan en una función. Piden a los clientes que ejecuten la función desde el servidor.
- `[Command]`: atributo que se coloca en una función. Pide al servidor que ejecute la función desde un cliente.

En un entorno de mods estos atributos no se pueden usar con normalidad. Solo funcionan si el Weaver de Mirror inserta el código de transmisión correspondiente a cada atributo durante la compilación, pero una DLL generada fuera del juego no pasa por el Weaver, así que aunque la compilación tenga éxito, estos atributos no hacen nada. Este tipo de error se ve normal en la pantalla del anfitrión y solo da problemas en la de los participantes, por lo que es difícil de detectar si pruebas solo. (Aunque forzaras el paso por el Weaver, podría haber errores como colisiones de hash de funciones.)


### `SyncVar<T>`, `ModRpc`

Para resolverlo, Sephiria ofrece código de comunicación para mods que sustituye a esos atributos: `SyncVar<T>` y `ModRpc`.

| Lo que quieres hacer | Qué usar |
|---|---|
| Mantener un valor sincronizado de forma continua (incluido un participante que se une tarde) | `SyncVar<T>` |
| Llamada del servidor a todos los clientes | `ModRpc.Send` |
| Llamada del servidor a un cliente concreto | `ModRpc.SendTo` |
| Llamada de un cliente al servidor | `ModRpc.SendCommand` |
| Cambiar un valor que el juego ya sincroniza | Propiedad `Network<FieldName>` |

- Esta función está disponible a partir de la versión 1.1.2 del juego.
- Los mods que usan esta función deben declarar `multiplayer.installation` como `everyone`.


### Preparar el componente

`SyncVar<T>` y `ModRpc` solo se pueden usar en un componente que herede de `NetworkBehaviour`. Este componente debe estar añadido al mismo objeto, **en el mismo orden**, tanto en el anfitrión como en los participantes. Si lo añades con `AddComponent` a un objeto que ya ha aparecido, no entra en la sincronización, así que debe estar ya añadido antes de registrar el objeto como prefab de red.

```csharp
private static GameObject prefab;

// Suscríbete a HorayModAPI.OnAllDatabasesReady desde OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Colócalo bajo un padre inactivo. El Awake del prefab no se ejecuta, y las copias creadas con Instantiate empiezan activas.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Haz aparecer el objeto desde el servidor. Si pasas el segundo argumento, ese jugador pasa a ser su propietario.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: sincronizar valores

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Como no hay Weaver, regístralo tú mismo en el constructor. El orden de los campos debe ser siempre el mismo.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Cambia el valor **en el servidor**, por ejemplo con `stack.Value = 3`. Un valor cambiado por un participante no se transmite al servidor.
- Un participante que se une tarde también recibe el valor actual al entrar.
- `Callback` se llama en el anfitrión cuando se asigna un valor y en los participantes cuando se recibe un valor. También se llama si el valor recibido al entrar es distinto del valor por defecto. Una excepción en el callback se registra en el log y la sincronización continúa.
- Puedes usar tipos básicos como `int`, `float`, `string` y `Vector3`. Para un struct creado por un mod no se genera automáticamente el código de serialización, así que envía sus campos por separado.
- Para referirte a otro objeto, en lugar de `SyncVar<GameObject>` envía su `netId` como `SyncVar<uint>`. Es posible que ese objeto todavía no se haya creado en el lado receptor.
- El número de SyncVar que se pueden registrar en un componente es 64, contando los heredados.

### `ModRpc`: llamar a funciones

Registra la función una vez al cargar el mod. El nombre que se usa al llamar es `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Servidor → todos los clientes que ven este objeto
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Servidor → un cliente. Si pasas null como conexión, se envía al propietario del objeto.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Cliente → servidor
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Lee en el mismo orden en que escribiste. Si el orden o los tipos de escritura y lectura difieren, los valores se descuadran o se produce una excepción.
- Por defecto, `RegisterCommand` solo puede ser llamado por **el propietario del objeto**. Para recibirlo en un objeto sin propietario, pasa `requiresAuthority: false`. En ese caso, cualquier participante puede llamarlo. Ten cuidado con las trampas, etc.
- Si lo envías en una dirección distinta de la registrada, o con un nombre no registrado, solo queda un log de error y no se transmite.
- Si la función no existe en el lado receptor o el tipo de componente es distinto, solo se deja un log de advertencia y se ignora. Una excepción en el manejador también queda en el log y el juego continúa.
- El canal por defecto es el transporte fiable. Para efectos que se envían con frecuencia, puedes pasar el argumento `channelId: Channels.Unreliable`.

### Lo que debes cumplir sí o sí

- Un cliente puede enviar valores arbitrarios mediante un Command. Valida en el manejador del servidor el número de ranura, la cantidad, el objetivo, etc.
- Un participante que se une tarde no recibe los RPC anteriores a su entrada. Envía con `SyncVar<T>` el estado que deba mantenerse, y usa los RPC para cosas pasajeras como sonidos y efectos.
- **Un RPC puede llegar antes que el SyncVar.** Si lees `stack.Value` en el manejador de un RPC, puede ser el valor anterior. Si necesitas el valor nuevo, envíalo también como argumento del RPC o procésalo en `Callback`.

### Cambiar valores del código base de Sephiria

Si asignas directamente a un campo `[SyncVar]` de una clase del juego, el valor no se transmite a los participantes. Usa la propiedad con el prefijo `Network`. Las mayúsculas y minúsculas son las del nombre del campo, tal cual.

```csharp
unit.maxHp = 500;         // Solo cambia el valor del anfitrión.
unit.NetworkmaxHp = 500;  // También se transmite a los participantes.
```

Antes de que el objeto aparezca (en el prefab o justo después de `Instantiate`), incluso una asignación directa se transmite junto con el valor al aparecer.

### Pruebas

El anfitrión solo no deja al descubierto los problemas anteriores. Antes de publicar, comprueba con dos PC las pantallas del anfitrión y del participante, y prueba también el caso de entrar tarde después de cambiar un valor. Si activas el paquete local (`WorkshopLocal`), nadie más puede entrar en la sala, así que las pruebas multijugador se hacen suscribiéndote a un elemento que hayas subido al Workshop como Unlisted. Un elemento que subes por primera vez es Unlisted automáticamente.


## Solución de problemas

| Síntoma | Qué comprobar |
|---|---|
| El resumen de estado dice que no se pudo encontrar la clase de entrada | El espacio de nombres o el nombre de clase de `entryClass` no coincide con tu código. Las clases anidadas se escriben `Outer+Inner`. |
| El resumen de estado muestra un error de tipo de la clase de entrada | La clase de entrada no hereda de `WorkshopModBase` (ni de `HorayModBase`), no es pública, es abstracta o no tiene un constructor público sin parámetros. |
| El resumen de estado dice que el mod se retuvo por un conflicto de nombre de ensamblado | Otro mod (incluidos los AddOns) usa el mismo `AssemblyName`. Cambia el nombre del tuyo. |
| El resumen de estado muestra un fallo al cargar una biblioteca | Falta una DLL listada en `libraries` o no es un ensamblado administrado. |
| `FileNotFoundException` en el registro | Una DLL referenciada no está en la carpeta de tu mod o no está declarada en `libraries`. O se copió una DLL del juego con `Private=true` y las dos entran en conflicto. |
| Excepción justo después de entrar en el mundo | Una excepción en un manejador de evento de base de datos. Busca el espacio de nombres de tu mod en la traza de pila del registro. |
| Los efectos se acumulan al volver al título y entrar de nuevo | Te estás suscribiendo de nuevo dentro de un manejador. Suscríbete solo una vez, en `OnModLoaded`. |


## Ejemplo

[`examples/dll-sample`](../../examples/dll-sample) es un mod mínimo que aumenta el Daño Físico en 5 cuando aparece el jugador. Su carpeta `package/` incluye una DLL ya compilada, así que copiar esa carpeta en `WorkshopLocal/` hace que funcione de inmediato sin compilar. Para convertirlo en tu propio mod, solo tienes que cambiar `AssemblyName`, `modId`, `entryClass` y la ruta de salida. Empieza una partida y abre la ventana de estadísticas para verlo funcionando como Daño Físico +5.
