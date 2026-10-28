# Mod di costumi

Aggiungiamo un costume usando soltanto file PNG e JSON, senza codice. Questo tipo di mod può aggiungere due cose:

- **Nuovi costumi**: crea un costume completamente nuovo. Puoi impostarne aspetto, statistiche, oggetti iniziali e altro ancora.
- **Aspetti per costumi esistenti**: aggiungi un aspetto (skin) a un costume esistente come il Coniglietto rosa.

Questa pagina si basa sull'esempio [`examples/costume-sample`](../../examples/costume-sample). Scaricalo per farti un'idea della struttura della mod.

## Struttura della mod

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← cartella dei costumi
│   └── WhiteCat/              ← una cartella = un costume
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← PNG delle altre animazioni
└── Localization/              ← testi di nome e descrizione del costume
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Quando crei la tua mod partendo dall'esempio, cambia `modId` e `author` in `metadata.json`. (Consulta [`metadata.json`](metadata.md) per i dettagli.)

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


## Animazioni degli sprite

- Un costume richiede parecchie animazioni se le includi tutte.
- Se è il tuo primo costume, prova a farne uno semplice con solo `idle, move, airborne, fall`.

| Animazione | Descrizione | Obbligatoria |
|---|---|---|
| `idle` | Inattivo | Obbligatoria |
| `move` | Movimento | Obbligatoria |
| `airborne` | In aria | Facoltativa |
| `fall` | Caduta a terra | Facoltativa |
| `attack` | Attacco | Facoltativa |
| `greatsword_lower` | Fendente rotante | Facoltativa |
| `greatsword_sweep` | Rotazione | Facoltativa |
| `hitfeedback` | Colpito | Facoltativa |

### Animazioni di spalle

- Per un costume dall'aspetto più naturale, puoi disegnare anche gli sprite di spalle. È facoltativo.
- Se decidi di aggiungere gli sprite di spalle, devi includere le animazioni `idle_back, move_back`.
- Le animazioni di spalle sono definite come segue.

| Animazione | Descrizione |
|---|---|
| `idle_back` | Inattivo (di spalle) |
| `move_back` | Movimento (di spalle) |
| `attack_back` | Attacco (di spalle) |
| `greatsword_upper` | Fendente rotante (di spalle) |

## Preparare la grafica

- Per ogni animazione, crea uno sprite sheet PNG con i fotogrammi di quell'animazione disposti in orizzontale.
- L'altezza dell'immagine è pari all'altezza del fotogramma, e la larghezza è larghezza del fotogramma × numero di fotogrammi. Non lasciare spazi tra i fotogrammi.
- Consigliamo di fare tutte le singole immagini della stessa dimensione.
- Nell'esempio del Gatto bianco, dove ogni sprite è 20×16, le dimensioni delle immagini sono le seguenti.

| File | Dimensione | Fotogrammi |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Immagine di esempio*


### Scrivere le informazioni del nuovo costume

Definisci il tuo costume scrivendo un file `costume.json`.

Esempio di `Costumes/WhiteCat/costume.json`:

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

| Campo | Obbligatorio | Descrizione |
|---|---|---|
| `schemaVersion` | Sì | Identificatore del formato. Inserisci il numero intero `1`. |
| `id` | Sì | Usa lettere minuscole dell'alfabeto inglese, cifre, trattini bassi e trattini, da 1 a 31 caratteri. Per evitare collisioni, nel gioco viene registrato come `<modId>:<id>`. |
| `name` | Sì | La **chiave di localizzazione** del nome del costume. Vedi [Preparare i file di localizzazione](#preparare-i-file-di-localizzazione) qui sotto. |
| `flavorText` | No | La **chiave di localizzazione** della descrizione del costume. Vedi [Preparare i file di localizzazione](#preparare-i-file-di-localizzazione) qui sotto. |
| `icon` | Sì | Percorso del PNG dell'icona di anteprima del costume. |
| `face` | No | Percorso del PNG del volto per l'HUD multigiocatore. |
| `pixelsPerUnit` | No | Cambialo per usare grafica più dettagliata. Valori più alti rendono la grafica con maggiore dettaglio. Il PPU predefinito è 16. |
| `frameSize` | No | La `[larghezza, altezza]` di ogni sprite. Predefinito: `[16, 16]`. |
| `pivot` | No | Il punto di pivot di ogni sprite. L'angolo in basso a sinistra è `[0, 0]`. Predefinito: `[larghezza/2, 0]`. |
| `animations` | Sì | Grafica per ogni animazione. Vedi [animations](#animations). |
| `stats` | No | Elenco delle statistiche da impostare. Vedi l'[Elenco delle statistiche](stats.md). |
| `startingItems` | No | Array di ID degli oggetti iniziali. Vedi l'[Elenco degli oggetti](item-table.md). |
| `body` | No | Imposta la hitbox. Vedi [body](#body). |
| `skins` | No | Aspetti aggiuntivi per lo stesso costume. Vedi [skins](#skins). |

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

- Definisci le animazioni che hai preparato sopra con quattro proprietà: `{ "file", "fps", "loop", "footstepFrames" }`.
- Puoi anche impostare `frameSize` e `pivot` separatamente per ogni animazione.
- Se hai disegnato una sola animazione con una dimensione diversa, come nell'ultima riga, imposta `frameSize` e `pivot` su quell'animazione.

#### Proprietà

| Campo | Obbligatorio | Descrizione |
|---|---|---|
| `file` | Sì | Il nome del file da riprodurre. |
| `fps` | No | Velocità di riproduzione dell'animazione. (Predefinito: 12) |
| `loop` | No | Se l'animazione va in loop. (Predefinito: true; false per le animazioni di attacco) |
| `footstepFrames` | No | Numeri dei fotogrammi a partire da zero. Quando vengono mostrati quei fotogrammi viene riprodotto un suono di passo. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Definisce le aree di collisione per il movimento e per i colpi del personaggio.
- I costumi integrati usano questi valori come predefiniti.
- A meno che tu non abbia un motivo specifico, consigliamo di ometterlo.

#### Proprietà

| Campo | Obbligatorio | Descrizione |
|---|---|---|
| `hitboxRadius` | No | Raggio del cerchio della hitbox, in pixel, da 0.5 a 64. Predefinito `4`. |
| `hitboxOffset` | No | Centro del cerchio della hitbox, `[x, y]`, in pixel rispetto al pivot; x positivo verso destra e y positivo verso l'alto, ±64. Predefinito `[0, 4]`. |
| `movementRadius` | No | Raggio del cerchio di collisione per il movimento, che collide con muri e ostacoli, in unità (16 pixel = 1), da 0.05 a 2. Predefinito `0.25`. |
| `bodyScale` | No | Scala per la posizione della spalla in cui viene impugnata l'arma, da 0.1 a 4. Predefinito `1`. Aumentalo quando l'arma affonda nel corpo di un costume grande. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Aggiunge aspetti extra allo stesso costume.
- Le animazioni seguono lo stesso formato visto sopra.
- Se ometti `frameSize` o `pivot`, vengono usati i valori del costume stesso.




## Preparare i file di localizzazione

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

- In `name` e `flavorText` scrivi una **chiave di localizzazione** invece del testo, e metti il testo nella cartella `Localization/` della stessa mod, un file per lingua.
- Per le chiavi usa lettere dell'alfabeto inglese, cifre, trattini bassi e trattini, senza spazi. Includere il nome della tua mod aiuta a evitare collisioni con altre mod.
- Se una chiave **non è presente in nessun file di lingua**, il costume non viene caricato. Devi fornire almeno una lingua.
- Consigliamo di includere anche l'inglese (`en-US`), così che più persone possano godersi il tuo costume.
- Non dimenticare di dichiarare anche un modulo `localization` in `modules` in `metadata.json`.



## Verifica

1. Metti la cartella della mod in `WorkshopLocal/` e avvia il gioco.
2. Avvia una partita, apri la selezione dei costumi nel villaggio (**Cambia costume**) e controlla che il tuo nuovo costume sia stato aggiunto.
3. Se modifichi il contenuto del costume, devi riavviare il gioco.



## Extra: aggiungere solo una skin a un costume esistente

Puoi creare un aspetto per un costume esistente scrivendo `skin.json` al posto di `costume.json`.

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

- `costume` è l'id del costume di destinazione. Usa l'id di un costume integrato (come `PinkRabbit`) oppure, per il costume di un'altra mod, il suo id a runtime che include il nome della mod. (Per l'esempio del Gatto bianco, `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` e `flavorText` vengono presi così come sono dal costume esistente, quindi non puoi impostarli.
- Sovrascrivere l'aspetto predefinito di un costume integrato non è supportato.
