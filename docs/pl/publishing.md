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
3. Wybierz swojego moda i naciśnij **Prześlij do Warsztatu**.
4. W przypadku modów bez kodu przesłanie do Warsztatu jest ostatnim krokiem.
5. W przypadku modów zawierających kod (pliki DLL) po zakończeniu przesyłania do Warsztatu mod jest automatycznie zgłaszany do **systemu weryfikacji** razem z ID elementu w Warsztacie.
6. Przejdź na stronę przesłanego elementu w Warsztacie, wykonaj ostatni test (np. zasubskrybuj go), a następnie zmień widoczność na „Publiczna”.

Jeśli przesyłanie do Warsztatu się zakończyło, a nie powiodło się tylko zgłoszenie do weryfikacji (na przykład z powodu błędu sieci), ponownie naciśnij **Prześlij do Warsztatu** dla tego samego moda. Jeśli zawartość się nie zmieniła, mod nie jest ponownie przesyłany do Warsztatu; ponawiane jest tylko zgłoszenie. Zgłoszenia należy dokonać z konta Steam, które utworzyło element w Warsztacie.

## Weryfikacja kodu

- System weryfikacji dekompiluje pliki DLL i sprawdza je pod kątem komunikacji sieciowej, dostępu do plików, obfuskacji i złośliwych działań. Weryfikacja łączy analizę automatyczną z kontrolą operatora i zdajemy sobie sprawę, że żaden system weryfikacji nie wychwyci wszystkich zagrożeń. Mimo to staramy się ograniczać rozprzestrzenianie złośliwych modów dzięki temu procesowi weryfikacji. Dziękujemy za współpracę.
- Każdy mod zawierający kod (pliki DLL) **musi również zostać zgłoszony do systemu weryfikacji modów.** W tym celu zawsze przesyłaj go za pomocą przycisku **Prześlij do Warsztatu** w grze.
- Mody przesłane do Warsztatu, ale niezgłoszone do systemu weryfikacji, co do zasady nie mogą być uruchamiane przez graczy.
- Gracze, którzy pobierają moda, sprawdzają jego status weryfikacji na podstawie ID elementu w Warsztacie i zainstalowanych plików DLL.
  - **Zgłoszony / Zatwierdzony**: mod jest uruchamiany. Działa nawet przed zakończeniem weryfikacji.
  - **Niezgłoszony / Odrzucony**: mod nie jest uruchamiany, a przyczyna jest wyświetlana w panelu modów.
- Jeśli podczas weryfikacji zostanie wykryty problem, mod może zostać odrzucony nawet po publikacji. W przypadku odrzuconego moda powód możesz sprawdzić za pomocą opcji **Wyświetl powód odrzucenia** w panelu. Odrzuconych plików DLL nie można ponownie opublikować w niezmienionej postaci; popraw problem i prześlij moda ponownie.
- Nawet bez modułu `dll` mod podlega weryfikacji, jeśli ma `libraries` albo zawiera plik DLL lub EXE ze zmienionym rozszerzeniem.
- Mody przechodzące weryfikację kodu po skompresowaniu folderu mogą mieć maksymalnie 20 MB.

### Przyczyny nieudanej weryfikacji

|Komunikat|Przyczyna|
|---|---|
|W systemie weryfikacji nie ma żadnego zgłoszenia.|Mod został przesłany do Warsztatu, ale nie został zgłoszony do systemu weryfikacji. Zgłoś go ponownie za pomocą przycisku **Prześlij do Warsztatu**.|
|System weryfikacji odrzucił zatwierdzenie tego moda.|Zespół operacyjny z jakiegoś powodu odrzucił moda. Sprawdź powód odrzucenia.|
|Nie można pobrać informacji z serwera weryfikacji.|Subskrybent może nie mieć połączenia z internetem albo wystąpił problem z serwerem weryfikacji.|
|To mod z poprzedniego systemu weryfikacji.|Serwer weryfikacji został zaktualizowany, a dotychczasowe dane weryfikacyjne moda zostały zresetowane. Zgłoś go ponownie.|
|W modzie wykryto nieautoryzowany plik DLL.|Jeśli zestaw plików DLL w modzie zmieni się podczas działania moda, ładowanie tego moda zostaje ograniczone. Innymi słowy, następujące działania mogą naruszać zasady systemu weryfikacji: mod podczas działania pobiera plik DLL z zewnątrz i ładuje zestaw; mod podczas działania tworzy i uruchamia własny plik DLL; mod wypakowuje i wykorzystuje plik DLL spakowany w nim własną metodą kompresji.|

## Widoczność i upublicznianie

- Przy pierwszym przesłaniu widoczność jest ustawiona na `Unlisted` (niepubliczna).
- Przed upublicznieniem zasubskrybuj moda samodzielnie, sprawdź, czy zainstalowana kopia działa poprawnie, a następnie zmień widoczność na „Publiczna”.
- Opis strony, zrzuty ekranu, opisy w innych językach itd. możesz edytować na stronie elementu w Warsztacie Steam.

## Aktualizowanie moda

Moda aktualizujesz w ten sam sposób, w jaki go opublikowano.

- Aktualizacja zmieniająca pliki DLL jest po przesłaniu automatycznie zgłaszana ponownie i jeszcze raz przechodzi weryfikację. Jeśli nie zmieniono żadnych plików DLL, status weryfikacji pozostaje bez zmian.
- Nie zmieniaj `modId`. Mod zostanie potraktowany jako inny mod, co może m.in. zepsuć zależności.
- **Przy każdej aktualizacji zawsze podnoś `version` w `metadata.json`.** Wersje modów są porównywane podczas dołączania do lobby w trybie wieloosobowym, więc jeśli zawartość się zmieniła, a wersja nie, gracze z różną zawartością modów mogą trafić do tej samej gry i napotkać problemy.

## Bilet przesyłania

Po przesłaniu w folderze moda pojawi się plik `uploadticket.json`. Zawiera on ID elementu w Warsztacie i informacje o zgłoszeniu do weryfikacji, więc **nie usuwaj go.** Jeśli zarządzasz folderem moda za pomocą git, najbezpieczniej jest commitować również ten plik. Jeśli zmienisz komputer lub sformatujesz dysk, będziesz potrzebować tego pliku, aby dalej aktualizować ten sam element w Warsztacie. Bez niego gra traktuje folder jako nowego, nigdy niepublikowanego moda.

Pliki `mod-guard-manifest.json` i `mod-guard.sig.json`, używane przez system weryfikacji przed wersją 1.1.1, nie są już używane. Nawet jeśli pozostaną, nie są przesyłane do Warsztatu i można je usunąć.

Jak odzyskać ten plik po jego utracie, przeczytasz w [FAQ](faq.md).


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
