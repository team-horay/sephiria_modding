# Pubblicare sul Workshop

Questa pagina spiega come caricare la tua mod sullo Steam Workshop, come funziona il sistema di revisione e come le mod vengono sincronizzate tra i giocatori in multigiocatore.


## Prima di caricare

- Per caricare una mod sul Workshop è obbligatoria un'immagine di anteprima (PNG o JPG, sotto 1 MB). Impostala con `info.preview`, oppure metti un `thumbnail.png` nella cartella della mod. Consulta il [Riferimento di metadata.json](metadata.md#preview) per i dettagli.
- `info.title` può avere al massimo 128 caratteri e `info.description` al massimo 7999. Se li hai scritti in più lingue, sulla pagina del Workshop viene usata la versione `en-US`.
- Inserisci le categorie del Workshop in `tags`. Consulta il [Riferimento di metadata.json](metadata.md) per le categorie disponibili.
- Ogni file della cartella è soggetto a revisione, quindi assicurati che la cartella della mod non contenga copie delle DLL del gioco, file `.pdb` o output intermedi della compilazione.

## Passaggi per la pubblicazione

1. Avvia Sephiria.
2. Apri il pannello **Steam Workshop** nella schermata del titolo e trova la mod che hai creato.
3. Seleziona la tua mod.
4. Le mod che includono codice (DLL) passano per la revisione del codice.
5. Le mod senza codice possono essere caricate subito sul Workshop.
6. Vai alla pagina del Workshop appena caricata, fai un test finale (per esempio iscrivendoti), poi cambia la visibilità in "Pubblico".

## Revisione del codice

- Il sistema di revisione controlla ogni file della cartella della mod. Le DLL vengono decompilate e verificate per comunicazioni di rete, accesso ai file, offuscamento e comportamenti malevoli; i file di dati come immagini, suoni e JSON vengono verificati per la validità del formato.
- Se non vengono trovati problemi, la mod viene approvata automaticamente; se qualcosa sembra sospetto, un operatore lo controlla manualmente. Le mod chiaramente malevole vengono rifiutate automaticamente.
- La revisione combina analisi automatica e controlli da parte di un operatore, e siamo consapevoli che nessun sistema di revisione può intercettare ogni rischio. Puntiamo comunque a ridurre la diffusione di mod malevole grazie a questo processo preventivo di revisione e approvazione. Grazie per la collaborazione.
- Se una mod contiene codice lo si stabilisce dal **contenuto** dei file, non dal nome. Anche senza un modulo `dll`, una mod è soggetta a revisione se ha `libraries` o contiene una DLL o un EXE con estensione modificata.
- Le mod che passano per la revisione del codice devono pesare al massimo 20 MB una volta compressa la cartella.

## Certificato di approvazione (solo mod con codice)

- Le mod che contengono codice ricevono un certificato di approvazione.
- Certifica che i file della mod sono identici alla versione approvata.
- Le mod DLL devono passare per questa certificazione prima che i giocatori che le scaricano possano eseguirne il codice.
- Se anche un solo file cambia dopo la certificazione, il certificato diventa non valido e la mod deve essere revisionata di nuovo.

## Visibilità e pubblicazione

- Al primo caricamento, la visibilità è impostata su `Non in elenco`.
- Prima di renderla pubblica, iscriviti tu stesso, verifica che la copia installata funzioni correttamente, poi cambia la visibilità in "Pubblico".
- Dalla pagina dell'elemento sullo Steam Workshop puoi modificare la descrizione della pagina, gli screenshot, le descrizioni in altre lingue e così via.

## Aggiornare la mod

Puoi aggiornare la tua mod nello stesso modo in cui l'hai pubblicata.

- Le mod DLL hanno bisogno di un nuovo certificato di approvazione.
- Non cambiare il `modId`. La mod verrebbe trattata come una mod diversa, con il rischio di rompere le dipendenze e altro ancora.
- **Aumenta sempre `version` in `metadata.json` a ogni aggiornamento.** Le versioni delle mod vengono confrontate quando si entra in una lobby multigiocatore, quindi se il contenuto è cambiato ma la versione no, giocatori con contenuti diversi possono ritrovarsi nella stessa partita e avere problemi.

## Ticket di caricamento

Dopo la richiesta di revisione e il caricamento, i file qui sotto vengono aggiunti alla cartella della mod. Contengono le informazioni di approvazione della revisione, quindi **non eliminarli.** Se gestisci la cartella della mod con git, la cosa più sicura è committare anche questi tre file. Se cambi computer o formatti, ti serviranno per continuare ad aggiornare lo stesso elemento del Workshop.

| File | Contenuto |
|---|---|
| `uploadticket.json` | Il ticket di revisione e l'ID dell'elemento del Workshop. Senza di esso, il gioco tratta la cartella come una nuova mod mai pubblicata. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | Il certificato di approvazione della revisione: l'elenco dei file approvati e una firma. Modificarli invalida l'approvazione. |

Consulta le [FAQ](faq.md) per sapere come recuperare questi file se li perdi.

## Revoca dell'approvazione

Se dopo la pubblicazione viene rilevata una violazione delle regole, un operatore può revocare l'approvazione. Una mod revocata non viene caricata anche se è installata, e nel pannello compare come "Approvazione della revisione revocata".


## Regole del multigiocatore

### Creazione di una lobby multigiocatore

Quando l'host crea una lobby multigiocatore, **l'elenco delle mod attive dell'host** viene registrato nella lobby.

- Mod con `installation` impostato su `host_only`: i partecipanti non devono installarle; vengono solo mostrate nell'elenco.
- Mod con `multiplayer.installation` impostato su `everyone` (o non dichiarato): anche i partecipanti devono installarle.
- Mod di costumi e mod di dati: anche i partecipanti devono installarle.

### Partecipare

Se la configurazione attuale delle mod di un giocatore differisce da quella della lobby, deve seguire questi passaggi: scaricare → attivare → riavviare il gioco e riprovare a entrare.

### Mod di prova installate in WorkshopLocal

Il multigiocatore non è disponibile in una partita in cui sono caricate mod locali da `WorkshopLocal/`. Per provare il multigiocatore, carica la mod sul Workshop, svuota la cartella locale e usa la copia a cui sei iscritto. Puoi iscriverti tramite il link anche mentre l'elemento è `Non in elenco`.
