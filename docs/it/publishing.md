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
3. Seleziona la tua mod e premi **Carica nel Workshop**.
4. Per le mod senza codice, il caricamento sul Workshop è l'ultimo passaggio.
5. Per le mod che includono codice (DLL), una volta completato il caricamento sul Workshop, la mod viene inviata automaticamente al **sistema di revisione** insieme all'ID dell'elemento del Workshop.
6. Vai alla pagina del Workshop appena caricata, fai un test finale (per esempio iscrivendoti), poi cambia la visibilità in "Pubblico".

Se il caricamento sul Workshop è stato completato ma non è riuscito solo l'invio alla revisione (per esempio a causa di un errore di rete), premi di nuovo **Carica nel Workshop** sulla stessa mod. Se il contenuto non è cambiato, la mod non viene caricata di nuovo sul Workshop; viene solo ripetuto l'invio. L'invio deve essere effettuato con l'account Steam che ha creato l'elemento del Workshop.

## Revisione del codice

- Il sistema di revisione decompila le DLL e le verifica per comunicazioni di rete, accesso ai file, offuscamento e comportamenti malevoli. La revisione combina analisi automatica e controlli da parte di un operatore, e siamo consapevoli che nessun sistema di revisione può intercettare ogni rischio. Puntiamo comunque a ridurre la diffusione di mod malevole grazie a questo processo di revisione. Grazie per la collaborazione.
- Ogni mod che contiene codice (DLL) **deve essere inviata anche al sistema di revisione delle mod.** Per farlo, caricala sempre tramite il pulsante **Carica nel Workshop** del gioco.
- Le mod caricate sul Workshop ma non inviate al sistema di revisione in genere non possono essere eseguite dai giocatori.
- I giocatori che scaricano una mod ne verificano lo stato di revisione tramite l'ID dell'elemento del Workshop e le DLL installate.
  - **Inviata / Approvata**: la mod viene eseguita. Viene eseguita anche prima che la revisione sia completata.
  - **Non inviata / Rifiutata**: la mod non viene eseguita e il motivo viene mostrato nel pannello delle mod.
- Se durante la revisione viene trovato un problema, una mod può essere rifiutata anche dopo la pubblicazione. Per una mod rifiutata, puoi vedere il motivo con **Visualizza il motivo del rifiuto** nel pannello. Le DLL rifiutate non possono essere pubblicate di nuovo così come sono; correggi il problema e caricala di nuovo.
- Anche senza un modulo `dll`, una mod è soggetta a revisione se ha `libraries` o contiene una DLL o un EXE con estensione modificata.
- Le mod che passano per la revisione del codice devono pesare al massimo 20 MB una volta compressa la cartella.

### Motivi della revisione non superata

|Messaggio|Causa|
|---|---|
|Non esiste alcuna richiesta nel sistema di revisione.|La mod è stata caricata sul Workshop ma non è stata inviata al sistema di revisione. Inviala di nuovo con il pulsante **Carica nel Workshop**.|
|Il sistema di revisione ha rifiutato l'approvazione di questa mod.|Il team operativo ha rifiutato la mod per qualche motivo. Controlla il motivo del rifiuto.|
|Impossibile ottenere informazioni dal server di revisione.|La connessione a internet dell'iscritto potrebbe non funzionare, oppure potrebbe esserci un problema con il server di revisione.|
|Questa mod proviene dal precedente sistema di revisione.|Il server di revisione è stato aggiornato e i dati di verifica esistenti della mod sono stati azzerati. Inviala di nuovo.|
|Nella mod è stata rilevata una DLL non autorizzata.|Se l'insieme dei file DLL della mod cambia mentre la mod è in esecuzione, il caricamento di quella mod viene limitato. In altre parole, quanto segue può violare il sistema di revisione: la mod scarica una DLL dall'esterno durante l'esecuzione e ne carica l'assembly; la mod crea ed esegue una propria DLL durante l'esecuzione; la mod estrae e usa una DLL racchiusa al suo interno con un proprio formato di compressione.|

## Visibilità e pubblicazione

- Al primo caricamento, la visibilità è impostata su `Non in elenco`.
- Prima di renderla pubblica, iscriviti tu stesso, verifica che la copia installata funzioni correttamente, poi cambia la visibilità in "Pubblico".
- Dalla pagina dell'elemento sullo Steam Workshop puoi modificare la descrizione della pagina, gli screenshot, le descrizioni in altre lingue e così via.

## Aggiornare la mod

Puoi aggiornare la tua mod nello stesso modo in cui l'hai pubblicata.

- Un aggiornamento che modifica le DLL viene inviato di nuovo automaticamente dopo il caricamento e passa di nuovo per la revisione. Se non hai modificato alcuna DLL, lo stato della revisione resta invariato.
- Non cambiare il `modId`. La mod verrebbe trattata come una mod diversa, con il rischio di rompere le dipendenze e altro ancora.
- **Aumenta sempre `version` in `metadata.json` a ogni aggiornamento.** Le versioni delle mod vengono confrontate quando si entra in una lobby multigiocatore, quindi se il contenuto è cambiato ma la versione no, giocatori con contenuti diversi possono ritrovarsi nella stessa partita e avere problemi.

## Ticket di caricamento

Dopo il caricamento, `uploadticket.json` viene aggiunto alla cartella della mod. Contiene l'ID dell'elemento del Workshop e le informazioni sull'invio alla revisione, quindi **non eliminarlo.** Se gestisci la cartella della mod con git, la cosa più sicura è committare anche questo file. Se cambi computer o formatti, ti servirà per continuare ad aggiornare lo stesso elemento del Workshop. Senza di esso, il gioco tratta la cartella come una nuova mod mai pubblicata.

`mod-guard-manifest.json` e `mod-guard.sig.json`, usati dal sistema di revisione prima della 1.1.1, non vengono più usati. Anche se rimangono, non vengono caricati sul Workshop e puoi eliminarli.

Consulta le [FAQ](faq.md) per sapere come recuperare il file se lo perdi.


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
