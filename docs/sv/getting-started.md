# Din första mod

Den här guiden förklarar grundstrukturen i en Sephiria-mod och leder dig genom att bygga din första mod och kontrollera den i spelet.

## Det här behöver du

- En bra textredigerare (till exempel [Visual Studio Code](https://code.visualstudio.com/))

## Hitta spelets installationsmapp

Öppna Sephirias installationsmapp.

- Från ditt Steam-bibliotek
  - `Steam-bibliotek → högerklicka på Sephiria → Hantera → Bläddra bland lokala filer`
- Windows (standardsökväg)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (standardsökväg)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (mappen som innehåller `Sephiria.app`)

## Skapa mappen WorkshopLocal och din egen modmapp

1. Skapa en mapp `WorkshopLocal/` i spelets installationsmapp.
2. Skapa en mapp för din egen mod inuti den.
   - Sephiria behandlar varje mapp som en mod.
   - Moddar som skapas här kan testas i spelet och laddas upp till Steam Workshop.

I den här guiden bygger du `my-first-mod`, en mod som ändrar texten på knappen "Starta spelet" på titelskärmen till "Hello World!". Den färdiga strukturen ser ut så här:

```
<spelets installationsmapp>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← en mod
    │   ├── metadata.json      ← kärnfilen som definierar modden
    │   ├── thumbnail.png      ← förhandsvisningsbild för modden (krävs för uppladdning till Workshop)
    │   └── Localization/      ← mapp som läses av lokaliseringsmodulen
    │       └── en-US.json     ← texten som ska ersättas
    └── another-mod/           ← en annan mod
        └── ...
```

## Filen metadata.json

- Skapa `metadata.json` i din modmapp och klistra in innehållet nedan.
- `metadata.json`: nyckelfilen som definierar din mod.
- `yourname`: byt ut mot ditt eget smeknamn.
- Spara filen som **UTF-8**. I Anteckningar väljer du UTF-8 som kodning under "Spara som".
- Se [Referens för metadata.json](metadata.md) för vad varje fält betyder.

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

## Skapa språkfilen

- Skapa en mapp `Localization/` i din modmapp.
- Skapa en fil `en-US.json` med innehållet nedan inuti `Localization/`.
- Du kan också döpa om filen till din egen språkkod för att testa.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Kontrollera

Nu kan du kontrollera om modden fungerar.

1. Starta Sephiria.
2. Titta på titelskärmen med spelets språk inställt på engelska: knappen "Start Game" visar nu "Hello World!". Moddar i `WorkshopLocal/` är aktiverade som standard, så du behöver inte slå på dem.
3. Tryck på knappen **Steam Workshop** på titelskärmen för att öppna Workshop-panelen. "My First Mod" ska visas under "Mina moddar".

### Min mod tillämpas inte!

- Kontrollera att din JSON-syntax är korrekt.
- `path` i `metadata.json` måste exakt matcha mappens faktiska namn, inklusive stora och små bokstäver.
- Kontrollera att `metadata.json` är kodad som UTF-8.
- Kontrollera att spelets språk matchar språket i filnamnet.

## Nästa steg

1. Lär dig hur moddar definieras i [Referens för metadata.json](metadata.md).
2. [Lokaliseringsmoddar](localization-mods.md) och [Dräktmoddar](costume-mods.md) är relativt enkla att göra, så börja med dem.
3. [DLL-moddar](dll-mods.md) rekommenderas om du har grundläggande programmeringskunskaper.
4. Släpp din mod på Workshop med [Publicera på Workshop](publishing.md).
