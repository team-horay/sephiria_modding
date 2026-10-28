<!-- Archivo generado automáticamente. Tools/ModApiDocGen en el repositorio del juego lo construye a partir de los comentarios /// del código fuente. No lo edites directamente; corrige los comentarios del código y vuelve a generarlo. -->
# Referencia de la API

Lista de los tipos y miembros públicos disponibles para los mods DLL. La configuración del proyecto, los puntos de entrada, el ciclo de vida y las reglas de multijugador se tratan en [Mods DLL](../dll-mods.md).

Generado el 2026-10-26 para la versión del juego **1.1.0**. Este documento se vuelve a generar cada vez que el juego se actualiza.

| Documento | Tipos | Contenido |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | El punto de entrada estático que el juego expone a los mods. |
| [Punto de entrada y contexto](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Las clases base de las que hereda la clase de entrada de un mod, más los tipos de contexto y manifiesto que adjunta el cargador. |
| [Opciones de mod](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Ajustes de mod por dispositivo y una API para añadir interruptores, cuadros de selección y deslizadores a la pantalla de opciones del juego. |
| [Asignación de apariciones en salas](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | El contexto que pasa el evento `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Estadísticas especiales](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Tipos de visualización de estadísticas que se registran con `RegisterSpecialStatCategory` y `RegisterSpecialStat` en `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Tipos de diálogo, interacción con objetos y widgets que usan `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` y `TryGetUIWidgets`. |

## Eventos de HorayModAPI de un vistazo

Listados en el orden en que se declaran en el código fuente. Los eventos de base de datos se disparan de nuevo cada vez que se carga un mundo.

| Evento | Firma | Descripción |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Se dispara justo después de inicializarse la base de datos de milagros. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Se dispara justo después de inicializarse la base de datos de armas. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Se dispara justo después de inicializarse la base de datos de estados (efectos de estadísticas). |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Se dispara justo después de cargarse las categorías y los estados de estadísticas especiales. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Se dispara justo después de inicializarse la base de datos de objetos. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Se dispara justo después de inicializarse la base de datos de palabras clave. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Se dispara justo después de inicializarse la base de datos de pasivas. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Se dispara justo después de inicializarse la base de datos de trajes. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Se dispara justo después de inicializarse la base de datos de props. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Se dispara justo después de inicializarse la base de datos de unidades (entidades de aparición de enemigos y NPC). |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Se dispara justo después de inicializarse la base de datos de roles de NPC. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Se dispara justo después de inicializarse la base de datos de partidas personalizadas. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Se dispara justo después de que todas las bases de datos terminen de inicializarse. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Se llama después de cargarse el menú de título. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Se llama cuando empieza la partida. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Se llama cuando empieza la partida. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Se llama cuando empieza una sesión de juego. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Se llama cuando empieza una sesión de juego. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Se llama después de cargarse los archivos de localización. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Se llama cuando se asigna una planta. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Se llama cuando se asigna una planta. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Se llama después de decidirse la composición aleatoria de enemigos de una etapa. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Se llama cuando aparece un jugador. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Se llama cuando aparece el avatar de un jugador. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Se dispara cuando una unidad (jugador, enemigo, invocación o compañero) aparece en el servidor. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Se dispara justo antes de que una unidad se elimine del servidor. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Se dispara cuando un jugador esquiva un ataque. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Permite sustituir el prefab de perjuicio justo antes de que se aplique un perjuicio a una unidad. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Se dispara cuando el servidor toma el permiso de escritura del inventario y empieza a eliminar todos los efectos de objetos. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Se dispara justo después de que el servidor devuelva el permiso de escritura y vuelva a aplicar los efectos de objetos. |
