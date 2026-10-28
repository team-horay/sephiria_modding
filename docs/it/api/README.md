<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# Riferimento API

Un elenco dei tipi e dei membri pubblici disponibili per le mod DLL. Configurazione del progetto, punti di ingresso, ciclo di vita e regole del multigiocatore sono trattati in [Mod DLL](../dll-mods.md).

Generato il 2026-10-26 per la versione del gioco **1.1.0**. Questo documento viene rigenerato a ogni aggiornamento del gioco.

| Documento | Tipi | Contenuto |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Il punto di ingresso statico che il gioco espone alle mod. |
| [Punto di ingresso e contesto](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Le classi base da cui eredita la classe di ingresso di una mod, più i tipi di contesto e manifest che il loader vi associa. |
| [Opzioni delle mod](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Impostazioni delle mod per dispositivo, e un'API per aggiungere interruttori, caselle di selezione e cursori alla schermata delle opzioni del gioco. |
| [Assegnazione degli spawn nelle stanze](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | Il contesto passato dall'evento `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Statistiche speciali](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Tipi per la visualizzazione delle statistiche registrati con `RegisterSpecialStatCategory` e `RegisterSpecialStat` in `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [Interfaccia](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Tipi per dialoghi, interazione con gli oggetti e widget usati da `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` e `TryGetUIWidgets`. |

## Eventi di HorayModAPI in sintesi

Elencati nell'ordine in cui sono dichiarati nel sorgente. Gli eventi dei database vengono emessi di nuovo ogni volta che viene caricato un mondo.

| Evento | Firma | Descrizione |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Emesso subito dopo l'inizializzazione del database dei miracoli. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Emesso subito dopo l'inizializzazione del database delle armi. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Emesso subito dopo l'inizializzazione del database degli stati (effetti sulle statistiche). |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Emesso subito dopo il caricamento delle categorie e degli stati delle statistiche speciali. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Emesso subito dopo l'inizializzazione del database degli oggetti. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Emesso subito dopo l'inizializzazione del database delle parole chiave. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Emesso subito dopo l'inizializzazione del database delle passive. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Emesso subito dopo l'inizializzazione del database dei costumi. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Emesso subito dopo l'inizializzazione del database dei prop. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Emesso subito dopo l'inizializzazione del database delle unità (entità di spawn di nemici e NPC). |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Emesso subito dopo l'inizializzazione del database dei ruoli degli NPC. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Emesso subito dopo l'inizializzazione del database delle partite personalizzate. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Emesso subito dopo che tutti i database hanno completato l'inizializzazione. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Chiamato dopo il caricamento del menu del titolo. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Chiamato all'avvio della partita. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Chiamato all'avvio della partita. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Chiamato all'avvio di una sessione di gioco. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Chiamato all'avvio di una sessione di gioco. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Chiamato dopo il caricamento dei file di localizzazione. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Chiamato quando viene allocato un piano. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Chiamato quando viene allocato un piano. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Chiamato dopo che è stata decisa la composizione casuale dei nemici di uno stage. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Chiamato quando un giocatore compare. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Chiamato quando compare l'avatar di un giocatore. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Emesso quando un'unità (giocatore, nemico, evocazione o compagno) compare sul server. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Emesso subito prima che un'unità venga rimossa dal server. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Emesso quando un giocatore schiva un attacco. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Permette di sostituire il prefab del debuff subito prima che venga applicato a un'unità. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Emesso quando il server prende il permesso di scrittura sull'inventario e inizia a rimuovere tutti gli effetti degli oggetti. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Emesso subito dopo che il server restituisce il permesso di scrittura e riapplica gli effetti degli oggetti. |
