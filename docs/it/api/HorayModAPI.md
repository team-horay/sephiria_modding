<!-- File generato automaticamente. Tools/ModApiDocGen nel repository del gioco lo costruisce dai commenti /// nel codice sorgente. Non modificarlo direttamente; correggi i commenti nel sorgente e rigenera. -->
# HorayModAPI

Il punto di ingresso statico che il gioco espone alle mod. Iscriviti agli eventi di caricamento dei database, di sessione e di spawn, e registra prefab di rete e Statistiche speciali. Iscriviti agli eventi una sola volta, in `OnModLoaded`. Per il ciclo di vita e le regole del multigiocatore, consulta [Mod DLL](../dll-mods.md).

Generato il 2026-10-26 per la versione del gioco **1.1.0**. [Indice del riferimento](README.md)

| Tipo | Descrizione |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Il punto di ingresso per le mod. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Un helper per registrare testi di localizzazione delle mod. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Il punto di ingresso per le mod.

Esempio:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Costanti e campi

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Nessuna descrizione per ora.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Nessuna descrizione per ora.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Nessuna descrizione per ora.*

### Eventi

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Emesso subito dopo l'inizializzazione del database dei miracoli. Registra o modifica i miracoli con `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Emesso subito dopo l'inizializzazione del database delle armi. Registra o modifica le armi con `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Emesso subito dopo l'inizializzazione del database degli stati (effetti sulle statistiche). Registra o modifica gli stati con `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Emesso subito dopo il caricamento delle categorie e degli stati delle statistiche speciali. Registra le voci da visualizzare con `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Emesso subito dopo l'inizializzazione del database degli oggetti. Registra o modifica gli oggetti con `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Emesso subito dopo l'inizializzazione del database delle parole chiave. Registra o modifica le parole chiave con `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Emesso subito dopo l'inizializzazione del database delle passive. Registra o modifica le passive con `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Emesso subito dopo l'inizializzazione del database dei costumi. Registra o modifica i costumi con `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Emesso subito dopo l'inizializzazione del database dei prop. Registra i prop con `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Emesso subito dopo l'inizializzazione del database delle unità (entità di spawn di nemici e NPC).


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Emesso subito dopo l'inizializzazione del database dei ruoli degli NPC.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Emesso subito dopo l'inizializzazione del database delle partite personalizzate.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Emesso subito dopo che tutti i database hanno completato l'inizializzazione.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Chiamato dopo il caricamento del menu del titolo.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Chiamato all'avvio della partita. (lato server) Per avvio si intende il clic su uno slot di salvataggio nella schermata del titolo per entrare nel gioco. I dati di sessione come il giocatore e la posizione attuale non sono ancora stati caricati.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Chiamato all'avvio della partita. (lato client) Per avvio si intende il clic su uno slot di salvataggio nella schermata del titolo per entrare nel gioco. I dati di sessione come il giocatore e la posizione attuale non sono ancora stati caricati.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Chiamato all'avvio di una sessione di gioco. (lato server) Una sessione è un'unità di gioco. Quando il giocatore muore, quella sessione termina e ne inizia una nuova.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Chiamato all'avvio di una sessione di gioco. (lato client) Una sessione è un'unità di gioco. Quando il giocatore muore, quella sessione termina e ne inizia una nuova.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Chiamato dopo il caricamento dei file di localizzazione.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Chiamato quando viene allocato un piano. (lato server) Ogni luogo di Sephiria è definito come un piano. Quando un giocatore si sposta tra un luogo e l'altro, vengono eseguiti in ordine i passaggi seguenti. 1. Allocazione del piano: crea e carica il nuovo piano. 2. Spostamento: sposta il giocatore nel punto richiesto del piano ormai caricato. 3. Rimozione del piano: rimuove il luogo in cui si trovava prima il giocatore, se è vuoto (= non vi rimane alcun giocatore).
param1: GUID del piano  
param2: nome del piano  
param3: istanza di FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Chiamato quando viene allocato un piano. (lato client) Ogni luogo di Sephiria è definito come un piano. Quando un giocatore si sposta tra un luogo e l'altro, vengono eseguiti in ordine i passaggi seguenti. 1. Allocazione del piano: crea e carica il nuovo piano. 2. Spostamento: sposta il giocatore nel punto richiesto del piano ormai caricato. 3. Rimozione del piano: rimuove il luogo in cui si trovava prima il giocatore, se è vuoto (= non vi rimane alcun giocatore).
param1: GUID del piano  
param2: nome del piano  
param3: istanza di FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Chiamato dopo che è stata decisa la composizione casuale dei nemici di uno stage. (lato server) Emesso subito dopo che il generatore di piani ha scelto le fasi dei mostri per una stanza di battaglia (o una zona di battaglia), prima che lo spawner le riceva. Sostituisci o modifica ctx.Phases per cambiare cosa compare lì. Il gioco non impone alcun bilanciamento in questo punto; consulta RoomSpawnAssignContext per le regole (RNG deterministico tramite ctx.Seed, non toccare mai ctx.Sources, le entità devono essere registrate in UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Chiamato quando un giocatore compare. (lato server) Dentro questo hook puoi registrare callback relative al giocatore. Emesso alla fine di PlayerAvatar.OnStartServer, dopo l'applicazione delle statistiche base. Costume, piano e contenuto dell'inventario potrebbero essere popolati più tardi nello stesso frame o nei frame successivi. Usalo per iscriverti agli eventi di istanza dell'avatar stesso (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Le iscrizioni sull'avatar muoiono con l'avatar, quindi per queste non serve disiscriversi.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Chiamato quando compare l'avatar di un giocatore. (lato client) Emesso una volta per avatar alla fine del suo OnStartClient; viene emesso anche per i giocatori remoti. Controlla avatar.isLocalPlayer / avatar.isOwned per distinguere il giocatore locale. Le SyncVar sono già applicate, ma piano e contenuto dell'inventario potrebbero arrivare più tardi.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Emesso quando un'unità (giocatore, nemico, evocazione o compagno) compare sul server. Solo server (host).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Emesso subito prima che un'unità venga rimossa dal server. Solo server (host).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Emesso quando un giocatore schiva un attacco. (lato server)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Permette di sostituire il prefab del debuff subito prima che venga applicato a un'unità. Il gestore riceve (prefab attuale, caster) e restituisce un prefab sostitutivo; restituire null lascia tutto invariato.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Emesso quando il server prende il permesso di scrittura sull'inventario e inizia a rimuovere tutti gli effetti degli oggetti.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Emesso subito dopo che il server restituisce il permesso di scrittura e riapplica gli effetti degli oggetti.

### Metodi

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Registra un oggetto (prefab) che può essere creato tramite la rete. L'oggetto deve avere un componente NetworkIdentity.

Il server (host) e i client devono ciascuno mantenere un elenco degli oggetti che possono usare la rete, e quell'elenco deve essere identico per tutti.

Esempio:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Restituisce: true se la registrazione è riuscita


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Registra un prefab creato a runtime (per esempio un clone di un template integrato) come oggetto creabile tramite la rete, usando un assetId fisso calcolato da `stableKey`. Usa il nome dell'unità della mod (`<modId>:<id>`) come chiave.

Restituisce: true se la registrazione è riuscita


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Registra più prefab in una volta sola come oggetti creabili tramite la rete.

Restituisce: il numero di prefab registrati con successo


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Annulla la registrazione di un prefab di rete registrato.

Restituisce: true se è stato rimosso


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Registra una categoria di Statistiche speciali nell'interfaccia delle statistiche.

Restituisce: true se registrata


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Registra una StatusEntity come voce di una categoria di Statistiche speciali nell'interfaccia delle statistiche. Se passi `reader`, il valore viene letto tramite quella funzione invece che dalle impostazioni dell'asset.

Restituisce: true se registrata


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Ottiene le opzioni per dispositivo associate a modId. Le mod del Workshop possono usare `Context.Options` al suo posto.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Apre un pannello di interazione con gli oggetti usato insieme all'inventario. Restituisce false se l'interfaccia è occupata.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Ottiene una factory di widget di interfaccia nello stile del gioco. Agganciali sotto un Canvas di interfaccia esistente.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Apre una finestra di dialogo nel mondo. Restituisce false se l'interfaccia è occupata; mostra la pagina successiva con `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Apre una finestra di dialogo con un offset nello spazio del mondo. Se null, viene usato l'offset predefinito del prefab.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Apre una finestra di dialogo con offset separati per il dialogo e per la telecamera. La messa a fuoco della telecamera si applica solo in locale.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Un helper per registrare testi di localizzazione delle mod.

### Metodi

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Registra un testo per una lingua specifica.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Registra un testo per più lingue in una volta sola
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
