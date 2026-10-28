# Publicera på Workshop

Den här sidan förklarar hur du laddar upp din mod till Steam Workshop, hur granskningssystemet fungerar och hur moddar synkroniseras mellan spelare i flerspelarläge.


## Innan du laddar upp

- För att ladda upp en mod till Workshop krävs en förhandsvisningsbild (PNG eller JPG, under 1 MB). Ange den med `info.preview`, eller lägg en `thumbnail.png` i din modmapp. Se [Referens för metadata.json](metadata.md#preview) för detaljer.
- `info.title` får vara upp till 128 tecken och `info.description` upp till 7999 tecken. Om du har skrivit dem på flera språk används `en-US`-versionen på Workshop-sidan.
- Ange Workshop-kategorier i `tags`. Se [Referens för metadata.json](metadata.md) för tillgängliga kategorier.
- Alla filer i mappen granskas, så se till att din modmapp inte innehåller kopior av spelets DLL-filer, `.pdb`-filer eller mellanliggande byggresultat.

## Publiceringssteg

1. Starta Sephiria.
2. Öppna panelen **Steam Workshop** på titelskärmen och leta reda på modden du har skapat.
3. Välj din mod.
4. Moddar som innehåller kod (DLL-filer) går igenom kodgranskning.
5. Moddar utan kod kan laddas upp till Workshop direkt.
6. Gå till den uppladdade Workshop-sidan, gör ett sista test, till exempel genom att prenumerera på den, och ändra sedan synligheten till "Offentlig".

## Kodgranskning

- Granskningssystemet kontrollerar alla filer i din modmapp. DLL-filer dekompileras och kontrolleras med avseende på nätverkskommunikation, filåtkomst, obfuskering och skadligt beteende; datafiler som bilder, ljud och JSON kontrolleras så att formatet är giltigt.
- Om inga problem hittas godkänns modden automatiskt; om något ser misstänkt ut kontrollerar en operatör den manuellt. Uppenbart skadliga moddar avvisas automatiskt.
- Granskningen kombinerar automatisk analys med operatörskontroller, och vi är medvetna om att inget granskningssystem kan fånga alla risker. Ändå strävar vi efter att minska spridningen av skadliga moddar genom den här förhandsgranskningen och godkännandeprocessen. Tack för ditt samarbete.
- Om en mod innehåller kod avgörs av filernas **innehåll**, inte filnamnen. Även utan en `dll`-modul granskas en mod om den har `libraries` eller innehåller en DLL- eller EXE-fil med ändrat filtillägg.
- Moddar som går igenom kodgranskning får vara högst 20 MB när mappen är komprimerad.

## Godkännandebevis (endast moddar med kod)

- Moddar som innehåller kod får ett godkännandebevis.
- Det intygar att moddens filer är identiska med den godkända versionen.
- DLL-moddar måste gå igenom den här certifieringen innan spelare som laddar ner modden kan köra dess kod.
- Om så mycket som en fil ändras efter certifieringen blir beviset ogiltigt och modden måste granskas igen.

## Synlighet och offentliggörande

- När du laddar upp första gången sätts synligheten till `Unlisted`.
- Innan du gör modden offentlig: prenumerera på den själv, kontrollera att den installerade kopian fungerar som den ska och ändra sedan till "Offentlig".
- Du kan redigera sidans beskrivning, skärmbilder, beskrivningar på andra språk med mera från Steam Workshop-objektets sida.

## Uppdatera din mod

Du uppdaterar din mod på samma sätt som du publicerade den.

- DLL-moddar behöver ett nytt godkännandebevis.
- Ändra inte ditt `modId`. Modden behandlas då som en annan mod, vilket bland annat kan bryta beroenden.
- **Höj alltid `version` i `metadata.json` vid varje uppdatering.** Modversioner jämförs när man går med i en flerspelarlobby, så om innehållet har ändrats men inte versionen kan spelare med olika modinnehåll hamna i samma spel och råka ut för problem.

## Uppladdningsbiljett

När du har begärt granskning och laddat upp läggs filerna nedan till i din modmapp. De innehåller din granskningsgodkännandeinformation, så **ta inte bort dem.** Om du hanterar din modmapp med git är det säkrast att även checka in de här tre filerna. Om du byter dator eller installerar om behöver du filerna för att kunna fortsätta uppdatera samma Workshop-objekt.

| Fil | Innehåll |
|---|---|
| `uploadticket.json` | Granskningsbiljetten och Workshop-objektets ID. Utan den behandlar spelet mappen som en ny mod som aldrig har publicerats. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | Granskningens godkännandebevis: listan över godkända filer och en signatur. Om du redigerar dem blir godkännandet ogiltigt. |

Se [FAQ](faq.md) för hur du återställer filerna om du förlorar dem.

## Återkallat godkännande

Om en regelöverträdelse upptäcks efter publiceringen kan en operatör återkalla godkännandet. En mod med återkallat godkännande läses inte in även om den är installerad, och den visas i panelen som "Granskningsgodkännandet har återkallats".


## Regler för flerspelarläge

### Uppsättning av flerspelarlobby

När värden skapar en flerspelarlobby registreras **listan över moddar som värden har aktiverat** i lobbyn.

- Moddar med `installation` satt till `host_only`: deltagarna behöver inte installera dem; de visas bara i listan.
- Moddar med `multiplayer.installation` satt till `everyone` (eller inte deklarerat): deltagarna måste också installera dem.
- Dräktmoddar och datamoddar: deltagarna måste också installera dem.

### Gå med

Om en spelares nuvarande modduppsättning skiljer sig från lobbyns går hen igenom de här stegen: ladda ner → aktivera → starta om spelet och försök gå med igen.

### Testmoddar installerade i WorkshopLocal

Flerspelarläge är inte tillgängligt i ett spel där lokala moddar från `WorkshopLocal/` har lästs in. För att testa flerspelarläge laddar du upp din mod till Workshop, tömmer den lokala mappen och använder den prenumererade kopian. Du kan prenumerera via länken även medan objektet är `Unlisted`.
