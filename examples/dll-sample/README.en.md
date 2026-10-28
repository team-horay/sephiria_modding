# DLL Mod Template: Physical Damage +5

A minimal mod that raises the Physical Damage custom stat by 5 on the server when the player spawns. Copy it and change the names, and it becomes your own mod. The [DLL Mods guide](../../docs/en/dll-mods.md) uses this folder as its reference.

## Try It Right Away (No Build Needed)

The `package/` folder contains a prebuilt DLL. Copy the whole `package/` folder into `<game install folder>/WorkshopLocal/` and launch the game, and it works right away. This DLL may stop matching after a game update; in that case, rebuild it with the steps below.

## Making It Your Own Mod

1. Copy this whole folder.
2. In `PhysicalDamagePlus.csproj`, set `GameDir` to your game install folder, and change `AssemblyName`, `RootNamespace`, and `ModFolderName` to your mod's name.
3. Change the namespace in `Entry.cs` to match `RootNamespace`.
4. In `package/metadata.json`, update `modId` (your nickname in place of `yourname`), `path` (`bin/<AssemblyName>.dll`), and `entryClass` (`<namespace>.Entry`).
5. Replace `package/thumbnail.png` with your mod's preview image (under 1 MB).
6. Build.

```bash
dotnet build -c Release
```

Building puts the DLL in `package/bin/` and copies the contents of `package/` to `<game install folder>/WorkshopLocal/<ModFolderName>/`. Launch the game and check that the mod appears in the Workshop panel on the title screen, then start a game and open the stats window: Physical Damage is +5.

## Layout

```
dll-sample/
├── PhysicalDamagePlus.csproj   ← Project. Just change GameDir and the names
├── Entry.cs                    ← Entry point. Subscribes to the spawn event and adds the stat
└── package/                    ← This folder is the mod itself; the unit you copy to WorkshopLocal
    ├── metadata.json           ← Mod definition
    ├── thumbnail.png           ← Preview image
    └── bin/
        └── PhysicalDamagePlus.dll ← Build output
```
