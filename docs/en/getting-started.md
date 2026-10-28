# Your First Mod

This guide explains the basic structure of a Sephiria mod and walks you through building your first mod and checking it in-game.

## What You Need

- A capable text editor (such as [Visual Studio Code](https://code.visualstudio.com/))

## Find the Game Install Folder

Open the Sephiria install folder.

- From your Steam library
  - `Steam Library → right-click Sephiria → Manage → Browse local files`
- Windows (default install path)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (default install path)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (the folder containing `Sephiria.app`)

## Create the WorkshopLocal Folder & Your Own Mod Folder

1. Create a `WorkshopLocal/` folder in the game install folder.
2. Inside it, create a folder for your own mod.
   - Sephiria treats each folder as one mod.
   - Mods created here can be tested in-game and uploaded to the Steam Workshop.

In this guide, you'll build `my-first-mod`, a mod that changes the text of the "Start Game" button on the title screen to "Hello World!". The finished structure looks like this:

```
<game install folder>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← one mod
    │   ├── metadata.json      ← the core file that defines the mod
    │   ├── thumbnail.png      ← mod preview thumbnail (required for Workshop upload)
    │   └── Localization/      ← folder read by the localization module
    │       └── en-US.json     ← the text to replace
    └── another-mod/           ← another mod
        └── ...
```

## The metadata.json File

- Create `metadata.json` in your mod folder and paste in the content below.
- `metadata.json`: the key file that defines your mod.
- `yourname`: replace this with your own nickname.
- Save the file as **UTF-8**. In Notepad, choose UTF-8 as the encoding under "Save As".
- See the [metadata.json Reference](metadata.md) for what each field means.

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## Create the Language File

- Create a `Localization/` folder inside your mod folder.
- Inside `Localization/`, create an `en-US.json` file with the content below.
- You can also rename the file to your own language code for testing.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Check It

At this point, you can check whether the mod works.

1. Launch Sephiria.
2. With the game language set to English, look at the title screen: the "Start Game" button now reads "Hello World!". Mods in `WorkshopLocal/` are enabled by default, so you don't need to turn them on.
3. Press the **Steam Workshop** button on the title screen to open the Workshop panel. "My First Mod" should appear in the "My Mods" section.

### My Mod Isn't Applied!

- Check that your JSON syntax is correct.
- The `path` in `metadata.json` must match the actual folder name exactly, including case.
- Check that `metadata.json` is encoded as UTF-8.
- Check that the game language matches the language in the file name.

## Next Steps

1. Learn how mods are defined in the [metadata.json Reference](metadata.md).
2. [Localization Mods](localization-mods.md) and [Costume Mods](costume-mods.md) are relatively easy to make, so start with those.
3. [DLL Mods](dll-mods.md) are recommended if you have basic coding knowledge.
4. Release your mod on the Workshop with [Publishing to the Workshop](publishing.md).
