# Twój pierwszy mod

Ten przewodnik wyjaśnia podstawową strukturę moda do Sephirii i krok po kroku prowadzi cię przez tworzenie pierwszego moda oraz sprawdzenie go w grze.

## Czego potrzebujesz

- Porządny edytor tekstu (na przykład [Visual Studio Code](https://code.visualstudio.com/))

## Znajdź folder instalacyjny gry

Otwórz folder, w którym zainstalowana jest Sephiria.

- Z biblioteki Steam
  - `Biblioteka Steam → kliknij prawym przyciskiem Sephiria → Zarządzaj → Przeglądaj pliki lokalne`
- Windows (domyślna ścieżka instalacji)
  - `C:\Program Files (x86)\Steam\steamapps\common\Sephiria`
- macOS (domyślna ścieżka instalacji)
  - `~/Library/Application Support/Steam/steamapps/common/Sephiria` (folder zawierający `Sephiria.app`)

## Utwórz folder WorkshopLocal i folder swojego moda

1. W folderze instalacyjnym gry utwórz folder `WorkshopLocal/`.
2. W jego wnętrzu utwórz folder dla swojego moda.
   - Sephiria traktuje każdy folder jako jeden mod.
   - Mody utworzone tutaj można testować w grze i przesyłać do Warsztatu Steam.

W tym przewodniku zbudujesz `my-first-mod`, czyli mod, który zmienia tekst przycisku „Rozpocznij grę” na ekranie tytułowym na „Hello World!”. Gotowa struktura wygląda tak:

```
<folder instalacyjny gry>/
├── Sephiria.exe
└── WorkshopLocal/
    ├── my-first-mod/          ← jeden mod
    │   ├── metadata.json      ← podstawowy plik definiujący moda
    │   ├── thumbnail.png      ← miniatura podglądu moda (wymagana do przesłania do Warsztatu)
    │   └── Localization/      ← folder odczytywany przez moduł lokalizacji
    │       └── en-US.json     ← tekst do podmiany
    └── another-mod/           ← inny mod
        └── ...
```

## Plik metadata.json

- Utwórz plik `metadata.json` w folderze moda i wklej do niego poniższą zawartość.
- `metadata.json`: kluczowy plik definiujący twojego moda.
- `yourname`: zastąp to swoim pseudonimem.
- Zapisz plik w kodowaniu **UTF-8**. W Notatniku wybierz UTF-8 jako kodowanie w oknie „Zapisz jako”.
- Znaczenie poszczególnych pól znajdziesz w [opisie pliku metadata.json](metadata.md).

```json
{
  "info": {
    "title": "My First Mod",
    "description": "Hello World!"
  },
  "mod": {
    "schemaVersion": 1,
    "modId": "yourname.my-first-mod",
    "version": "0.0.1",
    "author": "Your Name",
    "multiplayer": { "installation": "local_only" },
    "modules": [
      { "type": "localization", "path": "Localization" }
    ]
  }
}
```

## Utwórz plik językowy

- W folderze moda utwórz folder `Localization/`.
- W folderze `Localization/` utwórz plik `en-US.json` z poniższą zawartością.
- Do testów możesz też zmienić nazwę pliku na kod swojego języka.

```json
{
  "Title_GameStart": "Hello World!"
}
```

## Sprawdź efekt

Teraz możesz sprawdzić, czy mod działa.

1. Uruchom Sephirię.
2. Przy języku gry ustawionym na angielski spójrz na ekran tytułowy: przycisk „Start Game” wyświetla teraz „Hello World!”. Mody z folderu `WorkshopLocal/` są domyślnie włączone, więc nie musisz ich włączać.
3. Naciśnij przycisk **Warsztat Steam** na ekranie tytułowym, aby otworzyć panel Warsztatu. „My First Mod” powinien pojawić się w sekcji „Moje mody”.

### Mój mod nie działa!

- Sprawdź, czy składnia JSON jest poprawna.
- Wartość `path` w `metadata.json` musi dokładnie odpowiadać rzeczywistej nazwie folderu, łącznie z wielkością liter.
- Sprawdź, czy `metadata.json` jest zapisany w kodowaniu UTF-8.
- Sprawdź, czy język gry odpowiada językowi z nazwy pliku.

## Następne kroki

1. Dowiedz się, jak definiuje się mody, z [opisu pliku metadata.json](metadata.md).
2. [Mody lokalizacyjne](localization-mods.md) i [mody ze strojami](costume-mods.md) są stosunkowo proste do zrobienia, więc zacznij od nich.
3. [Mody DLL](dll-mods.md) polecamy, jeśli masz podstawową wiedzę o programowaniu.
4. Opublikuj swojego moda w Warsztacie według przewodnika [Publikowanie w Warsztacie](publishing.md).
