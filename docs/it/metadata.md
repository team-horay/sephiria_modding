# metadata.json

Il file `metadata.json` nella radice della cartella della mod è il file principale che descrive sia **cos'è** la tua mod sia **come** viene eseguita.


## Struttura generale

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

Il livello superiore definisce tre cose.

| Chiave | Ruolo | Obbligatorio |
|---|---|---|
| `info` | Titolo, descrizione e anteprima mostrati sullo Steam Workshop e nel pannello del gioco | Obbligatorio |
| `tags` | Tag del Workshop | Facoltativo |
| `mod` | Come viene eseguita la mod | Obbligatorio |

## info

### title

- Il nome della mod. Se vuoto, al suo posto viene usato `modId`.
- Puoi usare una mappa di lingue per definire il nome della mod in ciascuna lingua.
	- Oltre alla tua lingua, ti consigliamo di aggiungere l'inglese.
- Codici lingua supportati: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- La descrizione della mod.
- Come per `title`, puoi definire la descrizione per ogni lingua.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Per caricare sul Workshop è obbligatoria un'immagine di anteprima. (PNG o JPG, sotto 1 MB)
- Inserisci il percorso relativo dell'immagine di anteprima.
- Se omesso, il gioco cerca nella cartella radice, in quest'ordine, `thumbnail.png`, `preview.png`, `thumbnail.jpg` e `preview.jpg`.


## mod

### schemaVersion

- La versione attuale è 1.
- Tieni presente che questo numero potrebbe aumentare in futuro in caso di cambiamenti importanti al modo in cui le mod vengono eseguite.

### modId

- Una stringa univoca che identifica la tua mod. Usa lettere minuscole dell'alfabeto inglese, cifre, punti, trattini e trattini bassi. Massimo 64 caratteri.
- **Scegli un nome univoco.** Se il tuo `modId` coincide con quello di un'altra mod, non funzionerà correttamente. Anteporre il tuo nome di autore aiuta molto a evitare collisioni.
(es. `horay.artifact-pack`)
- **Non cambiare il `modId` dopo la pubblicazione.** Altri possono creare mod figlie basate sulla tua tramite `dependencies`, e identificano la mod padre proprio con il suo `modId`.


### version

- Una stringa che definisce la versione della tua mod.
- È il valore che viene confrontato quando un'altra mod richiede una versione esatta tramite `dependencies.version`.
- **Aumenta la versione a ogni aggiornamento della mod.** Quando si entra in una lobby multigiocatore, il gioco verifica che host e partecipanti abbiano le stesse versioni delle mod. Se cambi il contenuto ma mantieni la stessa versione, giocatori con contenuti diversi possono ritrovarsi nella stessa lobby, con il rischio che la sincronizzazione dei contenuti fallisca o che la partita si blocchi.
- Consigliamo lo stesso formato 1.0.0 usato dal gioco.

### author

Fai sapere a tutti chi sei!

### multiplayer / installation

Definisce se i giocatori di una partita multigiocatore devono condividere questa mod. Valori ammessi:

| Valore | Descrizione |
|---|---|
| `local_only` | Influisce solo sul tuo schermo e sulla tua interfaccia (rientrano qui le mod di localizzazione e le semplici mod di interfaccia) |
| `everyone` | Tutti nella stanza devono avere la stessa mod attiva (rientra qui qualsiasi mod che aggiunge o modifica contenuti) |
| `host_only` | Serve solo all'host; funziona senza modifiche sul lato degli altri partecipanti (rientrano qui le mod multigiocatore che influiscono solo sui calcoli lato server) |

Se dichiari `installation` come `local_only` o `host_only` ma la tua mod tocca contenuti che servono anche ai partecipanti (registrazione di nuovi oggetti o unità, oggetti di rete, codice che gira anche sui client dei partecipanti), i giocatori della stessa lobby potrebbero ritrovarsi a giocare partite diverse, oppure la sincronizzazione dei contenuti potrebbe fallire e bloccare il normale avanzamento della partita. Verifica con attenzione cosa fa davvero la tua mod.


### modules

- I moduli definiscono cosa fa la tua mod, perciò questa è **la sezione più importante**.
- I moduli attuali sono `localization, dll, costume, data`.
- Come nell'esempio qui sotto, puoi combinare più moduli in un unico pacchetto.
- Se anche un solo modulo è sconosciuto, l'intero pacchetto viene contrassegnato come "non supportato" e la mod viene saltata durante il caricamento.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: modulo localization

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- Il modulo `localization` è per le mod che includono file di lingua.
- Imposta `path` sulla cartella che contiene i file di lingua.
- Metti nella cartella `path` **file JSON chiamati con i codici lingua**, come `ko-KR.json` e `en-US.json`. Consulta [Mod di localizzazione](localization-mods.md) per i codici lingua e il formato dei file usati dal gioco.

#### type: modulo dll

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- Il modulo `dll` ti permette di agganciarti al gioco con codice C#.
- Imposta `path` sul percorso del file DLL.
- Imposta `entryClass` sul nome completo della classe del punto di ingresso nella DLL, namespace incluso. Il gioco non cerca un punto di ingresso nella DLL, quindi **devi specificarlo**. (Per le classi annidate usa `Ns.Outer+Inner`.)
- Non usare lo stesso `entryClass` in due moduli diversi.
- Funziona solo sulle build Mono.
- Consulta [Mod DLL](dll-mods.md) per i dettagli.

#### type: modulo costume

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Ti permette di creare costumi personalizzati senza scrivere codice C#.
- Imposta `path` sulla cartella che contiene i file del costume.
- In multigiocatore, tutti devono averla installata.
- Consulta [Mod di costumi](costume-mods.md) per i dettagli.


#### type: modulo data

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Un modulo dati generico per le mod. Non serve nei casi comuni; si usa principalmente per le dipendenze tra mod.
- `consumer` specifica quale mod userà questo modulo dati, ma di solito si omette. Se omesso, ogni mod elencata in `dependencies` può leggere questi dati.
- Se hai più mod padre e i dati sono destinati a una sola di esse, inserisci il `modId` di quella mod in `consumer`. Quel valore deve comparire anche in `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- Un elenco di assembly di supporto referenziati dal tuo modulo `dll`.
- Il gioco risolve i riferimenti usando **solo i file dichiarati qui**. Mettere semplicemente una DLL nella cartella non basta perché venga trovata.
- Le voci possono essere una stringa oppure un oggetto `{ "path": "..." }`.
- Ogni voce deve terminare con `.dll`, e la mod non funzionerà correttamente se la voce coincide con una DLL presente in `modules`.
- Le librerie non vengono trattate come punti di ingresso. Anche se una contiene una classe `WorkshopModBase`, questa non viene chiamata.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Se una mod padre è una mod di "creazione di contenuti", una mod figlia potrebbe aggiungere nuovi contenuti basati su di essa.
- In questo caso puoi dichiarare una dipendenza da un'altra mod tramite `dependencies`.
- Quando è specificata una mod padre, la tua mod non può essere caricata prima di quella mod padre.
- Se una mod padre manca, è disattivata o non riesce a caricarsi, il caricamento di tutte le sue mod figlie viene annullato.
- Una dipendenza può avere i tre campi seguenti.

| Campo | Obbligatorio | Regola |
|---|---|---|
| `modId` | Sì | Il `modId` della mod su cui ti basi |
| `version` | No | **Usalo solo se ti serve una stringa di versione esattamente corrispondente.** Se la versione è diversa il caricamento viene saltato, quindi di solito è più sicuro ometterlo. |
| `workshopId` | No | Permette agli utenti di trovare la mod richiesta e iscriversi. |


## tags

- Le categorie usate al momento del caricamento sul Workshop.
- Una mod può avere più tag. (Per una mod tutto-in-uno che aggiunge oggetti e costumi, usa `"tags": ["Item", "Costume"]`.)

| Categoria | Contenuto |
|---|---|
| `Item` | Oggetti (artefatti, tavolette, pozioni, ecc.), combo |
| `Costume` | Costumi |
| `Weapon` | Armi |
| `NPC` | Viaggiatori, mercanti, ecc. |
| `Enemy` | Nuovi nemici, boss, ecc. |
| `Gameplay` | Modifiche al gameplay |
| `QoL` | Miglioramenti a interfaccia, comodità e accessibilità |
| `Library` | Runtime, framework, pacchetti di dati, ecc. da cui dipendono altre mod |
| `Localization` | Traduzioni |

## Regole per i percorsi
 
- I percorsi in `modules[].path`, `libraries` e `info.preview` seguono tutti le stesse regole.
- Sono ammessi solo percorsi relativi alla cartella della mod. (Percorsi assoluti, lettere di unità e `..` vengono rifiutati.)
- Usa `/` come separatore.
- I percorsi che passano per collegamenti simbolici o junction vengono rifiutati.
- **Maiuscole e minuscole devono corrispondere esattamente al nome reale.** Su Windows, scrivere `localization` apre comunque la cartella, ma il gioco lo rifiuta perché diverso da `Localization`. (Questa regola garantisce lo stesso comportamento su Steam Deck (Linux).)
