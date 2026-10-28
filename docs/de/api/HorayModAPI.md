<!-- Automatisch generierte Datei. Tools/ModApiDocGen im Spiel-Repository erstellt sie aus den ///-Kommentaren im Quellcode. Nicht direkt bearbeiten; korrigiere die Quellkommentare und generiere neu. -->
# HorayModAPI

Der statische Einstiegspunkt, den das Spiel Mods zur Verfügung stellt. Abonniere Datenbank-Lade-, Sitzungs- und Spawn-Events und registriere Netzwerk-Prefabs und Spezialwerte. Abonniere Events nur einmal, in `OnModLoaded`. Lebenszyklus und Mehrspieler-Regeln findest du unter [DLL-Mods](../dll-mods.md).

Generiert am 2026-10-26 für Spielversion **1.1.0**. [Referenzindex](README.md)

| Typ | Beschreibung |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Der Einstiegspunkt für Mods. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Ein Helfer zum Registrieren von Lokalisierungstexten einer Mod. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Der Einstiegspunkt für Mods.

Beispiel:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Konstanten und Felder

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Noch keine Beschreibung.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Noch keine Beschreibung.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Noch keine Beschreibung.*

### Events

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Feuert direkt nach der Initialisierung der Wunder-Datenbank. Registriere oder verändere Wunder mit `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Feuert direkt nach der Initialisierung der Waffen-Datenbank. Registriere oder verändere Waffen mit `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Feuert direkt nach der Initialisierung der Status-Datenbank (Werteeffekte). Registriere oder verändere Status mit `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Feuert direkt nach dem Laden der Spezialwert-Kategorien und -Status. Registriere Anzeigeeinträge mit `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Feuert direkt nach der Initialisierung der Item-Datenbank. Registriere oder verändere Items mit `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Feuert direkt nach der Initialisierung der Schlüsselwort-Datenbank. Registriere oder verändere Schlüsselwörter mit `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Feuert direkt nach der Initialisierung der Passiv-Datenbank. Registriere oder verändere Passive mit `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Feuert direkt nach der Initialisierung der Kostüm-Datenbank. Registriere oder verändere Kostüme mit `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Feuert direkt nach der Initialisierung der Prop-Datenbank. Registriere Props mit `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Feuert direkt nach der Initialisierung der Einheiten-Datenbank (Spawn-Entitäten für Gegner und NPCs).


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Feuert direkt nach der Initialisierung der NPC-Rollen-Datenbank.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Feuert direkt nach der Initialisierung der Datenbank für benutzerdefinierte Spiele.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Feuert direkt nachdem alle Datenbanken ihre Initialisierung abgeschlossen haben.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Wird aufgerufen, nachdem das Titelmenü geladen wurde.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Wird aufgerufen, wenn das Spiel startet. (serverseitig) Starten bedeutet, auf dem Titelbildschirm einen Speicherplatz anzuklicken, um das Spiel zu betreten. Sitzungsdaten wie Spieler und aktueller Ort sind noch nicht geladen.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Wird aufgerufen, wenn das Spiel startet. (clientseitig) Starten bedeutet, auf dem Titelbildschirm einen Speicherplatz anzuklicken, um das Spiel zu betreten. Sitzungsdaten wie Spieler und aktueller Ort sind noch nicht geladen.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Wird aufgerufen, wenn eine Spielsitzung startet. (serverseitig) Eine Sitzung ist eine Spieleinheit. Stirbt der Spieler, endet diese Sitzung und eine neue beginnt.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Wird aufgerufen, wenn eine Spielsitzung startet. (clientseitig) Eine Sitzung ist eine Spieleinheit. Stirbt der Spieler, endet diese Sitzung und eine neue beginnt.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Wird aufgerufen, nachdem die Lokalisierungsdateien geladen wurden.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Wird aufgerufen, wenn eine Etage zugewiesen wird. (serverseitig) Jeder Ort in Sephiria ist als Etage definiert. Wenn ein Spieler zwischen Orten wechselt, laufen die folgenden Schritte der Reihe nach ab. 1. Etagenzuweisung: erstellt und lädt die neue Etage. 2. Bewegung: bewegt den Spieler zum angeforderten Punkt auf der vollständig geladenen Etage. 3. Etagenentfernung: entfernt den Ort, an dem der Spieler zuvor war, falls er leer ist (= keine Spieler mehr dort).
param1: GUID der Etage  
param2: Name der Etage  
param3: Instanz von FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Wird aufgerufen, wenn eine Etage zugewiesen wird. (clientseitig) Jeder Ort in Sephiria ist als Etage definiert. Wenn ein Spieler zwischen Orten wechselt, laufen die folgenden Schritte der Reihe nach ab. 1. Etagenzuweisung: erstellt und lädt die neue Etage. 2. Bewegung: bewegt den Spieler zum angeforderten Punkt auf der vollständig geladenen Etage. 3. Etagenentfernung: entfernt den Ort, an dem der Spieler zuvor war, falls er leer ist (= keine Spieler mehr dort).
param1: GUID der Etage  
param2: Name der Etage  
param3: Instanz von FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Wird aufgerufen, nachdem die zufällige Gegnerzusammensetzung einer Stage festgelegt wurde. (serverseitig) Feuert direkt nachdem der Etagengenerator die Monsterphasen für einen Kampfraum (oder eine Kampfzone) gewählt hat, bevor der Spawner sie erhält. Ersetze oder bearbeite ctx.Phases, um zu ändern, was dort spawnt. Das Spiel erzwingt hier keine Balance; die Regeln stehen in RoomSpawnAssignContext (deterministischer RNG über ctx.Seed, ctx.Sources niemals anfassen, Entitäten müssen in UnitDatabase registriert sein).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Wird aufgerufen, wenn ein Spieler spawnt. (serverseitig) In diesem Hook kannst du spielerbezogene Callbacks registrieren. Wird am Ende von PlayerAvatar.OnStartServer ausgelöst, nachdem die Basiswerte angewendet wurden. Kostüm, Etage und Inventarinhalt werden möglicherweise erst später im selben Frame oder in den folgenden Frames befüllt. Verwende ihn, um die Instanz-Events des Avatars selbst zu abonnieren (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Abonnements auf dem Avatar enden mit dem Avatar, für sie ist also kein Abbestellen nötig.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Wird aufgerufen, wenn ein Spieler-Avatar spawnt. (clientseitig) Wird einmal pro Avatar am Ende seines OnStartClient ausgelöst; feuert auch für entfernte Spieler. Prüfe avatar.isLocalPlayer / avatar.isOwned, um den lokalen Spieler zu unterscheiden. SyncVars sind bereits angewendet, aber Etage und Inventarinhalt können später eintreffen.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Feuert, wenn eine Einheit (Spieler, Gegner, Beschwörung oder Begleiter) auf dem Server spawnt. Nur Server (Host).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Feuert direkt bevor eine Einheit vom Server entfernt wird. Nur Server (Host).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Feuert, wenn ein Spieler einem Angriff ausweicht. (serverseitig)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Erlaubt es, das Debuff-Prefab direkt vor dem Anwenden eines Debuffs auf eine Einheit auszutauschen. Der Handler erhält (aktuelles Prefab, Verursacher) und gibt ein Ersatz-Prefab zurück; null lässt es unverändert.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Feuert, wenn der Server die Schreibberechtigung für das Inventar übernimmt und beginnt, alle Item-Effekte zu entfernen.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Feuert direkt nachdem der Server die Schreibberechtigung zurückgibt und die Item-Effekte erneut anwendet.

### Methoden

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Registriert ein Objekt (Prefab), das über das Netzwerk gespawnt werden kann. Das Objekt muss eine NetworkIdentity-Komponente haben.

Server (Host) und Clients müssen jeweils eine Liste der Objekte halten, die Netzwerkfunktionen nutzen können, und diese Liste muss bei allen identisch sein.

Beispiel:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Rückgabe: true, wenn die Registrierung erfolgreich war


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Registriert ein zur Laufzeit erstelltes Prefab (etwa einen Klon einer eingebauten Vorlage) als netzwerkspawnbares Objekt, mit einer festen assetId, die aus `stableKey` berechnet wird. Verwende den Einheitennamen der Mod (`<modId>:<id>`) als Schlüssel.

Rückgabe: true, wenn die Registrierung erfolgreich war


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Registriert mehrere Prefabs auf einmal als netzwerkspawnbare Objekte.

Rückgabe: die Anzahl der erfolgreich registrierten Prefabs


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Hebt die Registrierung eines registrierten Netzwerk-Prefabs auf.

Rückgabe: true, wenn es entfernt wurde


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Registriert eine Spezialwert-Kategorie in der Werte-UI.

Rückgabe: true, wenn registriert


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Registriert eine StatusEntity als Eintrag in einer Spezialwert-Kategorie der Werte-UI. Übergibst du `reader`, wird der Wert über diese Funktion statt über die Asset-Einstellungen gelesen.

Rückgabe: true, wenn registriert


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Ruft die gerätebezogenen Optionen ab, die an die modId gebunden sind. Workshop-Mods können stattdessen `Context.Options` verwenden.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Öffnet ein Item-Interaktionspanel, das zusammen mit dem Inventar verwendet wird. Gibt false zurück, wenn die UI beschäftigt ist.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Ruft eine Factory für UI-Widgets im Stil des Spiels ab. Hänge sie unter einen bestehenden UI-Canvas.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Öffnet eine Dialogbox in der Spielwelt. Gibt false zurück, wenn die UI beschäftigt ist; zeige die nächste Seite mit `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Öffnet eine Dialogbox mit einem Versatz im Weltkoordinatensystem. Bei null wird der Standardversatz des Prefabs verwendet.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Öffnet eine Dialogbox mit getrenntem Dialog- und Kameraversatz. Der Kamerafokus gilt nur lokal.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Ein Helfer zum Registrieren von Lokalisierungstexten einer Mod.

### Methoden

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Registriert Text für eine bestimmte Sprache.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Registriert Text für mehrere Sprachen auf einmal
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
