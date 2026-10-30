[한국어](../../README.md) | [English](../../README.en.md) | [日本語](../ja/README.md) | [简体中文](../zh-CN/README.md) | [繁體中文](../zh-TW/README.md) | [Deutsch](../de/README.md) | [Français](../fr/README.md) | [Español](../es/README.md) | **Italiano** | [Polski](../pl/README.md) | [Português (Brasil)](../pt-BR/README.md) | [Русский](../ru/README.md) | [Svenska](../sv/README.md) | [ไทย](../th/README.md) | [Türkçe](../tr/README.md)

# Guida al modding di Sephiria

Grazie per esserti unito alla community di modding di Sephiria.

Questo è il repository ufficiale di guide, esempi e template per il modding di [Sephiria](https://store.steampowered.com/app/2436940). Le mod possono essere condivise tramite lo [Steam Workshop](https://steamcommunity.com/app/2436940/workshop/).



## Regole

Per mantenere il gioco sicuro per tutti, rispetta le regole qui sotto. Le violazioni possono comportare restrizioni alla pubblicazione di mod o all'accesso al gioco.

1. **Niente comportamenti malevoli**: sono vietati malware, backdoor, keylogger, miner di criptovalute e qualsiasi altra funzione che comprometta il dispositivo, l'account o i dati di un utente.
2. **Uso non commerciale**: tutte le mod devono essere create e distribuite per scopi non commerciali.
3. **Niente violazioni di diritti o contenuti dannosi**: sono vietati i contenuti che violano il diritto d'autore, la privacy o il diritto all'immagine di altre persone, oppure che contengono materiale sessuale o promuovono odio, molestie o violenza nel mondo reale.
4. **Niente interferenze con il gioco**: non provocare intenzionalmente malfunzionamenti del gioco e non interferire con il normale gioco degli altri giocatori.
5. **Niente raccolta di dati**: non raccogliere informazioni sugli utenti e non inviarle a destinazioni esterne.
6. **Niente elusione dei controlli di accesso**: non aggirare in modo illecito i controlli di acquisto, proprietà o accesso del gioco o della piattaforma di distribuzione.
7. **Revisione delle mod**: le mod che contengono codice (DLL) passano per una revisione, che include analisi e decompilazione, per verificare eventuali violazioni delle regole, e possono essere rifiutate anche dopo la pubblicazione se viene trovato un problema. Non falsificare i risultati della revisione né i contrassegni di approvazione. I file devono essere forniti in uno stato revisionabile; l'offuscamento è vietato.
8. **Uso delle risorse del gioco**: decompilare e analizzare il codice, la grafica, l'audio e le altre risorse del gioco è consentito nella misura necessaria a creare mod. Tuttavia, non ridistribuire le risorse estratte dal gioco in quanto tali.
9. **Niente furto d'identità o manomissioni**: non spacciarti per altre persone e non modificare né ridistribuire mod create da altri senza il loro consenso.



## Prima di iniziare

- Le mod che contengono codice (DLL) vengono **inviate automaticamente al sistema di revisione** quando le carichi sul Workshop. La revisione controlla principalmente che il codice non contenga comportamenti malevoli, e le mod non inviate o rifiutate non vengono eseguite nelle partite degli altri giocatori. Le mod senza codice, come traduzioni e costumi, possono essere caricate subito senza revisione. Tieni presente che le mod che il sistema di revisione non riesce a leggere vengono rifiutate (file offuscati, DLL compilate da C/C++, ecc.).
- In multigiocatore, tutti i partecipanti della stanza devono avere attivo lo stesso insieme di mod. (Le mod personali di interfaccia, come le traduzioni, sono esenti.)
- Dopo aver attivato delle mod, devi riavviare il gioco per cambiarne l'ordine o disattivarle di nuovo.
- Le mod a cui sei iscritto sono disattivate per impostazione predefinita. Puoi attivarle tu stesso dall'elenco.



## Documentazione

| Documento | Contenuto |
|---|---|
| [La tua prima mod](getting-started.md) | Preparare una cartella di sviluppo, provare una mod in locale, leggere i log |
| [Riferimento di metadata.json](metadata.md) | Significato di ogni campo, regole per dipendenze e versioni, dichiarazione dei moduli |
| [Mod di localizzazione](localization-mods.md) | Struttura dei file di lingua, ricerca delle chiavi, regole per conservare tag e segnaposto |
| [Mod di costumi](costume-mods.md) | Creare un costume personalizzato |
| [Mod DLL](dll-mods.md) | Configurazione del progetto, punto di ingresso, eventi di `HorayModAPI`, debug |
| [Pubblicare sul Workshop](publishing.md) | Caricamento sul Workshop, revisione delle mod con codice, regole del multigiocatore |
| [Riferimento API](api/README.md) | Tipi e membri pubblici come `HorayModAPI`, `WorkshopModBase`, opzioni delle mod e interfaccia. Generato automaticamente dal codice sorgente del gioco |
| [Appendice e FAQ](faq.md) |  |


## Licenza

Il codice di questo repository (esempi e template) è distribuito con [licenza MIT](../../LICENSE).
La documentazione può essere liberamente citata, tradotta e ridistribuita; ti chiediamo di indicare la fonte.
Il gioco Sephiria e le sue risorse sono opere di TEAM HORAY e non sono coperti da questa licenza.
