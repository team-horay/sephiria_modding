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
- I loader esterni come BepInEx e MelonLoader non sono supportati. Una mod che si comporta come un loader esterno (cioè che trova ed esegue DLL di plugin esterne al Workshop) riceve un avviso e viene attivata solo dopo che l'utente lo ha accettato. Per i dettagli vedi [Mod che eseguono codice esterno al Workshop](#mod-che-eseguono-codice-esterno-al-workshop).


## DLL di supporto

Se la tua mod usa altre librerie (per esempio pacchetti di terze parti diversi da Newtonsoft.Json), metti le loro DLL nella cartella della mod e dichiarale in `libraries` in `metadata.json`. Le DLL non dichiarate non vengono usate per la risoluzione dei riferimenti.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Non includere DLL già fornite con il gioco (Unity, Mirror, Newtonsoft.Json, 0Harmony, ecc.). La copia del gioco viene caricata per prima, quindi quella inclusa nella tua mod viene ignorata oppure va in conflitto.
- Le DLL native (C/C++) non possono essere usate. Vengono caricati solo assembly gestiti.
- Se un assembly con lo stesso nome è già stato caricato da un'altra mod, per una DLL in `modules` il gioco non ne sceglie uno dei due: **sospende l'esecuzione**. Per una DLL in `libraries`, il gioco usa quella già caricata e ignora la tua copia, il che può causare malfunzionamenti se le versioni sono diverse. È più sicuro separare le librerie condivise in una mod a parte e collegarla tramite `dependencies`.

### Usare la libreria di un'altra mod

Se più mod usano lo stesso codice, inseriscilo in una mod principale e collega le mod dipendenti tramite `dependencies`.

1. La mod principale dichiara il codice condiviso in `modules` o `libraries` e aggiunge `Library` ai suoi `tags`.
2. La mod dipendente indica il `modId` della mod principale in `dependencies` e viene compilata rispetto alla DLL della mod principale con `Private=false`. **Non copiare la DLL della mod principale nella mod dipendente.**
3. Il gioco carica per prima la mod principale, quindi i riferimenti della mod dipendente vengono risolti con l'assembly della mod principale già caricato.

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "workshopId": "123456789" }
]
```

- Anche la mod dipendente deve avere un proprio modulo `dll` e una propria `entryClass`. Non è supportato che la mod principale trovi e carichi da sé le DLL della mod dipendente. Fai invece in modo che la mod dipendente chiami la funzione di registrazione della mod principale dal proprio `OnModLoaded`.
- Se la mod principale modifica i propri tipi o membri pubblici con un aggiornamento, le mod dipendenti possono smettere di funzionare. Evita di rimuovere o modificare le API pubbliche esistenti di una libreria condivisa.

### Non creare DLL durante l'esecuzione!

Tutto il codice eseguito da una mod deve trovarsi in un elemento del Workshop sottoposto a revisione. Se i file DLL di una mod cambiano mentre la mod è in esecuzione, il caricamento di quella mod viene limitato. Includi tutte le DLL nell'elemento del Workshop così come sono, senza alcuna compressione o streaming. In altre parole, quanto segue può violare il sistema di revisione. Al momento del caricamento, inserisci nella mod tutte le DLL di cui hai bisogno.

- La mod scarica una DLL dall'esterno durante l'esecuzione e ne carica l'assembly
- La mod crea ed esegue una propria DLL durante l'esecuzione (compreso riscrivere un'altra DLL con Mono.Cecil o simili e poi eseguirla)
- La mod estrae e usa una DLL racchiusa al suo interno con un proprio formato di compressione

### Mod che eseguono codice esterno al Workshop

Una mod che trova ed esegue DLL inserite dall'utente **fuori dalla propria cartella**, ad esempio nella cartella di installazione del gioco o in `AppData` (per esempio una mod di compatibilità per i plugin di un altro loader), non viene rifiutata, ma il sistema di revisione le aggiunge un **avviso**.

- Quando l'utente attiva la mod compare un avviso, e la mod viene attivata solo se lo accetta. Quando la mod viene aggiornata, la richiesta viene riproposta.
- Le DLL esterne eseguite in questo modo non vengono sottoposte a revisione, e i problemi che causano non sono supportati.
- Una mod che scarica da sé tali DLL durante l'esecuzione rientra nei casi vietati indicati sopra.

Per usare il codice di un'altra mod, non caricare DLL direttamente dalla cartella di quella mod: collegala tramite `dependencies` come descritto in [Usare la libreria di un'altra mod](#usare-la-libreria-di-unaltra-mod). Le DLL che il gioco non ha caricato da sé saltano l'ordine di caricamento, il controllo dei conflitti di nome e quello delle dipendenze, quindi non sono supportate.

Se devi riscrivere una DLL (per esempio convertire l'IL per un'altra API di Harmony), fallo prima del caricamento e includi nella mod la DLL convertita.


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
- Quando modifichi il codice, deve essere inviato di nuovo al sistema di revisione. Consulta [Pubblicare sul Workshop](publishing.md#revisione-del-codice).


## Considerazioni sul multigiocatore

- La maggior parte delle mod con codice avrà `multiplayer.installation` impostato su `everyone`.
- Se la tua mod modifica lo stato di gioco solo negli eventi `Serverside`, e i valori che modifica sono stato di gioco già sincronizzato (per esempio `customStats`), così che i partecipanti non abbiano bisogno di codice o registrazioni, puoi dichiararla `host_only`. I partecipanti allora non scaricano la mod. Una mod che registra nuovi oggetti, usa `RegisterNetworkPrefab` o modifica qualcosa negli eventi `Clientside` non può essere `host_only`.
- Gli oggetti creati tramite la rete (unità, proiettili, ecc.) devono essere registrati con `RegisterNetworkPrefab` per essere creati anche sul lato dei partecipanti. Se passi uno `stableKey`, lo stesso ID viene mantenuto anche quando cambia la versione della mod.
- Host e partecipanti devono avere persino lo stesso **ordine di caricamento** delle mod. Evita codice il cui risultato dipende da quell'ordine.


## Sincronizzazione di rete (SyncVar, RPC, Command)

Il multigiocatore di Sephiria è realizzato con la libreria di rete open source "Mirror". In Mirror puoi usare facilmente la comunicazione dichiarando i seguenti attributi.

- `[SyncVar]`: attributo da applicare a una variabile membro di una classe. Sincronizza il valore della variabile tra server e client.
- `[ClientRpc]`, `[TargetRpc]`: attributi da applicare a una funzione. Chiedono ai client di eseguire la funzione dal server.
- `[Command]`: attributo da applicare a una funzione. Chiede al server di eseguire la funzione da un client.

In un ambiente di mod questi attributi non si possono usare normalmente. Funzionano solo se il Weaver di Mirror inserisce in fase di compilazione il codice di trasmissione corrispondente a ciascun attributo, ma una DLL generata all'esterno non passa dal Weaver, quindi anche se la compilazione riesce, questi attributi non fanno nulla. Questo tipo di bug sembra normale sullo schermo dell'host e dà problemi solo su quello dei partecipanti, quindi è difficile da scoprire provando da soli. (Anche se si forzasse il passaggio dal Weaver, potrebbero esserci bug come collisioni dell'hash delle funzioni.)


### `SyncVar<T>`, `ModRpc`

Per risolvere il problema, Sephiria fornisce codice di comunicazione per le mod che sostituisce quegli attributi: `SyncVar<T>` e `ModRpc`.

| Cosa vuoi fare | Cosa usare |
|---|---|
| Mantenere un valore sempre allineato (compreso un partecipante che entra in ritardo) | `SyncVar<T>` |
| Chiamata dal server a tutti i client | `ModRpc.Send` |
| Chiamata dal server a un client specifico | `ModRpc.SendTo` |
| Chiamata da un client al server | `ModRpc.SendCommand` |
| Cambiare un valore che il gioco già sincronizza | Proprietà `Network<FieldName>` |

- Questa funzione è disponibile a partire dalla versione 1.1.2 del gioco.
- Le mod che usano questa funzione devono dichiarare `multiplayer.installation` come `everyone`.


### Preparare il componente

`SyncVar<T>` e `ModRpc` si possono usare solo in un componente che eredita da `NetworkBehaviour`. Questo componente deve essere collegato allo stesso oggetto, **nello stesso ordine**, sia sull'host sia sui partecipanti. Se lo colleghi con `AddComponent` a un oggetto già generato, non rientra nella sincronizzazione, quindi deve essere già collegato prima di registrare l'oggetto come prefab di rete.

```csharp
private static GameObject prefab;

// Iscriviti a HorayModAPI.OnAllDatabasesReady da OnModLoaded.
private static void RegisterPrefab()
{
    if (prefab) return;

    // Mettilo sotto un genitore inattivo. L'Awake del prefab non viene eseguito, e le copie create con Instantiate partono attive.
    var holder = new GameObject("MyMod_Prefabs");
    holder.SetActive(false);
    Object.DontDestroyOnLoad(holder);

    prefab = new GameObject("FlameOrb");
    prefab.transform.SetParent(holder.transform);
    prefab.AddComponent<NetworkIdentity>();
    prefab.AddComponent<FlameOrb>();
    HorayModAPI.RegisterNetworkPrefab(prefab, "yourname.my-mod:flame-orb");
}

// Genera l'oggetto dal server. Se passi il secondo argomento, quel giocatore ne diventa il proprietario.
private static void SpawnFor(PlayerAvatar player)
{
    var orb = Object.Instantiate(prefab);
    NetworkServer.Spawn(orb, player.connectionToClient);
}
```

### `SyncVar<T>`: sincronizzare i valori

```csharp
public class FlameOrb : NetworkBehaviour
{
    public readonly SyncVar<int> stack = new SyncVar<int>(0);

    public FlameOrb()
    {
        // Poiché non c'è il Weaver, registralo tu stesso nel costruttore. L'ordine dei campi deve essere sempre lo stesso.
        InitSyncObject(stack);
    }

    private void Awake()
    {
        stack.Callback += (oldValue, newValue) => UpdateVisual(newValue);
    }

    private void UpdateVisual(int value) { }
}
```

- Cambia il valore **sul server**, per esempio con `stack.Value = 3`. Un valore cambiato da un partecipante non viene trasmesso al server.
- Anche un partecipante che entra in ritardo riceve il valore attuale al momento dell'ingresso.
- `Callback` viene chiamato sull'host quando si assegna un valore e sui partecipanti quando si riceve un valore. Viene chiamato anche se il valore ricevuto all'ingresso è diverso da quello predefinito. Un'eccezione nel callback viene registrata nel log e la sincronizzazione prosegue.
- Puoi usare tipi di base come `int`, `float`, `string` e `Vector3`. Per uno struct creato da una mod il codice di serializzazione non viene generato automaticamente, quindi invia i suoi campi separatamente.
- Per riferirti a un altro oggetto, invece di `SyncVar<GameObject>` invia il suo `netId` come `SyncVar<uint>`. Sul lato ricevente quell'oggetto potrebbe non essere ancora stato creato.
- Il numero di SyncVar registrabili in un componente è 64, compresi quelli ereditati.

### `ModRpc`: chiamare funzioni

Registra la funzione una volta al caricamento della mod. Il nome usato nella chiamata è `modId:functionName`.

```csharp
protected override void OnModLoaded()
{
    ModRpc.RegisterRpc<FlameOrb>("yourname.my-mod", "explode", (orb, r) => orb.PlayExplosion(r.ReadVector3()));
    ModRpc.RegisterCommand<FlameOrb>("yourname.my-mod", "ignite", (orb, r, sender) => orb.ServerIgnite(r.ReadInt(), sender));
}
```

```csharp
// Server → tutti i client che vedono questo oggetto
ModRpc.Send(this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Server → un client. Se passi null come connessione, viene inviato al proprietario dell'oggetto.
ModRpc.SendTo(connectionToClient, this, "yourname.my-mod:explode", w => w.WriteVector3(position));

// Client → server
ModRpc.SendCommand(this, "yourname.my-mod:ignite", w => w.WriteInt(slot));
```

- Leggi nell'ordine in cui hai scritto. Se l'ordine o i tipi di scrittura e lettura sono diversi, i valori risultano sbagliati o viene generata un'eccezione.
- Per impostazione predefinita, `RegisterCommand` può essere chiamato solo dal **proprietario dell'oggetto**. Per riceverlo su un oggetto senza proprietario, passa `requiresAuthority: false`. In questo caso qualsiasi partecipante può chiamarlo. Fai attenzione a cheat e simili.
- Se lo invii in una direzione diversa da quella registrata, o con un nome non registrato, resta solo un log di errore e non viene trasmesso.
- Se sul lato ricevente la funzione non esiste o il tipo di componente è diverso, resta solo un log di avviso e la chiamata viene ignorata. Anche un'eccezione nel gestore viene registrata nel log e il gioco prosegue.
- Il canale predefinito è il trasporto affidabile. Per effetti inviati di frequente, puoi passare l'argomento `channelId: Channels.Unreliable`.

### Cose da rispettare assolutamente

- Un client può inviare valori arbitrari tramite un Command. Verifica nel gestore sul server il numero di slot, la quantità, il bersaglio e simili.
- Un partecipante che entra in ritardo non riceve gli RPC precedenti al suo ingresso. Invia con `SyncVar<T>` lo stato che deve restare, e usa gli RPC per cose passeggere come suoni ed effetti.
- **Un RPC può arrivare prima del SyncVar.** Se leggi `stack.Value` nel gestore di un RPC, potresti ottenere il valore precedente. Se ti serve il nuovo valore, invialo anche come argomento dell'RPC oppure gestiscilo in `Callback`.

### Modificare i valori del codice base di Sephiria

Se assegni direttamente un campo `[SyncVar]` di una classe del gioco, il valore non viene trasmesso ai partecipanti. Usa la proprietà con il prefisso `Network`. Maiuscole e minuscole sono quelle del nome del campo, così com'è.

```csharp
unit.maxHp = 500;         // Cambia solo il valore dell'host.
unit.NetworkmaxHp = 500;  // Viene trasmesso anche ai partecipanti.
```

Prima che l'oggetto venga generato (sul prefab o subito dopo `Instantiate`), anche un'assegnazione diretta viene trasmessa insieme al valore al momento della generazione.

### Test

L'host da solo non fa emergere i problemi descritti sopra. Prima di pubblicare, controlla con due PC gli schermi di host e partecipante, e prova anche il caso di un ingresso in ritardo dopo aver cambiato un valore. Con il pacchetto locale (`WorkshopLocal`) attivo nessun altro può entrare nella stanza, quindi i test multigiocatore si fanno iscrivendosi a un elemento caricato sul Workshop come Unlisted. Un elemento caricato per la prima volta è automaticamente Unlisted.


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
