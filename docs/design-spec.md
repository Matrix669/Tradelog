# Tradelog: specyfikacja designu v2

Wersja 2, 19.11.2026. Zastępuje ciemny, „terminalowy” system z października.

## Kierunek

Tradelog to osobisty dziennik tradera, nie terminal giełdowy. Ma być jasny, miękki i spokojny, z dużą ilością powietrza, jak dobra aplikacja do journalingu. Dane z giełdy są tłem. Na pierwszym planie są Twoje decyzje, notatki i samopoczucie.

Inspiracja: jasna aplikacja do dziennika z białymi kartami, delikatnymi ramkami, powitaniem, heatmapą nastroju i osią ostatnich wpisów. Różnice: kolor marki to śliwka, a zieleń i czerwień są zarezerwowane wyłącznie dla zysku i straty.

### Zasady

1. **Ty na pierwszym planie.** Każdy ekran zaczyna się od Ciebie (powitanie, nastrój, notatki), a dopiero potem pokazuje liczby.
2. **Spokój zamiast alarmu.** Zysk i strata mają przygaszone, czytelne kolory i miękkie tła. Nic nie miga i nic nie krzyczy na czerwono.
3. **Jeden wyróżniony element na ekran.** Na Przeglądzie jest to karta wniosku w kolorze marki. Reszta jest biała i cicha.
4. **Ramki, nie cienie.** Karty oddziela linia 1 px. Cień mają tylko elementy, które unoszą się nad stroną: menu, popovery, okna.
5. **Liczby są częścią tekstu.** Zostaje ta sama czcionka co w interfejsie, tylko z cyframi tabelarycznymi. Nie używamy monospace.

## Kolory

### Jasny (domyślny)

| Token | Hex | Użycie |
|---|---|---|
| `bg` | `#F6F5F8` | tło aplikacji, obszar pod kartami |
| `surface` | `#FFFFFF` | karty, sidebar, tabela, pola |
| `surface-muted` | `#F1EFF5` | nagłówek tabeli, hover wiersza, aktywna pozycja menu, puste komórki heatmapy |
| `border` | `#E8E5EE` | ramki kart, separatory |
| `border-strong` | `#D6D1DF` | ramki pól formularza i przycisków drugorzędnych |
| `ink` | `#221C2B` | tekst główny |
| `ink-muted` | `#6B6577` | etykiety, daty, podpisy, placeholdery |
| `brand` | `#6A4C93` | przycisk główny, linki, aktywne elementy, markery Twoich wejść/wyjść na wykresie |
| `brand-hover` | `#5A3E80` | hover/pressed przycisku głównego |
| `brand-soft` | `#F1ECF7` | karta wniosku, zaznaczony wiersz, aktywny filtr |
| `on-brand` | `#FFFFFF` | tekst na `brand` |
| `profit` | `#24784A` | dodatni PnL, Long, świeca wzrostowa |
| `profit-soft` | `#E6F3EC` | tło plakietki zysku, dni na plusie |
| `loss` | `#C43C37` | ujemny PnL, Short, świeca spadkowa, błędy |
| `loss-soft` | `#FBEAE9` | tło plakietki straty, dni na minusie |
| `warning` | `#A86A12` | brak notatki, problem z synchronizacją |
| `warning-soft` | `#FBF1DF` | tło ostrzeżeń |

### Ciemny

| Token | Hex |
|---|---|
| `bg` | `#16131C` |
| `surface` | `#1F1B27` |
| `surface-muted` | `#2A2533` |
| `border` | `#34303F` |
| `border-strong` | `#4A4458` |
| `ink` | `#EEEAF4` |
| `ink-muted` | `#A59FB2` |
| `brand` | `#B9A2E0` |
| `brand-hover` | `#CBB8EB` |
| `brand-soft` | `#2D2440` |
| `on-brand` | `#16131C` |
| `profit` | `#4CC38A` |
| `profit-soft` | `#173226` |
| `loss` | `#F0726C` |
| `loss-soft` | `#3A1E21` |
| `warning` | `#E3A94B` |
| `warning-soft` | `#3A2C14` |

**Kontrast:** tekst `ink`, `ink-muted`, `brand`, `profit` i `loss` na `surface` ma co najmniej 4,5:1 w obu motywach. Odcienie zieleni i czerwieni są przyciemnione względem przykładu właśnie po to, żeby kwoty były czytelne.

### Skala heatmapy (PnL dnia)

5 stopni dla zysku i 5 dla straty, od wersji `-soft` do pełnego koloru. Dzień bez handlu ma `surface-muted`. Stopień zależy od wielkości wyniku względem Twojego średniego dnia, a nie od kwoty bezwzględnej. Tooltip zawsze pokazuje kwotę ze znakiem, bo sam kolor nie wystarcza.

## Typografia

**Plus Jakarta Sans** (Google Fonts), wagi 400, 500, 600 i 700. Wszystkie liczby mają `font-variant-numeric: tabular-nums`.

| Styl | Rozmiar / interlinia | Waga | Użycie |
|---|---|---|---|
| `greeting` | 28 / 36 | 600 | powitanie na Przeglądzie |
| `title` | 24 / 32 | 600 | tytuł widoku (Transakcje, Statystyki) |
| `heading` | 18 / 26 | 600 | nagłówek karty większej sekcji |
| `card-title` | 15 / 22 | 600 | tytuł małej karty |
| `body` | 14 / 22 | 400 | tekst, notatki, komórki tabeli |
| `body-strong` | 14 / 22 | 600 | nazwa instrumentu, PnL w tabeli |
| `label` | 13 / 18 | 500 | etykiety pól, nagłówki kolumn, menu |
| `caption` | 12 / 16 | 400 | daty, podpisy, liczniki |
| `figure` | 26 / 32 | 700 | duże liczby (PnL dnia, wynik trade'u) |

Zasady: zdania zwykłą wielkością liter, bez etykiet WERSALIKAMI, bez wyróżniania pojedynczych słów kolorem.

## Kształt, odstępy, cienie

- **Zaokrąglenia:** `8` dla pól, chipów, plakietek i małych przycisków; `12` dla kart; `16` dla karty wniosku i okien dialogowych; `full` dla przycisku głównego w sidebarze, awatarów i kropek.
- **Odstępy (siatka 4):** 4, 8, 12, 16, 20, 24, 32, 40. Padding karty to 20, odstęp między kartami 16, padding strony 32.
- **Cień:** tylko dla elementów pływających, `0 8px 24px rgba(34, 28, 43, .08)`.
- **Wysokości:** przycisk i pole 40 px (36 w gęstych miejscach), wiersz tabeli 52 px, pozycja menu 36 px. Na telefonie cel dotyku ma co najmniej 44 px.

## Ikony i grafika

Ikony liniowe Lucide, obrys 1,5 px, 16 px w treści, 18 px w menu, kolor tekstu, w którym stoją. Bez emoji w interfejsie. Nastrój to skala 5 prostych ikon (od napięcia do spokoju) z podpisem słownym. Tagi mają kolorową kropkę (jak dzienniki w przykładzie), a kolor wybiera użytkownik z 6 przygaszonych odcieni.

## Układ aplikacji

```
┌──────────────┬───────────────────────────────────────────────┐
│ Tradelog     │  Pasek: ‹ › · zakładki widoku · motyw ☀/☾     │
│ [+ Notatka]  ├───────────────────────────────────────────────┤
│              │                                               │
│ Przegląd     │   treść widoku (max 1200 px, wyśrodkowana)    │
│ Transakcje   │                                               │
│ Statystyki   │                                               │
│ Dziennik     │                                               │
│              │                                               │
│ Konta        │                                               │
│ ● OKX Główne │                                               │
│ ● OKX Demo   │                                               │
│              │                                               │
│ [synchr.]    │                                               │
│ Max · OKX    │                                               │
└──────────────┴───────────────────────────────────────────────┘
```

Sidebar ma 240 px, tło `surface` i prawą ramkę. Na górze jest przycisk główny „Dodaj notatkę” (pigułka w `brand`). Konta giełdowe to kolorowe kropki, tak jak dzienniki w przykładzie. Na dole status synchronizacji i profil. Poniżej 960 px sidebar chowa się pod ikonę menu.

## Ekrany

### 1. Przegląd (nowy ekran startowy)

```
            Dzień dobry, Max
     czwartek, 19 listopada · Jak się czujesz przed sesją?

┌───────────── lewa kolumna (2/3) ─────────┐ ┌── prawa (1/3) ──┐
│ Nastrój dziś: [5 ikon]  krótki komentarz │ │ Ten tydzień     │
├──────────────────────────────────────────┤ │ Pn Wt Śr Cz Pt  │
│ Do opisania: 3 transakcje bez notatki    │ │ dni z notatką   │
│ [BTC Long +79,59] [ETH Short −63,27] ... │ ├─────────────────┤
├──────────────────────────────────────────┤ │ WNIOSEK (brand) │
│ Listopad: heatmapa PnL dni   [mies. ▾]   │ │ „Trade'y z tagiem│
│ ■■□■■■□■■ ...  legenda: zysk / strata    │ │ FOMO: 0 z 5 na   │
├──────────────────────────────────────────┤ │ plusie.”         │
│ Ostatnie transakcje (3 karty)            │ ├─────────────────┤
│                                          │ │ Ostatnie wpisy  │
└──────────────────────────────────────────┘ │ oś czasu notatek│
                                             └─────────────────┘
```

- **Nastrój dziś:** szybki wybór przed sesją, zapisywany do dnia. Później w statystykach widać wyniki według nastroju.
- **Do opisania:** zastępuje „Writing prompts” z przykładu. Pokazuje zamknięte transakcje bez notatki, a kliknięcie otwiera dziennik danego trade'u.
- **Wniosek:** jedna karta w `brand` (tekst `on-brand`) albo `brand-soft`. Wnioski liczy backend regułami (np. tag z najgorszym wynikiem, najlepsza godzina), bez AI. To dobre ćwiczenie z LINQ.
- **Ten tydzień:** dni, w których dodałeś notatkę. Ikona pióra zamiast płomieni.

### 2. Transakcje

Tabela zostaje w obecnej strukturze, z nowym wyglądem:

- karta `surface` z zaokrągleniem 12, nagłówek kolumn w `surface-muted`, etykiety `label` w `ink-muted`;
- wiersz 52 px, hover `surface-muted`, kliknięcie otwiera szczegóły;
- kierunek jako plakietka `Long`/`Short` (`profit-soft`/`loss-soft`, zaokrąglenie 8);
- PnL w `body-strong` ze znakiem, w kolorze `profit`/`loss`, a wynik w R obok w `caption`;
- daty jako jeden ciąg „21 paź, 09:14 → 12:26”;
- kolumna dziennika: kolorowe kropki tagów i fragment notatki w jednej linii, a przy braku notatki link „Dodaj notatkę” w `warning`;
- nad tabelą: wyszukiwarka, filtry jako segmenty (Wszystkie / Long / Short / Bez notatki), zakres dat i tagi.

### 3. Szczegóły transakcji

- Nagłówek: instrument, plakietka kierunku, duży wynik (`figure`) i zdanie podsumowania, np. „Wszedłeś 21 października o 09:14, wyszedłeś po 3 h 12 min”.
- Wykres (TradingView lightweight-charts) w karcie, na `surface`:
  - świece w `profit`/`loss`;
  - siatka `border`, osie w `caption`/`ink-muted`;
  - Twoje fille jako okrągłe znaczniki w `brand` z małą strzałką (kupno w górę, sprzedaż w dół);
  - średnie wejście jako przerywana linia `brand`, SL `loss`, TP `profit`;
  - czas trzymania pozycji podświetlony `brand-soft`.
- Po prawej (lub pod spodem na węższym ekranie) dziennik: nastrój, setup, ocena, tagi, notatka (pole tekstowe, `body`, min. 6 linii), zrzuty ekranu.
- Pod wykresem tabela wykonań i karta „Plan i wynik” (SL, TP, wynik w R).

### 4. Statystyki

Karty z kluczowymi wartościami w siatce o różnych szerokościach (PnL i krzywa kapitału szersze, mniejsze metryki węższe), żeby nie wyglądało to jak 8 identycznych pudełek. Dalej heatmapa roku, wyniki według tagu, instrumentu, godziny i nastroju.

### 5. Dziennik

Nowy widok: wszystkie notatki jako oś czasu, jak „Recent entries” z przykładu, w pełnej szerokości. Filtrowanie po tagu i nastroju. Każdy wpis linkuje do swojej transakcji.

### 6. Konta giełdowe

Lista kont z kolorową kropką, statusem i ostatnią synchronizacją. Formularz dodania klucza z informacją o uprawnieniu tylko do odczytu. Import CSV dla starszej historii.

## Komponenty (do zbudowania w Figmie)

1. Przycisk: główny (`brand`), drugorzędny (ramka `border-strong`), tekstowy, ikonowy; rozmiary 36 i 40; stany default, hover, pressed, disabled, focus.
2. Pigułka „Dodaj notatkę” w sidebarze.
3. Pozycja menu: default, hover, aktywna.
4. Pole tekstowe, textarea, select, wyszukiwarka; stany default, focus, błąd.
5. Kontrolka segmentowa (filtry, interwał wykresu).
6. Plakietka kierunku Long/Short; plakietka statusu.
7. Tag z kolorową kropką (6 kolorów).
8. Wartość PnL: dodatnia, ujemna, zero; rozmiary `body-strong` i `figure`.
9. Karta: zwykła, karta wniosku.
10. Wiersz tabeli transakcji; nagłówek tabeli.
11. Komórka heatmapy: 11 stanów i tooltip.
12. Wpis na osi czasu.
13. Wybór nastroju (5 stanów).
14. Ocena (5 gwiazdek albo kropek).
15. Kropka konta, awatar, status synchronizacji.

## Teksty

- Po polsku, krótko, w drugiej osobie: „Dodaj notatkę”, „Synchronizuj”, „3 transakcje czekają na opis”.
- Kwoty zawsze ze znakiem i walutą: `+79,59 USDT`, `−63,27 USDT`. Spacja jako separator tysięcy, przecinek dziesiętny.
- Daty: „21 paź, 09:14”, a w nagłówkach „czwartek, 19 listopada”.
- Puste stany zachęcają do działania, np. „Nie masz jeszcze transakcji. Podłącz konto OKX, a pobierzemy historię z ostatnich 3 miesięcy.”
- Błędy mówią, co się stało i co zrobić, np. „OKX odrzucił klucz. Sprawdź, czy passphrase jest taki sam jak przy tworzeniu klucza.”

## Dostępność

Kontrast tekstu co najmniej 4,5:1 w obu motywach. Widoczny focus: obrys 2 px w `brand` z odstępem 2 px. Zysk i strata nigdy nie są rozróżniane samym kolorem (zawsze znak +/− albo słowo). Animacje ograniczone do reakcji na akcję użytkownika i wyłączane przy `prefers-reduced-motion`.

## Kolejność pracy w Figmie

1. Zmienne kolorów z trybami Jasny/Ciemny, odstępy i zaokrąglenia jako zmienne.
2. Style tekstu.
3. Komponenty z listy wyżej.
4. Ekrany: Przegląd, Transakcje, Szczegóły transakcji, potem Statystyki, Dziennik i Konta.
5. Wersja ciemna ekranów przez przełączenie trybu zmiennych.
