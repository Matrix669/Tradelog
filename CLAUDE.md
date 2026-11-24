# Tradelog

Osobisty dziennik tradera krypto. Aplikacja pobiera transakcje z API giełdy (na start OKX, klucz tylko do odczytu), pokazuje je w tabeli i na wykresie z punktami wejścia/wyjścia, a użytkownik dopisuje do nich notatki, nastrój, tagi i ocenę. Eksport do CSV. Później: miesięczny raport CSV i wyliczenia do PIT.

Kolejność: najpierw web (React + .NET), potem klient w Swift (iOS/macOS) korzystający z tego samego API.

## Kim jest użytkownik

- Rozmawiaj po polsku.
- Frontend (React, Next.js, TypeScript, Tailwind) zna dobrze, ale nie wszystko. Backend (.NET, C#, EF Core) zna dopiero od podstaw.
- **Cały projekt służy nauce, z naciskiem na .NET.** Celem nie jest jak najszybciej skończyć aplikację, tylko żeby użytkownik rozumiał każdy kawałek kodu i umiał go napisać sam.

## Jak pracujemy

### Najpierw plan, potem kod

- **Nie pisz kodu od razu.** Przy każdym zadaniu najpierw opisz krótko plan: co trzeba zrobić, w jakich plikach, w jakiej kolejności i dlaczego tak. Poczekaj na akceptację.
- Po akceptacji zapytaj, czy użytkownik chce napisać dany fragment sam (z Twoimi wskazówkami), czy woli, żebyś napisał go Ty i wyjaśnił.
- Gdy piszesz kod, rób to małymi krokami (jeden plik albo jedna zmiana naraz), a po każdym kroku krótko wyjaśnij, co się wydarzyło. Nie generuj od razu całych warstw (serwis + repozytorium + DTO + kontroler naraz).
- Gdy użytkownik pisze kod sam, zrób code review: wskaż błędy i lepsze rozwiązania, ale wyjaśnij dlaczego, zamiast od razu poprawiać za niego.
- Drobne, mechaniczne rzeczy (literówka, import, formatowanie) możesz poprawiać od razu.

### Tłumaczenie pojęć

- **Backend:** każde nowe pojęcie (np. dependency injection, DbContext, migracja, DTO, async/await, LINQ, middleware) tłumacz krok po kroku: co to jest, jak działa i po co istnieje, z **dwoma różnymi przykładami**. Tam, gdzie się da, porównaj z odpowiednikiem we frontendzie, który użytkownik zna (np. DI ↔ React Context, DTO ↔ typ odpowiedzi API w TS).
- **Frontend:** zakładaj, że podstawy zna, ale gdy pojawia się coś mniej oczywistego (nowy hook, wzorzec, API Tailwind v4, zachowanie shadcn/Base UI, TanStack Query itp.) albo użytkownik pyta, wyjaśnij to krócej niż przy backendzie, ale też z uzasadnieniem.
- Jeśli widzisz, że rozwiązanie użytkownika działa, ale jest lepszy sposób, powiedz o tym i wyjaśnij różnicę.

### Funkcje pionowo: FE i BE razem

- Każdą funkcję robimy **od końca do końca naraz**: encja i migracja → endpoint w API → wywołanie z frontendu → widok. Przykład: „tabela ostatnich transakcji” to w jednym zadaniu model `Trade`, endpoint `GET /api/trades`, typ i fetch w React i tabela w UI.
- Nie budujemy najpierw całego frontendu na mockach, a potem całego backendu (ani odwrotnie).
- Na początku zadania pokaż, z jakich kroków FE i BE się składa, i idźcie przez nie po kolei, zaczynając zwykle od backendu (bo frontend potrzebuje kształtu danych).
- Po skończeniu funkcji zaproponuj commit z opisową wiadomością.

### Ogólne

- Nie zgaduj wersji pakietów ani API bibliotek. Jeśli nie masz pewności, sprawdź.
- Jeśli zadanie jest niejasne, zapytaj, zamiast zakładać.

## Struktura repo

```
Tradelog/                  ← root repo (jedno repo dla FE i BE, gałąź master)
├── backend/
│   ├── backend.slnx       ← solucja (otwierana w Riderze)
│   └── Tradelog.Api/      ← ASP.NET Core Web API
│       ├── Controllers/
│       ├── Data/          ← AppDbContext
│       └── Migrations/    ← migracje EF Core (commitowane)
├── frontend/              ← Vite + React + shadcn
├── docs/
│   └── design-spec.md     ← specyfikacja designu v2 (źródło prawdy dla UI)
├── docker-compose.yml     ← lokalny Postgres
└── CLAUDE.md
```

## Komendy

Baza (z roota):
```bash
docker compose up -d        # start Postgresa
docker compose down         # stop (dane zostają w volume tradelog-pgdata)
```

Backend (z `backend/Tradelog.Api`):
```bash
dotnet run                                  # API na http://localhost:5110
dotnet ef migrations add <NazwaZmiany>      # nowa migracja
dotnet ef database update                   # zastosowanie migracji
dotnet ef migrations script --idempotent -o migrations.sql
```
OpenAPI: `http://localhost:5110/openapi/v1.json`

Frontend (z `frontend/`):
```bash
pnpm dev      # http://localhost:5173
pnpm build
pnpm lint
```

## Backend

- **.NET 10 (LTS).** Nie przechodzimy na .NET 11. Wszystkie pakiety Microsoft/EF muszą być z linii 10.x. `dotnet add package` bez `--version` pobierze 11.x, które nie działa z `net10.0`, więc zawsze podawaj wersję. W `.csproj` wersje są przypięte na konkretny numer (nie `10.*`).
- ASP.NET Core Web API z **kontrolerami** (nie minimal API), OpenAPI włączone, bez autoryzacji na razie.
- **EF Core 10 + Npgsql**, PostgreSQL 18 w Dockerze. `dotnet-ef` zainstalowany globalnie w wersji 10.x.
- Connection string `ConnectionStrings:Default` jest w **user-secrets**, nie w `appsettings.json`. Tam trafią też klucze OKX. Nigdy nie wpisuj sekretów do plików w repo.
- Lokalne dane bazy: host `localhost`, port `5432`, baza/user `tradelog`, hasło `tradelog_dev` (tylko dev).
- Migracje tworzymy świadomie, z opisową nazwą. Na produkcję docelowo idempotentny skrypt SQL albo migration bundle w CI, nie `Migrate()` przy starcie.
- `WeatherForecast` to przykład z szablonu, do usunięcia.

## Frontend

- React 19 + TypeScript, Vite, **Tailwind CSS v4**, shadcn/ui (base: Base UI, preset wygenerowany przez `shadcn create`), ikony **Lucide**, walidacja **Zod**.
- **Tylko Tailwind**: żadnych osobnych plików CSS ani `style={}`. Jedyny wyjątek to tokeny (zmienne CSS) w `src/index.css`.
- Komponenty shadcn dodawaj przez `pnpm dlx shadcn@latest add <komponent>`, nie przepisuj ich ręcznie.
- Kolory, typografia, odstępy i zaokrąglenia pochodzą z `docs/design-spec.md`. Używaj tokenów (`bg`, `surface`, `brand`, `profit`, `loss`…), nigdy surowych hexów w komponentach.
- Font docelowy to **Plus Jakarta Sans** (spec). Preset shadcn ustawił Inter, do podmiany.
- Liczby zawsze z `tabular-nums`. Bez monospace.

## Zasady UI (skrót ze specu)

- Jasny, spokojny dziennik, nie terminal giełdowy. Jasny motyw domyślny + ciemny.
- Kolor marki: śliwka `#6A4C93`. Zieleń i czerwień wyłącznie dla zysku i straty.
- Ramki 1 px zamiast cieni. Cień tylko dla elementów pływających (menu, popover, dialog).
- Teksty po polsku, krótko, w drugiej osobie. Kwoty ze znakiem i walutą: `+79,59 USDT`, `−63,27 USDT` (spacja tysięcy, przecinek dziesiętny). Daty: „21 paź, 09:14”.
- Zysk/strata nigdy nie rozróżniane samym kolorem (zawsze znak albo słowo).

## Stan projektu

Zrobione:
- Web API (.NET 10, kontrolery, OpenAPI) działa.
- Postgres w Dockerze, EF Core podłączony, migracja `Init` zastosowana.
- Frontend z `shadcn create` (Vite) działa.
- Repo git w roocie.
- Tokeny ze specu w `frontend/src/index.css` (jasny i ciemny motyw, zmienne shadcn wskazują na tokeny ze specu, `@theme inline` dla kolorów, `rounded-field/card/dialog`, `shadow-float`, style tekstu `text-title`, `text-body`…), font Plus Jakarta Sans (fontsource).

Do poprawy przy okazji:
- Przycisk z presetu ma `rounded-2xl` i `h-8`, spec mówi 8 px i 40 px (poprawić przy komponentach przycisku).
- `ThemeProvider` ma `defaultTheme="system"`, spec mówi jasny domyślny.

Następne kroki:
1. CORS w backendzie dla `http://localhost:5173` i usunięcie `WeatherForecast`.
2. Pierwsza funkcja pionowa: lista transakcji (encja `Trade` + migracja → `GET /api/trades` → tabela w UI). Na start na danych testowych (seed), integracja z OKX później jako osobna funkcja.