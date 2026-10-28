# Mod di localizzazione

[La tua prima mod](getting-started.md) ha coperto le basi della creazione di una mod, quindi questa pagina tratta le regole comuni alle mod di localizzazione.
[`examples/translation-sample`](../../examples/translation-sample) contiene un esempio completo che puoi copiare direttamente in `WorkshopLocal/` per provarlo.



## Come funziona

- Il gioco ha una tabella **chiave → stringa** per ogni lingua. Quando una mod viene caricata, i valori della mod sovrascrivono la tabella originale.
- Il tuo file deve contenere solo le chiavi che vuoi cambiare.
- Il nome del file è la lingua. `ko-KR.json` sovrascrive la tabella coreana, `en-US.json` sovrascrive quella inglese.
- Se più mod di localizzazione impostano la stessa chiave, vince la mod caricata per ultima.
- Una mod che cambia solo il testo non influisce sugli altri partecipanti, quindi consigliamo di impostare `multiplayer.installation` in `metadata.json` su `local_only`.
- Sia le chiavi che i valori sono stringhe. Oggetti annidati e array non sono ammessi.
- Salva i file in **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← aggiungi { "type": "localization", "path": "Localization" } a modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Cosa non tradurre

Il testo del gioco contiene espressioni che vengono interpretate a runtime.
Non tradurre il testo qui sotto; lascialo in inglese esattamente com'è (maiuscole e minuscole contano).

| Tipo    | Esempio                                                                     | Descrizione                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Parole chiave   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Vengono sostituite a runtime con numeri o nomi. Non cambiare nemmeno un carattere dentro le parentesi graffe.   |
| Tag di gioco | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Vengono sostituiti con tooltip e icone delle parole chiave. I nomi dentro i tag sono fissi, maiuscole comprese. |
| Tag di formattazione | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Formattazione del testo. I tag di apertura e chiusura devono essere accoppiati.        |
| Interruzioni di riga   | `\n`                                                                        | Un'interruzione di riga dentro una stringa JSON.                        |




## Aggiungere una nuova lingua

- Puoi anche aggiungere una lingua che il gioco non supporta. Se il nome del file è un nuovo codice lingua, il gioco la registra come nuova lingua e la mostra nell'elenco delle impostazioni di lingua.
- I file di lingua originali si trovano in `<cartella di installazione del gioco>/Sephiria_Data/StreamingAssets/Localization/`, uno per lingua; usali come riferimento per tradurre.
- Le lingue già supportate dal gioco sono `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` e `tr-TR`.
- **Traduci tutte le chiavi.** Le chiavi mancanti nel file della tua mod non vengono mostrate vuote: ricadono sull'**inglese** (o sul coreano se manca anche l'inglese). Il modo più semplice per iniziare è copiare il file `en-US.json` originale.
- **Compila le chiavi di impostazione che iniziano con `#`.** `#LanguageName` è il nome mostrato nell'elenco delle impostazioni di lingua, e `#FontName` è il font da usare. Adatta le chiavi di punteggiatura come `#Colon`, `#Bullet` e `#ParenthesisOpen` alle convenzioni di quella lingua.
- Per `#FontName` puoi scegliere tra i font integrati nel gioco. Il gioco include font per i seguenti sistemi di scrittura:

| Sistema di scrittura | `#FontName` |
|---|---|
| Latino, cirillico, hangul | `GalCyrillic` |
| Giapponese | `JF-Dot-MPlus10` |
| Cinese semplificato | `Fusion_Pixel_10_Simple` |
| Cinese tradizionale | `BoutiqueBitmap9x9` |
| Thailandese | `NotoSansThai` |



## Risoluzione dei problemi

| Sintomo | Cosa controllare |
|---|---|
| La mod è attiva ma il testo non cambia | Che il nome della chiave corrisponda esattamente all'originale, carattere per carattere. Che il nome del file corrisponda alla lingua attuale del gioco (`ko-KR.json` si applica solo in coreano). |
| Solo alcune chiavi non cambiano | Che un'altra mod di localizzazione non stia sovrascrivendo la stessa chiave più tardi. Prova a cambiare l'ordine nel pannello. |
| `{VALUE}` compare così com'è nel testo | La parola chiave è scritta in modo diverso dall'originale. Copiala direttamente dal testo originale. |
| `<tag=…>` compare così com'è nel testo | Il nome del tag ha una grafia o maiuscole diverse. |
| Il testo coreano (o in altra lingua non inglese) appare corrotto | Il file non è in UTF-8. Salvalo di nuovo in UTF-8. |
| I caratteri compaiono come quadratini | Quei caratteri non sono inclusi nei font integrati del gioco. |
| Un intero file viene ignorato | Quel file ha un errore di sintassi JSON. `Player.log` registra l'errore insieme al nome del file. |

Il file di log si trova in `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Cerca "모드 내 언어 파일 발견" (file di lingua trovato nella mod) per vedere quali file sono stati letti e quante chiavi aveva ciascuno.
