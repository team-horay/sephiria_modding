# FAQ


## Jak przeglądać logi?

Logi gry są zapisywane w poniższym pliku. Jest on nadpisywany przy każdym uruchomieniu gry, a log z poprzedniej sesji jest zachowywany w `Player-prev.log`.

```
%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log
```

Wiersze związane z modami zaczynają się od tagu w nawiasach kwadratowych, więc wyszukaj ten tag.

| Tag | Zawartość |
|---|---|
| `[WorkshopModManager]` | Pobieranie listy subskrypcji, pobieranie plików, skanowanie folderu lokalnego, podsumowanie planu ładowania |
| `[WorkshopModRuntime]` | Aktywacja DLL i wyniki scalania lokalizacji |
| `[WorkshopUploader]` | Zgłoszenie do weryfikacji i przesyłanie do Warsztatu |
| `[ModGuardClient]` | Komunikacja z serwerem weryfikacji |
| `[WorkshopLobbyMods]` | Porównanie zestawów modów w pokojach trybu wieloosobowego |
| `모드 내 언어 파일 발견` | Liczba kluczy odczytanych z każdego pliku lokalizacji (komunikat po koreańsku, „znaleziono plik językowy w modzie”) |


## Co to jest „Lokalny mod (folder AddOns)” w panelu Warsztatu Steam?

W panelu może pojawić się wpis „Lokalny mod (folder AddOns)”. To przestarzała funkcja modowania, więc nie używaj jej do nowych modów.
Przestarzałych modów nie da się synchronizować z innymi graczami, dlatego zalecamy ich usunięcie.


## Chcę przenieść folder WorkshopLocal poza folder instalacyjny Sephirii.

Jeśli chcesz trzymać mody na innym dysku lub w folderze roboczym, możesz przekazać ścieżkę jako argument uruchamiania. W bibliotece Steam przejdź do Sephiria → Właściwości → Ogólne → Opcje uruchamiania i wpisz:

```
-workshop_local=D:\mods\my-first-mod
```

Kilka ścieżek rozdziel średnikami, a jeśli ścieżka zawiera spacje, ujmij całość w cudzysłów.

```
"-workshop_local=D:\My Mods\first;D:\My Mods\second"
```

Foldery przekazane jako argumenty są traktowane dokładnie tak samo jak podfoldery `WorkshopLocal/`.


## Zgubiły mi się pliki uploadticket.json, mod-guard-manifest.json i mod-guard.sig.json.

Jeśli brakuje tylko plików certyfikatu (`mod-guard-manifest.json`, `mod-guard.sig.json`), a `uploadticket.json` nadal istnieje, wybierz moda w panelu i naciśnij przycisk **Pobierz dowód zatwierdzenia**, aby ponownie pobrać je z serwera weryfikacji. Jeśli jednak brakuje również `uploadticket.json` (na przykład po formatowaniu dysku), gra traktuje folder jako nowego moda, więc przesłanie go w tej postaci utworzy nowy element w Warsztacie zamiast zaktualizować istniejący. Aby dalej aktualizować istniejący element, odzyskaj go w tej kolejności:

1. Zasubskrybuj własnego moda w Warsztacie.
2. Skopiuj cały folder pobrany przez Steam (`<biblioteka Steam>/steamapps/workshop/content/2436940/<ID elementu Warsztatu>/`) do `WorkshopLocal/`. Nazwa folderu może być dowolna.
3. W skopiowanym folderze utwórz plik `uploadticket.json` z poniższą zawartością. Jako liczbę wpisz cyfry występujące po `id=` w adresie URL strony Warsztatu.
```json
{
  "modId": "yourname.first-mod",
  "publishedFileId": 123456789
}
```
4. Po uruchomieniu gry mod wyświetli się w panelu jako „Nie przesłano”. Przejdź ponownie przez: zgłoszenie do weryfikacji → zatwierdzenie → pobranie dowodu zatwierdzenia → przesłanie do Warsztatu, a **istniejący element zostanie zaktualizowany**. (Uwaga: jeśli folder istniejącego moda został zduplikowany, aby stworzyć innego moda, **musisz usunąć** `uploadticket.json` i pliki certyfikatu. Jeśli je zostawisz, nowy mod nadpisze element oryginalnego moda w Warsztacie.)
