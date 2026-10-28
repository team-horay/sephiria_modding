# Mod DLL

I moduli DLL ti permettono di eseguire codice scritto in C# dentro il gioco. Iscriviti agli eventi di `HorayModAPI` per agganciarti a giocatori, oggetti, interfaccia e altro ancora.
Questo documento copre tutto, dalla creazione del progetto fino alla prima DLL in esecuzione nel gioco. Per l'elenco completo degli eventi e delle funzioni di registrazione, consulta il [Riferimento API](api/README.md).


## Requisiti

- [.NET SDK](https://dotnet.microsoft.com/download) 8 o successivo
- Un editor per C#


## Creare un progetto

Fai riferimento all'esempio [`examples/dll-sample`](../../examples/dll-sample). Questo documento lo usa come riferimento.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>9.0</LangVersion>
    <AssemblyName>PhysicalDamagePlus</AssemblyName>
    <RootNamespace>PhysicalDamagePlus</RootNamespace>
    <Nullable>disable</Nullable>
    <ImplicitUsings>disable</ImplicitUsings>
    <GenerateDependencyFile>false</GenerateDependencyFile>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <!-- Cartella di installazione del gioco. Modificala in base alla tua configurazione, oppure passala con dotnet build -p:GameDir=... -->
    <GameDir Condition="'$(GameDir)' == ''">C:\Program Files (x86)\Steam\steamapps\common\Sephiria</GameDir>
    <OutDir>$(GameDir)\WorkshopLocal\physical-damage-plus\bin\</OutDir>
  </PropertyGroup>

  <ItemGroup>
    <Reference Include="Assembly-CSharp">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Assembly-CSharp.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <Reference Include="UnityEngine.CoreModule">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\UnityEngine.CoreModule.dll</HintPath>
      <Private>false</Private>
    </Reference>
    <!-- Allo stesso modo, aggiungi Mirror se lavori con oggetti di rete come PlayerAvatar, UnityEngine.UI se costruisci interfacce e 0Harmony se applichi patch -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Usa `netstandard2.1` come target** e imposta la versione del linguaggio su C# 9.
2. **Referenzia le DLL del gioco, ma non copiarle.** Fai puntare i riferimenti ai file in `<cartella di installazione del gioco>/Sephiria_Data/Managed/` e impostali tutti su `Private=false`. Se le DLL del gioco finiscono copiate nella cartella della mod, lo stesso assembly viene caricato due volte, il che causa errori difficili da interpretare, e per di più aggiunge inutilmente file alla revisione.
3. `Assembly-CSharp.dll` contiene la maggior parte del codice che fa funzionare il gioco.


## Struttura del codice


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Chiamato una volta per processo. Iscriviti agli eventi qui, e una sola volta.
        protected override void OnModLoaded()
        {
            Debug.Log($"[PhysicalDamagePlus] Loaded v{metadata.modVersion} | folder={Context.PackageRoot}");

            HorayModAPI.OnPlayerAvatarSpawnedServerside += HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside += HandlePlayerSpawnedClient;
        }

        protected override void OnModUnloaded()
        {
            HorayModAPI.OnPlayerAvatarSpawnedServerside -= HandlePlayerSpawnedServer;
            HorayModAPI.OnPlayerAvatarSpawnedClientside -= HandlePlayerSpawnedClient;
        }

        // Chiamato solo sull'host (server). Modifica lo stato di gioco qui.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Chiamato su ogni partecipante. Il valore aggiunto dal server arriva tramite la sincronizzazione.
        private static void HandlePlayerSpawnedClient(PlayerAvatar player)
        {
            if (player == null)
                return;
            Debug.Log($"[PhysicalDamagePlus] PhysicalDamage={player.GetCustomStat(ECustomStat.PhysicalDamage)}");
        }
    }
}
```

```json
"modules": [
  { "type": "dll", "path": "bin/PhysicalDamagePlus.dll", "entryClass": "PhysicalDamagePlus.Entry" }
]
```

### WorkshopModBase

La tua classe eredita dalla classe base i membri seguenti.

| Membro | Descrizione |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Riempiti con i valori di `metadata.json`. |
| `Context` | `PackageRoot` (percorso assoluto della cartella della mod), `FileId` (ID dell'elemento sullo Steam Workshop; per i pacchetti locali, un valore separato che non collide mai con un ID di Steam), `Definition` (il manifest validato), `ResolvePath(relativePath)`. Usalo per trovare i file nella cartella della mod. Il percorso di installazione è diverso per ogni utente, quindi non scriverlo mai a mano nel codice. |
| `GetDataPackages()` | L'elenco dei pacchetti del modulo `data` che indicano questa mod come `consumer`. Chiamalo dopo che l'attivazione è completata (dentro un gestore `OnLoad*Database` o dopo `OnAllDatabasesReady`). |

`Context` è valido dal momento in cui viene chiamato `OnModLoaded`. Nel costruttore è ancora null.


## Ciclo di vita

Le mod DLL vengono attivate **quando avvii una partita ed entri nel mondo**. Nella schermata del titolo non viene ancora eseguito alcun codice. L'ordine di avvio è il seguente.

1. La scena del mondo viene caricata e `GameDataLoader` parte.
2. I moduli DLL del piano di caricamento vengono caricati in ordine, ogni classe di ingresso viene istanziata e viene chiamato `OnModLoaded`.
3. Man mano che i database si inizializzano, eventi come `OnLoadItemDatabase` e `OnLoadUnitDatabase` vengono emessi uno dopo l'altro.
4. Quando tutto è completato, viene chiamato `OnAllDatabasesReady`.

## Iscriversi agli eventi

La classe `HorayModAPI` offre eventi predefiniti che ti permettono di agganciarti a molte situazioni.

- Iscriviti agli eventi **una sola volta**, in `OnModLoaded`. Gli eventi dei database vengono emessi di nuovo a ogni caricamento, quindi fai le cose che "devono succedere ogni volta" (registrare oggetti, ecc.) dentro i gestori.
- Gestisci con cura iscrizione e disiscrizione. Disiscriviti (-=) dagli eventi a cui ti sei iscritto in `OnModUnloaded`.

L'elenco completo degli eventi si trova nel [Riferimento API](api/README.md). La tabella qui sotto indica l'evento consigliato per ciascuna cosa che potresti voler fare.

| Cosa vuoi fare | Evento |
|---|---|
| Aggiungere oggetti o artefatti | `OnLoadItemDatabase` |
| Aggiungere armi, miracoli, passive, costumi o prop | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Aggiungere unità nemiche o NPC | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Aggiungere viaggiatori (NPC del villaggio) | `OnLoadSocialIDDatabase` |
| Aggiungere la visualizzazione di una statistica | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Registrare testo localizzato dal codice | `OnLocalizationReady` (`AddText`) |
| Aggiungere un pulsante al menu del titolo | `OnTitleMenuReady` |
| Avvio di partita o sessione | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Quando viene generato un piano | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Spawn di giocatori o unità | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| Agganciarsi ai controlli di combattimento | `OnPlayerEvade`, `OnGetDebuff` |
| Permessi di manipolazione dell'inventario | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Evitare i conflitti

Le entità aggiunte dalla tua mod vengono chiamate nel formato `<modId>:<id>`, per esempio `yourname.artifact-pack:flame-ring`. Questa regola previene i conflitti con altre mod ed è il modo in cui i contenuti delle mod vengono distinti nella cronologia delle partite e nelle statistiche.


## Lato server, lato client

- Sephiria è costruito attorno al multigiocatore, quindi molti eventi esistono in due versioni separate, lato server e lato client.
- L'host emette entrambe; gli altri partecipanti emettono solo quella Clientside.
- Come regola generale, fai le cose che modificano lo stato di gioco (spawn, drop, danni) solo in Serverside, e le cose che cambiano solo ciò che viene mostrato in Clientside.


## Patch con Harmony

Quando devi cambiare qualcosa che gli eventi non raggiungono, puoi usare Harmony.
Dalla patch 1.1.0, **Harmony 2.4.3 è integrato** nel gioco, quindi basta aggiungere un riferimento.

```xml
<Reference Include="0Harmony">
  <HintPath>$(GameDir)\Sephiria_Data\Managed\0Harmony.dll</HintPath>
  <Private>false</Private>
</Reference>
```

```csharp
protected override void OnModLoaded()
{
    var harmony = new HarmonyLib.Harmony("yourname.first-mod");
    harmony.PatchAll();
}
```

- **Non includere `0Harmony.dll` nella tua mod.** Una copia inclusa viene ignorata, e anche se ne includi una versione diversa, la tua mod non girerà su quella versione.
- Usare il tuo `modId` così com'è come ID di Harmony evita collisioni con gli altri.
- Le patch si rompono con gli aggiornamenti del gioco molto più facilmente dell'API ufficiale. Fai il possibile con gli eventi e tieni le patch come ultima risorsa.
- I loader esterni come BepInEx e MelonLoader non sono supportati.


## DLL di supporto

Se la tua mod usa altre librerie (per esempio pacchetti di terze parti diversi da Newtonsoft.Json), metti le loro DLL nella cartella della mod e dichiarale in `libraries` in `metadata.json`. Le DLL non dichiarate non vengono usate per la risoluzione dei riferimenti.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Non includere DLL già fornite con il gioco (Unity, Mirror, Newtonsoft.Json, 0Harmony, ecc.). La copia del gioco viene caricata per prima, quindi quella inclusa nella tua mod viene ignorata oppure va in conflitto.
- Le DLL native (C/C++) non possono essere usate. Vengono caricati solo assembly gestiti.
- Se un assembly con lo stesso nome è già stato caricato da un'altra mod, il gioco non ne sceglie uno dei due: **sospende l'esecuzione** della mod. È più sicuro separare le librerie condivise in una mod a parte e collegarla tramite `dependencies`.


## Compilare e provare

```bash
dotnet build -c Release
```

- Quando la compilazione riesce e la DLL compare in `WorkshopLocal/<mod>/bin/`, avvia il gioco, controlla che la tua mod compaia sotto "Le mie mod" nel pannello dello Steam Workshop nella schermata del titolo, poi avvia una partita.
- Se tieni il `.pdb` accanto all'output della compilazione, le stack trace nel log includono i numeri di riga. Toglilo quando pubblichi.


## Aggiornamenti del gioco e compatibilità

Quando il gioco si aggiorna, `Assembly-CSharp.dll` cambia. Le mod che usano solo gli eventi ufficiali di solito continuano a funzionare, ma quelle che toccano direttamente le classi interne o applicano patch con Harmony possono rompersi.

- Dopo un aggiornamento, ricompila contro la nuova cartella `Managed/` e fai dei test.
- Se indichi la **versione del gioco supportata** nella descrizione della tua pagina del Workshop, gli utenti possono individuare rapidamente la causa dei problemi. La versione del gioco potrebbe anche essere registrata automaticamente nei metadati dell'elemento del Workshop al momento della pubblicazione.
- Se modifichi il codice, deve passare di nuovo la revisione prima di poter essere pubblicato. Consulta [Pubblicare sul Workshop](publishing.md).


## Considerazioni sul multigiocatore

- La maggior parte delle mod con codice avrà `multiplayer.installation` impostato su `everyone`.
- Se la tua mod modifica lo stato di gioco solo negli eventi `Serverside`, e i valori che modifica sono stato di gioco già sincronizzato (per esempio `customStats`), così che i partecipanti non abbiano bisogno di codice o registrazioni, puoi dichiararla `host_only`. I partecipanti allora non scaricano la mod. Una mod che registra nuovi oggetti, usa `RegisterNetworkPrefab` o modifica qualcosa negli eventi `Clientside` non può essere `host_only`.
- Gli oggetti creati tramite la rete (unità, proiettili, ecc.) devono essere registrati con `RegisterNetworkPrefab` per essere creati anche sul lato dei partecipanti. Se passi uno `stableKey`, lo stesso ID viene mantenuto anche quando cambia la versione della mod.
- Host e partecipanti devono avere persino lo stesso **ordine di caricamento** delle mod. Evita codice il cui risultato dipende da quell'ordine.


## Risoluzione dei problemi

| Sintomo | Cosa controllare |
|---|---|
| Il riepilogo di stato dice che la classe di ingresso non è stata trovata | Il namespace o il nome della classe in `entryClass` non corrisponde al tuo codice. Le classi annidate si scrivono `Outer+Inner`. |
| Il riepilogo di stato mostra un errore di tipo della classe di ingresso | La classe di ingresso non eredita da `WorkshopModBase` (o `HorayModBase`), non è pubblica, è astratta o non ha un costruttore pubblico senza parametri. |
| Il riepilogo di stato dice che la mod è stata sospesa per un conflitto di nome di assembly | Un'altra mod (AddOns inclusi) usa lo stesso `AssemblyName`. Rinomina il tuo. |
| Il riepilogo di stato mostra un errore di caricamento di una libreria | Una DLL elencata in `libraries` manca oppure non è un assembly gestito. |
| `FileNotFoundException` nel log | Una DLL referenziata non è nella cartella della mod oppure non è dichiarata in `libraries`. Oppure una DLL del gioco è stata copiata con `Private=true` e le due vanno in conflitto. |
| Eccezione subito dopo l'ingresso nel mondo | Un'eccezione in un gestore di eventi dei database. Cerca il namespace della tua mod nella stack trace del log. |
| Gli effetti si accumulano dopo essere tornati al titolo e rientrati | Ti stai iscrivendo di nuovo dentro un gestore. Iscriviti una sola volta, in `OnModLoaded`. |


## Esempio

[`examples/dll-sample`](../../examples/dll-sample) è una mod minimale che aumenta i Danni fisici di 5 quando il giocatore compare. La sua cartella `package/` include una DLL già compilata, quindi copiando quella cartella in `WorkshopLocal/` funziona subito senza compilare. Per trasformarla nella tua mod, ti basta cambiare `AssemblyName`, `modId`, `entryClass` e il percorso di output. Avvia una partita e apri la finestra delle statistiche per vederla in azione come Danni fisici +5.
