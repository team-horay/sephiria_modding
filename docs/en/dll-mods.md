# DLL Mods

DLL modules let you run code written in C# inside the game. Subscribe to `HorayModAPI` to hook into players, items, UI, and more.
This document covers everything from creating a project to getting your first DLL running in the game. For the full list of events and registration functions, see the [API Reference](api/README.md).


## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) 8 or later
- A C# editor


## Creating a Project

Refer to the [`examples/dll-sample`](../../examples/dll-sample) example. This document uses it as its reference.

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
    <!-- Game install folder. Change it to match your setup, or pass it with dotnet build -p:GameDir=... -->
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
    <!-- Add Mirror if you work with network objects such as PlayerAvatar, UnityEngine.UI if you build UI, and 0Harmony if you patch, the same way -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Target `netstandard2.1`** and set the language version to C# 9.
2. **Reference the game DLLs, but don't copy them.** Point the references at the files in `<game install folder>/Sephiria_Data/Managed/`, and set them all to `Private=false`. If game DLLs get copied into your mod folder, the same assembly is loaded twice, which causes obscure errors, and it also needlessly adds files to the review.
3. `Assembly-CSharp.dll` contains most of the code that runs the game.


## Code Structure


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Called once per process. Subscribe to events here, and only once.
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

        // Called only on the host (server). Change game state here.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Called on every participant. The value the server added arrives through sync.
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

Your class inherits the following from the base class.

| Member | Description |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Filled with the values from `metadata.json`. |
| `Context` | `PackageRoot` (absolute path of the mod folder), `FileId` (Steam Workshop item ID; for local packages, a separate value that never collides with a Steam ID), `Definition` (the validated manifest), `ResolvePath(relativePath)`. Use it to find files inside your mod folder. The install path differs for every user, so never hardcode it. |
| `GetDataPackages()` | The list of `data` module packages that name this mod as their `consumer`. Call it after activation is complete (inside an `OnLoad*Database` handler or after `OnAllDatabasesReady`). |

`Context` is valid from the moment `OnModLoaded` is called. In the constructor it is still null.


## Lifecycle

DLL mods are activated **when you start a game and enter the world**. No code runs yet on the title screen. The startup order is as follows.

1. The world scene loads and `GameDataLoader` starts.
2. The DLL modules in the load plan are loaded in order, each entry class is instantiated, and `OnModLoaded` is called.
3. As the databases initialize, events such as `OnLoadItemDatabase` and `OnLoadUnitDatabase` fire one after another.
4. When everything is done, `OnAllDatabasesReady` is called.

## Subscribing

The `HorayModAPI` class provides predefined events that let you hook into many situations.

- Subscribe to events **only once**, in `OnModLoaded`. Database events fire again on every load, so do the things that "must happen every time" (registering items, etc.) inside the handlers.
- Manage subscribing and unsubscribing carefully. Unsubscribe (-=) the events you subscribed to in `OnModUnloaded`.

The full list of events is in the [API Reference](api/README.md). The table below lists the recommended event for each thing you might want to do.

| What you want to do | Event |
|---|---|
| Add items or artifacts | `OnLoadItemDatabase` |
| Add weapons, miracles, passives, costumes, or props | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Add enemy or NPC units | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Add travelers (village NPCs) | `OnLoadSocialIDDatabase` |
| Add a stat display | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Register localized text from code | `OnLocalizationReady` (`AddText`) |
| Add a button to the title menu | `OnTitleMenuReady` |
| Game or session start | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| When a floor is generated | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Player or unit spawns | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Hook into combat checks | `OnPlayerEvade`, `OnGetDebuff` |
| Inventory manipulation permissions | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Avoiding Conflicts

Entities your mod adds are named in the `<modId>:<id>` format, e.g. `yourname.artifact-pack:flame-ring`. This rule prevents conflicts with other mods and is how mod content is told apart in run history and statistics.


## Server-side, Client-side

- Sephiria is built around multiplayer, so many events come in separate server-side and client-side versions.
- The host fires both; other participants fire only the Clientside one.
- As a rule, do things that change game state (spawning, drops, damage) only in Serverside, and do things that only change what's displayed in Clientside.


## Harmony Patches

When you need to change something events can't reach, you can use Harmony.
Since patch 1.1.0, **Harmony 2.4.3 is built into** the game, so just add a reference to it.

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

- **Don't bundle `0Harmony.dll` with your mod.** A bundled copy is ignored, and even if you include a different version, your mod won't run on that version.
- Using your `modId` as-is for the Harmony ID keeps it from colliding with others.
- Patches break on game updates far more easily than the official API. Do what you can with events, and keep patches as a last resort.
- External loaders such as BepInEx and MelonLoader are not supported. A mod that works like an external loader (one that finds and runs plugin DLLs from outside the Workshop) gets a warning and is enabled only after the user accepts it. See [Mods That Run Code from Outside the Workshop](#mods-that-run-code-from-outside-the-workshop) for details.


## Helper DLLs

If your mod uses other libraries (e.g. third-party packages other than Newtonsoft.Json), put their DLLs in your mod folder and declare them in `libraries` in `metadata.json`. DLLs that aren't declared are not used for reference resolution.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Don't include DLLs that already ship with the game (Unity, Mirror, Newtonsoft.Json, 0Harmony, etc.). The game's copy loads first, so the copy your mod includes is either ignored or conflicts.
- Native (C/C++) DLLs can't be used. Only managed assemblies are loaded.
- If an assembly with the same name has already been loaded by another mod, the game doesn't pick one of the two for a DLL in `modules`; it **holds off running** it. For a DLL in `libraries`, the game uses the one that's already loaded and ignores your copy, which can break things if the versions differ. It's safer to split shared libraries into a separate mod and link it through `dependencies`.

### Using Another Mod's Library

If several mods use the same code, put that code in one parent mod and have the child mods link to it through `dependencies`.

1. The parent mod declares the shared code in `modules` or `libraries` and adds `Library` to its `tags`.
2. The child mod lists the parent's `modId` in `dependencies` and builds against the parent's DLL with `Private=false`. **Don't copy the parent's DLL into the child mod.**
3. The game loads the parent mod first, so the child's references resolve to the parent's assembly that's already loaded.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- The child mod still needs its own `dll` module and `entryClass`. A parent mod finding and loading the child's DLLs itself isn't supported. Instead, have the child call the parent's registration function from its own `OnModLoaded`.
- If the parent changes its public types or members in an update, child mods can break. Avoid removing or changing existing public APIs in a shared library.

### Don't Create DLLs at Runtime!

All code a mod runs must be inside a reviewed Workshop item. If the DLL files in a mod change while the mod is running, loading that mod is restricted. Include every DLL in the Workshop item as is, without any compression or streaming. In other words, the following can violate the review system. Put every DLL you need in the mod when you upload it.

- The mod downloads a DLL from outside while running and loads the assembly
- The mod creates and runs its own DLL while running (including rewriting another DLL with Mono.Cecil or similar and then running it)
- The mod extracts a DLL packed inside it with its own compression scheme and uses it

### Mods That Run Code from Outside the Workshop

A mod that finds and runs DLLs the user puts **outside its mod folder**, such as in the game install folder or `AppData` (for example, a compatibility mod for another loader's plugins), isn't rejected, but the review system adds a **warning** to it.

- When the user enables the mod, a warning appears, and the mod is enabled only if they accept it. The user is asked again when the mod is updated.
- External DLLs run this way are not reviewed, and problems they cause are not supported.
- A mod that downloads such DLLs itself while running falls under the prohibited cases above.

To use another mod's code, don't load DLLs directly from that mod's folder. Link to it through `dependencies` as described in [Using Another Mod's Library](#using-another-mods-library). DLLs the game didn't load itself skip the load order, name conflict, and dependency checks, so this isn't supported.

If you need to rewrite a DLL (for example, converting IL to a different Harmony API), do it before uploading and include the converted DLL in your mod.


## Building and Testing

```bash
dotnet build -c Release
```

- Once the build succeeds and the DLL appears in `WorkshopLocal/<mod>/bin/`, launch the game, check that your mod shows up under "My Mods" in the Steam Workshop panel on the title screen, then start a game.
- If you keep the `.pdb` alongside the build output, stack traces in the log include line numbers. Leave it out when you publish.


## Game Updates and Compatibility

When the game updates, `Assembly-CSharp.dll` changes. Mods that use only official events usually keep working, but mods that touch internal classes directly or patch with Harmony may break.

- After an update, rebuild against the new `Managed/` folder and test.
- If you note the **supported game version** in your Workshop page's description, users can find the cause quickly. The game version may also be recorded automatically in the Workshop item's metadata when you publish.
- When you change your code, it must be submitted to the review system again. See [Publishing to the Workshop](publishing.md#code-review).


## Multiplayer Considerations

- Most code mods will have `multiplayer.installation` set to `everyone`.
- If your mod changes game state only in `Serverside` events, and the values it changes are game state that's already synced (e.g. `customStats`) so participants need no code or registration, you can declare it `host_only`. Participants then don't download the mod. A mod that registers new items, uses `RegisterNetworkPrefab`, or changes anything in `Clientside` events can't be `host_only`.
- Objects spawned over the network (units, projectiles, etc.) must be registered with `RegisterNetworkPrefab` to be created on participants' side as well. If you pass a `stableKey`, the same ID is kept even when your mod version changes.
- The host and participants must have even the same mod **load order**. Avoid code whose results depend on that order.


## Network Synchronization (SyncVar, RPC, Command)

Sephiria's multiplayer is built on the open-source networking library 'Mirror'. In Mirror, you can use networking features easily by declaring the following attributes.

- `[SyncVar]`: An attribute you put on a class member variable. It synchronizes the variable's value between the server and clients.
- `[ClientRpc]`, `[TargetRpc]`: Attributes you put on a function. They ask clients to run the function from the server.
- `[Command]`: An attribute you put on a function. It asks the server to run the function from a client.

You cannot use the attributes above normally in a mod environment. They only work when Mirror's Weaver inserts the matching transmission code at compile time, and a DLL built outside the game doesn't go through the Weaver, so even if the build succeeds, the attributes above do nothing. This kind of bug looks fine on the host's screen and only breaks on the participant's screen, so it is hard to find by testing alone. (Even if you forced the code through the Weaver, there could still be bugs such as function hash collisions.)


### `SyncVar<T>`, `ModRpc`

To solve this, Sephiria provides networking code for mods that replaces the attributes above: `SyncVar<T>` and `ModRpc`.

| What you want to do | What to use |
|---|---|
| Keep a value in sync (including participants who join late) | `SyncVar<T>` |
| Server → call on all clients | `ModRpc.Send` |
| Server → call on a specific client | `ModRpc.SendTo` |
| Client → call on the server | `ModRpc.SendCommand` |
| Change a value the game already synchronizes | `Network<FieldName>` property |

- This feature is available from game version 1.1.2.
- A mod that uses this feature should declare `multiplayer.installation` as `everyone`.


### Preparing a Component

`SyncVar<T>` and `ModRpc` can only be used in a component that inherits `NetworkBehaviour`. This component must be attached to the same object on both the host and the participant, **in the same order**. If you attach it with `AddComponent` to an object that has already spawned, it is not included in synchronization, so it must already be attached before you register the object as a network prefab.

```csharp
private static GameObject prefab;

// Subscribe to HorayModAPI.OnAllDatabasesReady in OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Keep it under an inactive parent. The prefab's Awake does not run, and an Instantiate'd copy starts active.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Spawn on the server. If you pass the second argument, that player becomes the owner.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: Synchronizing Values

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // There is no Weaver, so register it directly in the constructor. The field order must always be the same.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Change the value **on the server**, like `stack.Value = 3`. A value changed by a participant is not sent to the server.
- A participant who joins late also receives the current value when they enter.
- `Callback` is called on the host when you assign a value, and on a participant when it receives a value. It is also called if the value received on entry differs from the default. An exception thrown in a callback is written to the log and synchronization continues.
- You can use basic types such as `int`, `float`, `string`, and `Vector3`. Serialization code is not generated automatically for a struct your mod defines, so split it into separate fields.
- To refer to another object, send its `netId` as a `SyncVar<uint>` instead of using `SyncVar<GameObject>`. That object may not have been created yet on the receiving side.
- A single component can register up to 64 SyncVars, counting those it inherits.

### `ModRpc`: Calling Functions

Register a function once when the mod loads. The name you use when calling it is `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Server → every client that can see this object
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Server → one client. If you pass null for the connection, it is sent to the object's owner.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Client → server
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Read in the same order you wrote. If the order or types of writes and reads differ, values get scrambled or an exception is thrown.
- By default, `RegisterCommand` can be called **only by the object's owner**. To receive it on an object that has no owner, pass `requiresAuthority: false`. In that case any participant can call it. Beware of cheating and the like.
- If you send in a direction other than the one you registered, or send a name you haven't registered, only an error is logged and nothing is sent.
- If the function doesn't exist on the receiving side or the component type is different, only a warning is logged and the call is ignored. An exception thrown in a handler is also logged and the game keeps running.
- The default channel is reliable transport. For frequently sent effects, you can pass the `channelId: Channels.Unreliable` argument.

### What You Must Follow

- A client can send arbitrary values through a Command. Validate slot numbers, quantities, targets, and so on in the server handler.
- A participant who joins late does not receive RPCs sent before they entered. Send state that must persist with `SyncVar<T>`, and use RPCs only for passing events such as sounds and effects.
- **An RPC can arrive before the SyncVar.** If you read `stack.Value` in an RPC handler, it may still be the previous value. If you need the new value, send it along as an RPC argument or handle it in `Callback`.

### Changing Values in Sephiria's Base Game Code

If you assign directly to a `[SyncVar]` field of a game class, it is not sent to participants. Use the property with `Network` prepended. The capitalization is the field name exactly as it is.

```csharp
unit.maxHp = 500;         // Only the host's value changes.
unit.NetworkmaxHp = 500;  // It is also sent to participants.
```

Before an object spawns (on a prefab, or right after `Instantiate`), a direct assignment is also sent along when it spawns.

### Testing

The problems above don't show up with the host alone. Before publishing, check both the host's and the participant's screens on two PCs, and also test the case where someone joins late after you change a value. If you enable a local package (`WorkshopLocal`), others cannot join the room, so do multiplayer testing by subscribing to an item you uploaded to the Workshop as Unlisted. An item you upload for the first time is automatically Unlisted.


## Troubleshooting

| Symptom | What to check |
|---|---|
| Status summary says the entry class couldn't be found | The namespace or class name in `entryClass` doesn't match your code. Nested classes are written `Outer+Inner`. |
| Status summary shows an entry class type error | The entry class doesn't inherit from `WorkshopModBase` (or `HorayModBase`), isn't public, is abstract, or has no public parameterless constructor. |
| Status summary says the mod was held due to an assembly name conflict | Another mod (including AddOns) uses the same `AssemblyName`. Rename yours. |
| Status summary shows a library load failure | A DLL listed in `libraries` is missing or isn't a managed assembly. |
| `FileNotFoundException` in the log | A referenced DLL isn't in your mod folder or isn't declared in `libraries`. Or a game DLL was copied with `Private=true` and the two conflict. |
| Exception right after entering the world | An exception in a database event handler. Look for your mod's namespace in the log's stack trace. |
| Effects stack up after returning to the title and re-entering | You're subscribing again inside a handler. Subscribe only once, in `OnModLoaded`. |


## Sample

[`examples/dll-sample`](../../examples/dll-sample) is a minimal mod that raises Physical Damage by 5 when the player spawns. Its `package/` folder includes a prebuilt DLL, so copying that folder into `WorkshopLocal/` makes it work right away without building. To turn it into your own mod, you only need to change `AssemblyName`, `modId`, `entryClass`, and the output path. Start a game and open the stats window to see it working as Physical Damage +5.
