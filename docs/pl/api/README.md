<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj go bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# Dokumentacja API

Lista publicznych typów i składowych dostępnych dla modów DLL. Konfiguracja projektu, punkty wejścia, cykl życia i zasady trybu wieloosobowego są opisane w [modach DLL](../dll-mods.md).

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. Ten dokument jest generowany ponownie przy każdej aktualizacji gry.

| Dokument | Typy | Zawartość |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Statyczny punkt wejścia, który gra udostępnia modom. |
| [Punkt wejścia i kontekst](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Klasy bazowe, po których dziedziczy klasa wejścia moda, oraz typy kontekstu i manifestu dołączane przez loader. |
| [Opcje modów](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Ustawienia moda zapisywane na urządzeniu oraz API do dodawania przełączników, pól wyboru i suwaków do ekranu opcji gry. |
| [Przydział spawnów w pokojach](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | Kontekst przekazywany przez zdarzenie `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Statystyki specjalne](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Typy wyświetlania statystyk rejestrowane przez `RegisterSpecialStatCategory` i `RegisterSpecialStat` w `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Typy dialogów, interakcji z przedmiotami i widżetów używane przez `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` i `TryGetUIWidgets`. |

## Zdarzenia HorayModAPI w skrócie

Wymienione w kolejności deklaracji w źródłach. Zdarzenia baz danych odpalają się ponownie przy każdym ładowaniu świata.

| Zdarzenie | Sygnatura | Opis |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych cudów. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych broni. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych statusów (efektów statystyk). |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Odpala się zaraz po załadowaniu kategorii i statusów statystyk specjalnych. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych przedmiotów. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych słów kluczowych. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych pasywów. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych strojów. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych rekwizytów. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych jednostek (encji spawnowanych wrogów i NPC). |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych ról NPC. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Odpala się zaraz po zainicjalizowaniu bazy danych gier niestandardowych. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Odpala się zaraz po zakończeniu inicjalizacji wszystkich baz danych. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Wywoływane po załadowaniu menu tytułowego. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Wywoływane, gdy gra się rozpoczyna. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Wywoływane, gdy gra się rozpoczyna. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Wywoływane, gdy rozpoczyna się sesja gry. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Wywoływane, gdy rozpoczyna się sesja gry. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Wywoływane po załadowaniu plików lokalizacji. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Wywoływane, gdy piętro zostaje przydzielone. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Wywoływane, gdy piętro zostaje przydzielone. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Wywoływane po ustaleniu losowego składu wrogów dla etapu. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Wywoływane, gdy pojawia się gracz. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Wywoływane, gdy pojawia się awatar gracza. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Odpala się, gdy na serwerze pojawia się jednostka (gracz, wróg, przywołaniec lub towarzysz). |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Odpala się tuż przed usunięciem jednostki z serwera. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Odpala się, gdy gracz unika ataku. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Pozwala podmienić prefab debuffa tuż przed nałożeniem go na jednostkę. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Odpala się, gdy serwer przejmuje uprawnienie do zapisu ekwipunku i zaczyna usuwać wszystkie efekty przedmiotów. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Odpala się zaraz po tym, jak serwer zwraca uprawnienie do zapisu i ponownie nakłada efekty przedmiotów. |
