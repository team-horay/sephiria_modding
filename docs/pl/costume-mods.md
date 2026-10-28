# Mody ze strojami

Dodajmy strój, używając wyłącznie plików PNG i JSON, bez żadnego kodu. Tego rodzaju mod może dodać dwie rzeczy:

- **Nowe stroje**: Stwórz zupełnie nowy strój. Możesz ustawić jego wygląd, statystyki, przedmioty startowe i więcej.
- **Wyglądy istniejących strojów**: Dodaj wygląd (skórkę) do istniejącego stroju, np. Różowego Króliczka.

Ta strona opiera się na przykładzie [`examples/costume-sample`](../../examples/costume-sample). Pobierz go, aby zapoznać się ze strukturą moda.

## Struktura moda

```
costume-sample/
├── metadata.json
├── thumbnail.png
├── Costumes/                  ← folder strojów
│   └── WhiteCat/              ← jeden folder = jeden strój
│       ├── costume.json
│       ├── icon.png
│       ├── idle.png
│       ├── move.png
│       ├── idle_back.png
│       ├── move_back.png
│       ├── attack.png
│       └── ...                ← pozostałe pliki PNG z ruchami
└── Localization/              ← nazwa i opis stroju
    ├── ko-KR.json
    ├── en-US.json
    └── ...
```


## metadata.json

Gdy budujesz własnego moda na podstawie przykładu, zmień `modId` i `author` w `metadata.json`. (Szczegóły w [`metadata.json`](metadata.md).)

```json
{
  "info": {
    "title": { "ko-KR": "하얀 고양이 코스튬", "en-US": "White Cat Costume" },
    "description": { "ko-KR": "하얀 고양이 코스튬을 추가합니다.", "en-US": "Adds the White Cat costume." }
  },
  "tags": ["Costume"],
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.costume-whitecat",
    "version": "1.0.0",
    "author": "Your Name",
    "multiplayer": { "installation": "everyone" },
    "modules": [
      { "type": "costume", "path": "Costumes" },
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```


## Animacje sprite'ów

- Strój z kompletem animacji wymaga ich całkiem sporo.
- Jeśli to twój pierwszy strój, spróbuj zrobić prosty, tylko z `idle, move, airborne, fall`.

| Ruch | Opis | Wymagany |
|---|---|---|
| `idle` | Bezczynność | Wymagany |
| `move` | Ruch | Wymagany |
| `airborne` | W powietrzu | Opcjonalny |
| `fall` | Upadek | Opcjonalny |
| `attack` | Atak | Opcjonalny |
| `greatsword_lower` | Obrotowe cięcie | Opcjonalny |
| `greatsword_sweep` | Obrót | Opcjonalny |
| `hitfeedback` | Otrzymanie ciosu | Opcjonalny |

### Animacje tyłem

- Aby strój wyglądał naturalniej, możesz narysować sprite'y pokazujące postać od tyłu. To opcjonalne.
- Jeśli zdecydujesz się dodać sprite'y od tyłu, musisz dołączyć ruchy `idle_back, move_back`.
- Animacje tyłem definiuje się w następujący sposób.

| Ruch | Opis |
|---|---|
| `idle_back` | Bezczynność (tył) |
| `move_back` | Ruch (tył) |
| `attack_back` | Atak (tył) |
| `greatsword_upper` | Obrotowe cięcie (tył) |

## Przygotowanie grafiki

- Dla każdego ruchu przygotuj arkusz sprite'ów w formacie PNG, z klatkami tego ruchu ułożonymi poziomo.
- Wysokość obrazu równa się wysokości klatki, a szerokość to szerokość klatki × liczba klatek. Nie zostawiaj odstępów między klatkami.
- Zalecamy, aby każdy pojedynczy obraz miał ten sam rozmiar.
- W przykładzie Białego Kota, gdzie każdy sprite ma 20×16, rozmiary obrazów są następujące.

| Plik | Rozmiar | Klatki |
|---|---|---|
| `idle.png` | 120×16 | 6 |
| `move.png` | 160×16 | 8 |
| `attack.png` | 60×16 | 3 |
| `fall.png` | 20×16 | 1 |

*Przykładowy obraz*


### Opisanie nowego stroju

Zdefiniuj własny strój, pisząc plik `costume.json`.

Przykładowy `Costumes/WhiteCat/costume.json`:

```json
{
  "schemaVersion": 1,
  "id": "whitecat",
  "name": "Costume_WhiteCat_Name",
  "flavorText": "Costume_WhiteCat_FlavorText",
  "icon": "icon.png",

  "frameSize": [20, 16],
  "pivot": [10, 2],
  "pixelsPerUnit": 16,

  "animations": {
    "idle":             { "file": "idle.png", "fps": 12 },
    "idle_back":        { "file": "idle_back.png", "fps": 12 },
    "move":             { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "move_back":        { "file": "move_back.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":           { "file": "attack.png", "fps": 20, "loop": false },
    "attack_back":      { "file": "attack_back.png", "fps": 20, "loop": false },
    "greatsword_lower": { "file": "greatsword_lower.png", "fps": 20, "loop": false },
    "greatsword_upper": { "file": "greatsword_upper.png", "fps": 20, "loop": false },
    "greatsword_sweep": { "file": "greatsword_sweep.png", "fps": 30, "loop": false },
    "airborne":         { "file": "airborne.png", "fps": 12 },
    "fall":             { "file": "fall.png", "fps": 1 },
    "hitfeedback":      { "file": "hitfeedback.png", "fps": 20, "loop": false }
  },

  "stats": ["DEFENSE/-10", "EVASION/1200"],
  "startingItems": [],
  "onlyDefaultWeapon": false
}
```

| Pole | Wymagane | Opis |
|---|---|---|
| `schemaVersion` | Tak | Identyfikator formatu. Wpisz liczbę całkowitą `1`. |
| `id` | Tak | Używaj małych liter alfabetu łacińskiego, cyfr, podkreśleń i myślników, 1–31 znaków. Aby uniknąć kolizji, w grze rejestrowany jest jako `<modId>:<id>`. |
| `name` | Tak | **Klucz lokalizacji** nazwy stroju. Zobacz [Przygotowanie plików lokalizacji](#przygotowanie-plików-lokalizacji) poniżej. |
| `flavorText` | Nie | **Klucz lokalizacji** opisu stroju. Zobacz [Przygotowanie plików lokalizacji](#przygotowanie-plików-lokalizacji) poniżej. |
| `icon` | Tak | Ścieżka do pliku PNG z ikoną podglądu stroju. |
| `face` | Nie | Ścieżka do pliku PNG z twarzą wyświetlaną w HUD trybu wieloosobowego. |
| `pixelsPerUnit` | Nie | Zmień tę wartość, aby użyć bardziej szczegółowej grafiki. Wyższe wartości renderują grafikę z większą szczegółowością. Domyślne PPU to 16. |
| `frameSize` | Nie | `[szerokość, wysokość]` każdego sprite'a. Domyślnie: `[16, 16]`. |
| `pivot` | Nie | Punkt obrotu każdego sprite'a. Lewy dolny róg to `[0, 0]`. Domyślnie: `[szerokość/2, 0]`. |
| `animations` | Tak | Grafika dla każdego ruchu. Zobacz [animations](#animations). |
| `stats` | Nie | Lista statystyk do ustawienia. Zobacz [listę statystyk](stats.md). |
| `startingItems` | Nie | Tablica ID przedmiotów startowych. Zobacz [listę przedmiotów](item-table.md). |
| `body` | Nie | Ustawia hitbox. Zobacz [body](#body). |
| `skins` | Nie | Dodatkowe wyglądy tego samego stroju. Zobacz [skins](#skins). |

### animations

```json
  "animations": {
    "idle":      { "file": "idle.png", "fps": 12 },
    "move":      { "file": "move.png", "fps": 4, "footstepFrames": [3, 7] },
    "attack":    { "file": "attack.png", "fps": 20, "loop": false },
    "fall":      { "file": "fall.png", "fps": 1 },
    "hitfeedback": { "file": "hitfeedback.png", "fps": 20, "loop": false, "frameSize": [24, 16], "pivot": [12, 2] }
  }
```

- Zdefiniuj przygotowane wyżej animacje za pomocą czterech właściwości: `{ "file", "fps", "loop", "footstepFrames" }`.
- Możesz też ustawić `frameSize` i `pivot` osobno dla każdego ruchu.
- Jeśli tylko jeden ruch jest narysowany w innym rozmiarze, jak w ostatnim wierszu, ustaw `frameSize` i `pivot` dla tego ruchu.

#### Właściwości

| Pole | Wymagane | Opis |
|---|---|---|
| `file` | Tak | Nazwa pliku do odtworzenia. |
| `fps` | Nie | Szybkość odtwarzania animacji. (Domyślnie: 12) |
| `loop` | Nie | Czy animacja ma się zapętlać. (Domyślnie: true; false dla animacji ataku) |
| `footstepFrames` | Nie | Numery klatek liczone od zera. Gdy te klatki są wyświetlane, odtwarzany jest dźwięk kroku. |


### body

```json
"body": { "hitboxRadius": 4, "hitboxOffset": [0, 4], "movementRadius": 0.25, "bodyScale": 1 }
```

- Definiuje obszary kolizji ruchu i trafień postaci.
- Wbudowane stroje używają tych wartości jako domyślnych.
- Jeśli nie masz konkretnego powodu, zalecamy pominięcie tej sekcji.

#### Właściwości

| Pole | Wymagane | Opis |
|---|---|---|
| `hitboxRadius` | Nie | Promień okręgu hitboxa w pikselach, 0.5–64. Domyślnie `4`. |
| `hitboxOffset` | Nie | Środek okręgu hitboxa, `[x, y]`, w pikselach względem punktu obrotu; dodatni x to prawo, a dodatni y to góra, ±64. Domyślnie `[0, 4]`. |
| `movementRadius` | Nie | Promień okręgu kolizji ruchu, który zderza się ze ścianami i przeszkodami, w jednostkach (16 pikseli = 1), 0.05–2. Domyślnie `0.25`. |
| `bodyScale` | Nie | Skala pozycji barku, w którym trzymana jest broń, 0.1–4. Domyślnie `1`. Zwiększ ją, gdy broń zapada się w ciało dużego stroju. |



### skins

```json
"skins": [
  { "id": "winter", "name": "Skin_WhiteCat_Winter_Name", "folder": "skins/Winter", "icon": "icon.png",
    "animations": { "idle": "idle.png", "move": "move.png" } }
]
```

- Dodaje dodatkowe wyglądy do tego samego stroju.
- Animacje mają ten sam format co powyżej.
- Jeśli pominiesz `frameSize` lub `pivot`, używane są wartości samego stroju.




## Przygotowanie plików lokalizacji

`Localization/ko-KR.json`:

```json
{
  "Costume_WhiteCat_Name": "하얀 고양이",
  "Costume_WhiteCat_FlavorText": "줄무늬가 인상적입니다."
}
```

`Localization/en-US.json`:

```json
{
  "Costume_WhiteCat_Name": "White Cat",
  "Costume_WhiteCat_FlavorText": "The stripes are rather impressive."
}
```

- W `name` i `flavorText` zamiast tekstu wpisz **klucz lokalizacji**, a sam tekst umieść w folderze `Localization/` tego samego moda, po jednym pliku na język.
- W kluczach używaj liter alfabetu łacińskiego, cyfr, podkreśleń i myślników, bez spacji. Umieszczenie w kluczu nazwy moda pomaga uniknąć kolizji z innymi modami.
- Jeśli klucza **nie ma w żadnym pliku językowym**, strój się nie załaduje. Musisz podać przynajmniej jeden język.
- Zalecamy dołączenie także angielskiego (`en-US`), aby więcej osób mogło cieszyć się twoim strojem.
- Nie zapomnij też zadeklarować modułu `localization` w `modules` w `metadata.json`.



## Sprawdź efekt

1. Umieść folder moda w `WorkshopLocal/` i uruchom grę.
2. Rozpocznij grę, otwórz wybór stroju w wiosce (**Zmień strój**) i sprawdź, czy twój nowy strój został dodany.
3. Po zmianie zawartości stroju musisz zrestartować grę.



## Dodatek: dodawanie tylko skórki do istniejącego stroju

Możesz stworzyć wygląd dla istniejącego stroju, tworząc `skin.json` zamiast `costume.json`.

```
Costumes/
└── PinkRabbit_Pajama/
    ├── skin.json
    ├── icon.png
    ├── idle.png
    └── move.png
```

```json
{
  "schemaVersion": 1,
  "costume": "PinkRabbit",
  "id": "pajama",
  "name": "Skin_PinkRabbit_Pajama_Name",
  "icon": "icon.png",
  "animations": { "idle": "idle.png", "move": "move.png" }
}
```

- `costume` to id docelowego stroju. Użyj id wbudowanego stroju (np. `PinkRabbit`) albo, w przypadku stroju z innego moda, jego id w czasie działania gry, łącznie z nazwą moda. (Dla przykładu Białego Kota: `yourname.costume-whitecat:whitecat`.)
- `stats`, `startingItems`, `body`, `skins` i `flavorText` są przejmowane bez zmian z istniejącego stroju, więc nie możesz ich ustawić.
- Nadpisywanie domyślnego wyglądu wbudowanego stroju nie jest obsługiwane.
