# DLL 模组

DLL 模块让你可以在游戏中运行用 C# 编写的代码。通过订阅 `HorayModAPI`，可以挂接到玩家、物品、UI 等各个方面。
本文档涵盖从创建项目到让第一个 DLL 在游戏中运行起来的全部过程。完整的事件和注册函数列表请参阅 [API 参考](api/README.md)。


## 环境要求

- [.NET SDK](https://dotnet.microsoft.com/download) 8 或更高版本
- 一个 C# 编辑器


## 创建项目

请参考 [`examples/dll-sample`](../../examples/dll-sample) 示例。本文档以它为基准进行说明。

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
    <!-- 游戏安装文件夹。请按你的环境修改，或通过 dotnet build -p:GameDir=... 传入 -->
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
    <!-- 若要使用 PlayerAvatar 等网络对象请添加 Mirror，制作 UI 请添加 UnityEngine.UI，打补丁请添加 0Harmony，方式相同 -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **目标框架设为 `netstandard2.1`**，语言版本设为 C# 9。
2. **引用游戏 DLL，但不要复制它们。** 把引用指向 `<游戏安装文件夹>/Sephiria_Data/Managed/` 中的文件，并全部设为 `Private=false`。如果游戏 DLL 被复制到模组文件夹中，同一个程序集会被加载两次，引发难以排查的错误，还会给审核增加不必要的文件。
3. `Assembly-CSharp.dll` 包含了驱动游戏运行的大部分代码。


## 代码结构


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // 每个进程只调用一次。在这里订阅事件，并且只订阅一次。
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

        // 仅在主机（服务器）上调用。在这里修改游戏状态。
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // 在每个参与者上调用。服务器添加的值会通过同步传到这里。
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

你的类会从基类继承以下成员。

| 成员 | 说明 |
|---|---|
| `metadata` | `modName`、`modVersion`、`modAuthor`。由 `metadata.json` 中的值填充。 |
| `Context` | `PackageRoot`（模组文件夹的绝对路径）、`FileId`（Steam 创意工坊物品 ID；本地包则为一个永远不会与 Steam ID 冲突的独立值）、`Definition`（经过验证的清单）、`ResolvePath(relativePath)`。用它来查找模组文件夹中的文件。每个用户的安装路径都不同，所以绝对不要硬编码。 |
| `GetDataPackages()` | 把本模组指定为 `consumer` 的 `data` 模块包列表。请在激活完成后调用（在 `OnLoad*Database` 处理程序内，或 `OnAllDatabasesReady` 之后）。 |

`Context` 从 `OnModLoaded` 被调用的那一刻起有效。在构造函数中它仍然是 null。


## 生命周期

DLL 模组在**开始游戏并进入世界时**被激活。在标题画面上还不会运行任何代码。启动顺序如下。

1. 世界场景加载，`GameDataLoader` 启动。
2. 按顺序加载加载计划中的 DLL 模块，实例化各个入口类，并调用 `OnModLoaded`。
3. 随着数据库初始化，`OnLoadItemDatabase`、`OnLoadUnitDatabase` 等事件依次触发。
4. 全部完成后，调用 `OnAllDatabasesReady`。

## 订阅事件

`HorayModAPI` 类提供了预定义的事件，让你可以挂接到各种场合。

- **只在 `OnModLoaded` 中订阅一次**事件。数据库事件在每次加载时都会再次触发，所以“每次都必须执行”的操作（注册物品等）请放在处理程序内部。
- 请谨慎管理订阅和取消订阅。在 `OnModUnloaded` 中取消订阅（-=）你订阅过的事件。

完整的事件列表见 [API 参考](api/README.md)。下表列出了各种需求对应的推荐事件。

| 想做的事 | 事件 |
|---|---|
| 添加物品或神器 | `OnLoadItemDatabase` |
| 添加武器、奇迹、被动、服装或道具 | `OnLoadWeaponDatabase`、`OnLoadMiracleDatabase`、`OnLoadPassiveDatabase`、`OnLoadCostumeDatabase`、`OnLoadPropDatabase` |
| 添加敌人或 NPC 单位 | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| 添加旅行者（村庄 NPC） | `OnLoadSocialIDDatabase` |
| 添加属性显示 | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| 从代码注册本地化文本 | `OnLocalizationReady`（`AddText`） |
| 在标题菜单添加按钮 | `OnTitleMenuReady` |
| 游戏或会话开始 | `OnStartGameServerside/Clientside`、`OnStartSessionServerside/Clientside` |
| 楼层生成时 | `OnFloorAllocatedServerside/Clientside`、`OnAssignRoomSpawnPhases` |
| 玩家或单位生成 | `OnPlayerAvatarSpawned*`、`OnUnitSpawnedServerside`、`OnUnitDespawnedServerside` |
| 挂接战斗判定 | `OnPlayerEvade`、`OnGetDebuff` |
| 背包操作权限 | `GridInventoryStartPermission`、`GridInventoryEndPermission` |


## 避免冲突

模组添加的实体以 `<modId>:<id>` 格式命名，例如 `yourname.artifact-pack:flame-ring`。这条规则可以防止与其他模组冲突，也是在探险记录和统计中区分模组内容的方式。


## 服务器端与客户端

- Sephiria 以多人游戏为核心构建，因此许多事件都分为服务器端（Serverside）和客户端（Clientside）两个版本。
- 主机会同时触发两者；其他参与者只触发 Clientside 版本。
- 原则上，修改游戏状态的操作（生成、掉落、伤害）只在 Serverside 中进行，只改变显示效果的操作在 Clientside 中进行。


## Harmony 补丁

当需要修改事件无法触及的内容时，可以使用 Harmony。
从 1.1.0 补丁起，游戏**内置了 Harmony 2.4.3**，只需添加引用即可。

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

- **不要把 `0Harmony.dll` 打包进你的模组。** 打包的副本会被忽略；即使你附带了不同的版本，模组也不会在那个版本上运行。
- 直接用你的 `modId` 作为 Harmony ID 可以避免与其他模组冲突。
- 补丁比官方 API 更容易因游戏更新而失效。能用事件解决的尽量用事件，补丁只作为最后的手段。
- 不支持 BepInEx、MelonLoader 等外部加载器。行为类似外部加载器的模组（即查找并运行创意工坊之外的插件 DLL 的模组）会被加上警告，只有用户同意后才会启用。详情请参阅[运行创意工坊之外代码的模组](#运行创意工坊之外代码的模组)。


## 辅助 DLL

如果你的模组使用了其他库（例如 Newtonsoft.Json 以外的第三方包），请把它们的 DLL 放进模组文件夹，并在 `metadata.json` 的 `libraries` 中声明。未声明的 DLL 不会用于解析引用。

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- 不要包含游戏已自带的 DLL（Unity、Mirror、Newtonsoft.Json、0Harmony 等）。游戏自带的副本会先加载，所以模组附带的副本要么被忽略，要么产生冲突。
- 不能使用原生（C/C++）DLL。只加载托管程序集。
- 如果同名的程序集已经被另一个模组加载，对于 `modules` 中的 DLL，游戏不会在两者中选择其一，而是**暂停运行**它。对于 `libraries` 中的 DLL，游戏会改用已加载的那个并忽略你附带的副本，因此版本不同时可能出现异常。更稳妥的做法是把共享库拆分成单独的模组，并通过 `dependencies` 关联。

### 使用其他模组的库

如果多个模组使用相同的代码，请把这些代码放进一个上级模组，并让下级模组通过 `dependencies` 关联到它。

1. 上级模组在 `modules` 或 `libraries` 中声明共用代码，并在 `tags` 中加入 `Library`。
2. 下级模组在 `dependencies` 中填写上级模组的 `modId`，并以 `Private=false` 引用上级模组的 DLL 进行构建。**不要把上级模组的 DLL 复制到下级模组中。**
3. 游戏会先加载上级模组，因此下级模组的引用会解析为已加载的上级模组的程序集。

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- 下级模组仍需拥有自己的 `dll` 模块和 `entryClass`。不支持由上级模组自行查找并加载下级模组 DLL 的结构。请让下级模组在自己的 `OnModLoaded` 中调用上级模组的注册函数。
- 如果上级模组在更新中更改了公开的类型或成员，下级模组可能会失效。共享库请注意不要删除或更改已有的公开 API。

### 不要在运行时生成 DLL！

模组运行的所有代码都必须包含在已通过审核的创意工坊物品中。如果模组运行期间模组内的 DLL 文件发生变化，该模组的加载会受到限制。请把所有 DLL 原样包含在创意工坊物品中，不要进行任何压缩或流式传输。也就是说，以下做法可能违反审核系统。请在上传时就把所需的所有 DLL 放进模组。

- 模组在运行时从外部下载 DLL 并加载程序集
- 模组在运行时自行生成并运行 DLL（包括用 Mono.Cecil 等工具改写其他 DLL 后再运行）
- 模组将以自有压缩方式打包在内部的 DLL 解压后使用

### 运行创意工坊之外代码的模组

如果模组会查找并运行用户放在**模组文件夹之外**（例如游戏安装文件夹或 `AppData`）的 DLL（例如兼容其他加载器插件的模组），不会被驳回，但审核系统会给它加上**警告**。

- 用户启用该模组时会弹出警告窗口，只有同意后才会启用。模组更新后会再次询问。
- 以这种方式运行的外部 DLL 不会经过审核，由此产生的问题不在支持范围内。
- 模组在运行时自行下载这类 DLL，属于上述禁止的情形。

如果要使用其他模组的代码，请不要直接从该模组的文件夹加载 DLL，而是像[使用其他模组的库](#使用其他模组的库)所述那样通过 `dependencies` 关联。游戏没有亲自加载的 DLL 不会经过加载顺序、名称冲突和依赖检查，因此不受支持。

如果需要改写 DLL（例如针对其他 Harmony API 进行 IL 转换），请在上传之前先完成转换，并把转换后的 DLL 放进模组上传。


## 构建与测试

```bash
dotnet build -c Release
```

- 构建成功且 DLL 出现在 `WorkshopLocal/<模组>/bin/` 中后，启动游戏，在标题画面的 Steam 创意工坊面板中确认你的模组出现在“我的模组”下，然后开始游戏。
- 如果把 `.pdb` 和构建产物放在一起，日志中的堆栈跟踪会包含行号。发布时请去掉它。


## 游戏更新与兼容性

游戏更新时，`Assembly-CSharp.dll` 会发生变化。只使用官方事件的模组通常可以继续工作，但直接操作内部类或使用 Harmony 打补丁的模组可能会失效。

- 更新后，请针对新的 `Managed/` 文件夹重新构建并测试。
- 如果在创意工坊页面的描述中注明**支持的游戏版本**，用户可以更快找到问题原因。发布时，游戏版本也可能会自动记录在创意工坊物品的元数据中。
- 修改代码后，必须重新提交到审核系统。参阅[发布到创意工坊](publishing.md#代码审核)。


## 多人游戏注意事项

- 大多数代码模组的 `multiplayer.installation` 都应设为 `everyone`。
- 如果你的模组只在 `Serverside` 事件中修改游戏状态，并且修改的值是已经会同步的游戏状态（例如 `customStats`），参与者不需要任何代码或注册，那么可以声明为 `host_only`。这样参与者就不需要下载该模组。注册新物品、使用 `RegisterNetworkPrefab`、或在 `Clientside` 事件中修改任何内容的模组都不能是 `host_only`。
- 通过网络生成的对象（单位、投射物等）必须用 `RegisterNetworkPrefab` 注册，才能同时在参与者一侧创建。如果传入 `stableKey`，即使模组版本变化也会保持相同的 ID。
- 主机和参与者连模组的**加载顺序**也必须一致。避免编写结果依赖于该顺序的代码。


## 网络同步（SyncVar, RPC, Command）

Sephiria 的多人联机功能是用开源网络库“Mirror”实现的。在 Mirror 中，只需声明下列特性（Attribute），就可以轻松使用通信功能。

- `[SyncVar]`：加在类成员变量上的特性。在服务器和客户端之间同步变量的值。
- `[ClientRpc]`、`[TargetRpc]`：加在函数上的特性。由服务器请求客户端执行函数。
- `[Command]`：加在函数上的特性。由客户端请求服务器执行函数。

在模组环境中，上述特性无法正常使用。代码编译时，需要由 Mirror 的 Weaver 为每个特性插入对应的传输代码，它们才能工作；但外部生成的 DLL 不会经过 Weaver，所以即使构建成功，上述特性也不会起任何作用。这类 bug 在主机画面上看起来一切正常，只有参与者的画面才会出问题，因此只靠自己一个人测试很难发现。（即使强行让 DLL 经过了 Weaver，也可能出现函数哈希冲突等 bug。）


### `SyncVar<T>`、`ModRpc`

为了解决这个问题，Sephiria 提供了可替代上述特性的模组专用通信代码，即 `SyncVar<T>` 和 `ModRpc`。

| 想做的事 | 使用的东西 |
|---|---|
| 让数值始终保持一致（包括后加入的参与者） | `SyncVar<T>` |
| 服务器 → 所有客户端调用 | `ModRpc.Send` |
| 服务器 → 特定客户端调用 | `ModRpc.SendTo` |
| 客户端 → 服务器调用 | `ModRpc.SendCommand` |
| 修改游戏已经在同步的值 | `Network<FieldName>` 属性 |

- 此功能从游戏版本 1.1.2 开始可用。
- 使用此功能的模组，请将 `multiplayer.installation` 声明为 `everyone`。


### 准备组件

`SyncVar<T>` 和 `ModRpc` 只能在继承自 `NetworkBehaviour` 的组件中使用。该组件必须在主机和参与者双方的同一个对象上，以**相同的顺序**挂载。对已经生成的对象用 `AddComponent` 添加组件，不会被纳入同步范围，所以必须在注册为网络预制体之前就已经挂载好。

```csharp
private static GameObject prefab;

// 在 OnModLoaded 中订阅 HorayModAPI.OnAllDatabasesReady。
private static void RegisterPrefab()
{
    if (prefab) return;

    // 放在未激活的父对象下。预制体的 Awake 不会运行，Instantiate 出来的副本则以激活状态开始。
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// 在服务器上生成。传入第二个参数后，该玩家就成为所有者。
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`：同步数值

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // 没有 Weaver，所以在构造函数中手动注册。字段顺序必须始终相同。
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- 数值要**在服务器上**修改，例如 `stack.Value = 3`。参与者修改的值不会传给服务器。
- 后加入的参与者在进入时也会收到当前值。
- `Callback` 在主机上于赋值时调用，在参与者上于收到值时调用。进入时收到的值与默认值不同，此时也会调用。回调中发生的异常会记录到日志，同步继续进行。
- 可以使用 `int`、`float`、`string`、`Vector3` 等基本数据类型。模组自己定义的 struct 不会自动生成序列化代码，请拆分成多个字段来使用。
- 要指向其他对象时，不要用 `SyncVar<GameObject>`，而是把 `netId` 用 `SyncVar<uint>` 发送。接收方可能还没有生成该对象。
- 一个组件上最多可注册的 SyncVar 数量，连同继承来的在内共 64 个。

### `ModRpc`：调用函数

加载模组时注册一次函数。调用时使用的名称是 `modId:functionName`。

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// 服务器 → 所有能看到该对象的客户端
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// 服务器 → 一个客户端。连接传入 null 时，发送给对象的所有者。
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// 客户端 → 服务器
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- 请按写入的顺序读取。写入和读取的顺序、数据类型不一致，会导致值错位或抛出异常。
- `RegisterCommand` 默认只有**对象的所有者**才能调用。如果要在没有所有者的对象上接收，请传入 `requiresAuthority: false`。这种情况下任何参与者都可以调用，请注意作弊等风险。
- 如果按与注册时不同的方向发送，或发送未注册的名称，只会留下错误日志，不会发送。
- 接收方没有该函数或组件类型不同时，只会留下警告日志并忽略。处理程序中发生的异常也会记录到日志，游戏继续运行。
- 默认通道是可靠传输。如果是频繁发送的演出效果，可以传入 `channelId: Channels.Unreliable` 参数。

### 必须遵守的事项

- 客户端可以通过 Command 发送任意值。请在服务器处理程序中验证槽位编号、数量、目标等。
- 后加入的参与者收不到进入之前的 RPC。需要保持的状态请用 `SyncVar<T>` 发送，RPC 只用于声音、特效这类转瞬即逝的事情。
- **RPC 可能比 SyncVar 先到达。** 在 RPC 处理程序中读取 `stack.Value`，可能得到旧值。如果需要新值，请把它作为 RPC 参数一起发送，或者在 `Callback` 中处理。

### 修改 Sephiria 本体代码中的数值

直接对游戏类的 `[SyncVar]` 字段赋值，不会传给参与者。请使用前面加了 `Network` 的属性。大小写与字段名完全一致。

```csharp
unit.maxHp = 500;         // 只有主机的值会改变。
unit.NetworkmaxHp = 500;  // 参与者也会收到。
```

对象生成之前（预制体或刚 `Instantiate` 之后），直接赋值也会在生成时一并传递。

### 测试

只有主机一个人时，上述问题不会暴露。发布前请用两台电脑，同时检查主机和参与者双方的画面，并且要测试修改数值之后再晚进入的情况。启用本地包（`WorkshopLocal`）后其他人无法进入房间，所以多人测试要订阅以 Unlisted 上传到创意工坊的物品来进行。第一次上传的物品会自动设为 Unlisted。


## 故障排查

| 症状 | 检查项 |
|---|---|
| 状态摘要显示找不到入口类 | `entryClass` 中的命名空间或类名与代码不一致。嵌套类写作 `Outer+Inner`。 |
| 状态摘要显示入口类类型错误 | 入口类没有继承 `WorkshopModBase`（或 `HorayModBase`）、不是 public、是 abstract，或没有 public 的无参构造函数。 |
| 状态摘要显示模组因程序集名称冲突而被暂停 | 另一个模组（包括 AddOns）使用了相同的 `AssemblyName`。请改名。 |
| 状态摘要显示库加载失败 | `libraries` 中列出的某个 DLL 缺失，或不是托管程序集。 |
| 日志中出现 `FileNotFoundException` | 引用的 DLL 不在模组文件夹中，或未在 `libraries` 中声明。或者游戏 DLL 以 `Private=true` 被复制了进来，两者产生冲突。 |
| 进入世界后立即出现异常 | 数据库事件处理程序中发生了异常。在日志的堆栈跟踪中查找你的模组的命名空间。 |
| 返回标题画面再重新进入后效果叠加 | 你在处理程序内部重复订阅了。只在 `OnModLoaded` 中订阅一次。 |


## 示例

[`examples/dll-sample`](../../examples/dll-sample) 是一个最小化的模组，在玩家生成时将物理伤害提高 5。它的 `package/` 文件夹中包含预先构建好的 DLL，所以把该文件夹复制到 `WorkshopLocal/` 中无需构建即可直接运行。要把它改成你自己的模组，只需修改 `AssemblyName`、`modId`、`entryClass` 和输出路径。开始游戏并打开属性窗口，就能看到它以“物理伤害 +5”的形式生效。
