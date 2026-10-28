# Publikowanie w Warsztacie

Ta strona wyjaśnia, jak przesłać moda do Warsztatu Steam, jak działa system weryfikacji i jak mody są synchronizowane między graczami w trybie wieloosobowym.


## Zanim prześlesz

- Do przesłania moda do Warsztatu wymagany jest obraz podglądu (PNG lub JPG, poniżej 1 MB). Ustaw go przez `info.preview` albo umieść plik `thumbnail.png` w folderze moda. Szczegóły znajdziesz w [opisie pliku metadata.json](metadata.md#preview).
- `info.title` może mieć do 128 znaków, a `info.description` do 7999 znaków. Jeśli podasz je w kilku językach, na stronie Warsztatu używana jest wersja `en-US`.
- Kategorie Warsztatu wpisz w `tags`. Dostępne kategorie znajdziesz w [opisie pliku metadata.json](metadata.md).
- Każdy plik w folderze podlega weryfikacji, więc upewnij się, że folder moda nie zawiera kopii plików DLL gry, plików `.pdb` ani pośrednich wyników kompilacji.

## Kroki publikacji

1. Uruchom Sephirię.
2. Otwórz panel **Warsztat Steam** na ekranie tytułowym i znajdź stworzonego przez siebie moda.
3. Wybierz swojego moda.
4. Mody zawierające kod (pliki DLL) przechodzą weryfikację kodu.
5. Mody bez kodu można przesłać do Warsztatu od razu.
6. Przejdź na stronę przesłanego elementu w Warsztacie, wykonaj ostatni test (np. zasubskrybuj go), a następnie zmień widoczność na „Publiczna”.

## Weryfikacja kodu

- System weryfikacji sprawdza każdy plik w folderze moda. Pliki DLL są dekompilowane i sprawdzane pod kątem komunikacji sieciowej, dostępu do plików, obfuskacji i złośliwych działań; pliki danych, takie jak obrazy, dźwięki i JSON, są sprawdzane pod kątem poprawności formatu.
- Jeśli nie zostaną wykryte żadne problemy, mod jest zatwierdzany automatycznie; jeśli coś wygląda podejrzanie, operator sprawdza go ręcznie. Wyraźnie złośliwe mody są odrzucane automatycznie.
- Weryfikacja łączy analizę automatyczną z kontrolą operatora i zdajemy sobie sprawę, że żaden system weryfikacji nie wychwyci wszystkich zagrożeń. Mimo to staramy się ograniczać rozprzestrzenianie złośliwych modów dzięki temu wstępnemu procesowi weryfikacji i zatwierdzania. Dziękujemy za współpracę.
- O tym, czy mod zawiera kod, decyduje **zawartość** plików, a nie ich nazwy. Nawet bez modułu `dll` mod podlega weryfikacji, jeśli ma `libraries` albo zawiera plik DLL lub EXE ze zmienionym rozszerzeniem.
- Mody przechodzące weryfikację kodu po skompresowaniu folderu mogą mieć maksymalnie 20 MB.

## Certyfikat zatwierdzenia (tylko mody z kodem)

- Mody zawierające kod otrzymują certyfikat zatwierdzenia.
- Poświadcza on, że pliki moda są identyczne z zatwierdzoną wersją.
- Mody DLL muszą przejść tę certyfikację, zanim gracze, którzy pobiorą moda, będą mogli uruchomić jego kod.
- Jeśli po certyfikacji zmieni się choć jeden plik, certyfikat traci ważność i mod musi zostać zweryfikowany ponownie.

## Widoczność i upublicznianie

- Przy pierwszym przesłaniu widoczność jest ustawiona na `Unlisted` (niepubliczna).
- Przed upublicznieniem zasubskrybuj moda samodzielnie, sprawdź, czy zainstalowana kopia działa poprawnie, a następnie zmień widoczność na „Publiczna”.
- Opis strony, zrzuty ekranu, opisy w innych językach itd. możesz edytować na stronie elementu w Warsztacie Steam.

## Aktualizowanie moda

Moda aktualizujesz w ten sam sposób, w jaki go opublikowano.

- Mody DLL potrzebują nowego certyfikatu zatwierdzenia.
- Nie zmieniaj `modId`. Mod zostanie potraktowany jako inny mod, co może m.in. zepsuć zależności.
- **Przy każdej aktualizacji zawsze podnoś `version` w `metadata.json`.** Wersje modów są porównywane podczas dołączania do lobby w trybie wieloosobowym, więc jeśli zawartość się zmieniła, a wersja nie, gracze z różną zawartością modów mogą trafić do tej samej gry i napotkać problemy.

## Bilet przesyłania

Po zgłoszeniu do weryfikacji i przesłaniu w folderze moda pojawią się poniższe pliki. Zawierają one informacje o zatwierdzeniu weryfikacji, więc **nie usuwaj ich.** Jeśli zarządzasz folderem moda za pomocą git, najbezpieczniej jest commitować również te trzy pliki. Jeśli zmienisz komputer lub sformatujesz dysk, będziesz potrzebować tych plików, aby dalej aktualizować ten sam element w Warsztacie.

| Plik | Zawartość |
|---|---|
| `uploadticket.json` | Bilet weryfikacji i ID elementu w Warsztacie. Bez niego gra traktuje folder jako nowego, nigdy niepublikowanego moda. |
| `mod-guard-manifest.json`, `mod-guard.sig.json` | Certyfikat zatwierdzenia weryfikacji: lista zatwierdzonych plików i podpis. Ich edycja unieważnia zatwierdzenie. |

Jak odzyskać te pliki po ich utracie, przeczytasz w [FAQ](faq.md).

## Cofnięcie zatwierdzenia

Jeśli po publikacji zostanie wykryte naruszenie zasad, operator może cofnąć zatwierdzenie. Mod z cofniętym zatwierdzeniem nie jest ładowany nawet po zainstalowaniu, a w panelu wyświetla się jako „Zatwierdzenie weryfikacji cofnięte”.


## Zasady trybu wieloosobowego

### Konfiguracja lobby trybu wieloosobowego

Gdy host tworzy lobby trybu wieloosobowego, w lobby rejestrowana jest **lista modów włączonych przez hosta**.

- Mody z `installation` ustawionym na `host_only`: uczestnicy nie muszą ich instalować; są tylko wyświetlane na liście.
- Mody z `multiplayer.installation` ustawionym na `everyone` (lub niezadeklarowanym): uczestnicy również muszą je zainstalować.
- Mody ze strojami i mody danych: uczestnicy również muszą je zainstalować.

### Dołączanie

Jeśli bieżąca konfiguracja modów gracza różni się od konfiguracji lobby, gracz przechodzi przez następujące kroki: pobranie → włączenie → restart gry i ponowna próba dołączenia.

### Mody testowe zainstalowane w WorkshopLocal

Tryb wieloosobowy nie jest dostępny w grze, w której załadowano lokalne mody z `WorkshopLocal/`. Aby przetestować tryb wieloosobowy, prześlij moda do Warsztatu, opróżnij folder lokalny i korzystaj z zasubskrybowanej kopii. Możesz zasubskrybować element przez link nawet wtedy, gdy ma status `Unlisted`.
