# Dräktmoddar

Nu lägger vi till en dräkt med bara PNG- och JSON-filer, utan kod. Den här typen av mod kan lägga till följande två saker:

- **Nya dräkter**: Skapa en helt ny dräkt. Du kan ange utseende, stats, startföremål med mera.
- **Utseenden för befintliga dräkter**: Lägg till ett utseende (skin) till en befintlig dräkt som Rosa kanin.

Den här sidan bygger på exemplet [`examples/costume-sample`](../../examples/costume-sample). Ladda ner det för att få en känsla för moddens struktur.

## Moddens struktur

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← dräktmapp
│   └── WhiteCat/              ← en mapp = en dräkt
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← PNG-filer för övriga rörelser
└── Localization/              ← text för dräktens namn och beskrivning
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

När du bygger din egen mod utifrån exemplet ändrar du `modId` och `author` i `metadata.json`. (Se [`metadata.json`](metadata.md) för detaljer.)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## Sprite-animationer

- En dräkt behöver ganska många animationer om du tar med alla.
- Om det här är din första dräkt kan du prova att göra en enkel med bara `idle, move, airborne, fall`.

| Rörelse | Beskrivning | Obligatorisk |
|---|---|---|
| `idle` | Stillastående | Obligatorisk |
| `move` | Rör sig | Obligatorisk |
| `airborne` | I luften | Valfri |
| `fall` | Faller ihop | Valfri |
| `attack` | Attack | Valfri |
| `greatsword_lower` | Snurrande sving | Valfri |
| `greatsword_sweep` | Snurr | Valfri |
| `hitfeedback` | Blir träffad | Valfri |

### Animationer bakifrån

- För att dräkten ska se mer naturlig ut kan du rita sprites bakifrån. Det är valfritt.
- Om du väljer att lägga till sprites bakifrån måste du ta med rörelserna `idle_back, move_back`.
- Animationer bakifrån definieras så här.

| Rörelse | Beskrivning |
|---|---|
| `idle_back` | Stillastående (bakifrån) |
| `move_back` | Rör sig (bakifrån) |
| `attack_back` | Attack (bakifrån) |
| `greatsword_upper` | Snurrande sving (bakifrån) |

## Förbereda grafiken

- Gör ett PNG-spritesheet för varje rörelse, med rörelsens bildrutor utlagda horisontellt.
- Bildens höjd är lika med bildrutans höjd, och bredden är bildrutans bredd × antalet bildrutor. Lämna inga mellanrum mellan bildrutorna.
- Vi rekommenderar att alla enskilda bilder har samma storlek.
- I exemplet med den vita katten, där varje sprite är 20×16, är bildstorlekarna följande.

| Fil | Storlek | Bildrutor |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Exempelbild*


### Skriva den nya dräktens information

Definiera din egen dräkt genom att skriva en `costume.json`-fil.

Exempel på `Costumes/WhiteCat/costume.json`:

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| Fält | Obligatoriskt | Beskrivning |
|---|---|---|
| `schemaVersion` | Ja | Formatidentifierare. Ange heltalet `1`. |
| `id` | Ja | Använd små engelska bokstäver, siffror, understreck och bindestreck, 1–31 tecken. För att undvika krockar registreras det i spelet som `<modId>:<id>`. |
| `name` | Ja | **Lokaliseringsnyckeln** för dräktens namn. Se [Förbereda lokaliseringsfiler](#förbereda-lokaliseringsfiler) nedan. |
| `flavorText` | Nej | **Lokaliseringsnyckeln** för dräktens beskrivning. Se [Förbereda lokaliseringsfiler](#förbereda-lokaliseringsfiler) nedan. |
| `icon` | Ja | Sökväg till dräktens förhandsvisningsikon (PNG). |
| `face` | Nej | Sökväg till ansikts-PNG:n för flerspelar-HUD:en. |
| `pixelsPerUnit` | Nej | Ändra det här för att använda mer detaljerad grafik. Högre värden renderar grafiken med finare detaljer. Standard-PPU är 16. |
| `frameSize` | Nej | Varje sprites `[bredd, höjd]`. Standard: `[16, 16]`. |
| `pivot` | Nej | Varje sprites pivotpunkt. Nedre vänstra hörnet är `[0, 0]`. Standard: `[bredd/2, 0]`. |
| `animations` | Ja | Grafik för varje rörelse. Se [animations](#animations). |
| `stats` | Nej | Lista över stats som ska sättas. Se [Statlistan](stats.md). |
| `startingItems` | Nej | Array med ID:n för startföremål. Se [Föremålslistan](item-table.md). |
| `body` | Nej | Ställer in hitboxen. Se [body](#body). |
| `skins` | Nej | Ytterligare utseenden för samma dräkt. Se [skins](#skins). |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- Definiera animationerna du förberedde ovan med fyra egenskaper: `{ "file", "fps", "loop", "footstepFrames" }`.
- Du kan också sätta `frameSize` och `pivot` separat för varje rörelse.
- Om du har ritat bara en rörelse i en annan storlek, som på sista raden, sätter du `frameSize` och `pivot` på den rörelsen.

#### Egenskaper

| Fält | Obligatoriskt | Beskrivning |
|---|---|---|
| `file` | Ja | Filnamnet som ska spelas upp. |
| `fps` | Nej | Animationens uppspelningshastighet. (Standard: 12) |
| `loop` | Nej | Om animationen ska loopa. (Standard: true; false för attackanimationer) |
| `footstepFrames` | Nej | Nollbaserade bildrutenummer. Ett fotstegsljud spelas upp när de bildrutorna visas. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Definierar karaktärens kollisionsområden för rörelse och träffar.
- De inbyggda dräkterna använder de här värdena som standard.
- Om du inte har en särskild anledning rekommenderar vi att du utelämnar det här.

#### Egenskaper

| Fält | Obligatoriskt | Beskrivning |
|---|---|---|
| `hitboxRadius` | Nej | Hitboxcirkelns radie i pixlar, 0,5–64. Standard `4`. |
| `hitboxOffset` | Nej | Hitboxcirkelns mittpunkt, `[x, y]`, i pixlar relativt pivotpunkten; positivt x är höger och positivt y är uppåt, ±64. Standard `[0, 4]`. |
| `movementRadius` | Nej | Radien för rörelsekollisionscirkeln som kolliderar med väggar och hinder, i enheter (16 pixlar = 1), 0,05–2. Standard `0.25`. |
| `bodyScale` | Nej | Skala för axelpositionen där vapnet hålls, 0,1–4. Standard `1`. Höj den när vapnet sjunker in i kroppen på en stor dräkt. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Lägger till extra utseenden för samma dräkt.
- Animationerna följer samma format som ovan.
- Om du utelämnar `frameSize` eller `pivot` används dräktens egna värden.




## Förbereda lokaliseringsfiler

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- I `name` och `flavorText` skriver du en **lokaliseringsnyckel** i stället för text, och lägger texten i samma mods `Localization/`-mapp, en fil per språk.
- Använd engelska bokstäver, siffror, understreck och bindestreck i nycklarna, utan mellanslag. Att ta med moddens namn hjälper till att undvika krockar med andra moddar.
- Om en nyckel **inte finns i någon språkfil** läses dräkten inte in. Du måste tillhandahålla minst ett språk.
- Vi rekommenderar att du även tar med engelska (`en-US`) så att fler kan ha glädje av din dräkt.
- Glöm inte att också deklarera en `localization`-modul i `modules` i `metadata.json`.



## Kontrollera

1. Lägg din modmapp i `WorkshopLocal/` och starta spelet.
2. Starta ett spel, öppna dräktvalet i byn (**Byt dräkt**) och kontrollera att din nya dräkt har lagts till.
3. Om du ändrar dräktens innehåll måste du starta om spelet.



## Extra: Lägga till bara ett skin till en befintlig dräkt

Du kan skapa ett utseende för en befintlig dräkt genom att göra en `skin.json` i stället för `costume.json`.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume` är måldräktens id. Använd ett inbyggt dräkt-id (som `PinkRabbit`) eller, för en annan mods dräkt, dess körtids-id inklusive moddens namn. (För exemplet med den vita katten: `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` och `flavorText` tas som de är från den befintliga dräkten, så du kan inte sätta dem.
- Att skriva över standardutseendet för en inbyggd dräkt stöds inte.
