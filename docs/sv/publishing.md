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
3. Välj din mod och tryck på **Ladda upp till Workshop**.
4. För moddar utan kod är uppladdningen till Workshop det sista steget.
5. För moddar som innehåller kod (DLL-filer) skickas modden automatiskt in till **granskningssystemet** tillsammans med Workshop-objektets ID när uppladdningen till Workshop är klar.
6. Gå till den uppladdade Workshop-sidan, gör ett sista test, till exempel genom att prenumerera på den, och ändra sedan synligheten till "Offentlig".

Om uppladdningen till Workshop blev klar men bara inskickningen till granskning misslyckades (till exempel på grund av ett nätverksfel) trycker du på **Ladda upp till Workshop** igen för samma mod. Om innehållet inte har ändrats laddas modden inte upp till Workshop igen; bara inskickningen skickas om. Inskickningen måste göras med det Steam-konto som skapade Workshop-objektet.

## Kodgranskning

- Granskningssystemet dekompilerar DLL-filer och kontrollerar dem med avseende på nätverkskommunikation, filåtkomst, obfuskering och skadligt beteende. Granskningen kombinerar automatisk analys med operatörskontroller, och vi är medvetna om att inget granskningssystem kan fånga alla risker. Ändå strävar vi efter att minska spridningen av skadliga moddar genom den här granskningsprocessen. Tack för ditt samarbete.
- Alla moddar som innehåller kod (DLL-filer) **måste också skickas in till granskningssystemet för moddar.** Ladda därför alltid upp via spelets knapp **Ladda upp till Workshop**.
- Moddar som har laddats upp till Workshop men inte skickats in till granskningssystemet kan i regel inte köras av spelare.
- Spelare som laddar ner en mod slår upp dess granskningsstatus med hjälp av moddens Workshop-objekt-ID och de installerade DLL-filerna.
  - **Inskickad / Godkänd**: Modden körs. Den körs även innan granskningen är klar.
  - **Inte inskickad / Avvisad**: Modden körs inte, och orsaken visas i modpanelen.
- Om ett problem hittas under granskningen kan en mod avvisas även efter att den har publicerats. För en avvisad mod kan du se orsaken med **Visa anledning till avslag** i panelen. De avvisade DLL-filerna kan inte publiceras igen som de är; åtgärda problemet och ladda upp igen.
- Även utan en `dll`-modul granskas en mod om den har `libraries` eller innehåller en DLL- eller EXE-fil med ändrat filtillägg.
- Moddar som går igenom kodgranskning får vara högst 20 MB när mappen är komprimerad.

### Orsaker till misslyckad granskning

|Meddelande|Orsak|
|---|---|
|Det finns ingen anmälan i granskningssystemet.|Modden har laddats upp till Workshop men har inte skickats in till granskningssystemet. Skicka in den igen med knappen **Ladda upp till Workshop**.|
|Granskningssystemet har avvisat godkännandet av den här modden.|Driftteamet har avvisat modden av någon anledning. Kontrollera anledningen till avslaget.|
|Det gick inte att hämta information från granskningsservern.|Prenumerantens internetanslutning kan vara nere, eller så kan det vara problem med granskningsservern.|
|Den här modden kommer från det tidigare granskningssystemet.|Granskningsservern har uppgraderats och moddens befintliga verifieringsdata har återställts. Skicka in den igen.|
|En otillåten DLL upptäcktes i modden.|Om uppsättningen DLL-filer i modden ändras medan modden körs begränsas inläsningen av modden. Det innebär att följande kan bryta mot granskningssystemets regler: modden laddar ner en DLL utifrån medan den körs och läser in assemblyn; modden skapar och kör en egen DLL medan den körs; modden packar upp en DLL som är packad i den med ett eget komprimeringsformat och använder den.|

## Synlighet och offentliggörande

- När du laddar upp första gången sätts synligheten till `Unlisted`.
- Innan du gör modden offentlig: prenumerera på den själv, kontrollera att den installerade kopian fungerar som den ska och ändra sedan till "Offentlig".
- Du kan redigera sidans beskrivning, skärmbilder, beskrivningar på andra språk med mera från Steam Workshop-objektets sida.

## Uppdatera din mod

Du uppdaterar din mod på samma sätt som du publicerade den.

- En uppdatering som ändrar DLL-filer skickas automatiskt in igen efter uppladdningen och granskas på nytt. Om du inte har ändrat några DLL-filer förblir granskningsstatusen densamma.
- Ändra inte ditt `modId`. Modden behandlas då som en annan mod, vilket bland annat kan bryta beroenden.
- **Höj alltid `version` i `metadata.json` vid varje uppdatering.** Modversioner jämförs när man går med i en flerspelarlobby, så om innehållet har ändrats men inte versionen kan spelare med olika modinnehåll hamna i samma spel och råka ut för problem.

## Uppladdningsbiljett

När du har laddat upp läggs `uploadticket.json` till i din modmapp. Den innehåller ditt Workshop-objekts ID och information om granskningsinskickningen, så **ta inte bort den.** Om du hanterar din modmapp med git är det säkrast att även checka in den här filen. Om du byter dator eller installerar om behöver du filen för att kunna fortsätta uppdatera samma Workshop-objekt. Utan den behandlar spelet mappen som en ny mod som aldrig har publicerats.

`mod-guard-manifest.json` och `mod-guard.sig.json`, som granskningssystemet använde före 1.1.1, används inte längre. Även om de finns kvar laddas de inte upp till Workshop, och du kan ta bort dem.

Se [FAQ](faq.md) för hur du återställer filen om du förlorar den.


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
