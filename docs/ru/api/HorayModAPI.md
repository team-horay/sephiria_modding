<!-- Автоматически сгенерированный файл. Tools/ModApiDocGen в репозитории игры собирает его из /// комментариев в исходном коде. Не редактируйте его напрямую; исправьте комментарии в исходном коде и сгенерируйте заново. -->
# HorayModAPI

Статическая точка входа, которую игра предоставляет модам. Подписывайтесь на события загрузки баз данных, сессий и спавна, регистрируйте сетевые префабы и особые характеристики. Подписывайтесь на события только один раз, в `OnModLoaded`. Жизненный цикл и правила многопользовательской игры описаны в разделе [DLL-моды](../dll-mods.md).

Сгенерировано 2026-10-26 для версии игры **1.1.0**. [Оглавление справочника](README.md)

| Тип | Описание |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Точка входа для модов. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Помощник для регистрации текста локализации мода. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Точка входа для модов.

Пример:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Константы и поля

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Описания пока нет.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Описания пока нет.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Описания пока нет.*

### События

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Срабатывает сразу после инициализации базы данных чудес. Регистрируйте или изменяйте чудеса через `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Срабатывает сразу после инициализации базы данных оружия. Регистрируйте или изменяйте оружие через `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Срабатывает сразу после инициализации базы данных статусов (эффектов характеристик). Регистрируйте или изменяйте статусы через `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Срабатывает сразу после загрузки категорий особых характеристик и статусов. Регистрируйте отображаемые записи через `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Срабатывает сразу после инициализации базы данных предметов. Регистрируйте или изменяйте предметы через `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Срабатывает сразу после инициализации базы данных ключевых слов. Регистрируйте или изменяйте ключевые слова через `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Срабатывает сразу после инициализации базы данных пассивных навыков. Регистрируйте или изменяйте пассивные навыки через `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Срабатывает сразу после инициализации базы данных костюмов. Регистрируйте или изменяйте костюмы через `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Срабатывает сразу после инициализации базы данных пропсов. Регистрируйте пропсы через `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Срабатывает сразу после инициализации базы данных юнитов (сущностей спавна врагов и NPC).


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Срабатывает сразу после инициализации базы данных ролей NPC.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Срабатывает сразу после инициализации базы данных пользовательских игр.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Срабатывает сразу после завершения инициализации всех баз данных.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Вызывается после загрузки меню титульного экрана.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Вызывается при начале игры. (серверная сторона) Начало означает нажатие на слот сохранения на титульном экране для входа в игру. Данные сессии, такие как игрок и текущая локация, ещё не загружены.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Вызывается при начале игры. (клиентская сторона) Начало означает нажатие на слот сохранения на титульном экране для входа в игру. Данные сессии, такие как игрок и текущая локация, ещё не загружены.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Вызывается при начале игровой сессии. (серверная сторона) Сессия является единицей игры. Когда игрок погибает, эта сессия завершается и начинается новая.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Вызывается при начале игровой сессии. (клиентская сторона) Сессия является единицей игры. Когда игрок погибает, эта сессия завершается и начинается новая.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Вызывается после загрузки файлов локализации.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Вызывается при выделении этажа. (серверная сторона) Каждая локация в Sephiria определена как этаж. Когда игрок перемещается между локациями, по порядку выполняются следующие шаги. 1. Выделение этажа: создаёт и загружает новый этаж. 2. Перемещение: переносит игрока в запрошенную точку полностью загруженного этажа. 3. Удаление этажа: удаляет локацию, в которой игрок находился раньше, если она пуста (= в ней не осталось игроков).
param1: GUID этажа  
param2: имя этажа  
param3: экземпляр FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Вызывается при выделении этажа. (клиентская сторона) Каждая локация в Sephiria определена как этаж. Когда игрок перемещается между локациями, по порядку выполняются следующие шаги. 1. Выделение этажа: создаёт и загружает новый этаж. 2. Перемещение: переносит игрока в запрошенную точку полностью загруженного этажа. 3. Удаление этажа: удаляет локацию, в которой игрок находился раньше, если она пуста (= в ней не осталось игроков).
param1: GUID этажа  
param2: имя этажа  
param3: экземпляр FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Вызывается после того, как определён случайный состав врагов для этапа. (серверная сторона) Срабатывает сразу после того, как генератор этажа выбрал фазы монстров для боевой комнаты (или боевой зоны), до того как их получит спавнер. Замените или отредактируйте ctx.Phases, чтобы изменить, что там появится. Игра не контролирует здесь баланс; правила см. в RoomSpawnAssignContext (детерминированный ГСЧ через ctx.Seed, никогда не трогайте ctx.Sources, сущности должны быть зарегистрированы в UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Вызывается при появлении игрока. (серверная сторона) Внутри этого хука можно регистрировать колбэки, связанные с игроком. Вызывается в конце PlayerAvatar.OnStartServer, после применения базовых характеристик. Костюм, этаж и содержимое инвентаря могут быть заполнены позже в том же кадре или в последующих кадрах. Используйте его для подписки на собственные события экземпляра аватара (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Подписки на аватаре умирают вместе с аватаром, поэтому отписываться от них не нужно.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Вызывается при появлении аватара игрока. (клиентская сторона) Вызывается один раз для каждого аватара в конце его OnStartClient; срабатывает и для удалённых игроков. Проверяйте avatar.isLocalPlayer / avatar.isOwned, чтобы отличить локального игрока. SyncVar уже применены, но этаж и содержимое инвентаря могут прийти позже.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Срабатывает, когда на сервере появляется юнит (игрок, враг, призванное существо или спутник). Только на сервере (хосте).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Срабатывает непосредственно перед удалением юнита с сервера. Только на сервере (хосте).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Срабатывает, когда игрок уклоняется от атаки. (серверная сторона)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Позволяет подменить префаб дебаффа непосредственно перед его применением к юниту. Обработчик получает (текущий префаб, источник) и возвращает префаб-замену; возврат null оставляет его без изменений.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Срабатывает, когда сервер забирает разрешение на запись в инвентарь и начинает снимать все эффекты предметов.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Срабатывает сразу после того, как сервер возвращает разрешение на запись и повторно применяет эффекты предметов.

### Методы

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Регистрирует объект (префаб), который можно создавать по сети. У объекта должен быть компонент NetworkIdentity.

Сервер (хост) и клиенты должны хранить список объектов, которые могут использовать сеть, и этот список должен быть одинаковым у всех.

Пример:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Возвращает: true, если регистрация прошла успешно


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Регистрирует префаб, созданный во время выполнения (например, клон встроенного шаблона), как объект, создаваемый по сети, используя фиксированный assetId, вычисленный из `stableKey`. В качестве ключа используйте имя юнита мода (`<modId>:<id>`).

Возвращает: true, если регистрация прошла успешно


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Регистрирует несколько префабов как объекты, создаваемые по сети, за один раз.

Возвращает: количество успешно зарегистрированных префабов


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Отменяет регистрацию зарегистрированного сетевого префаба.

Возвращает: true, если он был удалён


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Регистрирует категорию особых характеристик в UI характеристик.

Возвращает: true, если зарегистрирована


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Регистрирует StatusEntity как запись в категории особых характеристик в UI характеристик. Если передать `reader`, значение читается через эту функцию, а не из настроек ассета.

Возвращает: true, если зарегистрирована


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Получает настройки для устройства, привязанные к modId. Моды из Мастерской могут вместо этого использовать `Context.Options`.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Открывает панель взаимодействия с предметами, используемую вместе с инвентарём. Возвращает false, если UI занят.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Получает фабрику UI-виджетов в стиле игры. Размещайте их под существующим UI Canvas.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Открывает окно диалога в мире. Возвращает false, если UI занят; следующую страницу показывайте через `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Открывает окно диалога со смещением в мировых координатах. Если null, используется смещение префаба по умолчанию.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Открывает окно диалога с раздельными смещениями диалога и камеры. Фокус камеры применяется только локально.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Помощник для регистрации текста локализации мода.

### Методы

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Регистрирует текст для конкретного языка.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Регистрирует текст сразу для нескольких языков
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
