[한국어](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | **Polski** | [Português (Brasil)](README.pt-BR.md) | [Русский](README.ru.md) | [Svenska](README.sv.md) | [ไทย](README.th.md) | [Türkçe](README.tr.md)

# Przewodnik po modowaniu Sephirii

Dziękujemy, że dołączasz do społeczności modderów Sephirii.

To oficjalne repozytorium z przewodnikami, przykładami i szablonami do modowania gry [Sephiria](https://store.steampowered.com/app/2436940). Mody można udostępniać przez [Warsztat Steam](https://steamcommunity.com/app/2436940/workshop/).



## Zasady

Aby gra pozostała bezpieczna dla wszystkich, przestrzegaj poniższych zasad. Ich naruszenie może skutkować ograniczeniem możliwości publikowania modów lub grania.

1. **Zakaz złośliwego działania**: Złośliwe oprogramowanie, backdoory, keyloggery, koparki kryptowalut i wszelkie inne funkcje zagrażające urządzeniu, kontu lub danym użytkownika są zabronione.
2. **Użytek niekomercyjny**: Wszystkie mody muszą być tworzone i rozpowszechniane w celach niekomercyjnych.
3. **Zakaz naruszania praw i szkodliwych treści**: Zabronione są treści naruszające cudze prawa autorskie, prywatność lub prawo do wizerunku, a także treści seksualne oraz promujące nienawiść, nękanie lub przemoc w świecie rzeczywistym.
4. **Zakaz zakłócania rozgrywki**: Nie powoduj celowo błędnego działania gry ani nie przeszkadzaj innym graczom w normalnej rozgrywce.
5. **Zakaz zbierania danych**: Nie zbieraj informacji o użytkownikach ani nie wysyłaj ich do zewnętrznych odbiorców.
6. **Zakaz obchodzenia zabezpieczeń dostępu**: Nie obchodź w nieuprawniony sposób weryfikacji zakupu, własności ani dostępu do gry lub platformy dystrybucji.
7. **Weryfikacja modów**: Mody zawierające kod (pliki DLL) przechodzą przed zatwierdzeniem weryfikację, obejmującą analizę i dekompilację, pod kątem naruszeń zasad. Zatwierdzenie może zająć trochę czasu. Nie fałszuj wyników weryfikacji ani oznaczeń zatwierdzenia. Pliki muszą być dostarczone w stanie umożliwiającym weryfikację; obfuskacja jest zabroniona.
8. **Korzystanie z zasobów gry**: Dekompilacja i analiza kodu, grafiki, dźwięków i innych zasobów gry jest dozwolona w zakresie niezbędnym do tworzenia modów. Nie rozpowszechniaj jednak samych wyodrębnionych zasobów gry.
9. **Zakaz podszywania się i ingerowania w cudze mody**: Nie podszywaj się pod innych ani nie modyfikuj i nie rozpowszechniaj cudzych modów bez zgody ich autorów.



## Zanim zaczniesz

- Mody zawierające kod (pliki DLL) są **weryfikowane**, zanim będzie można je przesłać do Warsztatu. Weryfikacja sprawdza przede wszystkim, czy kod nie zawiera złośliwych działań. Mody bez kodu, takie jak tłumaczenia i stroje, można przesyłać od razu, bez weryfikacji. Pamiętaj, że mody, których system weryfikacji nie jest w stanie odczytać, zostaną odrzucone (pliki poddane obfuskacji, DLL skompilowane z C/C++ itp.).
- W trybie wieloosobowym wszyscy uczestnicy w pokoju muszą mieć włączony ten sam zestaw modów. (Nie dotyczy to osobistych modów interfejsu, takich jak tłumaczenia.)
- Po włączeniu modów musisz zrestartować grę, aby zmienić ich kolejność lub ponownie je wyłączyć.
- Subskrybowane mody są domyślnie wyłączone. Możesz je samodzielnie włączyć na liście.



## Dokumentacja

| Dokument | Zawartość |
|---|---|
| [Twój pierwszy mod](docs/pl/getting-started.md) | Przygotowanie folderu roboczego, sprawdzanie moda lokalnie, czytanie logów |
| [Opis pliku metadata.json](docs/pl/metadata.md) | Znaczenie każdego pola, zasady dotyczące zależności i wersji, deklarowanie modułów |
| [Mody lokalizacyjne](docs/pl/localization-mods.md) | Struktura plików językowych, wyszukiwanie kluczy, zasady zachowania tagów i symboli zastępczych |
| [Mody ze strojami](docs/pl/costume-mods.md) | Tworzenie własnego stroju |
| [Mody DLL](docs/pl/dll-mods.md) | Konfiguracja projektu, punkt wejścia, zdarzenia `HorayModAPI`, debugowanie |
| [Publikowanie w Warsztacie](docs/pl/publishing.md) | Przesyłanie do Warsztatu, weryfikacja modów z kodem, zasady trybu wieloosobowego |
| [Dokumentacja API](docs/pl/api/README.md) | Publiczne typy i składowe, takie jak `HorayModAPI`, `WorkshopModBase`, opcje modów i interfejs. Generowana automatycznie ze źródeł gry |
| [Dodatek i FAQ](docs/pl/faq.md) |  |


## Licencja

Kod w tym repozytorium (przykłady i szablony) jest objęty [licencją MIT](LICENSE).
Dokumentację można swobodnie cytować, tłumaczyć i rozpowszechniać; prosimy o podanie źródła.
Sama gra Sephiria i jej zasoby są dziełem TEAM HORAY i nie są objęte tą licencją.
