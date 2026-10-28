<!-- Automatiskt genererad fil. Tools/ModApiDocGen i spelets repo bygger den från ///-kommentarer i källkoden. Redigera den inte direkt; rätta källkodskommentarerna och generera om. -->
# API-referens

En lista över de publika typer och medlemmar som är tillgängliga för DLL-moddar. Projektuppsättning, startpunkter, livscykel och regler för flerspelarläge beskrivs i [DLL-moddar](../dll-mods.md).

Genererad 2026-10-26 för spelversion **1.1.0**. Det här dokumentet genereras om varje gång spelet uppdateras.

| Dokument | Typer | Innehåll |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Den statiska startpunkt som spelet exponerar för moddar. |
| [Startpunkt och kontext](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Basklasserna som en mods startpunktsklass ärver, plus de kontext- och manifesttyper som laddaren kopplar på. |
| [Modalternativ](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Modinställningar per enhet, och ett API för att lägga till reglage, valrutor och skjutreglage på spelets alternativskärm. |
| [Tilldelning av rumsspawn](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | Kontexten som skickas av händelsen `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Specialstats](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Statvisningstyper som registreras med `RegisterSpecialStatCategory` och `RegisterSpecialStat` i `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Dialog-, föremålsinteraktions- och widgettyper som används av `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` och `TryGetUIWidgets`. |

## Översikt över HorayModAPI-händelser

Listade i den ordning de deklareras i källkoden. Databashändelser avfyras igen varje gång en värld läses in.

| Händelse | Signatur | Beskrivning |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Avfyras direkt efter att mirakeldatabasen har initierats. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Avfyras direkt efter att vapendatabasen har initierats. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Avfyras direkt efter att status-databasen (stateffekter) har initierats. |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Avfyras direkt efter att specialstat-kategorier och statusar har lästs in. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Avfyras direkt efter att föremålsdatabasen har initierats. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Avfyras direkt efter att nyckelordsdatabasen har initierats. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Avfyras direkt efter att databasen för passiva förmågor har initierats. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Avfyras direkt efter att dräktdatabasen har initierats. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Avfyras direkt efter att rekvisitadatabasen har initierats. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Avfyras direkt efter att enhetsdatabasen (spawn-entiteter för fiender och NPC:er) har initierats. |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Avfyras direkt efter att NPC-rolldatabasen har initierats. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Avfyras direkt efter att databasen för anpassade spel har initierats. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Avfyras direkt efter att alla databaser har initierats klart. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Anropas efter att titelmenyn har lästs in. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Anropas när spelet startar. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Anropas när spelet startar. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Anropas när en spelsession startar. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Anropas när en spelsession startar. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Anropas efter att lokaliseringsfilerna har lästs in. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Anropas när en våning allokeras. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Anropas när en våning allokeras. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Anropas efter att den slumpmässiga fiendesammansättningen för en bana har bestämts. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Anropas när en spelare spawnar. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Anropas när en spelaravatar spawnar. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Avfyras när en enhet (spelare, fiende, åkallad varelse eller följeslagare) spawnar på servern. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Avfyras direkt innan en enhet tas bort från servern. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Avfyras när en spelare undviker en attack. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Låter dig byta ut debuff-prefaben direkt innan en debuff appliceras på en enhet. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Avfyras när servern tar skrivbehörighet för inventariet och börjar ta bort alla föremålseffekter. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Avfyras direkt efter att servern lämnar tillbaka skrivbehörigheten och applicerar föremålseffekterna igen. |
