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
- No se admiten cargadores externos como BepInEx y MelonLoader.


## DLL auxiliares

Si tu mod usa otras bibliotecas (p. ej. paquetes de terceros distintos de Newtonsoft.Json), pon sus DLL en la carpeta de tu mod y decláralas en `libraries` dentro de `metadata.json`. Las DLL que no se declaran no se usan para resolver referencias.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- No incluyas DLL que ya vienen con el juego (Unity, Mirror, Newtonsoft.Json, 0Harmony, etc.). La copia del juego se carga primero, así que la copia que incluye tu mod se ignora o entra en conflicto.
- No se pueden usar DLL nativas (C/C++). Solo se cargan ensamblados administrados.
- Si otro mod ya ha cargado un ensamblado con el mismo nombre, el juego no elige uno de los dos; **deja el mod sin ejecutar**. Es más seguro separar las bibliotecas compartidas en un mod aparte y enlazarlo mediante `dependencies`.


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
- Si cambias tu código, debe pasar la revisión de nuevo antes de poder publicarlo. Consulta [Publicar en el Workshop](publishing.md).


## Consideraciones sobre multijugador

- La mayoría de los mods con código tendrán `multiplayer.installation` en `everyone`.
- Si tu mod cambia el estado del juego solo en eventos `Serverside`, y los valores que cambia son estado del juego que ya se sincroniza (p. ej. `customStats`), de modo que los participantes no necesitan ni código ni registros, puedes declararlo como `host_only`. Los participantes entonces no descargan el mod. Un mod que registra objetos nuevos, usa `RegisterNetworkPrefab` o cambia algo en eventos `Clientside` no puede ser `host_only`.
- Los objetos que aparecen a través de la red (unidades, proyectiles, etc.) deben registrarse con `RegisterNetworkPrefab` para que también se creen en el lado de los participantes. Si pasas un `stableKey`, se mantiene el mismo ID aunque cambie la versión de tu mod.
- El anfitrión y los participantes deben tener incluso el mismo **orden de carga** de mods. Evita código cuyos resultados dependan de ese orden.


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
