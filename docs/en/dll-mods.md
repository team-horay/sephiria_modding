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
