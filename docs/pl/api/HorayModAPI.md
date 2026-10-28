<!-- Plik generowany automatycznie. Tools/ModApiDocGen w repozytorium gry buduje go z komentarzy /// w źródłach. Nie edytuj go bezpośrednio; popraw komentarze w źródłach i wygeneruj ponownie. -->
# HorayModAPI

Statyczny punkt wejścia, który gra udostępnia modom. Subskrybuj zdarzenia ładowania baz danych, sesji i spawnów oraz rejestruj prefaby sieciowe i statystyki specjalne. Subskrybuj zdarzenia tylko raz, w `OnModLoaded`. Cykl życia i zasady trybu wieloosobowego opisano w [modach DLL](../dll-mods.md).

Wygenerowano 2026-10-26 dla wersji gry **1.1.0**. [Indeks dokumentacji](README.md)

| Typ | Opis |
|---|---|
| [`HorayModAPI`](#horaymodapi) | Punkt wejścia dla modów. |
| [`HorayModLocalizationContext`](#horaymodlocalizationcontext) | Pomocnik do rejestrowania tekstów lokalizacji moda. |


## HorayModAPI

```csharp
public static class HorayModAPI
```

Punkt wejścia dla modów.

Przykład:

```csharp
HorayModAPI.OnLoadMiracleDatabase += () => {
    MiracleDatabase.Register(myCustomMiraclePrefab);
    MiracleDatabase.Modify("Scholar", m => m.categories = new[] { "Lake" });
};
```

### Stałe i pola

#### WORK_IN_PROGRESS_README

```csharp
public const string WORK_IN_PROGRESS_README = "The MOD System is still under development. Most features are not work properly yet."
```

*Brak opisu.*


#### WORK_IN_PROGRESS_README_URL

```csharp
public const string WORK_IN_PROGRESS_README_URL = "https://teamhoray.com/mod-api"
```

*Brak opisu.*


#### NAME

```csharp
public const string NAME = "HorayMod"
```

*Brak opisu.*

### Zdarzenia

#### OnLoadMiracleDatabase

```csharp
public static event Action OnLoadMiracleDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych cudów. Rejestruj lub modyfikuj cuda przez `MiracleDatabase.Register`/`Modify`.


#### OnLoadWeaponDatabase

```csharp
public static event Action OnLoadWeaponDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych broni. Rejestruj lub modyfikuj bronie przez `WeaponDatabase.Register`/`Modify`.


#### OnLoadStatusDatabase

```csharp
public static event Action OnLoadStatusDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych statusów (efektów statystyk). Rejestruj lub modyfikuj statusy przez `StatusDatabase.Register`/`Modify`.


#### OnLoadSpecialStatDatabase

```csharp
public static event Action OnLoadSpecialStatDatabase
```

Odpala się zaraz po załadowaniu kategorii i statusów statystyk specjalnych. Rejestruj wpisy do wyświetlania przez `RegisterSpecialStatCategory`/`RegisterSpecialStat`.


#### OnLoadItemDatabase

```csharp
public static event Action OnLoadItemDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych przedmiotów. Rejestruj lub modyfikuj przedmioty przez `ItemDatabase.Register`/`Modify`.


#### OnLoadKeywordDatabase

```csharp
public static event Action OnLoadKeywordDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych słów kluczowych. Rejestruj lub modyfikuj słowa kluczowe przez `KeywordDatabase.Register`/`Modify`.


#### OnLoadPassiveDatabase

```csharp
public static event Action OnLoadPassiveDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych pasywów. Rejestruj lub modyfikuj pasywy przez `PassiveDatabase.Register`/`Modify`.


#### OnLoadCostumeDatabase

```csharp
public static event Action OnLoadCostumeDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych strojów. Rejestruj lub modyfikuj stroje przez `CostumeDatabase.Register`/`RegisterSkin`/`Modify`.


#### OnLoadPropDatabase

```csharp
public static event Action OnLoadPropDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych rekwizytów. Rejestruj rekwizyty przez `PropDatabase.Register`.


#### OnLoadUnitDatabase

```csharp
public static event Action OnLoadUnitDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych jednostek (encji spawnowanych wrogów i NPC).


#### OnLoadSocialIDDatabase

```csharp
public static event Action OnLoadSocialIDDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych ról NPC.


#### OnLoadCustomGameDatabase

```csharp
public static event Action OnLoadCustomGameDatabase
```

Odpala się zaraz po zainicjalizowaniu bazy danych gier niestandardowych.


#### OnAllDatabasesReady

```csharp
public static event Action OnAllDatabasesReady
```

Odpala się zaraz po zakończeniu inicjalizacji wszystkich baz danych.


#### OnTitleMenuReady

```csharp
public static event Action<UI_TitleLobby> OnTitleMenuReady
```

Wywoływane po załadowaniu menu tytułowego.


#### OnStartGameServerside

```csharp
public static event Action OnStartGameServerside
```

Wywoływane, gdy gra się rozpoczyna. (strona serwera) Rozpoczęcie oznacza kliknięcie slotu zapisu na ekranie tytułowym, aby wejść do gry. Dane sesji, takie jak gracz i bieżąca lokacja, nie są jeszcze załadowane.


#### OnStartGameClientside

```csharp
public static event Action OnStartGameClientside
```

Wywoływane, gdy gra się rozpoczyna. (strona klienta) Rozpoczęcie oznacza kliknięcie slotu zapisu na ekranie tytułowym, aby wejść do gry. Dane sesji, takie jak gracz i bieżąca lokacja, nie są jeszcze załadowane.


#### OnStartSessionServerside

```csharp
public static event Action<bool> OnStartSessionServerside
```

Wywoływane, gdy rozpoczyna się sesja gry. (strona serwera) Sesja to jednostka rozgrywki. Gdy gracz ginie, ta sesja się kończy i zaczyna się nowa.


#### OnStartSessionClientside

```csharp
public static event Action<bool> OnStartSessionClientside
```

Wywoływane, gdy rozpoczyna się sesja gry. (strona klienta) Sesja to jednostka rozgrywki. Gdy gracz ginie, ta sesja się kończy i zaczyna się nowa.


#### OnLocalizationReady

```csharp
public static event Action<HorayModLocalizationContext> OnLocalizationReady
```

Wywoływane po załadowaniu plików lokalizacji.


#### OnFloorAllocatedServerside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedServerside
```

Wywoływane, gdy piętro zostaje przydzielone. (strona serwera) Każda lokacja w Sephirii jest zdefiniowana jako piętro. Gdy gracz przemieszcza się między lokacjami, kolejno wykonywane są następujące kroki. 1. Przydział piętra: tworzy i ładuje nowe piętro. 2. Przeniesienie: przenosi gracza do żądanego punktu na w pełni załadowanym piętrze. 3. Usunięcie piętra: usuwa lokację, w której gracz był wcześniej, jeśli jest pusta (= nie został w niej żaden gracz).
param1: GUID piętra  
param2: Nazwa piętra  
param3: Instancja FloorGenerator


#### OnFloorAllocatedClientside

```csharp
public static event Action<string, string, FloorGenerator> OnFloorAllocatedClientside
```

Wywoływane, gdy piętro zostaje przydzielone. (strona klienta) Każda lokacja w Sephirii jest zdefiniowana jako piętro. Gdy gracz przemieszcza się między lokacjami, kolejno wykonywane są następujące kroki. 1. Przydział piętra: tworzy i ładuje nowe piętro. 2. Przeniesienie: przenosi gracza do żądanego punktu na w pełni załadowanym piętrze. 3. Usunięcie piętra: usuwa lokację, w której gracz był wcześniej, jeśli jest pusta (= nie został w niej żaden gracz).
param1: GUID piętra  
param2: Nazwa piętra  
param3: Instancja FloorGenerator


#### OnAssignRoomSpawnPhases

```csharp
public static event Action<RoomSpawnAssignContext> OnAssignRoomSpawnPhases
```

Wywoływane po ustaleniu losowego składu wrogów dla etapu. (strona serwera) Odpala się zaraz po tym, jak generator piętra wybierze fazy potworów dla pokoju walki (lub strefy walki), zanim otrzyma je spawner. Zastąp lub edytuj ctx.Phases, aby zmienić, co się tam pojawi. Gra nie wymusza tutaj balansu; zasady znajdziesz w RoomSpawnAssignContext (deterministyczny RNG przez ctx.Seed, nigdy nie dotykaj ctx.Sources, encje muszą być zarejestrowane w UnitDatabase).


#### OnPlayerAvatarSpawnedServerside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedServerside
```

Wywoływane, gdy pojawia się gracz. (strona serwera) W tym hooku możesz rejestrować callbacki związane z graczem. Wywoływane na końcu PlayerAvatar.OnStartServer, po zastosowaniu statystyk bazowych. Strój, piętro i zawartość ekwipunku mogą zostać wypełnione później w tej samej klatce lub w kolejnych klatkach. Używaj go do subskrybowania zdarzeń instancji samego awatara (OnDamagedServerside, OnAttackUnit, OnKillUnit, OnDie, OnAddedBuff, OnEnteredFloorServerside, ...). Subskrypcje na awatarze giną razem z awatarem, więc nie trzeba ich anulować.


#### OnPlayerAvatarSpawnedClientside

```csharp
public static event Action<PlayerAvatar> OnPlayerAvatarSpawnedClientside
```

Wywoływane, gdy pojawia się awatar gracza. (strona klienta) Wywoływane raz na awatar na końcu jego OnStartClient; odpala się także dla zdalnych graczy. Sprawdź avatar.isLocalPlayer / avatar.isOwned, aby odróżnić lokalnego gracza. SyncVars są już zastosowane, ale piętro i zawartość ekwipunku mogą dotrzeć później.


#### OnUnitSpawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitSpawnedServerside
```

Odpala się, gdy na serwerze pojawia się jednostka (gracz, wróg, przywołaniec lub towarzysz). Tylko serwer (host).


#### OnUnitDespawnedServerside

```csharp
public static event Action<UnitAvatar> OnUnitDespawnedServerside
```

Odpala się tuż przed usunięciem jednostki z serwera. Tylko serwer (host).


#### OnPlayerEvade

```csharp
public static event Action<PlayerAvatar, DamageInstance> OnPlayerEvade
```

Odpala się, gdy gracz unika ataku. (strona serwera)


#### OnGetDebuff

```csharp
public static event Func<CharacterDebuff, UnitAvatar, CharacterDebuff> OnGetDebuff
```

Pozwala podmienić prefab debuffa tuż przed nałożeniem go na jednostkę. Handler otrzymuje (bieżący prefab, rzucający) i zwraca prefab zastępczy; zwrócenie null pozostawia go bez zmian.


#### GridInventoryStartPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryStartPermission
```

Odpala się, gdy serwer przejmuje uprawnienie do zapisu ekwipunku i zaczyna usuwać wszystkie efekty przedmiotów.


#### GridInventoryEndPermission

```csharp
public static event Action<GridInventory, PlayerAvatar> GridInventoryEndPermission
```

Odpala się zaraz po tym, jak serwer zwraca uprawnienie do zapisu i ponownie nakłada efekty przedmiotów.

### Metody

#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab)
```

Rejestruje obiekt (prefab), który może być spawnowany przez sieć. Obiekt musi mieć komponent NetworkIdentity.

Serwer (host) i klienci muszą przechowywać listę obiektów, które mogą korzystać z sieci, i ta lista musi być identyczna u wszystkich.

Przykład:

```csharp
HorayModAPI.OnAllDatabasesReady += () => {
    HorayModAPI.RegisterNetworkPrefab(myNewCustomPrefab);
};
```

Zwraca: true, jeśli rejestracja się powiodła


#### RegisterNetworkPrefab

```csharp
public static bool RegisterNetworkPrefab(GameObject prefab, string stableKey)
```

Rejestruje prefab utworzony w czasie działania gry (np. klon wbudowanego szablonu) jako obiekt spawnowany przez sieć, używając stałego assetId obliczonego ze `stableKey`. Jako klucza użyj nazwy jednostki moda (`<modId>:<id>`).

Zwraca: true, jeśli rejestracja się powiodła


#### RegisterNetworkPrefabs

```csharp
public static int RegisterNetworkPrefabs(IEnumerable<GameObject> prefabs)
```

Rejestruje wiele prefabów jako obiekty spawnowane przez sieć za jednym razem.

Zwraca: liczbę prefabów zarejestrowanych pomyślnie


#### UnregisterNetworkPrefab

```csharp
public static bool UnregisterNetworkPrefab(GameObject prefab)
```

Wyrejestrowuje zarejestrowany prefab sieciowy.

Zwraca: true, jeśli został usunięty


#### RegisterSpecialStatCategory

```csharp
public static bool RegisterSpecialStatCategory(SpecialStatCategoryEntity category)
```

Rejestruje kategorię statystyk specjalnych w interfejsie statystyk.

Zwraca: true, jeśli zarejestrowano


#### RegisterSpecialStat

```csharp
public static bool RegisterSpecialStat(StatusEntity status, Func<SpecialStatContext, SpecialStatValue> reader = null)
```

Rejestruje StatusEntity jako wpis w kategorii statystyk specjalnych w interfejsie statystyk. Jeśli przekażesz `reader`, wartość jest odczytywana przez tę funkcję zamiast z ustawień zasobu.

Zwraca: true, jeśli zarejestrowano


#### GetModOptions

```csharp
public static ModOptions GetModOptions(string modId)
```

Pobiera opcje zapisywane na urządzeniu powiązane z modId. Mody z Warsztatu mogą zamiast tego używać `Context.Options`.


#### TryShowItemInteraction

```csharp
public static bool TryShowItemInteraction(ItemInteractionOptions options, out ItemInteractionSession session)
```

Otwiera panel interakcji z przedmiotami używany razem z ekwipunkiem. Zwraca false, jeśli interfejs jest zajęty.


#### TryGetUIWidgets

```csharp
public static bool TryGetUIWidgets(out UIWidgetFactory widgets)
```

Pobiera fabrykę widżetów interfejsu w stylu gry. Dołączaj je pod istniejącym Canvasem UI.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session)
```

Otwiera okno dialogowe w świecie gry. Zwraca false, jeśli interfejs jest zajęty; kolejną stronę pokaż przez `session.Show`.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset)
```

Otwiera okno dialogowe z przesunięciem w przestrzeni świata. Jeśli null, używane jest domyślne przesunięcie prefabu.


#### TryShowDialogue

```csharp
public static bool TryShowDialogue(Transform anchor, DialoguePage page, out DialogueSession session, Vector3? offset = null, Vector2? cameraOffset = null, bool focusCamera = true)
```

Otwiera okno dialogowe z osobnymi przesunięciami dialogu i kamery. Skupienie kamery działa tylko lokalnie.


## HorayModLocalizationContext

```csharp
public class HorayModLocalizationContext
```

Pomocnik do rejestrowania tekstów lokalizacji moda.

### Metody

#### AddText

```csharp
public void AddText(string language, string key, string value)
```

Rejestruje tekst dla określonego języka.


#### AddText

```csharp
public void AddText(string key, Dictionary<string, string> texts)
```

Rejestruje tekst dla wielu języków naraz
texts: { "ko-KR" : "불의 검", "en-US" : "Fire Sword", ... }
