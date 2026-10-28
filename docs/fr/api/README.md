<!-- Fichier généré automatiquement. Tools/ModApiDocGen dans le dépôt du jeu le construit à partir des commentaires /// du code source. Ne le modifiez pas directement ; corrigez les commentaires du code source et régénérez-le. -->
# Référence de l'API

La liste des types et membres publics disponibles pour les mods DLL. La configuration du projet, les points d'entrée, le cycle de vie et les règles du multijoueur sont traités dans [Mods DLL](../dll-mods.md).

Généré le 2026-10-26 pour la version **1.1.0** du jeu. Ce document est régénéré à chaque mise à jour du jeu.

| Document | Types | Contenu |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Le point d'entrée statique que le jeu expose aux mods. |
| [Point d'entrée et contexte](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Les classes de base dont hérite la classe de point d'entrée d'un mod, ainsi que les types de contexte et de manifeste attachés par le chargeur. |
| [Options de mod](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Les paramètres de mod propres à chaque appareil, et une API pour ajouter des interrupteurs, des listes de sélection et des curseurs à l'écran des options du jeu. |
| [Attribution des apparitions en salle](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | Le contexte transmis par l'événement `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Stats spéciales](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Les types d'affichage de stats enregistrés avec `RegisterSpecialStatCategory` et `RegisterSpecialStat` dans `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [Interface](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Les types de dialogue, d'interaction avec les objets et de widgets utilisés par `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` et `TryGetUIWidgets`. |

## Aperçu des événements de HorayModAPI

Listés dans l'ordre de leur déclaration dans le code source. Les événements de base de données se déclenchent à nouveau à chaque chargement d'un monde.

| Événement | Signature | Description |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Se déclenche juste après l'initialisation de la base de données des miracles. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Se déclenche juste après l'initialisation de la base de données des armes. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Se déclenche juste après l'initialisation de la base de données des statuts (effets de stats). |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Se déclenche juste après le chargement des catégories et statuts de stats spéciales. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Se déclenche juste après l'initialisation de la base de données des objets. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Se déclenche juste après l'initialisation de la base de données des mots-clés. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Se déclenche juste après l'initialisation de la base de données des passifs. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Se déclenche juste après l'initialisation de la base de données des tenues. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Se déclenche juste après l'initialisation de la base de données des props. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Se déclenche juste après l'initialisation de la base de données des unités (entités d'apparition d'ennemis et de PNJ). |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Se déclenche juste après l'initialisation de la base de données des rôles de PNJ. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Se déclenche juste après l'initialisation de la base de données des parties personnalisées. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Se déclenche juste après la fin de l'initialisation de toutes les bases de données. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Appelé après le chargement du menu titre. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Appelé au démarrage de la partie. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Appelé au démarrage de la partie. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Appelé au démarrage d'une session de jeu. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Appelé au démarrage d'une session de jeu. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Appelé après le chargement des fichiers de localisation. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Appelé lors de l'allocation d'un étage. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Appelé lors de l'allocation d'un étage. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Appelé une fois la composition aléatoire des ennemis d'une étape décidée. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Appelé à l'apparition d'un joueur. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Appelé à l'apparition de l'avatar d'un joueur. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Se déclenche lorsqu'une unité (joueur, ennemi, invocation ou compagnon) apparaît sur le serveur. |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Se déclenche juste avant qu'une unité soit retirée du serveur. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Se déclenche lorsqu'un joueur esquive une attaque. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Permet de remplacer le prefab de debuff juste avant qu'un debuff soit appliqué à une unité. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Se déclenche lorsque le serveur prend la permission d'écriture sur l'inventaire et commence à retirer tous les effets d'objets. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Se déclenche juste après que le serveur a rendu la permission d'écriture et réappliqué les effets d'objets. |
