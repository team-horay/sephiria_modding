# DLL-моды

DLL-модули позволяют выполнять код на C# внутри игры. Подписывайтесь на события `HorayModAPI`, чтобы подключаться к игрокам, предметам, UI и многому другому.
Этот документ охватывает всё от создания проекта до запуска первой DLL в игре. Полный список событий и функций регистрации см. в [Справочнике API](api/README.md).


## Требования

- [.NET SDK](https://dotnet.microsoft.com/download) 8 или новее
- Редактор для C#


## Создание проекта

Ориентируйтесь на пример [`examples/dll-sample`](../../examples/dll-sample). В этом документе он используется как эталон.

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
    <!-- Папка установки игры. Измените под свою систему или передайте через dotnet build -p:GameDir=... -->
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
    <!-- Таким же образом добавьте Mirror, если работаете с сетевыми объектами вроде PlayerAvatar, UnityEngine.UI, если делаете UI, и 0Harmony, если применяете патчи -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Целевая платформа `netstandard2.1`**, версия языка C# 9.
2. **Ссылайтесь на игровые DLL, но не копируйте их.** Направьте ссылки на файлы в `<папка установки игры>/Sephiria_Data/Managed/` и у всех установите `Private=false`. Если игровые DLL попадут в папку мода, одна и та же сборка загрузится дважды, что вызывает трудноуловимые ошибки, а также без нужды добавляет файлы к проверке.
3. `Assembly-CSharp.dll` содержит большую часть кода, на котором работает игра.


## Структура кода


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Вызывается один раз за процесс. Подписывайтесь на события здесь, и только один раз.
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

        // Вызывается только на хосте (сервере). Изменяйте состояние игры здесь.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Вызывается у каждого участника. Значение, добавленное сервером, приходит через синхронизацию.
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

Ваш класс наследует от базового класса следующее.

| Член | Описание |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Заполняется значениями из `metadata.json`. |
| `Context` | `PackageRoot` (абсолютный путь к папке мода), `FileId` (ID элемента Мастерской Steam; для локальных пакетов отдельное значение, которое никогда не совпадает с ID Steam), `Definition` (проверенный манифест), `ResolvePath(relativePath)`. Используйте его для поиска файлов внутри папки мода. Путь установки у каждого пользователя свой, поэтому никогда не прописывайте его жёстко. |
| `GetDataPackages()` | Список пакетов модулей `data`, которые указывают этот мод в качестве `consumer`. Вызывайте после завершения активации (внутри обработчика `OnLoad*Database` или после `OnAllDatabasesReady`). |

`Context` действителен с момента вызова `OnModLoaded`. В конструкторе он ещё равен null.


## Жизненный цикл

DLL-моды активируются, **когда вы начинаете игру и входите в мир**. На титульном экране код ещё не выполняется. Порядок запуска следующий.

1. Загружается сцена мира, и запускается `GameDataLoader`.
2. DLL-модули из плана загрузки загружаются по порядку, создаётся экземпляр каждого класса точки входа, и вызывается `OnModLoaded`.
3. По мере инициализации баз данных одно за другим срабатывают события, такие как `OnLoadItemDatabase` и `OnLoadUnitDatabase`.
4. Когда всё готово, вызывается `OnAllDatabasesReady`.

## Подписка на события

Класс `HorayModAPI` предоставляет предопределённые события, которые позволяют подключаться к множеству ситуаций.

- Подписывайтесь на события **только один раз**, в `OnModLoaded`. События баз данных срабатывают заново при каждой загрузке, поэтому то, что «должно происходить каждый раз» (регистрация предметов и т. п.), делайте внутри обработчиков.
- Внимательно управляйте подпиской и отпиской. Отписывайтесь (-=) от событий, на которые подписались, в `OnModUnloaded`.

Полный список событий находится в [Справочнике API](api/README.md). В таблице ниже приведено рекомендуемое событие для каждой задачи.

| Что вы хотите сделать | Событие |
|---|---|
| Добавить предметы или артефакты | `OnLoadItemDatabase` |
| Добавить оружие, чудеса, пассивные навыки, костюмы или пропсы | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Добавить вражеских или NPC-юнитов | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Добавить путешественников (NPC деревни) | `OnLoadSocialIDDatabase` |
| Добавить отображение характеристики | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Зарегистрировать локализованный текст из кода | `OnLocalizationReady` (`AddText`) |
| Добавить кнопку в меню титульного экрана | `OnTitleMenuReady` |
| Начало игры или сессии | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| При генерации этажа | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Появление игрока или юнита | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Подключиться к боевым проверкам | `OnPlayerEvade`, `OnGetDebuff` |
| Разрешения на изменение инвентаря | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Как избежать конфликтов

Сущности, добавляемые вашим модом, именуются в формате `<modId>:<id>`, например `yourname.artifact-pack:flame-ring`. Это правило предотвращает конфликты с другими модами, и именно так контент модов различается в истории забегов и статистике.


## Серверная сторона, клиентская сторона

- Sephiria построена вокруг многопользовательской игры, поэтому многие события существуют в отдельных серверной и клиентской версиях.
- Хост запускает обе; остальные участники запускают только версию Clientside.
- Как правило, всё, что меняет состояние игры (спавн, дроп, урон), делайте только в Serverside, а всё, что меняет только отображение, в Clientside.


## Патчи Harmony

Когда нужно изменить то, до чего события не дотягиваются, можно использовать Harmony.
Начиная с патча 1.1.0 **Harmony 2.4.3 встроена** в игру, поэтому достаточно добавить на неё ссылку.

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

- **Не включайте `0Harmony.dll` в состав мода.** Вложенная копия игнорируется, а если вы положите другую версию, ваш мод на ней всё равно не запустится.
- Используйте свой `modId` как есть в качестве ID Harmony, чтобы он не совпадал с другими.
- Патчи ломаются при обновлениях игры гораздо чаще, чем официальный API. Делайте всё возможное через события, а патчи оставьте на крайний случай.
- Внешние загрузчики, такие как BepInEx и MelonLoader, не поддерживаются.


## Вспомогательные DLL

Если ваш мод использует другие библиотеки (например, сторонние пакеты, кроме Newtonsoft.Json), положите их DLL в папку мода и объявите в `libraries` файла `metadata.json`. Необъявленные DLL не используются при разрешении ссылок.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Не включайте DLL, которые уже поставляются с игрой (Unity, Mirror, Newtonsoft.Json, 0Harmony и т. д.). Копия игры загружается первой, поэтому копия из вашего мода либо игнорируется, либо конфликтует.
- Нативные (C/C++) DLL использовать нельзя. Загружаются только управляемые сборки.
- Если сборка с тем же именем уже загружена другим модом, игра не выбирает одну из двух; она **откладывает запуск** мода. Безопаснее вынести общие библиотеки в отдельный мод и связать его через `dependencies`.

### Не создавайте DLL во время работы!

Если DLL-файлы мода меняются во время его работы, загрузка этого мода ограничивается. Включайте все DLL в элемент Мастерской как есть, без какого-либо сжатия или потоковой загрузки. Иными словами, следующие действия могут нарушать требования системы проверки. Кладите все нужные DLL в мод при загрузке.

- Мод во время работы скачивает DLL извне и загружает сборку
- Мод во время работы сам создаёт и запускает DLL
- Мод извлекает упакованную в нём DLL собственным способом сжатия и использует её


## Сборка и тестирование

```bash
dotnet build -c Release
```

- Когда сборка пройдёт успешно и DLL появится в `WorkshopLocal/<мод>/bin/`, запустите игру, убедитесь, что ваш мод отображается в разделе «Мои моды» панели Мастерской Steam на титульном экране, и затем начните игру.
- Если оставить `.pdb` рядом с результатом сборки, трассировки стека в логе будут содержать номера строк. При публикации его не включайте.


## Обновления игры и совместимость

При обновлении игры меняется `Assembly-CSharp.dll`. Моды, использующие только официальные события, обычно продолжают работать, но моды, которые напрямую обращаются к внутренним классам или применяют патчи Harmony, могут сломаться.

- После обновления пересоберите мод с новой папкой `Managed/` и протестируйте.
- Если указать **поддерживаемую версию игры** в описании на странице Мастерской, пользователи смогут быстро найти причину проблемы. Версия игры также может автоматически записываться в метаданные элемента Мастерской при публикации.
- Если вы изменили код, его нужно снова отправить в систему проверки. См. [Публикация в Мастерской](publishing.md#проверка-кода).


## Особенности многопользовательской игры

- У большинства модов с кодом `multiplayer.installation` будет равен `everyone`.
- Если ваш мод меняет состояние игры только в событиях `Serverside`, а изменяемые значения являются уже синхронизируемым состоянием игры (например, `customStats`), так что участникам не нужен ни код, ни регистрация, его можно объявить как `host_only`. Тогда участники не скачивают мод. Мод, который регистрирует новые предметы, использует `RegisterNetworkPrefab` или меняет что-либо в событиях `Clientside`, не может быть `host_only`.
- Объекты, создаваемые по сети (юниты, снаряды и т. д.), должны быть зарегистрированы через `RegisterNetworkPrefab`, чтобы создаваться и на стороне участников. Если передать `stableKey`, тот же ID сохранится даже при изменении версии мода.
- У хоста и участников должен совпадать даже **порядок загрузки** модов. Избегайте кода, результат которого зависит от этого порядка.


## Устранение неполадок

| Симптом | Что проверить |
|---|---|
| В сводке состояния сказано, что класс точки входа не найден | Пространство имён или имя класса в `entryClass` не совпадает с кодом. Вложенные классы записываются как `Outer+Inner`. |
| В сводке состояния ошибка типа класса точки входа | Класс точки входа не наследуется от `WorkshopModBase` (или `HorayModBase`), не является public, является abstract или не имеет публичного конструктора без параметров. |
| В сводке состояния сказано, что мод отложен из-за конфликта имён сборок | Другой мод (включая AddOns) использует тот же `AssemblyName`. Переименуйте свою сборку. |
| В сводке состояния ошибка загрузки библиотеки | DLL из списка `libraries` отсутствует или не является управляемой сборкой. |
| `FileNotFoundException` в логе | DLL, на которую есть ссылка, отсутствует в папке мода или не объявлена в `libraries`. Либо игровая DLL скопирована с `Private=true`, и две копии конфликтуют. |
| Исключение сразу после входа в мир | Исключение в обработчике события базы данных. Найдите пространство имён своего мода в трассировке стека в логе. |
| Эффекты накапливаются после возврата на титульный экран и повторного входа | Вы подписываетесь повторно внутри обработчика. Подписывайтесь только один раз, в `OnModLoaded`. |


## Пример

[`examples/dll-sample`](../../examples/dll-sample) является минимальным модом, который повышает Физический урон на 5 при появлении игрока. В его папке `package/` есть готовая DLL, поэтому достаточно скопировать эту папку в `WorkshopLocal/`, и мод заработает сразу без сборки. Чтобы сделать из него собственный мод, нужно изменить только `AssemblyName`, `modId`, `entryClass` и путь вывода. Начните игру и откройте окно характеристик, чтобы увидеть, что он работает: Физический урон +5.
