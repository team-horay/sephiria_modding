# metadata.json

Filen `metadata.json` i roten av din modmapp är kärnfilen som beskriver både **vad** din mod är och **hur** den körs.


## Övergripande struktur

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

Den översta nivån definierar tre saker.

| Nyckel | Roll | Obligatorisk |
|---|---|---|
| `info` | Titel, beskrivning och förhandsvisning som visas på Steam Workshop och i panelen i spelet | Obligatorisk |
| `tags` | Workshop-taggar | Valfri |
| `mod` | Hur modden körs | Obligatorisk |

## info

### title

- Moddens namn. Om det är tomt används `modId` som namn i stället.
- Du kan använda en språkkarta för att definiera moddens namn för varje språk.
	- Utöver ditt eget språk rekommenderar vi att du lägger till engelska.
- Språkkoder som stöds: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- Moddens beskrivning.
- Precis som med `title` kan du definiera beskrivningen per språk.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- En förhandsvisningsbild krävs för att ladda upp till Workshop. (PNG eller JPG, under 1 MB)
- Ange den relativa sökvägen till förhandsvisningsbilden.
- Om den utelämnas letar spelet efter `thumbnail.png`, `preview.png`, `thumbnail.jpg` och `preview.jpg` i rotmappen, i den ordningen.


## mod

### schemaVersion

- Den aktuella versionen är 1.
- Observera att det här numret kan höjas i framtiden om det sker större förändringar i hur moddar körs.

### modId

- En unik sträng som identifierar din mod. Använd små engelska bokstäver, siffror, punkter, bindestreck och understreck. Högst 64 tecken.
- **Välj ett unikt namn.** Om ditt `modId` krockar med en annan mods körs den inte korrekt. Att använda ditt skaparnamn som prefix är ett bra sätt att undvika krockar.
(t.ex. `horay.artifact-pack`)
- **Ändra inte ditt `modId` när modden väl är publicerad.** Andra kan bygga underordnade moddar ovanpå din med `dependencies`, och de identifierar den överordnade modden via dess `modId`.


### version

- En sträng som definierar din mods version.
- Det är den som jämförs när en annan mod kräver en exakt version via `dependencies.version`.
- **Höj versionen varje gång du uppdaterar din mod.** När man går med i en flerspelarlobby kontrollerar spelet att värden och deltagarna har samma modversioner. Om du ändrar innehållet men behåller samma version kan spelare med olika modinnehåll hamna i samma lobby, vilket kan göra att innehållssynkroniseringen misslyckas eller att spelet inte kan fortsätta.
- Vi rekommenderar samma format 1.0.0 som spelet använder.

### author

Låt alla veta vem du är!

### multiplayer / installation

Definierar om spelarna i ett flerspelarspel behöver dela den här modden. Tillåtna värden:

| Värde | Beskrivning |
|---|---|
| `local_only` | Påverkar bara din egen skärm och ditt UI (lokaliseringsmoddar och enkla UI-moddar hör hit) |
| `everyone` | Alla i rummet måste ha samma mod aktiverad (alla moddar som lägger till eller ändrar innehåll hör hit) |
| `host_only` | Bara värden behöver ha den aktiverad; fungerar utan ändringar på övriga deltagares sida (flerspelarmoddar som bara påverkar beräkningar på serversidan hör hit) |

Om du deklarerar `installation` som `local_only` eller `host_only` men din mod rör innehåll som deltagarna också behöver (registrering av nya föremål eller enheter, nätverksobjekt, kod som också körs på deltagarnas klienter) kan spelare i samma lobby hamna i olika spel, eller så kan innehållssynkroniseringen misslyckas och blockera spelets normala förlopp. Kontrollera noga vad din mod faktiskt gör.


### modules

- Moduler definierar vad din mod gör, vilket gör det här till **det viktigaste avsnittet**.
- De nuvarande modulerna är `localization, dll, costume, data`.
- Som i exemplet nedan kan du blanda flera moduler i ett paket.
- Om så mycket som en modul är okänd markeras hela paketet som "stöds inte" och modden hoppas över vid inläsningen.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: localization-modul

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- Modulen `localization` är till för moddar som innehåller språkfiler.
- Sätt `path` till mappen som innehåller språkfilerna.
- Lägg **JSON-filer namngivna efter språkkoder**, som `ko-KR.json` och `en-US.json`, i `path`-mappen. Se [Lokaliseringsmoddar](localization-mods.md) för de språkkoder och det filformat spelet använder.

#### type: dll-modul

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- Modulen `dll` låter dig koppla in dig i spelet med C#-kod.
- Sätt `path` till DLL-filens sökväg.
- Sätt `entryClass` till det fullständiga namnet på startpunktsklassen i DLL-filen, inklusive namnrymd. Spelet söker inte igenom DLL-filen efter en startpunkt, så du **måste ange den**. (För nästlade klasser använder du `Ns.Outer+Inner`.)
- Använd inte samma `entryClass` i två olika moduler.
- Fungerar bara i Mono-byggen.
- Se [DLL-moddar](dll-mods.md) för detaljer.

#### type: costume-modul

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Låter dig skapa egna dräkter utan att skriva någon C#-kod.
- Sätt `path` till mappen som innehåller dina dräktfiler.
- I flerspelarläge måste alla ha den installerad.
- Se [Dräktmoddar](costume-mods.md) för detaljer.


#### type: data-modul

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- En allmän datamodul för moddar. Den används inte i vanliga fall; den är främst till för beroenden mellan moddar.
- `consumer` anger vilken mod som ska använda den här datamodulen, men du utelämnar det vanligtvis. Om det utelämnas kan alla moddar som listas i `dependencies` läsa dessa data.
- Om du har flera överordnade moddar och datan bara är avsedd för en av dem anger du den moddens `modId` i `consumer`. Det värdet måste också finnas i `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- En lista över hjälpassemblies som din `dll`-modul refererar till.
- Spelet löser referenser med **endast de filer som deklareras här**. Det räcker inte att bara lägga en DLL i mappen för att den ska hittas.
- Posterna kan vara antingen en sträng eller ett objekt `{ "path": "..." }`.
- Varje post måste sluta på `.dll`, och den körs inte korrekt om den överlappar med en DLL i `modules`.
- Bibliotek behandlas inte som startpunkter. Även om ett bibliotek innehåller en `WorkshopModBase`-klass anropas den inte.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Om en överordnad mod är en mod för "innehållsskapande" kan en underordnad mod lägga till nytt innehåll som bygger på den.
- I så fall kan du deklarera ett beroende till en annan mod med `dependencies`.
- När en överordnad mod anges kan din mod inte läsas in före den överordnade modden.
- Om en överordnad mod saknas, är inaktiverad eller inte kan läsas in avbryts inläsningen av alla dess underordnade moddar.
- Ett beroende kan ha följande tre fält.

| Fält | Obligatoriskt | Regel |
|---|---|---|
| `modId` | Ja | `modId` för modden du bygger på |
| `version` | Nej | **Använd bara när du behöver en exakt matchande versionssträng.** Om versionen skiljer sig hoppas inläsningen över, så det är oftast säkrare att utelämna det. |
| `workshopId` | Nej | Låter användare hitta och prenumerera på den mod som krävs. |


## tags

- Kategorier som används vid uppladdning till Workshop.
- En mod kan ha flera taggar. (För en allt-i-ett-mod som lägger till föremål och dräkter använder du `"tags": ["Item", "Costume"]`.)

| Kategori | Innehåll |
|---|---|
| `Item` | Föremål (artefakter, stentavlor, drycker med mera), kombinationer |
| `Costume` | Dräkter |
| `Weapon` | Vapen |
| `NPC` | Resenärer, handlare med mera |
| `Enemy` | Nya fiender, bossar med mera |
| `Gameplay` | Ändringar i spelmekaniken |
| `QoL` | Förbättringar av UI, bekvämlighet och tillgänglighet |
| `Library` | Körtidsmiljöer, ramverk, datapaket med mera som andra moddar är beroende av |
| `Localization` | Översättningar |

## Regler för sökvägar
 
- Sökvägarna i `modules[].path`, `libraries` och `info.preview` följer alla samma regler.
- Endast sökvägar relativt modmappen är tillåtna. (Absoluta sökvägar, enhetsbokstäver och `..` avvisas.)
- Använd `/` som avgränsare.
- Sökvägar som går via symboliska länkar eller junctions avvisas.
- **Skiftläget måste exakt matcha det faktiska namnet.** På Windows öppnas mappen även om du skriver `localization`, men spelet avvisar det som något annat än `Localization`. (Den här regeln säkerställer samma beteende på Steam Deck (Linux).)
