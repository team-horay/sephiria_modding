<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте его напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# Справочник API

Список публичных типов и членов, доступных DLL-модам. Настройка проекта, точки входа, жизненный цикл и правила многопользовательской игры описаны в разделе [DLL-моды](../dll-mods.md).

Сгенерировано 2026-10-26 для версии игры **1.1.0**. Этот документ генерируется заново при каждом обновлении игры.

| Документ | Типы | Содержание |
|---|---|---|
| [HorayModAPI](HorayModAPI.md) | `HorayModAPI`, `HorayModLocalizationContext` | Статическая точка входа, которую игра предоставляет модам. |
| [Точка входа и контекст](ModBase.md) | `WorkshopModBase`, `WorkshopModContext`, `HorayModBase`, `WorkshopDataPackage`, `WorkshopModDefinition`, `WorkshopModuleDefinition`, `WorkshopDependency`, `WorkshopModuleType`, `WorkshopInstallation` | Базовые классы, от которых наследуется класс точки входа мода, а также типы контекста и манифеста, которые подключает загрузчик. |
| [Настройки модов](ModOptions.md) | `ModOptions`, `ModOptionsPage`, `ModOptionDefinition`, `ModOption<T>`, `ModToggleOption`, `ModSelectionOption`, `ModSliderOption`, `ModOptionText`, `ModOptionKind`, `ModOptionsRegistry` | Настройки мода для каждого устройства и API для добавления переключателей, полей выбора и ползунков на экран настроек игры. |
| [Назначение спавна в комнатах](RoomSpawn.md) | `RoomSpawnAssignContext`, `ERoomSpawnKind` | Контекст, передаваемый событием `HorayModAPI.OnAssignRoomSpawnPhases`. |
| [Особые характеристики](SpecialStats.md) | `SpecialStatContext`, `SpecialStatValue`, `SpecialStatCategoryEntity`, `SpecialStatCategoryVisibility`, `SpecialStatDisplaySettings`, `SpecialStatValueSource`, `SpecialStatLabelColor`, `SpecialStatValueProvider` | Типы отображения характеристик, регистрируемые через `RegisterSpecialStatCategory` и `RegisterSpecialStat` в `HorayModAPI.OnLoadSpecialStatDatabase`. |
| [UI](UI.md) | `UIWidgetFactory`, `UIWidgetTextStyle`, `DialoguePage`, `DialogueChoice`, `DialogueSession`, `DialogueCloseReason`, `ItemInteractionOptions`, `ItemInteractionSession`, `ItemInteractionItem`, `ItemInteractionCloseReason`, `ItemInteractionItemSource` | Типы диалогов, взаимодействия с предметами и виджетов, используемые `HorayModAPI.TryShowDialogue`, `TryShowItemInteraction` и `TryGetUIWidgets`. |

## Обзор событий HorayModAPI

Перечислены в порядке объявления в исходном коде. События баз данных срабатывают заново при каждой загрузке мира.

| Событие | Сигнатура | Описание |
|---|---|---|
| [`OnLoadMiracleDatabase`](HorayModAPI.md#onloadmiracledatabase) | `Action OnLoadMiracleDatabase` | Срабатывает сразу после инициализации базы данных чудес. |
| [`OnLoadWeaponDatabase`](HorayModAPI.md#onloadweapondatabase) | `Action OnLoadWeaponDatabase` | Срабатывает сразу после инициализации базы данных оружия. |
| [`OnLoadStatusDatabase`](HorayModAPI.md#onloadstatusdatabase) | `Action OnLoadStatusDatabase` | Срабатывает сразу после инициализации базы данных статусов (эффектов характеристик). |
| [`OnLoadSpecialStatDatabase`](HorayModAPI.md#onloadspecialstatdatabase) | `Action OnLoadSpecialStatDatabase` | Срабатывает сразу после загрузки категорий особых характеристик и статусов. |
| [`OnLoadItemDatabase`](HorayModAPI.md#onloaditemdatabase) | `Action OnLoadItemDatabase` | Срабатывает сразу после инициализации базы данных предметов. |
| [`OnLoadKeywordDatabase`](HorayModAPI.md#onloadkeyworddatabase) | `Action OnLoadKeywordDatabase` | Срабатывает сразу после инициализации базы данных ключевых слов. |
| [`OnLoadPassiveDatabase`](HorayModAPI.md#onloadpassivedatabase) | `Action OnLoadPassiveDatabase` | Срабатывает сразу после инициализации базы данных пассивных навыков. |
| [`OnLoadCostumeDatabase`](HorayModAPI.md#onloadcostumedatabase) | `Action OnLoadCostumeDatabase` | Срабатывает сразу после инициализации базы данных костюмов. |
| [`OnLoadPropDatabase`](HorayModAPI.md#onloadpropdatabase) | `Action OnLoadPropDatabase` | Срабатывает сразу после инициализации базы данных пропсов. |
| [`OnLoadUnitDatabase`](HorayModAPI.md#onloadunitdatabase) | `Action OnLoadUnitDatabase` | Срабатывает сразу после инициализации базы данных юнитов (сущностей спавна врагов и NPC). |
| [`OnLoadSocialIDDatabase`](HorayModAPI.md#onloadsocialiddatabase) | `Action OnLoadSocialIDDatabase` | Срабатывает сразу после инициализации базы данных ролей NPC. |
| [`OnLoadCustomGameDatabase`](HorayModAPI.md#onloadcustomgamedatabase) | `Action OnLoadCustomGameDatabase` | Срабатывает сразу после инициализации базы данных пользовательских игр. |
| [`OnAllDatabasesReady`](HorayModAPI.md#onalldatabasesready) | `Action OnAllDatabasesReady` | Срабатывает сразу после завершения инициализации всех баз данных. |
| [`OnTitleMenuReady`](HorayModAPI.md#ontitlemenuready) | `Action<UI_TitleLobby> OnTitleMenuReady` | Вызывается после загрузки меню титульного экрана. |
| [`OnStartGameServerside`](HorayModAPI.md#onstartgameserverside) | `Action OnStartGameServerside` | Вызывается при начале игры. |
| [`OnStartGameClientside`](HorayModAPI.md#onstartgameclientside) | `Action OnStartGameClientside` | Вызывается при начале игры. |
| [`OnStartSessionServerside`](HorayModAPI.md#onstartsessionserverside) | `Action<bool> OnStartSessionServerside` | Вызывается при начале игровой сессии. |
| [`OnStartSessionClientside`](HorayModAPI.md#onstartsessionclientside) | `Action<bool> OnStartSessionClientside` | Вызывается при начале игровой сессии. |
| [`OnLocalizationReady`](HorayModAPI.md#onlocalizationready) | `Action<HorayModLocalizationContext> OnLocalizationReady` | Вызывается после загрузки файлов локализации. |
| [`OnFloorAllocatedServerside`](HorayModAPI.md#onfloorallocatedserverside) | `Action<string, string, FloorGenerator> OnFloorAllocatedServerside` | Вызывается при выделении этажа. |
| [`OnFloorAllocatedClientside`](HorayModAPI.md#onfloorallocatedclientside) | `Action<string, string, FloorGenerator> OnFloorAllocatedClientside` | Вызывается при выделении этажа. |
| [`OnAssignRoomSpawnPhases`](HorayModAPI.md#onassignroomspawnphases) | `Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases` | Вызывается после того, как определён случайный состав врагов для этапа. |
| [`OnPlayerAvatarSpawnedServerside`](HorayModAPI.md#onplayeravatarspawnedserverside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside` | Вызывается при появлении игрока. |
| [`OnPlayerAvatarSpawnedClientside`](HorayModAPI.md#onplayeravatarspawnedclientside) | `Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside` | Вызывается при появлении аватара игрока. |
| [`OnUnitSpawnedServerside`](HorayModAPI.md#onunitspawnedserverside) | `Action<UnitAvatar> OnUnitSpawnedServerside` | Срабатывает, когда на сервере появляется юнит (игрок, враг, призванное существо или спутник). |
| [`OnUnitDespawnedServerside`](HorayModAPI.md#onunitdespawnedserverside) | `Action<UnitAvatar> OnUnitDespawnedServerside` | Срабатывает непосредственно перед удалением юнита с сервера. |
| [`OnPlayerEvade`](HorayModAPI.md#onplayerevade) | `Action<PlayerAvatar, DamageInstance> OnPlayerEvade` | Срабатывает, когда игрок уклоняется от атаки. |
| [`OnGetDebuff`](HorayModAPI.md#ongetdebuff) | `Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff` | Позволяет подменить префаб дебаффа непосредственно перед его применением к юниту. |
| [`GridInventoryStartPermission`](HorayModAPI.md#gridinventorystartpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryStartPermission` | Срабатывает, когда сервер забирает разрешение на запись в инвентарь и начинает снимать все эффекты предметов. |
| [`GridInventoryEndPermission`](HorayModAPI.md#gridinventoryendpermission) | `Action<GridInventory, PlayerAvatar> GridInventoryEndPermission` | Срабатывает сразу после того, как сервер возвращает разрешение на запись и повторно применяет эффекты предметов. |
