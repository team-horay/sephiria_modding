# La tua prima mod

Questa guida spiega la struttura di base di una mod di Sephiria e ti accompagna nella creazione della tua prima mod e nella sua verifica all'interno del gioco.

## Cosa ti serve

- Un buon editor di testo (per esempio [Visual Studio Code](https://code.visualstudio.com/))

## Trova la cartella di installazione del gioco

Apri la cartella di installazione di Sephiria.

- Dalla libreria di Steam
  - `Libreria di Steam → clic destro su Sephiria → Gestisci → Sfoglia i file locali`
- Windows (percorso di installazione predefinito)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (percorso di installazione predefinito)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (la cartella che contiene `Sephiria.app`)

## Crea la cartella WorkshopLocal e la cartella della tua mod

1. Crea una cartella `WorkshopLocal/` nella cartella di installazione del gioco.
2. Al suo interno, crea una cartella per la tua mod.
   - Sephiria tratta ogni cartella come una singola mod.
   - Le mod create qui possono essere provate nel gioco e caricate sullo Steam Workshop.

In questa guida creerai `my-first-mod`, una mod che cambia il testo del pulsante "Inizia partita" nella schermata del titolo in "Hello World!". La struttura finale è questa:

```
<cartella di installazione del gioco>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← una mod
    │   ├── metadata.json      ← il file principale che definisce la mod
    │   ├── thumbnail.png      ← anteprima della mod (obbligatoria per il caricamento sul Workshop)
    │   └── Localization/      ← cartella letta dal modulo di localizzazione
    │       └── en-US.json     ← il testo da sostituire
    └── another-mod/           ← un'altra mod
        └── ...
```

## Il file metadata.json

- Crea `metadata.json` nella cartella della tua mod e incollaci il contenuto qui sotto.
- `metadata.json`: il file chiave che definisce la tua mod.
- `yourname`: sostituiscilo con il tuo nickname.
- Salva il file in **UTF-8**. Nel Blocco note, scegli UTF-8 come codifica in "Salva con nome".
- Consulta il [Riferimento di metadata.json](metadata.md) per il significato di ogni campo.

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

## Crea il file di lingua

- Crea una cartella `Localization/` nella cartella della tua mod.
- Dentro `Localization/`, crea un file `en-US.json` con il contenuto qui sotto.
- Per fare delle prove puoi anche rinominare il file con il codice della tua lingua.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Verifica

A questo punto puoi controllare se la mod funziona.

1. Avvia Sephiria.
2. Con la lingua del gioco impostata su inglese, guarda la schermata del titolo: il pulsante "Start Game" ora mostra "Hello World!". Le mod in `WorkshopLocal/` sono attive per impostazione predefinita, quindi non devi attivarle.
3. Premi il pulsante **Steam Workshop** nella schermata del titolo per aprire il pannello del Workshop. "My First Mod" dovrebbe comparire nella sezione "Le mie mod".

### La mia mod non viene applicata!

- Controlla che la sintassi JSON sia corretta.
- Il `path` in `metadata.json` deve corrispondere esattamente al nome reale della cartella, maiuscole e minuscole comprese.
- Controlla che `metadata.json` sia codificato in UTF-8.
- Controlla che la lingua del gioco corrisponda alla lingua indicata nel nome del file.

## Prossimi passi

1. Scopri come si definiscono le mod nel [Riferimento di metadata.json](metadata.md).
2. Le [mod di localizzazione](localization-mods.md) e le [mod di costumi](costume-mods.md) sono relativamente facili da creare, quindi inizia da quelli.
3. Le [mod DLL](dll-mods.md) sono consigliate se hai conoscenze di base di programmazione.
4. Pubblica la tua mod sul Workshop seguendo [Pubblicare sul Workshop](publishing.md).
