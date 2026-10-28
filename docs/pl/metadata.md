# metadata.json

Plik `metadata.json` w katalogu głównym folderu moda to podstawowy plik, który opisuje zarówno **czym** jest twój mod, jak i **jak** działa.


## Ogólna struktura

```json
{
  "info": {
    "title": "New Artifact Pack",
    "description": "Adds 12 artifacts.",
    "preview": "thumbnail.png"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.artifact-pack",
    "version": "0.9.1",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
      { "type": "localization", "path": "Localization" }
    ],
    "libraries": [
      "bin/Helper.dll"
    ],
    "dependencies": [
      { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
    ]
  },
  "tags": ["Item"]
}
```

Najwyższy poziom definiuje trzy rzeczy.

| Klucz | Rola | Wymagany |
|---|---|---|
| `info` | Tytuł, opis i podgląd wyświetlane w Warsztacie Steam i w panelu w grze | Wymagany |
| `tags` | Tagi Warsztatu | Opcjonalny |
| `mod` | Sposób działania moda | Wymagany |

## info

### title

- Nazwa moda. Jeśli jest pusta, zamiast niej używany jest `modId`.
- Możesz użyć mapy języków, aby zdefiniować nazwę moda osobno dla każdego języka.
	- Oprócz własnego języka zalecamy dodanie angielskiego.
- Obsługiwane kody języków: `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH`, `tr-TR`

```json
"info": {
  "title": { "en-US": "New Artifact Pack", "ko-KR": "새 아티팩트 팩" }
}
```

### description

- Opis moda.
- Podobnie jak `title`, opis możesz zdefiniować osobno dla każdego języka.

```json
"info": {
  "description": { "en-US": "Adds 12 artifacts.", "ko-KR": "아티팩트 12종을 추가합니다." }
}
```

### preview

- Do przesłania moda do Warsztatu wymagany jest obraz podglądu. (PNG lub JPG, poniżej 1 MB)
- Podaj względną ścieżkę do obrazu podglądu.
- Jeśli pominiesz to pole, gra szuka w katalogu głównym kolejno plików `thumbnail.png`, `preview.png`, `thumbnail.jpg` i `preview.jpg`.


## mod

### schemaVersion

- Obecna wersja to 1.
- Pamiętaj, że ta liczba może w przyszłości wzrosnąć, jeśli sposób działania modów ulegnie poważnym zmianom.

### modId

- Unikalny ciąg znaków identyfikujący twojego moda. Używaj małych liter alfabetu łacińskiego, cyfr, kropek, myślników i podkreśleń. Maksymalnie 64 znaki.
- **Wybierz unikalną nazwę.** Jeśli twój `modId` pokryje się z identyfikatorem innego moda, mod nie będzie działać poprawnie. Dodanie na początku nazwy twórcy w dużym stopniu pomaga uniknąć kolizji.
(np. `horay.artifact-pack`)
- **Nie zmieniaj `modId` po opublikowaniu.** Inni mogą tworzyć mody potomne oparte na twoim za pomocą `dependencies`, a mod nadrzędny identyfikują właśnie po jego `modId`.


### version

- Ciąg znaków definiujący wersję twojego moda.
- To właśnie ta wartość jest porównywana, gdy inny mod wymaga dokładnie określonej wersji przez `dependencies.version`.
- **Podnoś wersję przy każdej aktualizacji moda.** Podczas dołączania do lobby w trybie wieloosobowym gra sprawdza, czy host i uczestnicy mają te same wersje modów. Jeśli zmienisz zawartość, ale zostawisz tę samą wersję, gracze z różną zawartością modów mogą trafić do tego samego lobby, co może spowodować błąd synchronizacji zawartości lub zablokować postęp gry.
- Zalecamy ten sam format 1.0.0, którego używa gra.

### author

Daj wszystkim znać, kim jesteś!

### multiplayer / installation

Określa, czy gracze w trybie wieloosobowym muszą współdzielić tego moda. Dozwolone wartości:

| Wartość | Opis |
|---|---|
| `local_only` | Wpływa tylko na twój własny ekran i interfejs (należą tu mody lokalizacyjne i proste mody interfejsu) |
| `everyone` | Wszyscy w pokoju muszą mieć włączony ten sam mod (należy tu każdy mod, który dodaje lub zmienia zawartość) |
| `host_only` | Wystarczy, że włączy go host; u pozostałych uczestników działa bez zmian (należą tu mody do trybu wieloosobowego, które wpływają tylko na obliczenia po stronie serwera) |

Jeśli zadeklarujesz `installation` jako `local_only` lub `host_only`, ale twój mod ingeruje w zawartość, której potrzebują także uczestnicy (rejestrowanie nowych przedmiotów lub jednostek, obiekty sieciowe, kod uruchamiany również na klientach uczestników), gracze w tym samym lobby mogą w praktyce grać w różne gry, albo synchronizacja zawartości zawiedzie i zablokuje normalny postęp. Koniecznie sprawdź, co twój mod faktycznie robi.


### modules

- Moduły definiują, co robi twój mod, dlatego to **najważniejsza sekcja**.
- Obecne moduły to `localization, dll, costume, data`.
- Jak w poniższym przykładzie, w jednym pakiecie możesz łączyć wiele modułów.
- Jeśli choć jeden moduł jest nieznany, cały pakiet zostaje oznaczony jako „nieobsługiwany”, a mod jest pomijany podczas ładowania.

```json
"modules": [
  { "type": "localization", "path": "Localization" },
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" },
  { "type": "costume", "path": "Costumes" },
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

#### type: moduł localization

```json
"modules": [
  { "type": "localization", "path": "Localization" }
]
```

- Moduł `localization` jest przeznaczony dla modów zawierających pliki językowe.
- W `path` podaj folder zawierający pliki językowe.
- W folderze `path` umieść **pliki JSON nazwane kodami języków**, np. `ko-KR.json` i `en-US.json`. Kody języków i format plików używany przez grę znajdziesz w [modach lokalizacyjnych](localization-mods.md).

#### type: moduł dll

```json
"modules": [
  { "type": "dll", "path": "bin/ArtifactPack.dll", "entryClass": "ArtifactPack.Entry" }
]
```

- Moduł `dll` pozwala podpiąć się do gry kodem w C#.
- W `path` podaj ścieżkę do pliku DLL.
- W `entryClass` podaj pełną nazwę klasy punktu wejścia w DLL, łącznie z przestrzenią nazw. Gra nie przeszukuje DLL w poszukiwaniu punktu wejścia, więc **musisz go podać**. (W przypadku klas zagnieżdżonych użyj `Ns.Outer+Inner`.)
- Nie używaj tego samego `entryClass` w dwóch różnych modułach.
- Działa tylko w kompilacjach Mono.
- Szczegóły znajdziesz w [modach DLL](dll-mods.md).

#### type: moduł costume

```json
"modules": [
  { "type": "costume", "path": "Costumes" }
]
```

- Pozwala tworzyć własne stroje bez pisania kodu w C#.
- W `path` podaj folder zawierający pliki stroju.
- W trybie wieloosobowym wszyscy muszą mieć go zainstalowanego.
- Szczegóły znajdziesz w [modach ze strojami](costume-mods.md).


#### type: moduł data

```json
"modules": [
  { "type": "data", "path": "Packs", "consumer": "someone.shared-runtime" }
]
```

- Ogólny moduł danych dla modów. W typowych przypadkach nie jest używany; służy głównie do obsługi zależności między modami.
- `consumer` określa, który mod będzie korzystał z tego modułu danych, ale zwykle się go pomija. Jeśli zostanie pominięty, dane może odczytać każdy mod wymieniony w `dependencies`.
- Jeśli masz wiele modów nadrzędnych, a dane są przeznaczone tylko dla jednego z nich, wpisz `modId` tego moda w `consumer`. Ta wartość musi się też znajdować w `dependencies`.

### libraries

```json
"libraries": [
  "bin/Helper.dll",
  { "path": "bin/Newtonsoft.Json.dll" }
]
```

- Lista pomocniczych zestawów (assemblies), do których odwołuje się twój moduł `dll`.
- Gra rozwiązuje odwołania **wyłącznie na podstawie plików zadeklarowanych tutaj**. Samo umieszczenie DLL w folderze nie wystarczy, aby został znaleziony.
- Wpisy mogą być ciągiem znaków albo obiektem `{ "path": "..." }`.
- Każdy wpis musi kończyć się na `.dll` i nie będzie działać poprawnie, jeśli pokrywa się z plikiem DLL z `modules`.
- Biblioteki nie są traktowane jako punkty wejścia. Nawet jeśli któraś zawiera klasę `WorkshopModBase`, nie zostanie ona wywołana.

### dependencies

```json
"dependencies": [
  { "modId": "someone.shared-runtime", "version": "2.5.3", "workshopId": "123456789" }
]
```

- Jeśli mod nadrzędny jest modem „tworzącym zawartość”, mod potomny może dodawać nową zawartość zbudowaną na jego bazie.
- W takim przypadku możesz zadeklarować zależność od innego moda za pomocą `dependencies`.
- Gdy wskazany jest mod nadrzędny, twój mod nie może zostać załadowany przed nim.
- Jeśli moda nadrzędnego brakuje, jest wyłączony lub nie uda się go załadować, ładowanie wszystkich jego modów potomnych zostaje anulowane.
- Zależność może mieć następujące trzy pola.

| Pole | Wymagane | Zasada |
|---|---|---|
| `modId` | Tak | `modId` moda, na którym bazujesz |
| `version` | Nie | **Używaj tylko wtedy, gdy potrzebujesz dokładnie pasującego ciągu wersji.** Jeśli wersja się różni, ładowanie jest pomijane, więc zwykle bezpieczniej jest to pole pominąć. |
| `workshopId` | Nie | Pozwala użytkownikom znaleźć i zasubskrybować wymaganego moda. |


## tags

- Kategorie używane podczas przesyłania do Warsztatu.
- Mod może mieć wiele tagów. (Dla moda „wszystko w jednym”, który dodaje przedmioty i stroje, użyj `"tags": ["Item", "Costume"]`.)

| Kategoria | Zawartość |
|---|---|
| `Item` | Przedmioty (artefakty, tabliczki, mikstury itp.), kombinacje |
| `Costume` | Stroje |
| `Weapon` | Bronie |
| `NPC` | Wędrowcy, kupcy itp. |
| `Enemy` | Nowi wrogowie, bossowie itp. |
| `Gameplay` | Zmiany w rozgrywce |
| `QoL` | Ulepszenia interfejsu, wygody i dostępności |
| `Library` | Środowiska uruchomieniowe, frameworki, paczki danych itp., od których zależą inne mody |
| `Localization` | Tłumaczenia |

## Zasady dotyczące ścieżek
 
- Ścieżki w `modules[].path`, `libraries` i `info.preview` podlegają tym samym zasadom.
- Dozwolone są tylko ścieżki względne wobec folderu moda. (Ścieżki bezwzględne, litery dysków i `..` są odrzucane.)
- Jako separatora używaj `/`.
- Ścieżki prowadzące przez dowiązania symboliczne lub junctions są odrzucane.
- **Wielkość liter musi dokładnie odpowiadać rzeczywistej nazwie.** W systemie Windows zapis `localization` nadal otworzy folder, ale gra odrzuci go jako różny od `Localization`. (Ta zasada zapewnia takie samo działanie na Steam Decku (Linux).)
