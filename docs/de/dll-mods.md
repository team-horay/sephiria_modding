# DLL-Mods

Mit DLL-Modulen kannst du in C# geschriebenen Code im Spiel ausführen. Abonniere `HorayModAPI`, um dich in Spieler, Items, UI und mehr einzuklinken.
Dieses Dokument behandelt alles vom Erstellen eines Projekts bis zum ersten Lauf deiner DLL im Spiel. Die vollständige Liste der Events und Registrierungsfunktionen findest du in der [API-Referenz](api/README.md).


## Voraussetzungen

- [.NET SDK](https://dotnet.microsoft.com/download) 8 oder neuer
- Ein C#-Editor


## Ein Projekt erstellen

Orientiere dich am Beispiel [`examples/dll-sample`](../../examples/dll-sample). Dieses Dokument verwendet es als Referenz.

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
    <!-- Installationsordner des Spiels. Passe ihn an deine Umgebung an oder übergib ihn mit dotnet build -p:GameDir=... -->
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
    <!-- Füge auf dieselbe Weise Mirror hinzu, wenn du mit Netzwerkobjekten wie PlayerAvatar arbeitest, UnityEngine.UI, wenn du UI baust, und 0Harmony, wenn du patchst -->
    <Reference Include="Mirror">
      <HintPath>$(GameDir)\Sephiria_Data\Managed\Mirror.dll</HintPath>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
```

1. **Ziele auf `netstandard2.1`** und setze die Sprachversion auf C# 9.
2. **Verweise auf die Spiel-DLLs, aber kopiere sie nicht.** Richte die Verweise auf die Dateien in `<Installationsordner des Spiels>/Sephiria_Data/Managed/` und setze sie alle auf `Private=false`. Werden Spiel-DLLs in deinen Mod-Ordner kopiert, wird dieselbe Assembly zweimal geladen, was schwer nachvollziehbare Fehler verursacht, und außerdem landen unnötig Dateien in der Prüfung.
3. `Assembly-CSharp.dll` enthält den Großteil des Codes, der das Spiel ausführt.


## Code-Struktur


```csharp
using UnityEngine;

namespace PhysicalDamagePlus
{
    public class Entry : WorkshopModBase
    {
        public const int Bonus = 5;

        // Wird einmal pro Prozess aufgerufen. Abonniere Events hier, und nur einmal.
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

        // Wird nur auf dem Host (Server) aufgerufen. Ändere den Spielzustand hier.
        private static void HandlePlayerSpawnedServer(PlayerAvatar player)
        {
            if (player == null)
                return;
            player.AddCustomStat(ECustomStat.PhysicalDamage, Bonus);
        }

        // Wird bei jedem Teilnehmer aufgerufen. Der vom Server hinzugefügte Wert kommt über die Synchronisierung an.
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

Deine Klasse erbt Folgendes von der Basisklasse.

| Member | Beschreibung |
|---|---|
| `metadata` | `modName`, `modVersion`, `modAuthor`. Wird mit den Werten aus `metadata.json` gefüllt. |
| `Context` | `PackageRoot` (absoluter Pfad des Mod-Ordners), `FileId` (Steam-Workshop-Beitrags-ID; bei lokalen Paketen ein separater Wert, der nie mit einer Steam-ID kollidiert), `Definition` (das validierte Manifest), `ResolvePath(relativePath)`. Verwende es, um Dateien in deinem Mod-Ordner zu finden. Der Installationspfad ist bei jedem Nutzer anders, also codiere ihn niemals fest. |
| `GetDataPackages()` | Die Liste der `data`-Modulpakete, die diese Mod als ihren `consumer` angeben. Rufe sie nach Abschluss der Aktivierung auf (in einem `OnLoad*Database`-Handler oder nach `OnAllDatabasesReady`). |

`Context` ist ab dem Aufruf von `OnModLoaded` gültig. Im Konstruktor ist es noch null.


## Lebenszyklus

DLL-Mods werden aktiviert, **wenn du ein Spiel startest und die Welt betrittst**. Auf dem Titelbildschirm läuft noch kein Code. Die Startreihenfolge ist wie folgt.

1. Die Weltszene wird geladen und `GameDataLoader` startet.
2. Die DLL-Module im Ladeplan werden der Reihe nach geladen, jede Einstiegsklasse wird instanziiert und `OnModLoaded` aufgerufen.
3. Während die Datenbanken initialisiert werden, feuern nacheinander Events wie `OnLoadItemDatabase` und `OnLoadUnitDatabase`.
4. Wenn alles erledigt ist, wird `OnAllDatabasesReady` aufgerufen.

## Abonnieren

Die Klasse `HorayModAPI` stellt vordefinierte Events bereit, mit denen du dich in viele Situationen einklinken kannst.

- Abonniere Events **nur einmal**, in `OnModLoaded`. Datenbank-Events feuern bei jedem Laden erneut, erledige also die Dinge, die „jedes Mal passieren müssen“ (Items registrieren usw.), innerhalb der Handler.
- Verwalte Abonnieren und Abbestellen sorgfältig. Bestelle die abonnierten Events in `OnModUnloaded` wieder ab (-=).

Die vollständige Liste der Events steht in der [API-Referenz](api/README.md). Die folgende Tabelle nennt das empfohlene Event für jedes Vorhaben.

| Was du tun willst | Event |
|---|---|
| Items oder Artefakte hinzufügen | `OnLoadItemDatabase` |
| Waffen, Wunder, Passive, Kostüme oder Props hinzufügen | `OnLoadWeaponDatabase`, `OnLoadMiracleDatabase`, `OnLoadPassiveDatabase`, `OnLoadCostumeDatabase`, `OnLoadPropDatabase` |
| Gegner- oder NPC-Einheiten hinzufügen | `OnLoadUnitDatabase` + `RegisterNetworkPrefab(prefab, stableKey)` |
| Reisende (Dorf-NPCs) hinzufügen | `OnLoadSocialIDDatabase` |
| Eine Wertanzeige hinzufügen | `OnLoadSpecialStatDatabase` + `RegisterSpecialStat` |
| Lokalisierten Text aus dem Code registrieren | `OnLocalizationReady` (`AddText`) |
| Eine Schaltfläche zum Titelmenü hinzufügen | `OnTitleMenuReady` |
| Spiel- oder Sitzungsstart | `OnStartGameServerside/Clientside`, `OnStartSessionServerside/Clientside` |
| Wenn eine Etage generiert wird | `OnFloorAllocatedServerside/Clientside`, `OnAssignRoomSpawnPhases` |
| Spieler oder Einheit spawnt | `OnPlayerAvatarSpawned*`, `OnUnitSpawnedServerside`, `OnUnitDespawnedServerside` |
| In Kampfprüfungen einklinken | `OnPlayerEvade`, `OnGetDebuff` |
| Berechtigungen zur Inventarmanipulation | `GridInventoryStartPermission`, `GridInventoryEndPermission` |


## Konflikte vermeiden

Entitäten, die deine Mod hinzufügt, werden im Format `<modId>:<id>` benannt, z. B. `yourname.artifact-pack:flame-ring`. Diese Regel verhindert Konflikte mit anderen Mods, und so werden Mod-Inhalte in Durchlauf-Historie und Statistiken auseinandergehalten.


## Serverseitig, clientseitig

- Sephiria ist auf den Mehrspielermodus ausgelegt, daher gibt es viele Events getrennt in einer serverseitigen und einer clientseitigen Version.
- Der Host feuert beide; andere Teilnehmer feuern nur die Clientside-Version.
- Grundsätzlich gilt: Erledige Dinge, die den Spielzustand ändern (Spawnen, Drops, Schaden), nur in Serverside, und Dinge, die nur die Anzeige ändern, in Clientside.


## Harmony-Patches

Wenn du etwas ändern musst, das Events nicht erreichen, kannst du Harmony verwenden.
Seit Patch 1.1.0 ist **Harmony 2.4.3 im Spiel eingebaut**, füge also einfach einen Verweis darauf hinzu.

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

- **Lege `0Harmony.dll` nicht in deine Mod.** Eine mitgelieferte Kopie wird ignoriert, und selbst wenn du eine andere Version beilegst, läuft deine Mod nicht mit dieser Version.
- Verwendest du deine `modId` unverändert als Harmony-ID, kollidiert sie nicht mit anderen.
- Patches gehen bei Spiel-Updates deutlich leichter kaputt als die offizielle API. Erledige, was du kannst, mit Events, und halte Patches als letztes Mittel bereit.
- Externe Loader wie BepInEx und MelonLoader werden nicht unterstützt.


## Hilfs-DLLs

Wenn deine Mod andere Bibliotheken verwendet (z. B. Drittanbieter-Pakete außer Newtonsoft.Json), lege deren DLLs in deinen Mod-Ordner und deklariere sie in `libraries` in `metadata.json`. Nicht deklarierte DLLs werden nicht zur Verweisauflösung verwendet.

```json
"libraries": [ "bin/MyHelper.dll" ]
```

- Lege keine DLLs bei, die das Spiel bereits mitbringt (Unity, Mirror, Newtonsoft.Json, 0Harmony usw.). Die Kopie des Spiels wird zuerst geladen, die Kopie deiner Mod wird also entweder ignoriert oder sorgt für Konflikte.
- Native (C/C++) DLLs können nicht verwendet werden. Es werden nur verwaltete Assemblys geladen.
- Wurde eine Assembly mit demselben Namen bereits von einer anderen Mod geladen, wählt das Spiel keine der beiden aus, sondern **hält die Ausführung zurück**. Es ist sicherer, gemeinsam genutzte Bibliotheken in eine separate Mod auszulagern und sie über `dependencies` zu verknüpfen.

### Erzeuge keine DLLs zur Laufzeit!

Ändern sich die DLL-Dateien einer Mod, während die Mod läuft, wird das Laden dieser Mod eingeschränkt. Lege alle DLLs unverändert in den Workshop-Beitrag, ohne jegliche Komprimierung oder Streaming. Folgendes kann also gegen das Prüfsystem verstoßen. Lege alle benötigten DLLs beim Upload in die Mod.

- Die Mod lädt während der Ausführung eine DLL von außen herunter und lädt die Assembly
- Die Mod erzeugt während der Ausführung eine eigene DLL und führt sie aus
- Die Mod entpackt eine DLL, die mit einem eigenen Komprimierungsverfahren in ihr verpackt ist, und verwendet sie


## Bauen und Testen

```bash
dotnet build -c Release
```

- Sobald der Build erfolgreich ist und die DLL in `WorkshopLocal/<mod>/bin/` erscheint, starte das Spiel, prüfe, ob deine Mod im Steam-Workshop-Panel auf dem Titelbildschirm unter „Meine Mods“ auftaucht, und starte dann ein Spiel.
- Wenn du die `.pdb` neben der Build-Ausgabe behältst, enthalten Stacktraces im Log Zeilennummern. Lass sie bei der Veröffentlichung weg.


## Spiel-Updates und Kompatibilität

Wenn das Spiel aktualisiert wird, ändert sich `Assembly-CSharp.dll`. Mods, die nur offizielle Events verwenden, funktionieren meist weiter, aber Mods, die interne Klassen direkt anfassen oder mit Harmony patchen, können kaputtgehen.

- Baue nach einem Update gegen den neuen `Managed/`-Ordner neu und teste.
- Wenn du die **unterstützte Spielversion** in der Beschreibung deiner Workshop-Seite vermerkst, finden Nutzer die Ursache schnell. Die Spielversion wird beim Veröffentlichen möglicherweise auch automatisch in den Metadaten des Workshop-Beitrags festgehalten.
- Wenn du deinen Code änderst, muss er erneut beim Prüfsystem eingereicht werden. Siehe [Im Workshop veröffentlichen](publishing.md#code-prüfung).


## Hinweise zum Mehrspielermodus

- Bei den meisten Code-Mods wird `multiplayer.installation` auf `everyone` stehen.
- Wenn deine Mod den Spielzustand nur in `Serverside`-Events ändert und die geänderten Werte bereits synchronisierter Spielzustand sind (z. B. `customStats`), sodass die Teilnehmer weder Code noch Registrierung brauchen, kannst du sie als `host_only` deklarieren. Teilnehmer laden die Mod dann nicht herunter. Eine Mod, die neue Items registriert, `RegisterNetworkPrefab` verwendet oder irgendetwas in `Clientside`-Events ändert, kann nicht `host_only` sein.
- Objekte, die über das Netzwerk gespawnt werden (Einheiten, Projektile usw.), müssen mit `RegisterNetworkPrefab` registriert werden, damit sie auch auf der Seite der Teilnehmer erstellt werden. Wenn du einen `stableKey` übergibst, bleibt dieselbe ID auch bei Änderungen deiner Mod-Version erhalten.
- Host und Teilnehmer müssen sogar dieselbe **Ladereihenfolge** der Mods haben. Vermeide Code, dessen Ergebnisse von dieser Reihenfolge abhängen.


## Fehlerbehebung

| Symptom | Was zu prüfen ist |
|---|---|
| Die Statuszusammenfassung sagt, die Einstiegsklasse wurde nicht gefunden | Namespace oder Klassenname in `entryClass` passen nicht zu deinem Code. Verschachtelte Klassen werden als `Outer+Inner` geschrieben. |
| Die Statuszusammenfassung zeigt einen Typfehler der Einstiegsklasse | Die Einstiegsklasse erbt nicht von `WorkshopModBase` (oder `HorayModBase`), ist nicht public, ist abstract oder hat keinen öffentlichen parameterlosen Konstruktor. |
| Die Statuszusammenfassung sagt, die Mod wurde wegen eines Assembly-Namenskonflikts zurückgehalten | Eine andere Mod (einschließlich AddOns) verwendet denselben `AssemblyName`. Benenne deinen um. |
| Die Statuszusammenfassung zeigt einen Fehler beim Laden einer Bibliothek | Eine in `libraries` aufgeführte DLL fehlt oder ist keine verwaltete Assembly. |
| `FileNotFoundException` im Log | Eine referenzierte DLL liegt nicht in deinem Mod-Ordner oder ist nicht in `libraries` deklariert. Oder eine Spiel-DLL wurde mit `Private=true` kopiert und die beiden kollidieren. |
| Exception direkt nach dem Betreten der Welt | Eine Exception in einem Datenbank-Event-Handler. Suche im Stacktrace des Logs nach dem Namespace deiner Mod. |
| Effekte stapeln sich nach Rückkehr zum Titel und erneutem Betreten | Du abonnierst erneut innerhalb eines Handlers. Abonniere nur einmal, in `OnModLoaded`. |


## Beispiel

[`examples/dll-sample`](../../examples/dll-sample) ist eine minimale Mod, die den Physischen Schaden um 5 erhöht, wenn der Spieler spawnt. Ihr Ordner `package/` enthält eine vorgebaute DLL, sodass das Kopieren dieses Ordners nach `WorkshopLocal/` sofort ohne Build funktioniert. Um daraus deine eigene Mod zu machen, musst du nur `AssemblyName`, `modId`, `entryClass` und den Ausgabepfad ändern. Starte ein Spiel und öffne das Wertefenster, um es als Physischer Schaden +5 in Aktion zu sehen.
