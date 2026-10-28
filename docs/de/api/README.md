<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# API-Referenz

Eine Liste der öffentlichen Typen und Member, die DLL-Mods zur Verfügung stehen. Projekteinrichtung, Einstiegspunkte, Lebenszyklus und Mehrspieler-Regeln werden unter [DLL-Mods](../dll-mods.md) behandelt.

Generiert am 2026-10-26 für Spielversion **1.1.0**. Dieses Dokument wird bei jedem Spiel-Update neu generiert.

| Dokument | Typen | Inhalt |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Der statische Einstiegspunkt, den das Spiel Mods zur Verfügung stellt. |
| [Einstiegspunkt und Kontext](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Die Basisklassen, von denen die Einstiegsklasse einer Mod erbt, sowie die Kontext- und Manifesttypen, die der Loader anhängt. |
| [Mod-Optionen](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Gerätebezogene Mod-Einstellungen und eine API, um Schalter, Auswahlfelder und Schieberegler zum Optionsbildschirm des Spiels hinzuzufügen. |
| [Raum-Spawn-Zuweisung](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | Der Kontext, den das Event `HorayModAPI.OnAssignRoomSpawnPhases` übergibt. |
| [Spezialwerte](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Typen für die Werteanzeige, die in `HorayModAPI.OnLoadSpecialStatDatabase` mit `RegisterSpecialStatCategory` und `RegisterSpecialStat` registriert werden. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Dialog-, Item-Interaktions- und Widget-Typen, die von `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` und `TryGetUIWidgets` verwendet werden. |

## HorayModAPI-Events auf einen Blick

Aufgeführt in der Reihenfolge ihrer Deklaration im Quellcode. Datenbank-Events feuern bei jedem Laden einer Welt erneut.

| Event | Signatur | Beschreibung |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Feuert direkt nach der Initialisierung der Wunder-Datenbank. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Feuert direkt nach der Initialisierung der Waffen-Datenbank. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Feuert direkt nach der Initialisierung der Status-Datenbank (Werteeffekte). |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Feuert direkt nach dem Laden der Spezialwert-Kategorien und -Status. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Feuert direkt nach der Initialisierung der Item-Datenbank. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Feuert direkt nach der Initialisierung der Schlüsselwort-Datenbank. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Feuert direkt nach der Initialisierung der Passiv-Datenbank. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Feuert direkt nach der Initialisierung der Kostüm-Datenbank. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Feuert direkt nach der Initialisierung der Prop-Datenbank. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Feuert direkt nach der Initialisierung der Einheiten-Datenbank (Spawn-Entitäten für Gegner und NPCs). |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Feuert direkt nach der Initialisierung der NPC-Rollen-Datenbank. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Feuert direkt nach der Initialisierung der Datenbank für benutzerdefinierte Spiele. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Feuert direkt nachdem alle Datenbanken ihre Initialisierung abgeschlossen haben. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Wird aufgerufen, nachdem das Titelmenü geladen wurde. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Wird aufgerufen, wenn das Spiel startet. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Wird aufgerufen, wenn das Spiel startet. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Wird aufgerufen, wenn eine Spielsitzung startet. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Wird aufgerufen, wenn eine Spielsitzung startet. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Wird aufgerufen, nachdem die Lokalisierungsdateien geladen wurden. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Wird aufgerufen, wenn eine Etage zugewiesen wird. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Wird aufgerufen, wenn eine Etage zugewiesen wird. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Wird aufgerufen, nachdem die zufällige Gegnerzusammensetzung einer Stage festgelegt wurde. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Wird aufgerufen, wenn ein Spieler spawnt. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Wird aufgerufen, wenn ein Spieler-Avatar spawnt. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Feuert, wenn eine Einheit (Spieler, Gegner, Beschwörung oder Begleiter) auf dem Server spawnt. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Feuert direkt bevor eine Einheit vom Server entfernt wird. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Feuert, wenn ein Spieler einem Angriff ausweicht. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Erlaubt es, das Debuff-Prefab direkt vor dem Anwenden eines Debuffs auf eine Einheit auszutauschen. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Feuert, wenn der Server die Schreibberechtigung für das Inventar übernimmt und beginnt, alle Item-Effekte zu entfernen. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Feuert direkt nachdem der Server die Schreibberechtigung zurückgibt und die Item-Effekte erneut anwendet. |
