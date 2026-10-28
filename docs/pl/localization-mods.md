# Mody lokalizacyjne

[Twój pierwszy mod](getting-started.md) omówił podstawy tworzenia moda, więc ta strona opisuje ogólne zasady dotyczące modów lokalizacyjnych.
[`examples/translation-sample`](../../examples/translation-sample) zawiera kompletny przykład, który możesz skopiować bezpośrednio do `WorkshopLocal/` i wypróbować.



## Jak to działa

- Gra ma jedną tabelę **klucz → tekst** na każdy język. Gdy mod jest ładowany, wartości z moda nadpisują oryginalną tabelę.
- Twój plik musi zawierać tylko te klucze, które chcesz zmienić.
- Nazwa pliku określa język. `ko-KR.json` nadpisuje tabelę koreańską, a `en-US.json` nadpisuje tabelę angielską.
- Jeśli kilka modów lokalizacyjnych ustawia ten sam klucz, wygrywa mod załadowany później.
- Mod, który zmienia tylko tekst, nie wpływa na innych uczestników, dlatego zalecamy ustawienie `multiplayer.installation` w `metadata.json` na `local_only`.
- Zarówno klucze, jak i wartości są ciągami znaków. Zagnieżdżone obiekty i tablice nie są dozwolone.
- Zapisuj pliki w kodowaniu **UTF-8**.

```
my-translation-mod/
├── metadata.json              ← dodaj { "type": "localization", "path": "Localization" } do modules
├── thumbnail.png
└── Localization/
    └── ko-KR.json
```



## Czego nie tłumaczyć

Teksty w grze zawierają wyrażenia interpretowane w trakcie działania gry.
Nie tłumacz poniższych elementów; zostaw je po angielsku dokładnie w takiej postaci (wielkość liter ma znaczenie).

| Typ    | Przykład                                                                     | Opis                                 |
| ----- | --------------------------------------------------------------------------- | ------------------------------------------- |
| Słowa kluczowe   | `{VALUE}`, `{EFFECT0}`, `{LV}`, `{COUNT}`, `{ITEMNAME}`, `{COUNTER}/{GOAL}` | Zastępowane liczbami lub nazwami w trakcie gry. Nie zmieniaj ani jednego znaku wewnątrz nawiasów klamrowych.   |
| Tagi gry | `<tag=FinalDamage>`, `<tag=WeaponAction_Fury>`, `<tag=TEXT:Qliphoth>`       | Zastępowane podpowiedziami słów kluczowych i ikonami. Nazwy wewnątrz tagów są stałe, łącznie z wielkością liter. |
| Tagi formatowania | `<color=red>…</color>`, `<sprite="Keyword" name=Dice>`, `<indent=10>`       | Formatowanie tekstu. Tagi otwierające i zamykające muszą występować w parach.        |
| Podziały wiersza   | `\n`                                                                        | Podział wiersza wewnątrz ciągu JSON.                        |




## Dodawanie nowego języka

- Możesz też dodać język, którego gra nie obsługuje. Jeśli nazwa pliku jest nowym kodem języka, gra rejestruje go jako nowy język i pokazuje na liście w ustawieniach języka.
- Oryginalne pliki językowe znajdują się w `<folder instalacyjny gry>/Sephiria_Data/StreamingAssets/Localization/`, po jednym na język; korzystaj z nich jako wzorca podczas tłumaczenia.
- Języki obsługiwane przez grę to `ko-KR`, `en-US`, `ja-JP`, `zh-CN`, `zh-TW`, `de-DE`, `fr-FR`, `es-ES`, `it-IT`, `pl-PL`, `pt-BR`, `ru-RU`, `sv-SE`, `th-TH` i `tr-TR`.
- **Przetłumacz każdy klucz.** Klucze, których brakuje w pliku moda, nie są wyświetlane jako puste; zamiast nich używany jest **angielski** (lub koreański, jeśli angielskiego też brakuje). Najprościej zacząć od skopiowania oryginalnego pliku `en-US.json`.
- **Wypełnij klucze ustawień zaczynające się od `#`.** `#LanguageName` to nazwa wyświetlana na liście w ustawieniach języka, a `#FontName` to czcionka, która ma być użyta. Dostosuj klucze interpunkcji, takie jak `#Colon`, `#Bullet` i `#ParenthesisOpen`, do konwencji danego języka.
- W `#FontName` możesz wybrać jedną z czcionek wbudowanych w grę. Gra zawiera czcionki dla następujących systemów pisma:

| System pisma | `#FontName` |
|---|---|
| Łaciński, cyrylica, hangul | `GalCyrillic` |
| Japoński | `JF-Dot-MPlus10` |
| Chiński uproszczony | `Fusion_Pixel_10_Simple` |
| Chiński tradycyjny | `BoutiqueBitmap9x9` |
| Tajski | `NotoSansThai` |



## Rozwiązywanie problemów

| Objaw | Co sprawdzić |
|---|---|
| Mod jest włączony, ale tekst się nie zmienia | Czy nazwa klucza dokładnie, co do znaku, odpowiada oryginałowi. Czy nazwa pliku odpowiada bieżącemu językowi gry (`ko-KR.json` działa tylko przy języku koreańskim). |
| Tylko niektóre klucze się nie zmieniają | Czy inny mod lokalizacyjny nie nadpisuje tego samego klucza później. Spróbuj zmienić kolejność w panelu. |
| `{VALUE}` wyświetla się w tekście dosłownie | Słowo kluczowe jest zapisane inaczej niż w oryginale. Skopiuj je bezpośrednio z oryginalnego tekstu. |
| `<tag=…>` wyświetla się w tekście dosłownie | Pisownia lub wielkość liter w nazwie tagu jest inna. |
| Polskie (lub inne nieangielskie) znaki wyświetlają się jako krzaczki | Plik nie jest w UTF-8. Zapisz go ponownie jako UTF-8. |
| Znaki wyświetlają się jako kwadraty | Tych znaków nie ma w czcionkach wbudowanych w grę. |
| Cały plik jest ignorowany | Ten plik ma błąd składni JSON. `Player.log` zapisuje błąd razem z nazwą pliku. |

Plik logu znajduje się w `%USERPROFILE%\AppData\LocalLow\TEAMHORAY\Sephiria\Player.log`. Wyszukaj „모드 내 언어 파일 발견” (znaleziono plik językowy w modzie), aby zobaczyć, które pliki zostały odczytane i ile kluczy zawierał każdy z nich.
