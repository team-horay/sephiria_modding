# FAQ


## Come vedo i log?

I log del gioco vengono scritti nel file qui sotto. Viene riscritto a ogni avvio del gioco, e la sessione precedente è conservata in `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Le righe relative alle mod iniziano con un tag tra parentesi quadre, quindi cerca quel tag.

| Tag | Contenuto |
|---|---|
| `[WorkshopModManager]` | Lettura dell'elenco delle iscrizioni, download, scansione delle cartelle locali, riepilogo del piano di caricamento |
| `[WorkshopModRuntime]` | Attivazione delle DLL e risultati dell'unione delle localizzazioni |
| `[WorkshopUploader]` | Caricamento sul Workshop e invio alla revisione |
| `[ModGuardClient]` | Comunicazione con il server di revisione |
| `[WorkshopLobbyMods]` | Confronto degli insiemi di mod nelle stanze multigiocatore |
| `모드 내 언어 파일 발견` | Numero di chiavi lette da ciascun file di localizzazione |


## Cos'è "Mod locale (cartella AddOns)" nel pannello dello Steam Workshop?

Nel pannello potresti vedere una voce "Mod locale (cartella AddOns)". È una funzione di modding legacy, quindi non usarla per le nuove mod.
Le mod legacy non possono essere sincronizzate con gli altri giocatori, quindi consigliamo di rimuoverle.


## Voglio spostare la cartella WorkshopLocal fuori dalla cartella di installazione di Sephiria.

Se vuoi tenere le tue mod su un'altra unità o in una cartella di lavoro, puoi passare il percorso come argomento di avvio. Nella libreria di Steam, vai su Sephiria → Proprietà → Generale → Opzioni di avvio e inserisci:

```
-workshop_local=D:\mods\my-first-mod
```

Separa più percorsi con punti e virgola, e racchiudi tutto tra virgolette se un percorso contiene spazi.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Le cartelle passate come argomento vengono trattate esattamente come sottocartelle di `WorkshopLocal/`.


## Ho perso uploadticket.json.

`mod-guard-manifest.json` e `mod-guard.sig.json` non vengono più usati dalla 1.1.1, quindi non è un problema se li perdi. L'unico file necessario è `uploadticket.json`.

Se hai perso `uploadticket.json` (per esempio dopo una formattazione), il gioco tratta la cartella come una nuova mod, quindi caricarla così com'è crea un nuovo elemento del Workshop invece di aggiornare quello esistente. Per continuare ad aggiornare l'elemento esistente, recuperalo in quest'ordine:

1. Iscriviti alla tua mod sul Workshop.
2. Copia l'intera cartella scaricata da Steam (`<libreria di Steam>/steamapps/workshop/content/2436940/<ID dell'elemento del Workshop>/`) dentro `WorkshopLocal/`. Il nome della cartella è indifferente.
3. Nella cartella copiata, crea `uploadticket.json` con il contenuto qui sotto. Per il numero, usa le cifre dopo `id=` nell'URL della pagina del Workshop.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Avvia il gioco, seleziona la mod nel pannello e premi **Carica nel Workshop**. **L'elemento esistente viene aggiornato** e, se è una mod con codice, viene inviata di nuovo alla revisione con lo stesso ID dell'elemento. (Nota: se hai duplicato una cartella di una mod esistente per crearne una diversa, **devi eliminare** `uploadticket.json`. Se lo lasci, la nuova mod sovrascriverà l'elemento del Workshop della mod originale.)
